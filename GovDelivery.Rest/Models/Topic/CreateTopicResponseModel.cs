using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Topic
{
    [XmlRoot("topic")]
    public class CreateTopicResponseModel
    {
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("topic-uri")]
        public string TopicUri { get; set; }
    }

}
