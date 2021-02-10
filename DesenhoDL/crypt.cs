using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace DesenhoDL {
    // bom dia grupo
    class Crypt {

        public static byte[] PadIV(Int64 _iv) {
            byte[] iv = new byte[16];
            byte[] c = BitConverter.GetBytes(_iv);

            for(int i = 16 - c.Length; i < 16; i++) {
                iv[i] = c[i - c.Length];
            }

            return iv;
        }

        public static byte[] DecryptBytes(byte[] _content, byte[] _key, int _iv) {
            byte[] decryptedBytes = new byte[_content.Length];
            byte[] iv = PadIV(_iv);

            using(Aes alg = Aes.Create()) {
                alg.Key = _key;
                alg.Mode = CipherMode.CBC;
                alg.IV = iv;

                ICryptoTransform decryptor = alg.CreateDecryptor(alg.Key, alg.IV);

                using(MemoryStream ms = new MemoryStream(_content)) {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) {
                        cs.Read(decryptedBytes, 0, _content.Length);
                    }
                }

                return decryptedBytes;
            }
        }

    }
}
