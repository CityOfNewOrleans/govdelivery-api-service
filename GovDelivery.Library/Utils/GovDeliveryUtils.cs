﻿using GovDelivery.Library.Interfaces;
using GovDelivery.Library.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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

        public static async Task<T> ResponseContentToModel<T>(HttpContent hc, XmlSerializer serializer = null) 
        {
            if (serializer == null) serializer = new XmlSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                var contentString = await hc.ReadAsStringAsync();

                await hc.CopyToAsync(stream);
                stream.Position = 0; // set stream to beginning, or deserialization will bomb.

                return (T)serializer.Deserialize(stream);
            }
        }

        public static string RemoveAllNamespaces(string xmlString)
        {
            var xmlDocSansNamespaces = RemoveAllNamespaces(XElement.Parse(xmlString));

            return xmlDocSansNamespaces.ToString();
        }

        private static XElement RemoveAllNamespaces(XElement xmlDoc)
        {
            if (xmlDoc.HasElements)
                return new XElement(xmlDoc.Name.LocalName, xmlDoc.Elements().Select(el => RemoveAllNamespaces(el)));

            var elCopy = new XElement(xmlDoc.Name.LocalName);
            elCopy.Value = xmlDoc.Value;

            foreach (var attr in xmlDoc.Attributes()) elCopy.Add(attr);

            return elCopy;
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