using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using GovDelivery.Csv.Models;
using System;

namespace GovDelivery.Csv.TypeConversion
{
    public class SubscriberOriginConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, ICsvReaderRow row, CsvPropertyMapData propertyMapData)
        {
            switch (text.Trim().ToLowerInvariant())
            {
                case "direct": return SubscriberOrigin.Direct;
                case "upload": return SubscriberOrigin.Upload;
            }

            return 0;
        }

    }
}