using GovDelivery.Models;
using GovDelivery.Models.Rest.Subscriber;
using GovDelivery.Models.Rest.Topic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GovDelivery.Http
{
    public class GovDeliveryApiService
    {
        public const string STAGING_URI = "https://stage-api.govdelivery.com";

        public const string MAIN_URI = "https://api.govdelivery.com";

        private XmlSerializer subscriberSerializer = new XmlSerializer(typeof(ReadSubscriberModel));
        private XmlSerializer topicSubscriptionSerializer = new XmlSerializer(typeof(AddTopicToSubscriberModel));

        private HttpClient client;

        public GovDeliveryApiService (string baseUri, string accountCode)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{baseUri}/api/account/{accountCode}");
        }

        public async Task<GovDeliveryResponseModel<UpdateSubscriberModel>> UpdateSubscriberAsync(UpdateSubscriberModel model)
        {
            var res = await client.PostAsync("subscriptions.xml", toStringContent(model, subscriberSerializer));

            return new GovDeliveryResponseModel<UpdateSubscriberModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        public async Task<GovDeliveryResponseModel<CreateSubscriberModel>> CreateSubscriberAsync(CreateSubscriberModel model)
        {
            var res = await client.PutAsync("subscriptions.xml", toStringContent(model, subscriberSerializer));
            return new GovDeliveryResponseModel<CreateSubscriberModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        public async Task<GovDeliveryResponseModel<ReadSubscriberModel>> ReadSubscriber(string email)
        {

            var res = await client.GetAsync("subscriptions.xml");

            var model = new ReadSubscriberModel();

            return new GovDeliveryResponseModel<ReadSubscriberModel>
            {
                HttpResponse = res,
                Data = model,
            };
        }

        public async Task<Models.GovDeliveryResponseModel<DeleteSubscriberModel>> DeleteSubscriberAsync(DeleteSubscriberModel model)
        {
            var res = await client.DeleteAsync("subscriptions.xml");

            return new GovDeliveryResponseModel<DeleteSubscriberModel> {
                Data = new DeleteSubscriberModel()
            };
        }

        // Topics

        public async Task<GovDeliveryResponseModel<AddTopicToSubscriberModel>> AddTopicToSubscriber (ReadSubscriberModel subscriber, IEnumerable<TopicModel> topics)
        {
            var model = new AddTopicToSubscriberModel
            {
                
            };

            var res = await client.PostAsync("subscriptions.xml", toStringContent(model, topicSubscriptionSerializer));

            return new GovDeliveryResponseModel<AddTopicToSubscriberModel>();
        }

        // TODO - SP: RemoveSubscriberFromTopic
        // TODO - SP: CreateTopic
        // TODO - SP: GetTopic
        // TODO - SP: GetAllTopics
        // TODO - SP: UpdateTopic
        // TODO - SP: DeleteTopic
        // TODO - SP: GetTopicCategories
        // TODO - SP: UpdateTopicCategories

        private StringContent toStringContent<T>(T m, XmlSerializer serializer) {

            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    serializer.Serialize(xw, m);

                    return new StringContent(xw.ToString(), Encoding.UTF8, "text/xml");
                }
            }
        }


        private string base64Encode(string plainText) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        private string base64Decode(string encodedText) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));
    }
}
