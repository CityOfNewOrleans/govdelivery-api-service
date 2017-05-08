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
    public class ListCategoriesResponseModel : BaseSerializableArray<ReadCategoryResponseModel>
    {
        [XmlElement("category")]
        public override List<ReadCategoryResponseModel> Items { get; set; }

    }
}
