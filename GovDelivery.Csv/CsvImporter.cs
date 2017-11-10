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

        public async Task<IEnumerable<IImportSubscriberModel>> ImportSubscriberAsync(Stream fileStream) =>
            ParseFileContents(await ReadStreamContentsAsync(fileStream));

        public async Task<IEnumerable<IImportSubscriberModel>> ImportSubscribersAsync(string filePath) =>
            ParseFileContents(await ReadCsvFileContentsAsync(filePath));

        protected IEnumerable<IImportSubscriberModel> ParseFileContents(string fileContents) {
            using (var txtReader = new StringReader(fileContents))
            using (var csvReader = new CsvReader(txtReader))
            {
                csvReader.Configuration.RegisterClassMap<ImportSubcriberModelMap>();
                return csvReader.GetRecords<ImportSubscriberModel>().ToList();
            }
        }
        
        protected async Task<string> ReadCsvFileContentsAsync(string filePath)
        {
            if (!filePath.ToLowerInvariant().EndsWith(".csv"))
                throw new Exception("File must be .csv format");

            using (var reader = File.OpenText(filePath))
            {
                return (await reader.ReadToEndAsync()).Trim();
            }
        }

        protected async Task<string> ReadStreamContentsAsync(Stream fileStream) {
            using (var reader = new StreamReader(fileStream)) {
               return await reader.ReadToEndAsync();
            }
        }
    }
}