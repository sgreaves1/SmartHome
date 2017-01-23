using System;
using System.IO;
using System.Xml;
using SmartHome.Model;

namespace SmartHome.XMLReader
{
    public class DataReader
    {
        public void ReadXml()
        {
            XmlReader xmlReader = XmlReader.Create("...\\...\\Data\\Data.xml", new XmlReaderSettings {IgnoreWhitespace = true});
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Floor"))
                {
                    FloorModel floor = new FloorModel();

                    if (xmlReader.HasAttributes)
                    {
                        floor.Name = xmlReader.GetAttribute("Name");
                        floor.ImageName = Directory.GetCurrentDirectory() + "\\" + xmlReader.GetAttribute("ImageName");
                    }
                    
                    while (xmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        xmlReader.Read();

                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Light"))
                        {
                            LightModel light = new LightModel {Name = xmlReader.GetAttribute("Name")};

                            string x = xmlReader.GetAttribute("X");
                            string y = xmlReader.GetAttribute("Y");
                            if (x != null)
                                light.X = int.Parse(x);
                            if (y != null)
                                light.Y = int.Parse(y);

                            floor.Devices.Add(light);
                        }

                        if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Radio"))
                        {
                            RadioModel radio = new RadioModel { Name = xmlReader.GetAttribute("Name") };

                            string x = xmlReader.GetAttribute("X");
                            string y = xmlReader.GetAttribute("Y");
                            if (x != null)
                                radio.X = int.Parse(x);
                            if (y != null)
                                radio.Y = int.Parse(y);

                            floor.Devices.Add(radio);
                        }
                    }

                    ModelReady?.Invoke(null, new DataReaderEventArgs { Floor = floor });
                }
            }
        }

        public EventHandler ModelReady;
    }
}
