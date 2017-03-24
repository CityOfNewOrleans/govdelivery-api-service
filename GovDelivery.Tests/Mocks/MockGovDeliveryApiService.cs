using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovDelivery.Library.Http;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Models;
using GovDelivery.Models.Rest.Category;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using System.Net.Http;
using GovDelivery.Library.Utils;
using System.Net;
using System.IO;

namespace GovDelivery.Library.Tests.Mocks
{
    public class MockGovDeliveryApiService : BaseGovDeliveryService
    {
        private string accountCode { get; set; }

        public MockGovDeliveryApiService(string baseUri, string accountCode) : base(baseUri, accountCode)
        {
            this.accountCode = accountCode;
        }

        // Subscriber
        public override async Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> 
            CreateSubscriberAsync(CreateSubscriberRequestModel requestModel)
        {

            var subscriberId = 555;

            var responseModel = new CreateSubscriberResponseModel
            {
                SubscriberId = subscriberId,
                SubscriberInfoLink = $"/api/account/{accountCode}/subscribers/{subscriberId}"
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<CreateSubscriberResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<CreateSubscriberResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email)
        {
            var encodedEmail = GovDeliveryUtils.Base64Encode(email);

            var responseModel = new ReadSubscriberResponseModel
            {
                DigestFor = SendBulletins.Immediately,
                Id = 555,
                Email = email,
                
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel),
            };
        }

        public override async Task<GovDeliveryResponseModel<DeleteSubscriberResponseModel>> DeleteSubscriberAsync(DeleteSubscriberRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        // Topic
        public override async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ReadAllTopicsResponseModel>> ReadAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<DeleteTopicResponseModel>> DeleteTopicAsync(DeleteTopicRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        // Category
        public override async Task<GovDeliveryResponseModel<AddTopicToSubscriberResponseModel>> AddTopicToSubscriberAsync(AddTopicToSubscriberRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<IEnumerable<ReadCategoryModel>>> ReadTopicCategoriesAsync(int topicId)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<RemoveTopicFromSubscriberResponseModel>> RemoveTopicFromSubscriberAsync(RemoveTopicFromSubscriberRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public override async Task<GovDeliveryResponseModel<AddTopicToCategoryModel>> UpdateTopicCategoriesAsync(AddTopicToCategoryModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
