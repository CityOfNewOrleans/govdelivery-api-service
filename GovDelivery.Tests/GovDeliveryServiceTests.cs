using GovDelivery.Data.Entities;
using GovDelivery.Http;
using GovDelivery.Library.Tests.Mocks;
using GovDelivery.Models.Rest.Subscriber;
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
            service = new MockGovDeliveryApiService();
        }

        #region MemberData

        public static CreateSubscriberRequestModel CreateSubscriberAsyncData
        {
            get
            {
                return new CreateSubscriberRequestModel
                {
                    Email = "example@nola.gov",
                    BulletinFrequency = BulletinFrequency.Daily,
                    SendSubscriberUpdateNotifications = true,
                };
            }
        }

        #endregion MemberData

        [Theory, MemberData("CreateSubscriberAsyncData")]
        public async void CreateSubscriberAsync_ValidModel_SuccessfullyCreateRemoteSubscriber(CreateSubscriberRequestModel requestModel)
        {
            var responseModel = await service.CreateSubscriberAsync(requestModel);

            Assert.NotNull(responseModel.HttpResponse);
            Assert.Equal(HttpStatusCode.OK, responseModel.HttpResponse.StatusCode);
            Assert.IsType(typeof(int), responseModel.Data.SubscriberId);
            Assert.NotNull(responseModel.Data);
            var expectedLink = $"/api/account/{ACCOUNT_CODE}/subscribers/{responseModel.Data.SubscriberId}";
            Assert.Equal(expectedLink, responseModel.Data.SubscriberInfoLink);
        }

    }
}
