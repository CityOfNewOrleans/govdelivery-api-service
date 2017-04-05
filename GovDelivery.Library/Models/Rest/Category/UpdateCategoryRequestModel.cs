using System;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot(ElementName = "category")]
    public class UpdateCategoryRequestModel
    {
        [XmlIgnore]
        public Guid Id { get; set; }

        [XmlElement(ElementName = "allow-subscriptions")]
        public bool AllowUserInitiatedSubscriptions { get; set; }

        [XmlElement(ElementName = "default-open")]
        public bool DefaultOpen { get; set; }

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
}
