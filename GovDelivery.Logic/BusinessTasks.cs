using GovDelivery.Entity;
using GovDelivery.Entity.Models;
using GovDelivery.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovDelivery.Logic
{
    public class BusinessTasks
    {

        public async static Task SyncTopics(IGovDeliveryApiService service, IGovDeliveryContext ctx) {
            var topicsResult = await service.ListTopicsAsync();

            if (!topicsResult.HttpResponse.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($@"Error getting Topics: {topicsResult.HttpResponse.StatusCode} - {topicsResult.HttpResponse.ReasonPhrase}");
            }

            var numTopics = topicsResult.Data.Items != null ? topicsResult.Data.Items.Count : 0;
            Console.WriteLine($"Fetched {numTopics} topics.");

            if (numTopics > 0)
            {

                var remoteTopics = topicsResult.Data.Items
                    .Select(i => new Topic
                    {
                        Id = Guid.NewGuid(),
                        Code = i.Code,
                        Description = i.Description.Value,
                        Name = i.Name,
                        ShortName = i.ShortName,
                        WirelessEnabled = i.WirelessEnabled.Value
                    })
                    .ToList();

                var localTopics = ctx.Topics.ToList();

                // Add new topics not present locally:

                var newTopics = remoteTopics
                    .Where(rt => !localTopics.Any(lt => lt.Code == rt.Code))
                    .ToList();

                ctx.AddRange(newTopics);
                ctx.SaveChanges();

                // Update topics present both remotely and locally:

                var existingTopics = localTopics
                    .Where(lt => remoteTopics.Any(rt => rt.Code == lt.Code))
                    .ToList();

                foreach (var localTopic in existingTopics)
                {
                    var remoteTopic = remoteTopics.First(rt => rt.Code == localTopic.Code);

                    localTopic.Name = remoteTopic.Name;
                    localTopic.ShortName = remoteTopic.ShortName;
                    localTopic.Description = remoteTopic.Description;

                }

                ctx.SaveChanges();

                // Delete all local topics not present remotely:

                var deletableTopics = localTopics
                    .Where(lt => !remoteTopics.Any(rt => rt.Code == lt.Code))
                    .ToList();

                ctx.RemoveRange(deletableTopics);
                ctx.SaveChanges();
            }


        }

        public async static Task SyncCategories(IGovDeliveryApiService service, IGovDeliveryContext ctx) {

            var categoriesResult = await service.ListCategoriesAsync();

            if (!categoriesResult.HttpResponse.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($@"Error getting Categories: {categoriesResult.HttpResponse.StatusCode} - {categoriesResult.HttpResponse.ReasonPhrase}");
            }

            var numCategories = categoriesResult.Data.Items != null ? categoriesResult.Data.Items.Count() : 0;
            Console.WriteLine($"Fetched {numCategories} categories");

            if (numCategories > 0)
            {
                var remoteCategories = categoriesResult.Data.Items
                    .Select(i => new Category
                    {
                        Id = Guid.NewGuid(),
                        Code = i.Code,
                        Description = i.Description,
                        DefaultOpen = i.DefaultOpen.Value,
                        AllowUserInitiatedSubscriptions = i.AllowSubscriptions.Value,
                        Name = i.Name,
                        ShortName = i.ShortName,
                    }).ToList();

                var localCategories = ctx.Categories.ToList();

                // Add new categories:

                var newCategories = remoteCategories
                    .Where(rc => !localCategories.Any(lc => lc.Code == rc.Code))
                    .ToList();

                ctx.AddRange(newCategories);
                ctx.SaveChanges();


                // Update existing categories:

                var existingCategories = localCategories
                    .Where(lc => remoteCategories.Any(rc => rc.Code == lc.Code))
                    .ToList();

                foreach (var localCategory in existingCategories)
                {
                    var remoteTopic = remoteCategories.First(rc => rc.Code == localCategory.Code);

                    // update category info:
                }

                ctx.SaveChanges();

                // Delete categories not present remotely:

                var deletableCategories = localCategories
                    .Where(lc => !remoteCategories.Any(rc => rc.Code == lc.Code))
                    .ToList();

                ctx.RemoveRange(deletableCategories);
                ctx.SaveChanges();
            }
        }

        public async static Task UpdateSubscribers(IGovDeliveryApiService service, IGovDeliveryContext ctx)
        {
            var localSubscribers = ctx.Subscribers.ToList();

            var subscriberEnumerator = localSubscribers.GetEnumerator();

            // pull x subscribers and request their data
            var updateTasks = Enumerable.Range(0, 5)
                .Select(n =>
                {
                    var subscriberEntity = subscriberEnumerator.Current;
                    subscriberEnumerator.MoveNext();

                    return UpdateSubscriberAsync(subscriberEntity, service, ctx);
                })
                .ToList();

            // after each request comes back, save data, pick next eligible subscriber until none are left.
            while (updateTasks.Count() > 0)
            {
                var t = await Task.WhenAny(updateTasks);

                updateTasks.Remove(t);
                if (subscriberEnumerator.MoveNext())
                {
                    var subscriber = subscriberEnumerator.Current;
                    updateTasks.Add(UpdateSubscriberAsync(subscriber, service, ctx));
                }
            }
        }

        protected async static Task UpdateSubscriberAsync(Subscriber subscriber, IGovDeliveryApiService service, IGovDeliveryContext ctx)
        {
            var subscriberInfoTask = service.ReadSubscriberAsync(subscriber.Email);
            var subscriberTopicsTask = service.ListSubscriberTopicsAsync(subscriber.Email);
            var subscriberCategoriesTask = service.ListSubscriberCategoriesAsync(subscriber.Email);

            await Task.WhenAll(new List<Task> { subscriberInfoTask, subscriberTopicsTask, subscriberCategoriesTask });

            var subscriberInfo = subscriberInfoTask.Result.Data;

            // Update detailed subscriber info
            subscriber.BulletinFrequency = (BulletinFrequency)subscriberInfo.DigestFor.Value;
            subscriber.GovDeliveryId = subscriberInfo.Id.Value;
            subscriber.Phone = subscriberInfo.Phone;
            subscriber.SendSubscriberUpdateNotifications = subscriberInfo.SendSubscriberUpdateNotifications.Value;

            // Update Category Subscriptions
            var subscriberCategories = subscriber
                .CategorySubscriptions.Select(esc => esc.Category)
                .ToList();

            var subscriberCategoryInfo = subscriberCategoriesTask.Result.Data.Items;

            // existing category subscriptions - do nothing.

            var newCategorySubscriptions = subscriberCategoryInfo
                .Where(sci => !subscriberCategories.Any(sc => sc.Code == sci.CategoryCode));

            foreach (var nCS in newCategorySubscriptions)
            {
                var cat = ctx.Categories.First(c => c.Code == nCS.CategoryCode);
                ctx.Add(new CategorySubscription
                {
                    CategoryId = cat.Id,
                    Category = cat,
                    SubscriberId = subscriber.Id,
                    Subscriber = subscriber
                });
            }

            var deleteableCategorySubscriptions = subscriberCategories
                .Where(sc => !subscriberCategoryInfo.Any(sci => sci.CategoryCode == sc.Code))
                .ToList();

            foreach (var dSC in deleteableCategorySubscriptions)
            {
                var categorySub = ctx.CategorySubscriptions.First(cs => cs.Category.Code == dSC.Code);
                ctx.Remove(categorySub);
            }

            ctx.SaveChanges();

            // New Topic subscriptions
            var subscriberTopics = subscriber
                .TopicSubscriptions.Select(est => est.Topic)
                .ToList();

            var subscriberTopicInfo = subscriberTopicsTask.Result.Data.Items;

            var newTopicSubscriptions = subscriberTopicInfo
                .Where(sti => !subscriberTopics.Any(st => st.Code == sti.TopicCode))
                .ToList();

            foreach (var nTS in newTopicSubscriptions)
            {
                var topic = ctx.Topics.First(t => t.Code == nTS.TopicCode);
                ctx.Add(new TopicSubscription
                {
                    Subscriber = subscriber,
                    SubscriberId = subscriber.Id,
                    Topic = topic,
                    TopicId = topic.Id,
                });
            }

            // deleteable topic subscriptions
            var deleteableTopicSubscriptions = subscriberTopics
                .Where(st => !subscriberTopicInfo.Any(sti => sti.TopicCode == st.Code))
                .ToList();

            foreach (var dts in deleteableTopicSubscriptions)
            {
                var topicSub = ctx.TopicSubscriptions.First(ts => ts.TopicId == dts.Id);
                ctx.Remove(topicSub);
            }

            ctx.SaveChanges();
        }

    }
}
