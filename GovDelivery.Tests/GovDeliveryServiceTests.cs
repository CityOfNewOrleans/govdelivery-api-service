using GovDelivery.Library.Http;
using GovDelivery.Library.Models.Rest.Category;
using GovDelivery.Library.Models.Rest.Misc;
using GovDelivery.Library.Models.Rest.Subscriber;
using GovDelivery.Library.Models.Rest.Topic;
using GovDelivery.Library.Tests.Mocks;
using GovDelivery.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GovDelivery.Library.Tests
{
    public class GovDeliveryServiceTests
    {

        private const string ACCOUNT_CODE = "XXXXX";

        private IGovDeliveryApiService service { get; set; }

        public GovDeliveryServiceTests()
        {
            service = new MockGovDeliveryApiService("foo.com", ACCOUNT_CODE);
        }

        #region MemberData

        public static IEnumerable<object[]> CreateSubscriberAsyncData()
        {
            yield return new object[] 
            {
                new CreateSubscriberRequestModel
                {
                    Email = "example@nola.gov",
                    SendBulletins = SendBulletins.Daily,
                    SendSubscriberUpdateNotifications = new SerializableBool(true),
                }
            };
        }
        public static IEnumerable<object[]> UpdateSubscriberData()
        {
            yield return new object[]
            {
                new UpdateSubscriberRequestModel
                {
                    Email = "example@nola.gov",
                    SendBulletins = SendBulletins.Weekly,
                    SendNotifications = new SerializableBool(true)
                }
            };
        } 
        
        public static IEnumerable<object[]> CreateTopicData()
        {
            yield return new object[]
            {
                new CreateTopicRequestModel
                {
                    Name = "Example Topic 1",
                    ShortName = "Example1",
                    Description = new NillableSerializableString("I'm a topic!"),
                    PagewatchAutosend = new SerializableBool(true)
                }
            };
        } 

        public static IEnumerable<object[]> UpdateTopicData()
        {
            yield return new object[]
            {
                new UpdateTopicRequestModel
                {

                }
            };
        }

        public static IEnumerable<object[]> CreateCategoryData()
        {
            yield return new object[]
            {
                new CreateCategoryRequestModel
                {

                }
            };
        }

        public static IEnumerable<object[]> UpdateCategoryData()
        {
            yield return new object[]
            {
                new UpdateCategoryRequestModel
                {

                }
            };
        }

        #endregion MemberData

        // Subscriber:
        [Theory(DisplayName = "CreateSubscriberAsync method performs as expected.")]
        [MemberData(nameof(CreateSubscriberAsyncData))]
        public async void TestCreateSubscriberAsync(CreateSubscriberRequestModel requestModel)
        {
            var responseModel = await service.CreateSubscriberAsync(requestModel);

            Assert.NotNull(responseModel.HttpResponse);
            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.IsType(typeof(int), responseModel.Data.SubscriberId);
            Assert.NotNull(responseModel.Data);
            var expectedLink = $"/api/account/{ACCOUNT_CODE}/subscribers/{responseModel.Data.SubscriberId}";
            Assert.Equal(expectedLink, responseModel.Data.SubscriberInfoLink.Href);
        }

        [Theory(DisplayName = "ReadSubscriberAsync method performs as expected.")]
        [InlineData("example@nola.gov")]
        public async void TestReadSuscriberAsync(string subscriberEmail)
        {
            var responseModel = await service.ReadSubscriberAsync(subscriberEmail);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.Equal(subscriberEmail, responseModel.Data.Email);

            Assert.NotNull(responseModel.Data.CategoriesLink);
            Assert.NotNull(responseModel.Data.SelfLink);
            Assert.NotNull(responseModel.Data.QuestionsLink);
            Assert.NotNull(responseModel.Data.QuestionResponsesLink);
            Assert.NotNull(responseModel.Data.TopicsLink);

        }

        [Theory(DisplayName = "UpdateSubscriberAsync performs as expected.")]
        [MemberData(nameof(UpdateSubscriberData))]
        public async void TestUpdateSubscriberAsync (UpdateSubscriberRequestModel requestModel)
        {
            var responseModel = await service.UpdateSubscriberAsync(requestModel);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);
            Assert.Equal(requestModel.Email, responseModel.Data.ToParam);

            var expectedSelfLink = $"/api/account/{ACCOUNT_CODE}/subscribers/{requestModel.Email}";

            Assert.Equal(expectedSelfLink, responseModel.Data.SubscriberInfoLink.Href);
        }

        [Theory(DisplayName = "DeleteSubscriberAsync performs as expected.")]
        [InlineData("example@nola.gov", false)]
        public async void TestDeleteSubscriberAsync(string email, bool sendNotification)
        {
            var responseModel = await service.DeleteSubscriberAsync(email, sendNotification);

            Assert.Equal(HttpStatusCode.OK, responseModel.StatusCode);
        }

        // Category:
        [Theory(DisplayName = "CreateCategoryAsync performs as expected.")]
        public async void TestCreateCategoryAsync(CreateCategoryRequestModel requestModel)
        {
            var responseModel = await service.CreateCategoryAsync(requestModel);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);

            Assert.True(!string.IsNullOrWhiteSpace(responseModel.Data.ToParam));
            var categoryCode = responseModel.Data.ToParam;
            var expectedCategoryUri = $"/api/account/{ACCOUNT_CODE}/categories/{categoryCode}.xml";
            Assert.Equal(expectedCategoryUri, responseModel.Data.CategoryUri);
        }

        [Theory(DisplayName = "ReadCategoryAsync performs as expected.")]
        public async void TestReadCategoryAsync(string categoryCode)
        {
            var responseModel = await service.ReadCategoryAsync(categoryCode);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);

        }

        [Theory(DisplayName = "UpdateCategoryAsync performs as expected")]
        public async void TestUpdateCategoryAsync(UpdateCategoryRequestModel requestModel)
        {
            var responseModel = await service.UpdateCategoryAsync(requestModel);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);

            var categoryCode = responseModel.Data.ToParam;
            var expectedCategoryUri = $"/api/account/{ACCOUNT_CODE}/categories/{categoryCode}.xml";
        }

        [Theory(DisplayName = "DeleteCategoryAsync performs as expected.")]
        public async void TestDeleteCategoryAsync(string categoryCode)
        {
            var response = await service.DeleteCategoryAsync(categoryCode);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory(DisplayName = "ListCategoriesAsync performs as expected.")]
        public async void TestListCategoriesAsync()
        {
            var responseModel = await service.ListCategoriesAsync();

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);
            Assert.NotNull(responseModel.Data.Items);

            Assert.True(responseModel.Data.Items.Count > 0);

        }

        //Topic:
        [Theory(DisplayName = "CreateTopicAsync performs as expected.")]
        public async void TestCreateTopicAsync(CreateTopicRequestModel requestModel)
        {
            var responseModel = await service.CreateTopicAsync(requestModel);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);

            var topicCode = responseModel.Data.ToParam;
            var expectedTopicUri = $"/api/account/{ACCOUNT_CODE}/topics/{topicCode}.xml";
            Assert.Equal(expectedTopicUri, responseModel.Data.TopicUri);
        }

        [Theory(DisplayName = "ReadTopicAsync performs as expected.")]
        public async void TestReadTopicAsync(string topicCode)
        {
            var responseModel = await service.ReadTopicAsync(topicCode);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);

            Assert.True(!string.IsNullOrEmpty(responseModel.Data.Code));
            Assert.NotNull(responseModel.Data.DefaultPagewatchResults);
        }

        [Theory(DisplayName = "UpdateTopicAsync performs as expected.")]
        public async void TestUpdateTopicAsync(UpdateTopicRequestModel requestModel)
        {
            var responseModel = await service.UpdateTopicAsync(requestModel);

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);

            Assert.True(!string.IsNullOrEmpty(responseModel.Data.ToParam));
            var expectedTopicUri = $"/api/account/{ACCOUNT_CODE}/topics/{responseModel.Data.ToParam}.xml";
            Assert.Equal(expectedTopicUri, responseModel.Data.TopicUri);
        }

        [Theory(DisplayName = "DeleteTopicAsync performs as expected.")]
        public async void TestDeleteTopicAsync(string topicCode)
        {
            var response = await service.DeleteTopicAsync(topicCode);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory(DisplayName = "ListTopicsAsync performs as expected.")]
        public async void TestListTopicsAsync()
        {
            var responseModel = await service.ListTopicsAsync();

            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.NotNull(responseModel.Data);
            Assert.NotNull(responseModel.Data.Items);
            Assert.True(responseModel.Data.Items.Count > 0);
        }


    }
}
