using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot("categories")]
    public class ListCategoriesResponseModel: List<ReadCategoryResponseModel>, IEnumerable<ReadCategoryResponseModel>
    {
        [XmlElement]
        public SerializableCategoryArray Items { get; set; }

        public class SerializableCategoryArray : BaseSerializableArray<ReadCategoryResponseModel>
        {
            [XmlElement("category")]
            public override List<ReadCategoryResponseModel> Items { get; set; }
        }
    }
}
