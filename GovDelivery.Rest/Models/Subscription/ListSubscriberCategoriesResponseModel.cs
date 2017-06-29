using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Subscription
{
    [XmlRoot("categories")]
    public class ListSubscriberCategoriesResponseModel : BaseSerializableArray<SubscriberCategory>
    {
        [XmlElement("category")]
        public override List<SubscriberCategory> Items { get; set ; }
    }

    public class SubscriberCategory
    {
        [XmlElement("to-param")]
        public string CategoryCode { get; set; }

        [XmlElement("link")]
        public LinkModel Link { get; set; }
    }
}
