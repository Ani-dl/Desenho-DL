using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace DesenhoDL {
    class Utils {
        public static byte[] RequestBytes(Uri externalSource) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(externalSource);

            using(WebResponse res = req.GetResponse()) {
                using(Stream response = res.GetResponseStream()) {
                    using(Stream s = response) {
                        byte[] responseBody = (new BinaryReader(s)).ReadBytes(Convert.ToInt32(res.ContentLength));

                        return responseBody;
                    }
                }
            }

        }

        public static List<String> BuildM3u8Timeline(Uri externalSource) {
            String responseBody = "";
            WebRequest req = WebRequest.Create(externalSource);

            using(StreamReader response = new StreamReader(req.GetResponse().GetResponseStream())) {
                responseBody = response.ReadToEnd();
            }

            return BuildM3u8Timeline(responseBody);
        }

        public static List<String> BuildM3u8Timeline(String input, bool isFileName) {
            if(isFileName)
                return BuildM3u8Timeline(File.ReadAllText(input));
            else
                return BuildM3u8Timeline(input);
        }

        public static List<String> BuildM3u8Timeline(String input) {
            // poderia usar regex em td, mas seria impreciso
            List<String> timeline = new List<String> { };
            String[] lines = input.Replace("\r", "").Split('\n');

            if(!input.Contains("d33et"))
                throw new Exception("Host das partes não disponível no arquivo.");

            foreach(String line in lines) {
                if(line.Contains(".ts"))
                    timeline.Add(line);
                else if(line.Contains(".key"))
                    timeline.Add(line.Split('"')[1]);
            }

            //Console.Write(lines[].Split('"')[1]);

            return timeline;
        }
    }
}
