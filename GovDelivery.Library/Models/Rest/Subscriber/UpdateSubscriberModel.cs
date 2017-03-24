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
    public class UpdateSubscriberRequestModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "send-notifications", DataType = "boolean")]
        public bool SendNotifications { get; set; }

        [XmlElement(ElementName = "digest-for")]
        public SendBulletins SendBulletins { get; set; }
    }

    [XmlRoot(ElementName = "subscriber")]
    public class UpdateSubscriberResponseModel
    {
        [XmlElement(ElementName = "to-param")]
        public string ToParam { get; set; }

        [XmlElement(ElementName = "link")]
        public LinkModel SubscriberInfoLink { get; set; } 
    }

}
