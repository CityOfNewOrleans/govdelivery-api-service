using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Subscriber
{
    [XmlRoot("subscriber")]
    public class UpdateSubscriberRequestModel
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("send-notifications")]
        public SerializableBool SendNotifications { get; set; }

        [XmlElement("digest-for")]
        public SendBulletins SendBulletins { get; set; }
    }

    [XmlRoot("subscriber")]
    public class UpdateSubscriberResponseModel
    {
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("link")]
        public LinkModel SubscriberInfoLink { get; set; } 
    }

}
