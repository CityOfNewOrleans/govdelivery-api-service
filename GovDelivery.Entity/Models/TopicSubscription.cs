using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class TopicSubscription
    {
        public Guid SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }

        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
