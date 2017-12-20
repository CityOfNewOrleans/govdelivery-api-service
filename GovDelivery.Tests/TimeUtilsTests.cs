using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Xunit;
using System.Xml.Linq;
using GovDelivery.Csv.Utils;
using GovDelivery.Rest.Utils;
using GovDelivery.Rest.Models.Misc;

namespace GovDelivery.Tests
{
    public class TimeUtilsTests
    {
        public TimeUtilsTests()
        {
        }

        [Theory(DisplayName = "Timezones in date strings become UTC offsets")]
        [InlineData("10/22/2015 03:49 PM CDT")]
        public void TimeZoneBecomesUtcOffset(string data)
        {
            Assert.Equal("10/22/2015 03:49 PM -5", TimeUtils.ReplaceTimeZoneWithUtcOffset(data));
        }

        [Theory(DisplayName = "Fixed date string becomes DateTime With Expected Values")]
        [InlineData("10/22/2015 03:49 PM CDT")]
        public void FixedDateStringParsesToDateTime(string date)
        {
            var dateParts = TimeUtils.ReplaceTimeZoneWithUtcOffset(date).Split(' ').ToList();
            var dateDay = dateParts.ElementAt(0);
            var dateTime = dateParts
                .GetRange(1, dateParts.Count - 1)
                .Aggregate((memo, next) => $"{memo} {next}");

            Assert.Equal("10/22/2015", dateDay);
            Assert.Equal("03:49 PM -5", dateTime);

            var parsedDay = DateTime.ParseExact(dateDay, TimeUtils.DATE_FORMAT, CultureInfo.CurrentCulture).ToUniversalTime();
            Assert.Equal(2015, parsedDay.Year);
            Assert.Equal(10, parsedDay.Month);
            Assert.Equal(22, parsedDay.Day);

            var parsedTime = DateTime.ParseExact(dateTime, TimeUtils.TIME_FORMAT, CultureInfo.CurrentCulture).ToUniversalTime();
            Assert.Equal(20, parsedTime.Hour);
            Assert.Equal(49, parsedTime.Minute);

            var parsedDate = TimeUtils.DateStringToDateTimeUtc(date);
            Assert.True(parsedDate.GetType() == typeof(DateTime));
        }

    }

}