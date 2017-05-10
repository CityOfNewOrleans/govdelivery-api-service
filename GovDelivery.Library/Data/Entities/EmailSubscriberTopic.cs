using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Library.Data.Entities
{
    public class EmailSubscriberTopic
    {
        public Guid EmailSubscriberId { get; set; }
        public EmailSubscriber EmailSubscriber { get; set; }

        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
