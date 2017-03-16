using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Category
{
    [XmlRoot]
    public class ReadCategoryModel
    {
        [XmlElement(ElementName = "code")]
        public string CategoryCode { get; set; }

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
