using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Util = GovDelivery.Library.Utils.GovDeliveryUtils;

namespace GovDelivery.Library.Data.Csv.TypeConversion
{
    public class DateStringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, ICsvReaderRow row, CsvPropertyMapData propertyMapdata)
        {
            var trimmed = text.Trim(new char[] { ' ', '"', '\n' });
            return Util.DateStringToDateTimeUtc(trimmed);
        }
    }
}