using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dell_Warranty_Checker
{
    class ComputerInformation
    {
        public ComputerInformation()
        {
            ComputerName = "";
            ServiceTag = "";
            LoggedOnUserName = "";
        }

        // Constructor that takes one argument:
        public ComputerInformation(string computerName, string serviceTag, string loggedOnUserName)
        {
            ComputerName = computerName;
            ServiceTag = serviceTag;
            LoggedOnUserName = loggedOnUserName;
        }

        // Auto-implemented readonly property:
        public string ComputerName { get; set; }
        public string ServiceTag { get; set; }
        public string LoggedOnUserName { get; set; }

        public override string ToString()
        {
            return ComputerName.ToString();
        }
    }
}
