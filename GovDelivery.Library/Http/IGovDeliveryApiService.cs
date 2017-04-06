using System.Collections.Generic;
using System.Threading.Tasks;
using GovDelivery.Models;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Topic;
using System.Net.Http;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Subscription;

namespace GovDelivery.Library.Http
{
    public interface IGovDeliveryApiService
    {
        // Subscriber
        Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel);
        Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotification);

        // Subscriptions
        Task<HttpResponseMessage> AddSubscriptionsAsync(AddSubscriptionsRequestModel requestModel);
        Task<GovDeliveryResponseModel<RemoveSubscriptionsResponseModel>> RemoveSubscriptionsAsync(RemoveSubscriptionsRequestModel requestModel);
        

        // Topic
        Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        Task<HttpResponseMessage> DeleteTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync();

        // Category
        Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel);
        Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel);
        Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync(int topicId);


        Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel requestModel);
    }
}