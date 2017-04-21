using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Models.Rest.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Subscription
{
    [XmlRoot("subscriber")]
    public class AddSubscriptionsRequestModel
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("send-notifications ")]
        public SerializableBool SendNotifications { get; set; }

        [XmlElement("topics")]
        public SerializableTopicsArray Topics { get; set; }

        public class SerializableTopicsArray : BaseSerializableArray<TopicModel>
        {
            [XmlElement("topic")]
            public override List<TopicModel> Items { get; set; }
        }
    }
    
}
