using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Category
{
    public class UpdateCategoryResponseModel
    {
        [XmlElement("to-param")]
        public string ToParam { get; set; }

        [XmlElement("category-uri")]
        public string CategoryUri { get; set; }
    }
}
