using System.Collections.Generic;
using System.Threading.Tasks;
using GovDelivery.Models;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Topic;

namespace GovDelivery.Http
{
    public interface IGovDeliveryApiService
    {
        // Subscriber
        Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel model);
        Task<GovDeliveryResponseModel<DeleteSubscriberResponseModel>> DeleteSubscriberAsync(DeleteSubscriberRequestModel model);
        Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email);
        Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel model);
        Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel model);
        Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel model);
        
        // Topic
        Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model);
        Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id);
        Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync();
        Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model);
        Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(DeleteTopicRequestModel model);
        
        // Category
        Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId);
        Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel model);
    }
}