using Fclp;
using GovDelivery.Data.Csv;
using GovDelivery.Library.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GovDelivery.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();

            p.Setup<string>('i', "import")
                .Callback(path => {
                    var importer = new CsvImporter();


                    var optionsBuilder = new DbContextOptionsBuilder<GovDeliveryContext>()
                        .UseSqlServer(ConfigurationManager.ConnectionStrings["GovDeliveryContext"].ConnectionString);

                    var context = new GovDeliveryContext(optionsBuilder.Options);
                });

            
        }

        protected static void ImportSubscribers (string csvPath)
        {

        }
    }
}
