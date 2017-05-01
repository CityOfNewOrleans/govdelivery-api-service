using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Topic
{
    [XmlRoot("categories")]
    public class ListTopicCategoriesResponseModel : BaseSerializableArray<TopicCategoryModel>
    {
        [XmlElement("category")]
        public override List<TopicCategoryModel> Items { get; set; }
    }

    public class TopicCategoryModel
    {
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("category-uri")]
        public string CategoryUri { get; set; }
    }
}
