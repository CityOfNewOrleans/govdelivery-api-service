using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscriber
{
    [XmlRoot("subscriber")]
    public class ReadSubscriberResponseModel
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("send-notifications")]
        public SerializableBool SendSubscriberUpdateNotifications { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Use SendBulletins enum to set acceptable values.
        /// </summary>
        [XmlElement("digest-for")]
        public SerializableInt DigestFor { get; set; }

        [XmlElement("id")]
        public SerializableInt Id { get; set; }

        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("lock-version")]
        public SerializableInt LockVersion { get; set; }

        [XmlElement("link")]
        public List<LinkModel> Links { get; set; }

        [XmlIgnore]
        public LinkModel SelfLink { get { return Links.FirstOrDefault(l => l.Rel == "self"); } }

        [XmlIgnore]
        public LinkModel CategoriesLink { get { return Links.FirstOrDefault(l => l.Rel == "categories"); } }

        [XmlIgnore]
        public LinkModel TopicsLink { get { return Links.FirstOrDefault(l => l.Rel == "topics"); } }

        [XmlIgnore]
        public LinkModel QuestionsLink { get { return Links.FirstOrDefault(l => l.Rel == "questions"); } }

        [XmlIgnore]
        public LinkModel QuestionResponsesLink { get { return Links.FirstOrDefault(l => l.Rel == "responses"); } }
    }

    public enum SendBulletins
    {
        Immediately = 0,
        Daily = 1,
        Weekly = 7,
    }
}