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

        public override async Task<GovDeliveryResponseModel<DeleteSubscriberResponseModel>> DeleteSubscriberAsync(DeleteSubscriberRequestModel model)
        {
            var res = await client.DeleteAsync("subscriptions.xml");

            return new GovDeliveryResponseModel<DeleteSubscriberResponseModel> {
                HttpResponse = res,
                Data = new DeleteSubscriberResponseModel()
            };
        }

        // Topic
        public override async Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync (AddTopicToSubscriberRequestModel model)
        {
            var res = await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(model));

            var responseModel = new AddTopicToSubscriberResponseModel
            {

            };

            return new GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>();
        }

        public override async Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(DeleteTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
