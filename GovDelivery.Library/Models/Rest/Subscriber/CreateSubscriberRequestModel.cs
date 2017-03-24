using GovDelivery.Data.Entities;
using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Subscriber
{
    [XmlRoot(ElementName = "subscriber")]
    public class CreateSubscriberRequestModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "country-code")]
        public string CountryCode { get; set; }

        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "send-notifications")]
        public bool SendSubscriberUpdateNotifications { get; set; }

        [XmlElement(DataType = "integer", ElementName = "digest-for")]
        public SendBulletins SendBulletins { get; set; }

    }

    [XmlRoot(ElementName = "subscriber")]
    public class CreateSubscriberResponseModel
    {
        [XmlElement(ElementName = "to-param")]
        public int SubscriberId { get; set; }

        [XmlElement(ElementName = "link")]
        public LinkModel SubscriberInfoLink { get; set; }
    }
}
