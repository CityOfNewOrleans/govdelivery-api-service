using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class CategorySubscription
    {
        public Guid SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
