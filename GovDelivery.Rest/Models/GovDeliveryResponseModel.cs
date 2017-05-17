using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Rest.Models
{
    public class GovDeliveryResponseModel<T>
    {
        public HttpResponseMessage HttpResponse { get; set; }

        public T Data { get; set; }

        public GovDeliveryResponseModel() { }

        public GovDeliveryResponseModel(T data)
        {
            Data = data;
        }
    }
}
