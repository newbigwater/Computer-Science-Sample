using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSMS_Message_Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            XmlParser.Message outputMessage = new XmlParser.Message
            {
                Name = "On-Line Data (D)",
                Stream = "1",
                Function = "2",
                WaitBit = false,
                Direction = "FromEquipment",
                LogLevel = "Full",
                UndefinedMessage = false,
                AutoReply = false,
                ReplyMessage = "None",
                Group = "STREAM 1: Equipment Status",
                Items = new List<XmlParser.Item>
                {
                    new XmlParser.Item
                    {
                        Name = "L",
                        Format = "L",
                        FixedCount = true,
                        Count = 2,
                        CharacterEncoding = "None",
                        ValueDefinition = "",
                        SubItems = new List<XmlParser.Item>
                        {
                            new XmlParser.Item
                            {
                                Name = "MDLN",
                                Format = "A",
                                FixedCount = false,
                                Count = 6,
                                CharacterEncoding = "None",
                                ValueDefinition = "U-TECH"
                            },
                            new XmlParser.Item
                            {
                                Name = "SOFTREV",
                                Format = "A",
                                FixedCount = false,
                                Count = 3,
                                CharacterEncoding = "None",
                                ValueDefinition = "1.0"
                            },
                            new XmlParser.Item
                            {
                            Name = "L",
                            Format = "L",
                            FixedCount = true,
                            Count = 2,
                            CharacterEncoding = "None",
                            ValueDefinition = "",
                            SubItems = new List<XmlParser.Item>
                            {
                                new XmlParser.Item
                                {
                                    Name = "MDLN",
                                    Format = "A",
                                    FixedCount = false,
                                    Count = 6,
                                    CharacterEncoding = "None",
                                    ValueDefinition = "U-TECH"
                                },
                                new XmlParser.Item
                                {
                                    Name = "SOFTREV",
                                    Format = "A",
                                    FixedCount = false,
                                    Count = 3,
                                    CharacterEncoding = "None",
                                    ValueDefinition = "1.0"
                                }

                            }
                        }
                        }
                    }
                }
            };

            // XML 파일로 직렬화
            string filePath = "output.xml";
            XmlParser.Serialize(outputMessage, filePath);


            XmlParser.Message inputMessage = XmlParser.Deserialize(filePath);

            filePath = "input.xml";
            XmlParser.Serialize(inputMessage, filePath);
        }
    }
}
