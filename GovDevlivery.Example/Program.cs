using CityBusiness.Data;
using Fclp;
using GovDelivery.Data.Csv;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    var dbOptionsBuilder = new DbContextOptionsBuilder<GovDeliveryContext>()
                    .UseInMemoryDatabase();
                    
                });

            
        }
    }
}
