using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Reflection
{
    public class XmlParser
    {
        public enum E_XML_ATTRIBUTE_TYPE
        {
            E_NONE      = -1,
            E_ATTRIBUTE = 0,
            E_CONTENT   ,
            E_LIST      
        }

        [AttributeUsage(AttributeTargets.Property, Inherited = true)]
        public abstract class FormatterAttribute : Attribute
        {
            public E_XML_ATTRIBUTE_TYPE AttributeType { get; set; } = E_XML_ATTRIBUTE_TYPE.E_NONE;

            protected FormatterAttribute(E_XML_ATTRIBUTE_TYPE attributeType)
            {
                AttributeType = attributeType;
            }
        }

        public class SerializeAsAttribute : FormatterAttribute
        {
            public SerializeAsAttribute() : base(E_XML_ATTRIBUTE_TYPE.E_ATTRIBUTE) { }
        }

        public class SerializeAsContent : FormatterAttribute
        {
            public SerializeAsContent() : base(E_XML_ATTRIBUTE_TYPE.E_CONTENT) { }
        }

        public class SerializeAsList : FormatterAttribute
        {
            public SerializeAsList() : base(E_XML_ATTRIBUTE_TYPE.E_LIST) { }
        }

        public interface ISerializable
        {
            void Serialize(out XElement element);
            void Deserialize(XElement element);
        }

        public abstract class XMLSerializable : ISerializable
        {
            public void Serialize(out XElement element)
            {
                element = new XElement(this.GetType().Name);

                foreach (var propInfo in this.GetType().GetProperties())
                {
                    if (!propInfo.CanRead || !propInfo.CanWrite)
                        continue;

                    var value = propInfo.GetValue(this);
                    if (value == null)
                        continue;

                    var attribute = propInfo.GetCustomAttribute<FormatterAttribute>();

                    if (attribute != null)
                    {
                        switch (attribute.AttributeType)
                        {
                            case E_XML_ATTRIBUTE_TYPE.E_CONTENT:
                                if (value is string content)
                                {
                                    element.SetValue(content);
                                }
                                break;

                            case E_XML_ATTRIBUTE_TYPE.E_LIST:
                                if (value is IEnumerable list)
                                {
                                    foreach (var item in list)
                                    {
                                        if (item is ISerializable serializableItem)
                                        {
                                            serializableItem.Serialize(out XElement listElement);
                                            element.Add(listElement);
                                        }
                                    }
                                }
                                break;

                            default:
                                element.Add(new XAttribute(propInfo.Name, value));
                                break;
                        }
                    }
                    else
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType) && propInfo.PropertyType.IsGenericType)
                        {
                            var list = (IEnumerable)value;
                            foreach (var item in list)
                            {
                                if (item is ISerializable serializableItem)
                                {
                                    serializableItem.Serialize(out XElement listElement);
                                    element.Add(listElement);
                                }
                            }
                        }
                        else
                            element.Add(new XAttribute(propInfo.Name, value));
                    }
                }
            }

            public void Deserialize(XElement element)
            {
                foreach (var propInfo in this.GetType().GetProperties())
                {
                    var attribute = propInfo.GetCustomAttribute<FormatterAttribute>();

                    if (attribute != null)
                    {
                        switch (attribute.AttributeType)
                        {
                            case E_XML_ATTRIBUTE_TYPE.E_CONTENT:
                                if (propInfo.PropertyType == typeof(string))
                                {
                                    propInfo.SetValue(this, element.Value);
                                }
                                break;

                            case E_XML_ATTRIBUTE_TYPE.E_LIST:
                                var listType = typeof(List<>).MakeGenericType(propInfo.PropertyType.GenericTypeArguments[0]);
                                var listInstance = Activator.CreateInstance(listType);

                                foreach (var subElement in element.Elements())
                                {
                                    var instance = (ISerializable)Activator.CreateInstance(propInfo.PropertyType.GenericTypeArguments[0]);
                                    instance.Deserialize(subElement);
                                    listInstance.GetType().GetMethod("Add").Invoke(listInstance, new object[] { instance });
                                }

                                propInfo.SetValue(this, listInstance);
                                break;

                            default:
                                var attributeValue = element.Attribute(propInfo.Name)?.Value;
                                if (attributeValue != null)
                                {
                                    var convertedValue = Convert.ChangeType(attributeValue, propInfo.PropertyType);
                                    propInfo.SetValue(this, convertedValue);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public class Message : XMLSerializable
        {
            [SerializeAsAttribute]
            public string Name { get; set; }

            [SerializeAsList]
            public List<Item> Items { get; set; }
        }

        public class Item : XMLSerializable
        {
            [SerializeAsAttribute]
            public string Name { get; set; }

            [SerializeAsContent]
            public string Value { get; set; }

            [SerializeAsList]
            public List<Item> Items { get; set; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var message = new XmlParser.Message
            {
                Name = "Test Message",
                Items = new List<XmlParser.Item>
                {
                    new XmlParser.Item { Name = "ItemA", Value = "1"},
                    new XmlParser.Item { Name = "ItemA" , Value = "2"},
                    new XmlParser.Item
                    {
                        Name = "List1",
                        Items = new List<XmlParser.Item>
                        {
                            new XmlParser.Item { Name = "ItemB", Value = "1"},
                            new XmlParser.Item { Name = "ItemB", Value = "2"},
                            new XmlParser.Item
                            {
                                Name = "List2",
                                Items = new List<XmlParser.Item>
                                {
                                    new XmlParser.Item { Name = "ItemC", Value = "1"},
                                    new XmlParser.Item { Name = "ItemC", Value = "2"},
                                    new XmlParser.Item
                                    {
                                        Name = "List3",
                                        Items = new List<XmlParser.Item>
                                        {
                                            new XmlParser.Item { Name = "ItemD", Value = "1"},
                                            new XmlParser.Item { Name = "ItemD", Value = "2"},
                                            new XmlParser.Item
                                            {
                                                Name = "List4",
                                                Items = new List<XmlParser.Item>
                                                {
                                                    new XmlParser.Item { Name = "ItemE", Value = "1"},
                                                    new XmlParser.Item { Name = "ItemE", Value = "2"}
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // XML로 직렬화
            message.Serialize(out XElement xmlElement);

            // XML 파일로 저장
            string filePath = "message.xml";
            File.WriteAllText(filePath, xmlElement.ToString());

            // XML 파일에서 역직렬화
            XElement loadedElement = XElement.Load(filePath);

            var deserializedMessage = new XmlParser.Message();
            deserializedMessage.Deserialize(loadedElement);

            filePath = "message2.xml";
            File.WriteAllText(filePath, loadedElement.ToString());
        }
    }
}
