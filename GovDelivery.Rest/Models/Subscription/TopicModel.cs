using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscription
{
    [XmlRoot(ElementName = "topic")]
    public class TopicModel
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }
}
