using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonModels
{
    public class GetDistanceJson
    {
        public class Root
        {
            public double generation_time { get; set; }
            public List<Route> routes { get; set; }
            public double attract_time { get; set; }
            public double build_matrix_time { get; set; }
        }

        public class Route
        {
            public string status { get; set; }
            public int source_id { get; set; }
            public int target_id { get; set; }
            public int distance { get; set; }
            public int duration { get; set; }
        }
    }
}
