using System.Xml;

namespace SmartHome.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            ReadXML();
        }

        void ReadXML()
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
