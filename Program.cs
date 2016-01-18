using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ExtractImageFromResx
{
    class Program
    {
        static void Main(string[] args)
        {

            var doc = new XmlDocument();
            doc.Load(args[0]);

            var root = doc.DocumentElement ;

            XmlNodeList datas = root.SelectNodes("data");

            foreach( XmlNode d in datas )
            {
                if ( d.Attributes["type"].Value == "System.Drawing.Bitmap, System.Drawing" ) 
                {
                    ExtractImage(d.FirstChild.InnerText, d.Attributes["name"].Value + ".png" );
                }
            }
        }

        private static void ExtractImage(string im_encode, string output_filename)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(im_encode);

            using (var bw = new BinaryWriter(new FileStream(output_filename, FileMode.Create)))
            {
                bw.Write(base64EncodedBytes);
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
