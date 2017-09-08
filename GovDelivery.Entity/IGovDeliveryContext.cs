using GovDelivery.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovDelivery.Entity
{
    public interface IGovDeliveryContext 
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Page> Pages { get; set; }
        DbSet<Subscriber> Subscribers { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<CategorySubscription> CategorySubscriptions { get; set; }
        DbSet<TopicSubscription> TopicSubscriptions { get; set; }
        DbSet<CategoryTopic> CategoryTopics { get; set; }

        EntityEntry Add(object entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void RemoveRange(IEnumerable<object> entities);
        void AddRange(params object[] entities);
        EntityEntry Remove(object entity);
    }
}