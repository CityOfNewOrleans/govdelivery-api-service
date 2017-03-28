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

namespace GovDelivery.Library.Http
{
    public abstract class BaseGovDeliveryService : IGovDeliveryApiService
    {
        public BaseGovDeliveryService(string baseUri, string accountCode)
        {

        }

        public abstract Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        public abstract Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotifications);
        public abstract Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(string topicCode);
        public abstract Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync();
        public abstract Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        public abstract Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id);
        public abstract Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId);
        public abstract Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        public abstract Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel requestModel);
    }
}
