using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Csv.Interfaces
{
    public interface ICsvImporter
    {
        Task<IEnumerable<IImportSubscriberModel>> ImportSubscribersAsync(string fileContents);
    }
}
