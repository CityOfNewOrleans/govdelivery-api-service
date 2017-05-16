using GovDelivery.Csv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Csv.Interfaces
{
    public interface IImportSubscriberModel
    {
        string Contact { get; set; }

        int Subscriptions { get; set; }

        SubscriberOrigin Origin { get; set; }

        DateTime LastModified { get; set; }

        SubscriberType Type { get; }
    }
}
