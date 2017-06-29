using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using GovDelivery.Rest.Models;
using GovDelivery.Rest.Models.Subscriber;
using GovDelivery.Rest.Models.Subscription;
using GovDelivery.Rest.Models.Topic;
using GovDelivery.Rest.Models.Category;

namespace GovDelivery.Rest
{
    public interface IGovDeliveryApiService
    {
        // Subscriber
        Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel);
        Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotification);

        // Topic Subscriptions
        Task<HttpResponseMessage> AddTopicSubscriptionsAsync(AddTopicSubscriptionsRequestModel requestModel);
        Task<GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>> RemoveTopicSubscriptionsAsync(RemoveTopicSubscriptionsRequestModel requestModel);
        Task<GovDeliveryResponseModel<ListSubscriberTopicsResponseModel>> ListSubscriberTopicsAsync(string email);

        // Topic
        Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        Task<HttpResponseMessage> DeleteTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync();

        // Topic Categories:
        Task<HttpResponseMessage> UpdateTopicCategoriesAsync(string topicCode, UpdateTopicCategoriesRequestModel requestModel);
        Task<GovDeliveryResponseModel<ListTopicCategoriesResponseModel>> ListTopicCategoriesAsync(string topicCode);
        Task<GovDeliveryResponseModel<ListSubscriberCategoriesResponseModel>> ListSubscriberCategoriesAsync(string email);

        // Category
        Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel);
        Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel);
        Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync();


        
    }
}