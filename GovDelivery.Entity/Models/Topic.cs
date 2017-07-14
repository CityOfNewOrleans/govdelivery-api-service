using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity.Models
{
    public class Topic
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public ICollection<TopicSubscription> EmailSubscriberTopics { get; set; }

        public ICollection<CategoryTopic> TopicCategories { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public bool WirelessEnabled { get; set; }

        public bool SendByEmailEnabled { get; set; }

        public string RssFeedUrl { get; set; }

        public string RssFeedTitle { get; set; }

        public string RssFeedDescription { get; set; }

        public bool PageWatchEnabled { get; set; }

        public bool PageWatchSuspended { get; set; }

        public int DefaultPageWatchResults { get; set; }

        public bool PageWatchAutosend { get; set; }

        public PageWatchType? PagewatchType { get; set; }

        public bool WatchTaggedContent { get; set; }

        public List<Page> Pages { get; set; }
        
        public List<Category> Categories { get; set; } 
    }

    public enum PageWatchType
    {
        HTML = 1,
        RssAtomFeed = 2,
        File = 3,
    }
}
