using GovDelivery.Http;
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

namespace GovDelivery.Library.Tests.Mocks
{
    public class MockGovDeliveryApiService : IGovDeliveryApiService
    {

        // Subscriber
        public async Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<DeleteSubscriberResponseModel>> DeleteSubscriberAsync(DeleteSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        // Topic
        public async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(DeleteTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        // Category
        public async Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
