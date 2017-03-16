using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Library.Data.Csv.TypeConversion
{
    public class QuotedInt32Converter : Int32Converter
    {
        public override object ConvertFromString(string text, ICsvReaderRow row, CsvPropertyMapData propertyMapData)
        {
            if (text == null) throw new Exception("No text passed in.");

            var dequotedText = text.Replace('"', ' ').Trim();

            return base.ConvertFromString(dequotedText, row, propertyMapData);
        }
    }
}
