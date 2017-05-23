using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class TopicCategory
    {
        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
