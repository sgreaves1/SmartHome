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
                            LightModel light = new LightModel();
                            light.Name = xmlReader.GetAttribute("Name");
                            string X = xmlReader.GetAttribute("X");
                            string Y = xmlReader.GetAttribute("Y");
                            if (X != null)
                                light.X = int.Parse(X);
                            if (Y != null)
                                light.Y = int.Parse(Y);

                            floor.Lights.Add(light);
                        }
                    }

                    ModelReady?.Invoke(null, new DataReaderEventArgs() { Floor = floor });
                }
            }
        }

        public EventHandler ModelReady;
    }
}
