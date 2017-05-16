using CsvHelper;
using GovDelivery.Csv.Interfaces;
using GovDelivery.Csv.Models;
using GovDelivery.Library.Data.Csv.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GovDelivery.Csv
{
    public class CsvImporter : ICsvImporter
    {

        protected async Task<string> GetCsvFileContentsAsync(string filePath)
        {
            if (!filePath.ToLowerInvariant().EndsWith(".csv"))
                throw new Exception("File must be .csv format");

            using (var fileStream = File.Open(filePath, FileMode.Open))
            using (var reader = new StreamReader(fileStream, true))
            {
                var fileBytes = new byte[fileStream.Length];

                await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

                return reader.CurrentEncoding.GetString(fileBytes);
            }
        }

        public async Task<IEnumerable<IImportSubscriberModel>> ImportSubscribersAsync(string filePath)
        {
            var fileContents = await GetCsvFileContentsAsync(filePath);

            var trimmedFileContents = fileContents.Trim();

            using (var txtReader = new StringReader(trimmedFileContents))
            using (var csv = new CsvReader(txtReader))
            {
                csv.Configuration.RegisterClassMap<ImportSubcriberModelMap>();
                return csv.GetRecords<ImportSubscriberModel>().ToList();
            }
        }
        
        
    }
}