using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovDelivery.Rest;

namespace GovDelivery.ConsoleApp.Configuration
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public GovDelivery GovDelivery { get; set; }
    }

    public class GovDelivery
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public GovDeliveryServer Server { get; set; }

        public string AccountCode { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class ConnectionStrings
    {
        public string GovDelivery { get; set; }
    }
}
