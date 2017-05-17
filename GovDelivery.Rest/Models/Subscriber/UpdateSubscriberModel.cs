using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscriber
{
    [XmlRoot("subscriber")]
    public class UpdateSubscriberRequestModel
    {
        /// <summary>
        /// This field indicates the subscriber ID. Leave null to allow the system to generate the subscriber's ID. If use an ID that has already in use, the call will return an error.
        /// </summary>
        public SerializableInt Id { get; set; }

        /// <summary>
        ///  The subscriber's email address. 
        /// </summary>
        [XmlElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Determines whether to send an email notification to the subscriber that their record has been updated.
        /// </summary>
        [XmlElement("send-notifications")]
        public SerializableBool SendNotifications { get; set; }

        /// <summary>
        /// Determine the frequency for how often the subscriber receives bulletins.
        /// </summary>
        [XmlElement("digest-for")]
        public SendBulletins SendBulletins { get; set; }
    }

}
