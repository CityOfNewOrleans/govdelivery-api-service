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
    public class UpdateSubscriberResponseModel
    {
        /// <summary>
        /// Contains the subscriber's ID
        /// </summary>
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        /// <summary>
        ///  URI to see additional information about the subscriber, such as their category subscriptions, topic subscriptions, etc.
        /// </summary>
        [XmlElement("link")]
        public LinkModel SubscriberInfoLink { get; set; }
    }
}
