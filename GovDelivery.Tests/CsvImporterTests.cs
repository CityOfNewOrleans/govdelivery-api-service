using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GovDelivery.Csv;
using GovDelivery.Csv.Models;
using GovDelivery.Csv.TypeConversion;
using GovDelivery.Csv.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;


namespace GovDelivery.Tests
{
    public class CsvImporterTests
    {
        private const string mockData =
            @"""Contacts"",""Subscriptions"",""Origin"",""Modified""
            ""504 555 5555"",""0"",""Direct"",""10/22/2015 03:49 PM CDT""
            ""email1@nola.gov"",""5"",""Direct"",""11/07/2016 11:35 AM CST""
            ""email2@gmail.com"",""18"",""Direct"",""11/30/2015 12:33 PM CST""
            ""email3@msn.com"",""25"",""Upload"",""06/30/2015 01:03 PM CDT""";

        public CsvImporterTests() { }

        [Fact(DisplayName = "Contacts field type converter works as expected.")]
        public void ContactsFieldConvertsCorrectly()
        {
            var converter = new QuotedStringConverter();
            var propertyMapData = new CsvPropertyMapData(null)
            {
                TypeConverterOptions = new TypeConverterOptions()
            };

            Assert.Equal("504 555 5555", (string)converter.ConvertFromString(@"""504 555 5555""", null, propertyMapData));
            Assert.Equal("email1@nola.gov", (string)converter.ConvertFromString(@"""email1@nola.gov""", null, propertyMapData));
        }

        [Fact(DisplayName = "Origin field type converter works as expected.")]
        public void OriginFieldConvertsCorrectly()
        {
            var converter = new SubscriberOriginConverter();
            var directOptions = new TypeConverterOptions();
            var uploadOptions = new TypeConverterOptions();

            Assert.Equal(SubscriberOrigin.Direct, (SubscriberOrigin)converter.ConvertFromString("Direct", null, null));
            Assert.Equal(SubscriberOrigin.Upload, (SubscriberOrigin)converter.ConvertFromString("Upload", null, null));
        }

        [Theory(DisplayName = "Modified field type converter works as expected.")]
        [InlineData(@"""10/22/2015 03:49 PM CDT""", @"""01/23/2016 11:35 AM CST""")]
        public void ModifiedFieldConvertsCorrectly(string dateString1, string dateString2)
        {
            var converter = new DateStringConverter();
            var options = new TypeConverterOptions();

            // Test CDT
            var referenceDate1 = new DateTime(2015, 10, 22, 15, 49, 0).ToUniversalTime();
            var convertedDate1 = (DateTime)converter.ConvertFromString(dateString1, null, null);
            Assert.Equal(referenceDate1, convertedDate1);

            // Test CST
            var referenceDate2 = new DateTime(2016, 1, 23, 11, 35, 0).ToUniversalTime();
            var convertedDate2 = (DateTime)converter.ConvertFromString(dateString2, null, null);
            Assert.Equal(referenceDate2, convertedDate2);
        }

        [Fact(DisplayName = "Subscriptions field type converter works as expected.")]
        public void SubscriberFieldConvertsCorrectly()
        {
            var converter = new QuotedInt32Converter();
            var propertyMapData = new CsvPropertyMapData(null)
            {
                TypeConverterOptions = new TypeConverterOptions
                {
                    NumberStyle = NumberStyles.Integer
                }
            };

            Assert.Equal(10, (int)converter.ConvertFromString(@"""10""", null, propertyMapData));
        }

        [Theory(DisplayName = "Valid CSV data should become a collection of ImportModels")]
        [InlineData(@"MockData\mockCsvImportData.csv")]
        public async void ValidCsvStringsConvertCorrectly(string filePath)
        {
            var importer = new CsvImporter();

            var subscribers = await importer.ImportSubscribersAsync(filePath);

            Assert.Equal(4, subscribers.Count());

            var first = subscribers.ElementAt(0);
            Assert.Equal("504 555 5555", first.Contact);
            Assert.Equal(SubscriberType.Phone, first.Type);
            Assert.Equal(SubscriberOrigin.Direct, first.Origin);
            Assert.Equal(0, first.Subscriptions);
            Assert.Equal(TimeUtils.DateStringToDateTimeUtc("10/22/2015 03:49 PM CDT"), first.LastModified);

            var second = subscribers.ElementAt(1);
            Assert.Equal("email1@nola.gov", second.Contact);
            Assert.Equal(SubscriberType.Email, second.Type);
            Assert.Equal(SubscriberOrigin.Direct, second.Origin);
            Assert.Equal(5, second.Subscriptions);
            Assert.Equal(TimeUtils.DateStringToDateTimeUtc("11/07/2016 11:35 AM CST"), second.LastModified);

            var third = subscribers.ElementAt(2);
            Assert.Equal("email2@gmail.com", third.Contact);
            Assert.Equal(SubscriberType.Email, third.Type);
            Assert.Equal(SubscriberOrigin.Direct, third.Origin);
            Assert.Equal(18, third.Subscriptions);
            Assert.Equal(TimeUtils.DateStringToDateTimeUtc("11/30/2015 12:33 PM CST"), third.LastModified);

            var fourth = subscribers.ElementAt(3);
            Assert.Equal("email3@msn.com", fourth.Contact);
            Assert.Equal(SubscriberType.Email, fourth.Type);
            Assert.Equal(SubscriberOrigin.Upload, fourth.Origin);
            Assert.Equal(25, fourth.Subscriptions);
            Assert.Equal(TimeUtils.DateStringToDateTimeUtc("06/30/2015 01:03 PM CDT"), fourth.LastModified);
        }
    }
}