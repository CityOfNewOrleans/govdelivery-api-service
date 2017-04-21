using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Models.Rest.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Topic
{
    [XmlRoot("topics")]
    public class ListTopicsResponseModel : BaseSerializableArray<ListTopicsResponseModel.Topic>
    {
        [XmlElement("topic")]
        public override List<Topic> Items { get; set; }

        public class Topic
        {
            [XmlElement("code")]
            public string Code { get; set; }

            [XmlElement("description")]
            public NillableSerializableString Description { get; set; }

            [XmlElement("name")]
            public string Name { get; set; }

            [XmlElement("short-name")]
            public string ShortName { get; set; }

            [XmlElement("wireless-enabled")]
            public SerializableBool WirelessEnabled { get; set; }

            [XmlElement("visibility")]
            public TopicVisibility Visibility { get; set; }

            [XmlElement("link")]
            public LinkModel Link { get; set; }
        }

    }
}
