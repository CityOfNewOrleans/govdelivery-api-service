using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot(ElementName = "categories", DataType = "array")]
    public class ListCategoriesResponseModel: List<ReadCategoryResponseModel>, IEnumerable<ReadCategoryResponseModel>
    {
        
    }
}
