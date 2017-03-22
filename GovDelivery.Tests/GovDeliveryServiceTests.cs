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
        public async void CreateSubscriberAsync_ValidModel_SuccessfullyCreateRemoteSubscriber(CreateSubscriberRequestModel model)
        {
            var createResponse = await service.CreateSubscriberAsync(model);

            Assert.Equal(HttpStatusCode.OK, createResponse.HttpResponse.StatusCode);
        }

    }
}
