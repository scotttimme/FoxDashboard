using FoxDashboard.FrontEnd.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxDashboard.FrontEnd.Models
{
    public class DataStore
    {
        public int AppCount { get; set; }

        public string AppPath { get; set; }

        public List<Common.App> Apps = new List<Common.App>();
    }
}
