using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Topic
{
    [XmlRoot(ElementName = "topic")]
    public class UpdateTopicResponseModel
    {
        [XmlElement(ElementName = "to-param")]
        public string ToParam { get; set; }

        [XmlElement(ElementName = "topic-uri")]
        public string TopicUri { get; set; }
    }
}
