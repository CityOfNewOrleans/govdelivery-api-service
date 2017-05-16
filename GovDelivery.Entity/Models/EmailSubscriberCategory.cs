using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class EmailSubscriberCategory
    {
        public Guid EmailSubscriberId { get; set; }
        public EmailSubscriber EmailSubscriber { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
