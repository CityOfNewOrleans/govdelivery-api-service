using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Subscription;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Library.Models.Rest.Subscriber;
using GovDelivery.Library.Models;

namespace GovDelivery.Library.Http
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
        

        // Topic
        Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        Task<HttpResponseMessage> DeleteTopicAsync(string topicCode);
        Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync();

        // Topic Categories:
        Task<HttpResponseMessage> UpdateTopicCategoriesAsync(string topicCode, UpdateTopicCategoriesRequestModel requestModel);
        Task<GovDeliveryResponseModel<ListTopicCategoriesResponseModel>> ListTopicCategoriesAsync(string topicCode);

        // Category
        Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel resquestModel);
        Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel);
        Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode);
        Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync();


        
    }
}