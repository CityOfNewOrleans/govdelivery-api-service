using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovDelivery.Library.Http;
using GovDelivery.Library.Models.Rest.Topic;
using System.Net.Http;
using GovDelivery.Library.Utils;
using System.Net;
using System.IO;
using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Subscription;
using GovDelivery.Library.Models;
using GovDelivery.Library.Models.Rest.Subscriber;

namespace GovDelivery.Library.Tests.Mocks
{
    public class MockGovDeliveryApiService : BaseGovDeliveryService, IGovDeliveryApiService
    {
        public MockGovDeliveryApiService(string baseUri, string accountCode) : base(baseUri, accountCode) { }

        // Subscriber
        public override async Task<GovDeliveryResponseModel<CreateSubscriberResponseModel>> CreateSubscriberAsync(CreateSubscriberRequestModel requestModel)
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

            var responseData = await GovDeliveryUtils.ResponseContentToModel<CreateSubscriberResponseModel>(httpResponse.Content);

            return new GovDeliveryResponseModel<CreateSubscriberResponseModel>
            {
                HttpResponse = httpResponse,
                Data = responseData
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadSubscriberResponseModel>> ReadSubscriberAsync(string email)
        {
            var encodedEmail = GovDeliveryUtils.Base64Encode(email);

            var responseModel = new ReadSubscriberResponseModel
            {
                DigestFor = new SerializableInt { Value = (int)SendBulletins.Immediately },
                Id = new SerializableInt { Value = 555 },
                Email = email,
                LockVersion = new SerializableInt { Value = 0 },
                ToParam = encodedEmail,
                Links = new List<LinkModel>
                {
                    new LinkModel
                    {
                        Rel = "self",
                        Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}"
                    },
                    new LinkModel
                    {
                        Rel = "categories",
                        Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/categories"
                    },
                    new LinkModel
                    {
                        Rel = "topics",
                        Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/topics"
                    },
                    new LinkModel
                    {
                        Rel = "questions",
                        Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/questions"
                    },
                    new LinkModel
                    {
                        Rel = "responses",
                        Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}/responses"
                    }
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

        public override async Task<HttpResponseMessage> DeleteSubscriberAsync(string email, bool sendNotifiation) =>
            await Task.Run(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });


        // Subscription
        public override async Task<HttpResponseMessage> AddTopicSubscriptionsAsync(AddTopicSubscriptionsRequestModel requestModel) =>
            await Task.Run(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        public override async Task<GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>> RemoveTopicSubscriptionsAsync(RemoveTopicSubscriptionsRequestModel requestModel)
        {
            var encodedEmail = GovDeliveryUtils.Base64Encode(requestModel.Email);

            var responseModel = new RemoveTopicSubscriptionsResponseModel
            {
                ToParam = encodedEmail,
                Link = new LinkModel { Rel = "self", Href = $"/api/account/{accountCode}/subscribers/{encodedEmail}" },
                SubscriberUri = $"/api/account/{accountCode}/subscribers/{encodedEmail}",
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<RemoveTopicSubscriptionsResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<RemoveTopicSubscriptionsResponseModel>(httpResponse.Content)
            };
        }

        // Topic
        public override async Task<GovDeliveryResponseModel<CreateTopicResponseModel>> CreateTopicAsync(CreateTopicRequestModel requestModel)
        {
            var topicCode = !string.IsNullOrEmpty(requestModel.Code) ? requestModel.Code : "XXXXX";

            var responseModel = new CreateTopicResponseModel
            {
                ToParam = topicCode,
                TopicUri = $"/api/account/{accountCode}/topics/{topicCode}.xml"
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<CreateTopicResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<CreateTopicResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadTopicResponseModel>> ReadTopicAsync(string topicCode)
        {
            var responseModel = new ReadTopicResponseModel
            {
                Code = topicCode,
                Name = "Example Topic",
                ShortName = "Example",
                PagewatchAutosend = new SerializableBool { Value = false },
                PagewatchEnabled = new SerializableBool { Value = true },
                PagewatchSuspended = new SerializableBool { Value = false },
                WatchTaggedContent = new SerializableBool { Value = false },
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<ReadTopicResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ReadTopicResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<UpdateTopicResponseModel>> UpdateTopicAsync(UpdateTopicRequestModel requestModel)
        {
            var responseModel = new UpdateTopicResponseModel
            {
                ToParam = requestModel.Code,
                TopicUri = $"/api/accoount/{accountCode}/topics/{requestModel.Code}.xml"
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<UpdateTopicResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<UpdateTopicResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<HttpResponseMessage> DeleteTopicAsync(string topicCode) =>
            await Task.Run(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

        public override async Task<GovDeliveryResponseModel<ListTopicsResponseModel>> ListTopicsAsync()
        {
            var responseModel = new ListTopicsResponseModel
            {
                Items = new List<ListTopicsResponseModel.Topic> {
                    new ListTopicsResponseModel.Topic
                    {
                        Code = "123456",
                        Description = new NillableSerializableString { Value = "I'm a topic!" },
                        Name = "Example Topic 1",
                        ShortName = "Example 1",
                        WirelessEnabled = new SerializableBool { Value = false },
                        Visibility = TopicVisibility.Listed,
                        Link = new LinkModel
                        {
                            Rel = "self",
                            Href = $"/api/account/{accountCode}/topics/123456"
                        }
                    },
                    new ListTopicsResponseModel.Topic
                    {
                        Code = "678910",
                        Description = new NillableSerializableString { Value = "I'm another topic!" },
                        Name = "Example Topic 2",
                        ShortName = "Example 2",
                        WirelessEnabled = new SerializableBool { Value  = true },
                        Visibility = TopicVisibility.Unlisted,
                        Link = new LinkModel
                        {
                            Rel = "self",
                            Href = $"/api/account/{accountCode}/topics/678910"
                        }
                    }
                }
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel),
            };

            return new GovDeliveryResponseModel<ListTopicsResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListTopicsResponseModel>(httpResponse.Content)
            };
        }

        // Topic Categories:
        public override async Task<GovDeliveryResponseModel<ListTopicCategoriesResponseModel>> ListTopicCategoriesAsync(string topicCode)
        {
            var categoryCodes = new List<string> { "01234", "56789" };

            var responseModel = new ListTopicCategoriesResponseModel
            {
                Items = categoryCodes
                .Select(c => new TopicCategoryModel
                {
                    ToParam = c,
                    CategoryUri = $"categories/{c}.xml"
                })
                .ToList()
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<ListTopicCategoriesResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListTopicCategoriesResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<HttpResponseMessage> UpdateTopicCategoriesAsync(string topicCode, UpdateTopicCategoriesRequestModel requestModel) =>
            await Task.Run(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });


        // Category
        public override async Task<GovDeliveryResponseModel<CreateCategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel requestModel)
        {
            var responseModel = new CreateCategoryResponseModel
            {
                ToParam = "12345",
                CategoryUri = $"/api/account/{accountCode}/categories/12345.xml",
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<CreateCategoryResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<CreateCategoryResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<ReadCategoryResponseModel>> ReadCategoryAsync(string categoryCode)
        {
            var responseModel = new ReadCategoryResponseModel
            {
                Code = "12345",

            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<ReadCategoryResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ReadCategoryResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<GovDeliveryResponseModel<UpdateCategoryResponseModel>> UpdateCategoryAsync(UpdateCategoryRequestModel requestModel)
        {
            var responseModel = new UpdateCategoryResponseModel
            {

            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<UpdateCategoryResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<UpdateCategoryResponseModel>(httpResponse.Content)
            };
        }

        public override async Task<HttpResponseMessage> DeleteCategoryAsync(string categoryCode) =>
            await Task.Run(() => new HttpResponseMessage { StatusCode = HttpStatusCode.OK });


        public override async Task<GovDeliveryResponseModel<ListCategoriesResponseModel>> ListCategoriesAsync()
        {
            var responseModel = new ListCategoriesResponseModel
            {
                Items = new List<ReadCategoryResponseModel> {
                    new ReadCategoryResponseModel
                    {
                        Code = "12345",
                        AllowSubscriptions = new SerializableBool { Value = true },
                        DefaultOpen = new SerializableBool { Value = false },
                        Name = "Example Category 1",
                        ShortName = "Example 1",
                        Description = "I'm a category!",
                        QuickSubscribePage = new QuickSubscribePage { PageCode = "A" },
                        Link = new LinkModel { Rel = "Self", Href = $"/api/account/{accountCode}/categories/12345" },
                    },
                }
            };

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = GovDeliveryUtils.ModelToStringContent(responseModel)
            };

            return new GovDeliveryResponseModel<ListCategoriesResponseModel>
            {
                HttpResponse = httpResponse,
                Data = await GovDeliveryUtils.ResponseContentToModel<ListCategoriesResponseModel>(httpResponse.Content)
            };

        }


    }
}
