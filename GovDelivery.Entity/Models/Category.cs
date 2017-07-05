using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public Category ParentCategory { get; set; }

        public List<Category> Subcategories { get; set; }

        public ICollection<TopicCategory> TopicCategories { get; set; }

        public ICollection<CategorySubscription> EmailSubscriberCategories { get; set; }

        public string Code { get; set; }

        public bool AllowUserInitiatedSubscriptions { get; set; }

        public bool DefaultOpen { get; set; }

        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }


        public string QuickSubscribePageCode { get; set; }
    }
}
