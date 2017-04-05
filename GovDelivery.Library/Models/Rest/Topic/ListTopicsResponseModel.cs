using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Models.Rest.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Topic
{
    [XmlRoot(ElementName = "topics", DataType = "array")]
    public class ListTopicsResponseModel : List<ListTopicsResponseModel.Topic>, IEnumerable<ListTopicsResponseModel.Topic>
    {

        [XmlRoot(ElementName = "topic")]
        public class Topic
        {
            [XmlElement(ElementName = "code")]
            public string Code { get; set; }

            [XmlElement(ElementName = "description", IsNullable = true)]
            public string Description { get; set; }

            [XmlElement(ElementName = "name")]
            public string Name { get; set; }

            [XmlElement(ElementName = "short-name")]
            public string ShortName { get; set; }

            [XmlElement(ElementName = "wireless-enabled", DataType = "boolean")]
            public bool WirelessEnabled { get; set; }

            [XmlElement(ElementName = "visibility")]
            public TopicVisibility Visibility { get; set; }

            [XmlElement(ElementName = "link")]
            public LinkModel Link { get; set; }
        }

    }
}
