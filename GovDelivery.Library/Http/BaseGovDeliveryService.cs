using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Models;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using System.Net.Http;
using GovDelivery.Library.Models.Rest.Category;

namespace GovDelivery.Library.Http
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


        // Topic
        public abstract Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode);
        public abstract Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteTopicAsync(string topicCode);
        public abstract Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync();


        // Category
        public abstract Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel);
        public abstract Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode);
        public abstract Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode);
        public abstract Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryResponseModel>>> ListCategoriesAsync(int topicId);
        public abstract Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryResponseModel>>> ReadTopicCategoriesAsync(int topicId);


        public abstract Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel requestModel);

    }
}
