using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Subscriber
{
    public class ReadSubscriberResponseModel
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "send-notifications")]
        public bool SendSubscriberUpdateNotifications { get; set; }

        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "digest-for", DataType = "integer")]
        public SendBulletins DigestFor { get; set; }

        [XmlElement(ElementName = "id", DataType = "integer")]
        public int Id { get; set; }

        [XmlElement(ElementName = "phone")]
        public string Link { get; set; }

    }

    public enum SendBulletins
    {
        Immediately = 0,
        Daily = 1,
        Weekly = 7,
    }
}