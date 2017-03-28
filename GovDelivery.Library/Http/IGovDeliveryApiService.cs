using System.Collections.Generic;
using System.Threading.Tasks;
using GovDelivery.Models;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Topic;
using System.Net.Http;

namespace GovDelivery.Library.Http
{
    public interface IGovDeliveryApiService
    {
        // Subscriber
        Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel);
        Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotification);
        Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel requestModel);
        Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel requestModel);
        
        // Topic
        Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel);
        Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id);
        Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync();
        Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel);
        Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(string topicCode);
        
        // Category
        Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId);
        Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel requestModel);
    }
}