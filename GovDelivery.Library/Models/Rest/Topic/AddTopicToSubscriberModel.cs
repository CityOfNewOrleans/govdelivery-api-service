using GovDelivery.Models.Rest.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Topic
{
    [XmlRoot(ElementName = "subscriber")]
    public class AddTopicToSubscriberModel
    {
        [XmlIgnore()]
        public ReadSubscriberModel Subscriber { get; set; }

        [XmlElement(DataType = "array", ElementName = "topics")]
        public IEnumerable<TopicModel> Topics { get; set; }
    }

    [XmlRoot(ElementName = "topic")]
    public class TopicModel
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "send-notifications")]
        public bool NotifySubscriberOfTheseChanges { get; set; }
    }
}
