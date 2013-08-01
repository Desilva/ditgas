using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models
{
    public class TableContainer
    {
        public List<List<Dictionary<string, string>>> tableHeader { get; set; }
        public List<Dictionary<string, string>> tableContent { get; set; }
        public List<string> cellsName { get; set; }

        public TableContainer()
        {
            tableHeader = new List<List<Dictionary<string, string>>>();
            tableContent = new List<Dictionary<string, string>>();
            cellsName = new List<string>();
            
        }
    }
}