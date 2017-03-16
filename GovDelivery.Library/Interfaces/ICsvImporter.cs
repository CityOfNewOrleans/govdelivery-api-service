using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Library.Interfaces
{
    public interface ICsvImporter
    {
        Task<string> GetCsvFileContentsAsync(string filePath);

        IEnumerable<IImportSubscriberModel> ImportSubscribers(string fileContents);
    }
}
