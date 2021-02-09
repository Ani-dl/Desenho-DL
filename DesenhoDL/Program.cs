using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace DesenhoDL {
    class Program {
        static void Main(string[] args) {
            //byte[] file = File.ReadAllBytes("test/enc.ts");
            //byte[] key = File.ReadAllBytes("test/key.key");
            //byte[] dec = Crypt.DecryptBytes(file, key, 1);
            //File.WriteAllBytes("test/dec.ts", dec);

            //String m3u8 = File.ReadAllText("test/sample.m3u8");
            //List<String> s = Utils.BuildM3u8Timeline(m3u8);
            //Console.WriteLine(s[0]);
            Download d = new Download("test/test.mp4", "https://www.crunchy-dl.com/anime/assassination-classroom/colecao/1/episodio/1/");
            d.Start();

            Console.ReadKey();

        }
    }
}
