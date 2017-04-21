using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Subscription
{
    [XmlRoot("subscriber")]
    public class RemoveSubscriptionsResponseModel
    {
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("link")]
        public LinkModel Link { get; set; }

        [XmlElement("subscriber-uri")]
        public string SubscriberUri { get; set; }
    }
}
