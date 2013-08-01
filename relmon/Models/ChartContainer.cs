using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Models
{
    public class ChartContainer
    {
        public string title { get; set; }
        public List<Tuple<string, List<int>>> series { get; set; }
        public List<int> seriesSum { get; set; }
        public List<string> categories { get; set; }
        public string legendTitle { get; set; }
        public string xTitle { get; set; }
        public string yTitle { get; set; }

        public ChartContainer()
        {
            title = "";
            series = new List<Tuple<string, List<int>>>();
            seriesSum = new List<int>();
            categories = new List<string>();
            legendTitle = "";
            xTitle = "";
            yTitle = "";
        }
    }
}