using System;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Category
{
    [XmlRoot(ElementName = "category")]
    public class CreateCategoryModel
    {
        [XmlIgnore]
        public Guid Id { get; set; }

        [XmlElement(ElementName = "code")]
        public string AccountCode { get; set; }

        [XmlElement(ElementName = "allow-subscriptions")]
        public bool AllowUserInitiatedSubscriptions { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "short-name")]
        public string ShortName { get; set; }

        [XmlElement(ElementName = "parent")]
        public Parent Parent { get; set; }

        [XmlElement(ElementName = "qs_page")]
        public QuickSubscribePage QuickSubscribePage { get; set; }
        
    }

    public class Parent
    {
        [XmlElement(ElementName = "code")]
        public string CategoryCode { get; set; }
    }

    public class QuickSubscribePage
    {
        [XmlElement(ElementName = "code")]
        public string PageCode { get; set; }
    }

}
