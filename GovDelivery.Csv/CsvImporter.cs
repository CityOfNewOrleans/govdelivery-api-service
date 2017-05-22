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

        public async Task<string> ReadCsvFileContentsAsync(string filePath)
        {
            if (!filePath.ToLowerInvariant().EndsWith(".csv"))
                throw new Exception("File must be .csv format");

            using (var reader = File.OpenText(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<IEnumerable<IImportSubscriberModel>> ImportSubscribersAsync(string filePath)
        {
            var fileContents = await ReadCsvFileContentsAsync(filePath);

            var trimmedFileContents = fileContents.Trim();

            using (var txtReader = new StringReader(trimmedFileContents))
            using (var csvReader = new CsvReader(txtReader))
            {
                csvReader.Configuration.RegisterClassMap<ImportSubcriberModelMap>();
                return csvReader.GetRecords<ImportSubscriberModel>().ToList();
            }

        }
        
    }
}