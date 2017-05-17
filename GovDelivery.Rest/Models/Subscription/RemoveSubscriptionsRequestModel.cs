using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscription
{
    [XmlRoot("subscriber")]
    public class RemoveTopicSubscriptionsRequestModel
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("send-notifications ")]
        public SerializableBool SendNotifications { get; set; }

        [XmlElement("topics")]
        public SerializableTopicArray Topics { get; set; }

        public class SerializableTopicArray : BaseSerializableArray<TopicModel>
        {
            [XmlElement("topic")]
            public override List<TopicModel> Items { get; set; }
        }
    }
}
