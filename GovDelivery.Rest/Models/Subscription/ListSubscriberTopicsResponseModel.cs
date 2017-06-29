using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscription
{
    [XmlRoot("topics")]
    public class ListSubscriberTopicsResponseModel : BaseSerializableArray<SubscriberTopic>
    {
        [XmlElement("topic")]
        public override List<SubscriberTopic> Items { get; set; }
    }

    public class SubscriberTopic
    {
        [XmlElement("to-param")]
        public string TopicCode { get; set; }

        [XmlElement("link")]
        public LinkModel TopicLink { get; set; }
    }
}
