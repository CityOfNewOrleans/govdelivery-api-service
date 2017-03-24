using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Subscriber
{
    [XmlRoot(ElementName = "subscriber")]
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

        [XmlElement(ElementName = "to-param")]
        public string ToParam { get; set; }

        [XmlElement(ElementName = "link")]
        public Link SelfLink { get; set; }

        [XmlElement(ElementName = "link")]
        public Link CategoryLink { get; set; }

        [XmlElement(ElementName = "link")]
        public Link TopicsLink { get; set; }

        [XmlElement(ElementName = "link")]
        public Link QuestionsLink { get; set; }

        [XmlElement(ElementName = "link")]
        public Link QuestionResponsesLink { get; set; }

        public class Link
        {
            [XmlAttribute(AttributeName = "rel")]
            public string Rel { get; set; }

            [XmlAttribute(AttributeName = "href")]
            public string Href { get; set; }
        }

    }

    public enum SendBulletins
    {
        Immediately = 0,
        Daily = 1,
        Weekly = 7,
    }
}