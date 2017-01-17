using System.Xml;

namespace SmartHome.XMLReader
{
    public class DataReader
    {
        public DataReader()
        {
            XmlReader xmlReader = XmlReader.Create("...\\...\\Data\\Data.xml");
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Floor"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        string name = xmlReader.GetAttribute("Name");
                    }
                }
            }
        }
    }
}
