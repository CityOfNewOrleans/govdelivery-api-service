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
    public class SerializationUtilsTests
    {
        public SerializationUtilsTests()
        {
        }

        [Theory(DisplayName = "Valid test model serializes as expected.")]
        [MemberData(nameof(SerializeTestArgs))]
        public async void SerializeTest(string filePath, TestModel testModel)
        {
            using (var reader = File.OpenText(filePath))
            {
                var xmlString = await reader.ReadToEndAsync();

                var serializedModel = SerializationUtils.ModelToStringContent(testModel);

                var serializedModelString = await serializedModel.ReadAsStringAsync();

                Assert.Equal(xmlString, serializedModelString);

            }
            
        }

        [Theory(DisplayName = "Valid test model deserializes as expected.")]
        [MemberData(nameof(SerializeTestArgs))]
        public async void DeserializeTest(string filePath, TestModel testModel)
        {
            var serializer = new XmlSerializer(typeof(TestModel));
            using (var reader = File.OpenText(filePath))
            {
                var xmlString = await reader.ReadToEndAsync();

                using (var xmlStream = xmlString.ToStream())
                {
                    var deserializedModel = (TestModel)serializer.Deserialize(xmlStream);

                    Assert.Equal(testModel.TestArray.Items.Count(), deserializedModel.TestArray.Items.Count());
                    Assert.Equal(testModel.TestArray.Items.ElementAt(0).TestValue, deserializedModel.TestArray.Items.ElementAt(0).TestValue);
                    Assert.Equal(testModel.TestArray.Items.ElementAt(1).TestValue, deserializedModel.TestArray.Items.ElementAt(1).TestValue);
                    Assert.Equal(testModel.TestArray.Items.ElementAt(2).TestValue, deserializedModel.TestArray.Items.ElementAt(2).TestValue);

                    Assert.Equal(testModel.TestBool.Value, deserializedModel.TestBool.Value);
                    Assert.Equal(testModel.TestInt.Value, deserializedModel.TestInt.Value);
                    Assert.Equal(testModel.TestString, deserializedModel.TestString);
                }
            }
        }

        public static IEnumerable<object> SerializeTestArgs()
        {
            yield return new object[] {
                @"MockData/mockSerializationData.xml",
                new TestModel
                {
                    TestArray = new TestModel.SerializableTestItemArray
                    {
                        Items = new List<TestModel.TestItem>
                        {
                            new TestModel.TestItem { TestValue = "foo" },
                            new TestModel.TestItem { TestValue = "bar" },
                            new TestModel.TestItem { TestValue = "baz" },
                        }
                    },
                    TestString = "Hello, world!",
                    TestInt = new SerializableInt { Value = 42 },
                    TestBool = new SerializableBool { Value = true },
                },
            };
        }
    }

    
    [XmlRoot("test-model")]
    public class TestModel
    {
        [XmlElement("test-array")]
        public SerializableTestItemArray TestArray { get; set; }

        [XmlElement("test-string")]
        public string TestString { get; set; }

        [XmlElement("test-int")]
        public SerializableInt TestInt { get; set; }

        [XmlElement("test-bool")]
        public SerializableBool TestBool { get; set; }


        public class TestItem
        {
            [XmlElement("test-value")]
            public string TestValue { get; set; }
        }

        public class SerializableTestItemArray : BaseSerializableArray<TestItem>
        {
            [XmlElement("test-item")]
            public override List<TestItem> Items { get; set; }

        }
    }


}