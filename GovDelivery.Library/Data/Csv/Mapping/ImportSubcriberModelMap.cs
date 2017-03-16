using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GovDelivery.Library.Data.Csv.TypeConversion;
using GovDelivery.Library.Models.Csv;

namespace GovDelivery.Library.Data.Csv.Mapping
{
    public class ImportSubcriberModelMap : CsvClassMap<ImportSubscriberModel>
    {
        public ImportSubcriberModelMap()
        {
            Map(m => m.Contact)
                .Name("Contacts")
                .TypeConverter<QuotedStringConverter>();

            Map(m => m.Subscriptions)
                .Name("Subscriptions")
                .TypeConverter<QuotedInt32Converter>();

            Map(m => m.Origin)
                .Name("Origin")
                .TypeConverter<SubscriberOriginConverter>();

            Map(m => m.LastModified)
                .Name("Modified")
                .TypeConverter<DateStringConverter>();
        }
    }
}