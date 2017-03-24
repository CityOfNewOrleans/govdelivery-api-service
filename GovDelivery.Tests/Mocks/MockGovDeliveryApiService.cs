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
using GovDelivery.Library.Models.Rest.Misc;

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
                SubscriberInfoLink = new LinkModel
                {
                    Rel = "self",
                    Href = $"/api/account/{accountCode}/subscribers/{subscriberId}"
                }
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
                LockVersion = 0,
                ToParam = encodedEmail,
                SelfLink = new LinkModel
                {
                    Rel = "self",
                    Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}"
                },
                CategoriesLink = new LinkModel
                {
                    Rel = "categories",
                    Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/categories"
                },
                TopicsLink = new LinkModel
                {
                    Rel = "topics",
                    Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/topics"
                },
                QuestionsLink = new LinkModel
                {
                    Rel = "questions",
                    Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/questions"
                },
                QuestionResponsesLink = new LinkModel
                {
                    Rel = "responses",
                    Href = "/api/account/{accountCode}/subscribers/{encodedEmail}/responses"
                }
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel),
            };

            return new GovDeliveryResponseModel<ReadSubscriberResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ReadSubscriberResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<UpdateSubscriberResponseModel>> UpdateSubscriberAsync(UpdateSubscriberRequestModel requestModel)
        {
            var encodedEmail = GovDeliveryUtils.Base64Encode(requestModel.Email);

            var responseModel = new UpdateSubscriberResponseModel
            {
                ToParam = encodedEmail,
                SubscriberInfoLink = new LinkModel
                {
                    Rel = "self",
                    Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}"
                }
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<UpdateSubscriberResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<UpdateSubscriberResponseModel>(httpResponse.Content)
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
