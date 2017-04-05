using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot("category")]
    public class ReadCategoryResponseModel
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "allow-subscriptions", DataType = "boolean")]
        public bool AllowSubscriptions { get; set; }

        [XmlElement(ElementName = "default-open", DataType = "boolean")]
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

        [XmlElement(ElementName = "link")]
        public LinkModel Link { get; set; }

    }
}
