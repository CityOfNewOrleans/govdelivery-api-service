using GovDelivery.Data.Entities;
using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Subscriber
{
    [XmlRoot("subscriber")]
    public class CreateSubscriberRequestModel
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("country-code")]
        public string CountryCode { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("send-notifications")]
        public SerializableBool SendSubscriberUpdateNotifications { get; set; }

        [XmlElement("digest-for")]
        public SendBulletins SendBulletins { get; set; }

    }

    [XmlRoot("subscriber")]
    public class CreateSubscriberResponseModel
    {
        [XmlElement("to-param")]
        public int SubscriberId { get; set; }

        [XmlElement("link")]
        public LinkModel SubscriberInfoLink { get; set; }
    }
}
