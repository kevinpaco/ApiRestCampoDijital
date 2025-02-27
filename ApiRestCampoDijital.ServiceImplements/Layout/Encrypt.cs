using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    internal class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sHA256 = SHA256.Create();
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder stringBuilder = new StringBuilder();
            stream = sHA256.ComputeHash(aSCIIEncoding.GetBytes(str));
            for(int i = 0;i < stream.Length;i++) stringBuilder.AppendFormat("{0:x2}",stream[i]);
            return stringBuilder.ToString();
        }
    }
}
