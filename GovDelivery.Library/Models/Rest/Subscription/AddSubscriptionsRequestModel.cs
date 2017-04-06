using GovDelivery.Models.Rest.Subscriber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Subscription
{
    [XmlRoot(ElementName = "subscriber")]
    public class AddSubscriptionsRequestModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(DataType = "boolean", ElementName = "send-notifications ")]
        public bool SendNotifications { get; set; }

        [XmlElement(DataType = "array", ElementName = "topics")]
        public IEnumerable<TopicModel> Topics { get; set; }
    }
    
}
