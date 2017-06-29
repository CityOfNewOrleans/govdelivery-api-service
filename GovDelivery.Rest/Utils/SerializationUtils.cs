using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Utils
{
    public static class SerializationUtils
    {

        public static string Base64Encode(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Base64Decode(string encodedText) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));

        public static StringContent ModelToStringContent<T> (T model, XmlSerializer serializer = null)
        {
            if (serializer == null) serializer = new XmlSerializer(typeof(T));

            using (var sw = new Utf8StringWriter())
            {
                serializer.Serialize(sw, model);

                var serializedString = sw.ToString();

                return new StringContent(serializedString, Encoding.UTF8, "application/xml");
            }

        }

        public static async Task<T> ResponseContentToModel<T>(HttpContent hc, XmlSerializer serializer = null) where T : new()
        {
            if (serializer == null) serializer = new XmlSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                var contentString = await hc.ReadAsStringAsync();

                // response is a collection and it is empty:
                if (contentString.Contains("nil-classes")) return new T();

                await hc.CopyToAsync(stream);
                stream.Position = 0; // set stream to beginning, or deserialization will bomb.

                return (T)serializer.Deserialize(stream);
            }
        }
    }
    
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public static class StringExtensions
    {

        public static Stream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}