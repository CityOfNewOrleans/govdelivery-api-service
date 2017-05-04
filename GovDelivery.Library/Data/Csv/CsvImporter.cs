using CsvHelper;
using GovDelivery.Library.Data;
using GovDelivery.Library.Data.Csv.Mapping;
using GovDelivery.Library.Data.Entities;
using GovDelivery.Library.Interfaces;
using GovDelivery.Library.Models.Csv;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GovDelivery.Data.Csv
{
    public class CsvImporter : ICsvImporter
    {
        private DbContextOptionsBuilder dbOptions { get; set; }

        public CsvImporter() : this(new DbContextOptionsBuilder()) { }

        public CsvImporter(DbContextOptionsBuilder dbOptions)
        {
            this.dbOptions = dbOptions != null ? dbOptions : new DbContextOptionsBuilder();
        }

        public async Task<string> GetCsvFileContentsAsync(string filePath)
        {
            if (!filePath.ToLowerInvariant().EndsWith(".csv"))
                throw new FileFormatException("File must be .csv format");

            using (var fileStream = File.Open(filePath, FileMode.Open))
            using (var reader = new StreamReader(fileStream, true))
            {
                var fileBytes = new byte[fileStream.Length];

                await fileStream.ReadAsync(fileBytes, 0, (int)fileStream.Length);

                return reader.CurrentEncoding.GetString(fileBytes);
            }
        }

        public IEnumerable<IImportSubscriberModel> ImportSubscribers(string fileContents)
        {
            var trimmedFileContents = fileContents.Trim();

            using (var txtReader = new StringReader(trimmedFileContents))
            using (var csv = new CsvReader(txtReader))
            {
                csv.Configuration.RegisterClassMap<ImportSubcriberModelMap>();
                return csv.GetRecords<ImportSubscriberModel>().ToList();
            }
        }
        
        public void SaveSubscribers(IEnumerable<IImportSubscriberModel> subscribers)
        {
            using (var ctx = new GovDeliveryContext(dbOptions.Options))
            {
                var entities = subscribers.Select(s => new EmailSubscriber
                {
                    Id = Guid.NewGuid(),
                    Email = s.Type == SubscriberType.Email ? s.Contact : null,
                    Phone = s.Type == SubscriberType.Phone ? s.Contact : null,
                    
                });

                ctx.AddRange(entities);
            }
        }
    }
}