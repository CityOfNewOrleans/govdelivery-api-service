using GovDelivery.Rest.Models.Misc;
using System;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Category
{
    [XmlRoot("category")]
    public class CreateCategoryRequestModel
    {
        [XmlElement("code")]
        public string AccountCode { get; set; }

        [XmlElement("allow-subscriptions")]
        public SerializableBool AllowUserInitiatedSubscriptions { get; set; }

        [XmlElement("default-open")]
        public SerializableBool DefaultOpen { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("short-name")]
        public string ShortName { get; set; }

        [XmlElement("parent")]
        public Parent Parent { get; set; }

        [XmlElement("qs_page")]
        public QuickSubscribePage QuickSubscribePage { get; set; }
        
    }

    public class Parent
    {
        [XmlElement("code")]
        public string CategoryCode { get; set; }
    }

    public class QuickSubscribePage
    {
        [XmlElement("code")]
        public string PageCode { get; set; }
    }

}
