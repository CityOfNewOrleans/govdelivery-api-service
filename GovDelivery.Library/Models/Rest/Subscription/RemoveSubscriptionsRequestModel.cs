using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Subscription
{
    public class RemoveSubscriptionsRequestModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(DataType = "boolean", ElementName = "send-notifications ")]
        public bool SendNotifications { get; set; }

        [XmlElement(DataType = "array", ElementName = "topics")]
        public IEnumerable<TopicModel> Topics { get; set; }
    }
}
