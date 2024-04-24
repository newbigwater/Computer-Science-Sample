using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HSMS_Message_Parser
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlParser
    {
        public class Message
        {
            public string Name { get; set; }
            public string Stream { get; set; }
            public string Function { get; set; }
            public bool WaitBit { get; set; }
            public string Direction { get; set; }
            public string LogLevel { get; set; }
            public bool UndefinedMessage { get; set; }
            public bool AutoReply { get; set; }
            public string ReplyMessage { get; set; }
            public string Group { get; set; }
            public List<Item> Items { get; set; }
        }

        public class Item
        {
            public string Name { get; set; }
            public string Format { get; set; }
            public bool FixedCount { get; set; }
            public int Count { get; set; }
            public string CharacterEncoding { get; set; }
            public string ValueDefinition { get; set; }
            public List<Item> SubItems { get; set; }
        }

        public static void Serialize(Message message, string filePath)
        {
            XElement root = new XElement("Message",
                new XAttribute("Name", message.Name),
                new XAttribute("Stream", message.Stream),
                new XAttribute("Function", message.Function),
                new XAttribute("WaitBit", message.WaitBit),
                new XAttribute("Direction", message.Direction),
                new XAttribute("LogLevel", message.LogLevel),
                new XAttribute("UndefinedMessage", message.UndefinedMessage),
                new XAttribute("AutoReply", message.AutoReply),
                new XAttribute("ReplyMessage", message.ReplyMessage),
                new XAttribute("Group", message.Group)
            );

            Serialize(root, message.Items);

            string xmlContent = root.ToString();
            File.WriteAllText(filePath, xmlContent);
            Console.WriteLine($"XML saved to {filePath}");
        }

        private static void Serialize(XElement parent, List<Item> Items)
        {
            if(null == Items)
                return;

            foreach (Item item in Items)
            {
                XElement itemElement = new XElement("Item",
                    new XAttribute("Name", item.Name),
                    new XAttribute("Format", item.Format),
                    new XAttribute("FixedCount", item.FixedCount),
                    new XAttribute("Count", item.Count),
                    new XAttribute("CharacterEncoding", item.CharacterEncoding),
                    new XAttribute("ValueDefinition", item.ValueDefinition)
                );

                Serialize(itemElement, item.SubItems);

                parent.Add(itemElement);
            }
        }

        public static Message Deserialize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            XElement root = XElement.Load(filePath);

            Message message = new Message
            {
                Name = root.Attribute("Name")?.Value,
                Stream = root.Attribute("Stream")?.Value,
                Function = root.Attribute("Function")?.Value,
                WaitBit = bool.Parse(root.Attribute("WaitBit")?.Value ?? "false"),
                Direction = root.Attribute("Direction")?.Value,
                LogLevel = root.Attribute("LogLevel")?.Value,
                UndefinedMessage = bool.Parse(root.Attribute("UndefinedMessage")?.Value ?? "false"),
                AutoReply = bool.Parse(root.Attribute("AutoReply")?.Value ?? "false"),
                ReplyMessage = root.Attribute("ReplyMessage")?.Value,
                Group = root.Attribute("Group")?.Value,
                Items = new List<Item>()
            };

            Deserialize(message.Items, root);

            return message;
        }

        private static void Deserialize(List<Item> Items, XElement parent)
        {
            if (null == parent)
                return;

            foreach (XElement itemElement in parent.Elements("Item"))
            {
                Item item = new Item
                {
                    Name = itemElement.Attribute("Name")?.Value,
                    Format = itemElement.Attribute("Format")?.Value,
                    FixedCount = bool.Parse(itemElement.Attribute("FixedCount")?.Value ?? "false"),
                    Count = int.Parse(itemElement.Attribute("Count")?.Value ?? "0"),
                    CharacterEncoding = itemElement.Attribute("CharacterEncoding")?.Value,
                    ValueDefinition = itemElement.Attribute("ValueDefinition")?.Value,
                    SubItems = new List<Item>()
                };

                Deserialize(item.SubItems, itemElement);

                Items.Add(item);
            }
        }
    }

}
