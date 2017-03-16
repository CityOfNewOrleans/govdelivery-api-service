using System;
using System.Globalization;

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
    }
}