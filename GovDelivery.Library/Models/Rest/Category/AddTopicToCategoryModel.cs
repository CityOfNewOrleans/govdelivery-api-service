using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot("topic")]
    public class UpdateTopicCategoriesRequestModel
    {
        [XmlElement("categories")]
        public UpdateTopicCategoriesCategoryArrayModel Categories { get; set; }
    }

    public class UpdateTopicCategoriesCategoryArrayModel : BaseSerializableArray<UpdateTopicCategoriesCategoryModel>
    {
        [XmlElement("category")]
        public override List<UpdateTopicCategoriesCategoryModel> Items { get; set; }
    }

    public class UpdateTopicCategoriesCategoryModel
    {
        [XmlElement("code")]
        public string Code { get; set; }
    }
}
