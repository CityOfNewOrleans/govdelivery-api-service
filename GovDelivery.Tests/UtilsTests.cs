using GovDelivery.Library.Interfaces;
using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Library.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Xunit;
using System.Xml.Linq;

namespace GovDelivery.Library.Tests
{
    public class UtilsTests
    {
        public UtilsTests()
        {
        }

        [Theory(DisplayName = "Timezones in date strings become UTC offsets")]
        [InlineData("10/22/2015 03:49 PM CDT")]
        public void TimeZoneBecomesUtcOffset(string data)
        {
            Assert.Equal(GovDeliveryUtils.ReplaceTimeZoneWithUtcOffset(data), "10/22/2015 03:49 PM -5");
        }

        [Theory(DisplayName = "Fixed date string becomes DateTime With Expected Values")]
        [InlineData("10/22/2015 03:49 PM CDT")]
        public void FixedDateStringParsesToDateTime(string date)
        {
            var dateParts = GovDeliveryUtils.ReplaceTimeZoneWithUtcOffset(date).Split(' ').ToList();
            var dateDay = dateParts.ElementAt(0);
            var dateTime = dateParts
                .GetRange(1, dateParts.Count - 1)
                .Aggregate((memo, next) => $"{memo} {next}");

            Assert.Equal("10/22/2015", dateDay);
            Assert.Equal("03:49 PM -5", dateTime);

            var parsedDay = DateTime.ParseExact(dateDay, GovDeliveryUtils.DATE_FORMAT, CultureInfo.CurrentCulture).ToUniversalTime();
            Assert.Equal(parsedDay.Year, 2015);
            Assert.Equal(parsedDay.Month, 10);
            Assert.Equal(parsedDay.Day, 22);

            var parsedTime = DateTime.ParseExact(dateTime, GovDeliveryUtils.TIME_FORMAT, CultureInfo.CurrentCulture).ToUniversalTime();
            Assert.Equal(20, parsedTime.Hour);
            Assert.Equal(49, parsedTime.Minute);

            var parsedDate = GovDeliveryUtils.DateStringToDateTimeUtc(date);
            Assert.True(parsedDate.GetType() == typeof(DateTime));
        }

        [Theory(DisplayName = "Valid test model serializes as expected.")]
        [MemberData(nameof(SerializeTestArgs))]
        public async void SerializeTest(string xmlString, TestModel testModel)
        {
            var serializedModel = GovDeliveryUtils.ModelToStringContent(testModel);

            var serializedModelString = await serializedModel.ReadAsStringAsync();

            Assert.Equal(xmlString, serializedModelString);
        }

        [Theory(DisplayName = "Valid test model deserializes as expected.")]
        [MemberData(nameof(SerializeTestArgs))]
        public void DeserializeTest(string xmlString, TestModel testModel)
        {
            var serializer = new XmlSerializer(typeof(TestModel));

            using (var xmlStream = xmlString.ToStream())
            {
                var deserializedModel = (TestModel)serializer.Deserialize(xmlStream);

                Assert.Equal(testModel.TestArray.Items.Count(), deserializedModel.TestArray.Items.Count());
                Assert.Equal(testModel.TestArray.Items.ElementAt(0).TestValue, deserializedModel.TestArray.Items.ElementAt(0).TestValue);
                Assert.Equal(testModel.TestArray.Items.ElementAt(1).TestValue, deserializedModel.TestArray.Items.ElementAt(1).TestValue);
                Assert.Equal(testModel.TestArray.Items.ElementAt(2).TestValue, deserializedModel.TestArray.Items.ElementAt(2).TestValue);

                Assert.Equal(testModel.TestBool, deserializedModel.TestBool);
                Assert.Equal(testModel.TestInt, deserializedModel.TestInt);
                Assert.Equal(testModel.TestString, deserializedModel.TestString);
            }

        }

        public static IEnumerable<object> SerializeTestArgs()
        {
            yield return new object[] {
                
@"<?xml version=""1.0"" encoding=""utf-8""?>
<test-model>
  <test-array type=""array"">
    <test-item>
      <test-value>foo</test-value>
    </test-item>
    <test-item>
      <test-value>bar</test-value>
    </test-item>
    <test-item>
      <test-value>baz</test-value>
    </test-item>
  </test-array>
  <test-string>Hello, world!</test-string>
  <test-int type=""integer"">42</test-int>
  <test-bool type=""boolean"">true</test-bool>
</test-model>",

                new TestModel
                {
                    TestArray = new TestArrayModel
                    {
                        Items = new TestItem[] {
                            new TestItem { TestValue = "foo" },
                            new TestItem { TestValue = "bar" },
                            new TestItem { TestValue = "baz" },
                        }
                    },
                    TestString = "Hello, world!",
                    TestInt = 42,
                    TestBool = true,
                },
            };
        }
    }

    public class TestModel : IXDocConvertible<TestModel>
    {
        public List<TestItem> TestArray { get; set; }

        public string TestString { get; set; }

        public int TestInt { get; set; }

        public bool TestBool { get; set; }

        public XDocument ToXDoc(TestModel model)
        {
            var xDoc = new XDocument("test-model");

 
            return xDoc;
        }

        public TestModel FromXDoc(XDocument xDoc)
        {
            throw new NotImplementedException();
        }
    }


    public class TestItem
    {
        public string TestValue { get; set; }
    }



}