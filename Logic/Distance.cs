using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Distance
    {
        public Dictionary<string, double>[] points { get; set; }
        public int[] sources { get; set; }
        public int[] targets { get; set; }

        public Distance(List<double> allCoords)
        {
            points = new Dictionary<string, double>[] {new Dictionary<string, double>
                {
                    ["lat"] = allCoords[1],
                    ["lon"] = allCoords[0]
                },
                new Dictionary<string, double>
                {
                    ["lat"] = allCoords[3],
                    ["lon"] = allCoords[2]
                }
                };
            sources = new int[] { 0 };
            targets = new int[] { 1 };
        }

        public static double GetDistance(Distance distance)
        {
            string url = "https://routing.api.2gis.com/get_dist_matrix?key=9513efda-d835-4809-b0a5-11b1c05612e6&version=2.0";
            var jsonString = JsonSerializer.Serialize(distance);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://routing.api.2gis.com/get_dist_matrix?key=9513efda-d835-4809-b0a5-11b1c05612e6&version=2.0");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
            }


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Root result;
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(streamReader.ReadToEnd());
            }
            int mainResult = result.routes[0].distance / 1000;

            return mainResult;
        }
    }
}
