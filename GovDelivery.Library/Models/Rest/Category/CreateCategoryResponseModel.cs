using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    [XmlRoot(ElementName = "category")]
    public class CreateCategoryResponseModel
    {
        [XmlElement(ElementName = "to-param")]
        public string ToParam { get; set; }

        [XmlElement(ElementName = "category-uri")]
        public string CategoryUri { get; set; }
    }
}
