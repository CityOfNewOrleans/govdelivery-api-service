using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Category
{
    [XmlRoot("category")]
    public class ReadCategoryResponseModel
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("allow-subscriptions")]
        public SerializableBool AllowSubscriptions { get; set; }

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

        [XmlElement("link")]
        public LinkModel Link { get; set; }

    }
}
