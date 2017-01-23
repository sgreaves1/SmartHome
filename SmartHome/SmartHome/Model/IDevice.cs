using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Model
{
    public interface IDevice
    {
        bool IsOnline { get; set; }
        string Name { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
