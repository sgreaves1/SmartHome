using System;
using System.Xml;
using SmartHome.Model;

namespace SmartHome.XMLReader
{
    public class DataReader
    {
        public void ReadXml()
        {
            XmlReader xmlReader = XmlReader.Create("...\\...\\Data\\Data.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Floor"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        FloorModel floor = new FloorModel();
                        floor.Name = xmlReader.GetAttribute("Name");
                        floor.ImageName = xmlReader.GetAttribute("ImageName");

                        ModelReady?.Invoke(null, new DataReaderEventArgs() { Floor = floor });
                    }
                }
            }
        }

        public EventHandler ModelReady;
    }
}
