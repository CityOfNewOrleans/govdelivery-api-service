using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using GovDelivery.Rest.Models;
using GovDelivery.Rest.Models.Subscriber;
using GovDelivery.Rest.Models.Subscription;
using GovDelivery.Rest.Models.Topic;
using GovDelivery.Rest.Models.Category;

namespace GovDelivery.Rest
{
    public abstract class BaseGovDeliveryService : IGovDeliveryApiService
    {
        protected string baseUri { get; set; }
        protected string accountCode { get; set; }

        public BaseGovDeliveryService(string baseUri, string accountCode)
        {
            this.baseUri = baseUri;
            this.accountCode = accountCode;
        }

        // Subscriber
        public abstract Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        public abstract Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotifications);

        // Topic Subscriptions
        public abstract Task<HttpResponseMessage> AddTopicSubscriptionsAsync(AddTopicSubscriptionsRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>> RemoveTopicSubscriptionsAsync(RemoveTopicSubscriptionsRequestModel requestModel);

        // Topic
        public abstract Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode);
        public abstract Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteTopicAsync(string topicCode);
        public abstract Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync();

        // Topic Categories:
        public abstract Task<GovDeliveryResponseModel<ListTopicCategoriesResponseModel>> ListTopicCategoriesAsync(string topicCode);
        public abstract Task<HttpResponseMessage> UpdateTopicCategoriesAsync(string topicCode, UpdateTopicCategoriesRequestModel requestModel);


        // Category
        public abstract Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel);
        public abstract Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode);
        public abstract Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode);
        public abstract Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync();

    }
}
