using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GovDelivery.Library.Utils
{
    public class GovDeliveryUtils
    {
        public const string DATE_FORMAT = "MM/dd/yyyy";
        public const string TIME_FORMAT = "hh:mm tt z";

        public static string ReplaceTimeZoneWithUtcOffset(string dateString)
        {
            if (dateString.Contains("CST")) return dateString.Replace("CST", "-6");
            if (dateString.Contains("CDT")) return dateString.Replace("CDT", "-5");
            return dateString;
        }

        /// <summary>
        /// GovDelivery date strings
        /// </summary>
        public static DateTime DateStringToDateTimeUtc(string dateString)
        {
            var correctedDateString = ReplaceTimeZoneWithUtcOffset(dateString);
            var localTime = DateTime.ParseExact(correctedDateString, $"{DATE_FORMAT} {TIME_FORMAT}", CultureInfo.CurrentCulture);
            return localTime.ToUniversalTime();
        }

        public static string Base64Encode(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Base64Decode(string encodedText) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));

        public static StringContent ModelToStringContent<T>(T m, XmlSerializer serializer = null)
        {
            if (serializer == null) serializer = new XmlSerializer(typeof(T));

            using (var sw = new StringWriter())
            using (var xw = XmlWriter.Create(sw))
            {
                serializer.Serialize(xw, m);

                return new StringContent(xw.ToString(), Encoding.UTF8, "text/xml");
            }
        }

        public static async Task<T> ResponseContentToModel<T> (HttpContent hc, XmlSerializer serializer = null)
        {
            if (serializer == null) serializer = new XmlSerializer(typeof(T));

            var stream = new MemoryStream();
            await hc.CopyToAsync(stream);

            return (T)serializer.Deserialize(stream);
        }
    }
}