using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DesenhoDL {
    class Download {
        private String urlFinalSelectedResolution;
        private List<String> timeline;
        private bool keepRunning;
        private byte[] key;
        private String mp4Path;
        public Download(String mp4Path, String link) {
            // TODO: parsing de td
            // TODO: selecao de layer
            //this.urlFinalSelectedResolution = "";
            //this.timeline = Utils.BuildM3u8Timeline(new Uri(urlFinalSelectedResolution));
            this.timeline = Utils.BuildM3u8Timeline("test/sample.m3u8", true);
            this.keepRunning = true;
            this.mp4Path = mp4Path;
        }

        public void Start() {
            String[] timeline = this.timeline.ToArray();
            FileStream fs = new FileStream(this.mp4Path, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);
            int lineIndex = 0;

            while(keepRunning && lineIndex < timeline.Length) {
                String line = timeline[lineIndex];
                //Console.WriteLine(line);
                byte[] lineContent = Utils.RequestBytes(new Uri(line));
                lineIndex++;

                if(line.Contains(".key")) {
                    this.key = lineContent;
                    continue;
                }

                byte[] decryptedPart = Crypt.DecryptBytes(lineContent, this.key, lineIndex);
                bw.Write(decryptedPart);
            }

            fs.Dispose();
            bw.Dispose();


        }
    }
}
