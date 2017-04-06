using GovDelivery.Models;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Library.Utils;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Subscription;

namespace GovDelivery.Library.Http
{
    public class GovDeliveryApiService : BaseGovDeliveryService
    {
        public const string STAGING_URI = "https://stage-api.govdelivery.com";

        public const string MAIN_URI = "https://api.govdelivery.com";

        private HttpClient client;

        // Subscriber
        public GovDeliveryApiService (string baseUri, string accountCode): base (baseUri, accountCode)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{baseUri}/api/account/{accountCode}");
        }

        public override async Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel model)
        {
            var res = await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model));

            return new GovDeliveryResponseModel<UpdateSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = null,
            };
        }

        public override async Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel model)
        {
            var res = await client.PutAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model));
            return new GovDeliveryResponseModel<CreateSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = null,
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email)
        {

            var res = await client.GetAsync("subscriptions.xml");

            var model = new ReadSubscriberResponseModel();

            return new GovDeliveryResponseModel<ReadSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        // TODO - SP: FIX THIS
        public override async Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotification)
        {
            return await client.DeleteAsync("subscriptions.xml");
        }

        // Topic
        public override async Task<HttpResponseMessage> AddSubscriptionsAsync (AddSubscriptionsRequestModel requestModel)
        {
            return await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(requestModel));
        }

        public override async Task<GovDeliveryResponseModel<RemoveSubscriptionsResponseModel>> RemoveSubscriptionsAsync(RemoveSubscriptionsRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<HttpResponseMessage> DeleteTopicAsync(string topicCode)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryResponseModel>>> ReadTopicCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public override Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel)
        {
            throw new NotImplementedException();
        }

        public override Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode)
        {
            throw new NotImplementedException();
        }

        public override Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode)
        {
            throw new NotImplementedException();
        }

        public override Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }
    }
}
