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

namespace GovDelivery.Http
{
    public class GovDeliveryApiService : IGovDeliveryApiService
    {
        public const string STAGING_URI = "https://stage-api.govdelivery.com";

        public const string MAIN_URI = "https://api.govdelivery.com";

        private XmlSerializer subscriberSerializer = new XmlSerializer(typeof(ReadSubscriberResponseModel));
        private XmlSerializer topicSubscriptionSerializer = new XmlSerializer(typeof(AddTopicToSubscriberRequestModel));

        private HttpClient client;

        // Subscriber
        public GovDeliveryApiService (string baseUri, string accountCode)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{baseUri}/api/account/{accountCode}");
        }

        public async Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel model)
        {
            var res = await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model, subscriberSerializer));

            return new GovDeliveryResponseModel<UpdateSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = null,
            };
        }

        public async Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel model)
        {
            var res = await client.PutAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model, subscriberSerializer));
            return new GovDeliveryResponseModel<CreateSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = null,
            };
        }

        public async Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email)
        {

            var res = await client.GetAsync("subscriptions.xml");

            var model = new ReadSubscriberResponseModel();

            return new GovDeliveryResponseModel<ReadSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        public async Task<Models.GovDeliveryResponseModel<DeleteSubscriberResponseModel>> DeleteSubscriberAsync(DeleteSubscriberRequestModel model)
        {
            var res = await client.DeleteAsync("subscriptions.xml");

            return new GovDeliveryResponseModel<DeleteSubscriberResponseModel> {
                HttpResponse = res,
                Data = new DeleteSubscriberResponseModel()
            };
        }

        // Topic
        public async Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync (AddTopicToSubscriberRequestModel model)
        {
            var res = await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model, topicSubscriptionSerializer));

            var responseModel = new AddTopicToSubscriberResponseModel
            {

            };

            return new GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>();
        }

        public Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(DeleteTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
