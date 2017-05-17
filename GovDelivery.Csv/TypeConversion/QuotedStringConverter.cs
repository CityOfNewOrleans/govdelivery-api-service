using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Csv.TypeConversion
{
    public class QuotedStringConverter : StringConverter
    {

        public override object ConvertFromString(string text, ICsvReaderRow row, CsvPropertyMapData propertyMapData)
        {
            var dequotedText = text.Trim(new char[] {' ', '"'});

            return base.ConvertFromString(dequotedText, row, propertyMapData);
        }

    }
}
