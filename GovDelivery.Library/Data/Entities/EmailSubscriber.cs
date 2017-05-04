using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Library.Data.Entities
{
    public class EmailSubscriber
    {
        [Key]
        public Guid Id { get; set; }

        public int GovDeliveryId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string CountryCode { get; set; }

        public List<Topic> Topics { get; set; }

        public bool SendSubscriberUpdateNotifications { get; set; }

        /// <summary>
        /// How often the Subscriber receives bulletins. Maps to "digest-for" element in Xml Api.
        /// </summary>
        public BulletinFrequency BulletinFrequency { get; set; }

        public string Link { get; set; }

    }

    public enum BulletinFrequency
    {
        Immediately = 0,
        Daily = 1,
        Weekly = 7,
    }
}
