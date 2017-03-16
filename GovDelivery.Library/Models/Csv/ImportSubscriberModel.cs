using GovDelivery.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Library.Models.Csv
{
    public class ImportSubscriberModel : IImportSubscriberModel
    {
        public string Contact { get; set; }

        public int Subscriptions { get; set; }

        public SubscriberOrigin Origin { get; set; }

        public DateTime LastModified { get; set; }

        public SubscriberType Type
        {
            get
            {
                return string.IsNullOrWhiteSpace(Contact)
                    ? SubscriberType.None
                    : (Contact.Contains('@'))
                        ? SubscriberType.Email
                        : SubscriberType.Phone;
            }
        }
            
    }

    public enum SubscriberOrigin
    {
        Direct = 0,
        Upload = 2,
    }

    public enum SubscriberType
    {
        None = 0,
        Email = 1,
        Phone = 2,
    }
}
