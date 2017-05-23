using GovDelivery.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace GovDelivery.Entity
{
    public interface IGovDeliveryContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Page> Pages { get; set; }
        DbSet<EmailSubscriber> Subscribers { get; set; }
        DbSet<Topic> Topics { get; set; }
    }
}