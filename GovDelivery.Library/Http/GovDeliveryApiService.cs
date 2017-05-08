using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Library.Utils;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Subscription;
using GovDelivery.Library.Models.Rest.Subscriber;
using GovDelivery.Library.Models;

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

            var model = await GovDeliveryUtils.ResponseContentToModel<ReadSubscriberResponseModel>(res.Content);

            return new GovDeliveryResponseModel<ReadSubscriberResponseModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        /// <summary>
        /// Deletes a subscriber.
        /// </summary>
        /// <param name="email">The email of the subscriber to be deleted.</param>
        /// <param name="sendNotification">Determines whether to send a deletion confirmation message to the subscriber.</param>
        /// <returns></returns>
        public override async Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotification)
        {
            var encodedSubscriberId = GovDeliveryUtils.Base64Encode(email);

            if (sendNotification == true)
                return await client.DeleteAsync($"subscribers/{encodedSubscriberId}.xml?send_notifications=false");

            return await client.DeleteAsync($"subscribers/{encodedSubscriberId}.xml");
        }

        // Topic
        public override async Task<HttpResponseMessage> AddTopicSubscriptionsAsync (AddTopicSubscriptionsRequestModel requestModel)
        {
            return await client.PostAsync("subscriptions.xml", GovDeliveryUtils.ModelToStringContent(requestModel));
        }

        public override async Task<GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>> RemoveTopicSubscriptionsAsync(RemoveTopicSubscriptionsRequestModel requestModel)
        {
            var res = await client.SendAsync(new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(client.BaseAddress, "subscriptions.xml"),
                Content = GovDeliveryUtils.ModelToStringContent(requestModel)
            });

            return new GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<RemoveTopicSubscriptionsResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model)
        {
            var res = await client.PostAsync("topics.xml", GovDeliveryUtils.ModelToStringContent(model));

            return new GovDeliveryResponseModel<CreateTopicResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<CreateTopicResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode)
        {
            var res = await client.GetAsync($"topics/{topicCode}.xml");

            return new GovDeliveryResponseModel<ReadTopicResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<ReadTopicResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model)
        {
            var res = await client.PutAsync($"topics/{model.Code}.xml", GovDeliveryUtils.ModelToStringContent(model));

            return new GovDeliveryResponseModel<UpdateTopicResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<UpdateTopicResponseModel>(res.Content)
            };
        }

        public override async Task<HttpResponseMessage> DeleteTopicAsync(string topicCode)
        {
            return await client.DeleteAsync($"topics/{topicCode}");
        }

        public override async Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync()
        {
            var res = await client.GetAsync("topics.xml");

            return new GovDeliveryResponseModel<ListTopicsResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListTopicsResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ListTopicCategoriesResponseModel>> ListTopicCategoriesAsync(string topicCode)
        {
            var res = await client.GetAsync($"topics/{topicCode}/categories.xml");

            return new GovDeliveryResponseModel<ListTopicCategoriesResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListTopicCategoriesResponseModel>(res.Content)
            };
        }

        public override async Task<HttpResponseMessage> UpdateTopicCategoriesAsync(string topicCode, UpdateTopicCategoriesRequestModel requestModel) =>
            await client.PutAsync($"topics/{topicCode}/categories.xml", GovDeliveryUtils.ModelToStringContent(requestModel));

        public override async Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel requestModel)
        {
            var res = await client.PostAsync("categories.xml", GovDeliveryUtils.ModelToStringContent(requestModel));

            return new GovDeliveryResponseModel<CreateCategoryResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<CreateCategoryResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode)
        {
            var res = await client.GetAsync($"categories/{categoryCode}.xml");

            return new GovDeliveryResponseModel<ReadCategoryResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<ReadCategoryResponseModel>(res.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel)
        {
            var res = await client.PutAsync($"categories/{requestModel.Code}.xml", GovDeliveryUtils.ModelToStringContent(requestModel));

            return new GovDeliveryResponseModel<UpdateCategoryResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<UpdateCategoryResponseModel>(res.Content)
            };
        }

        public override async Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode) =>
            await client.DeleteAsync($"categories/{categoryCode}.xml");

        public override async Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync()
        {
            var res = await client.GetAsync("categories.xml");

            return new GovDeliveryResponseModel<ListCategoriesResponseModel>
            {
                HttpResponse = res,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListCategoriesResponseModel>(res.Content)
            };
        }
    }
}
