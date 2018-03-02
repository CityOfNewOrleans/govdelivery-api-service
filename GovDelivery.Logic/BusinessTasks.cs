using GovDelivery.Entity;
using GovDelivery.Entity.Models;
using GovDelivery.Rest;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovDelivery.Logic
{
    public class BusinessTasks
    {
        public async static Task SyncTopics(
            IGovDeliveryApiService service, 
            IGovDeliveryContext ctx, 
            Action<string> loggingDelegate = null
        ) {
            var topicsResult = await service.ListTopicsAsync();

            if (!topicsResult.HttpResponse.IsSuccessStatusCode)
            {
                loggingDelegate?.Invoke($@"Error getting Topics: {topicsResult.HttpResponse.StatusCode} - {topicsResult.HttpResponse.ReasonPhrase}");
            }

            var numTopics = topicsResult.Data.Items != null ? topicsResult.Data.Items.Count : 0;
            loggingDelegate?.Invoke($"Fetched {numTopics} topics.");

            if (numTopics > 0)
            {

                var remoteTopics = topicsResult.Data.Items
                    .Select(i => new Topic
                    {
                        Id = Guid.NewGuid(),
                        Code = i.Code,
                        Description = i.Description?.Value,
                        Name = i.Name,
                        ShortName = i.ShortName,
                        WirelessEnabled = (i.WirelessEnabled != null) ? i.WirelessEnabled.Value : false,
                    })
                    .ToList();

                var localTopics = ctx.Topics.ToList();

                // Add new topics not present locally:

                var newTopics = remoteTopics
                    .Where(rt => !localTopics.Any(lt => lt.Code == rt.Code))
                    .ToList();

                if (newTopics.Count > 0) {
                    ctx.Topics.AddRange(newTopics);
                    ctx.SaveChanges();
                }

                // Update topics present both remotely and locally:

                var existingTopics = localTopics
                    .Where(lt => remoteTopics.Any(rt => rt.Code == lt.Code))
                    .ToList();

                foreach (var localTopic in existingTopics)
                {
                    var remoteTopic = remoteTopics.First(rt => rt.Code == localTopic.Code);

                    var topicResult = await service.ReadTopicAsync(localTopic.Code);
                    var topicInfo = topicResult.Data;

                    var remoteTopicCategoriesResult = await service.ListTopicCategoriesAsync(localTopic.Code);
                    var remoteTopicCategories = remoteTopicCategoriesResult.Data.Items;

                    localTopic.Name = topicInfo.Name;
                    localTopic.ShortName = topicInfo.ShortName;
                    localTopic.Description = topicInfo.Description?.Value;
                    localTopic.DefaultPagewatchResults = topicInfo.DefaultPagewatchResults?.Value ?? 0;
                    localTopic.PagewatchAutosend = topicInfo.PagewatchAutosend?.Value ?? false;
                    localTopic.PagewatchEnabled = topicInfo.PagewatchSuspended?.Value ?? false;
                    localTopic.PagewatchType = (PageWatchType?)topicInfo.PagewatchType?.Value ?? null;
                    localTopic.SendByEmailEnabled = topicInfo.SendByEmailEnabled?.Value ?? false;
                    localTopic.WatchTaggedContent = topicInfo.WatchTaggedContent?.Value ?? false;
                    localTopic.WirelessEnabled = topicInfo.WirelessEnabled?.Value ?? false;
                    localTopic.Pages = topicInfo.Pages?.Items?
                        .Select(p => new Page { Id = Guid.NewGuid(), Url = p.Url })
                        .ToList()
                        ?? new List<Page>();

                    var localTopicCategories = localTopic.TopicCategories;

                    // new topic categories
                    var newTopicCategories = remoteTopicCategories
                        .Where(rtc => !localTopicCategories.Any(ltc => ltc.Category.Code == rtc.Code))
                        .Select(tcm => new CategoryTopic
                        {
                            TopicId = localTopic.Id,
                            CategoryId = ctx.Categories.First(c => c.Code == tcm.Code).Id
                        })
                        .ToList();

                    ctx.CategoryTopics.AddRange(newTopicCategories);

                    var deleteableTopicCategories = localTopicCategories
                        .Where(ltc => !remoteTopicCategories.Any(rtc => rtc.Code == ltc.Category.Code))
                        .ToList();

                    ctx.CategoryTopics.RemoveRange(deleteableTopicCategories);

                    await ctx.SaveChangesAsync();
                }


                // Delete all local topics not present remotely:

                var deletableTopics = localTopics
                    .Where(lt => !remoteTopics.Any(rt => rt.Code == lt.Code))
                    .ToList();

                ctx.RemoveRange(deletableTopics);
                ctx.SaveChanges();
            }


        }

        public async static Task SyncCategories(
            IGovDeliveryApiService service, 
            IGovDeliveryContext ctx, 
            Action<string> loggingDelegate = null
        ) {

            var categoriesResult = await service.ListCategoriesAsync();

            if (!categoriesResult.HttpResponse.IsSuccessStatusCode)
            {
                loggingDelegate?.Invoke($@"Error getting Categories: {categoriesResult.HttpResponse.StatusCode} - {categoriesResult.HttpResponse.ReasonPhrase}");
            }

            var numCategories = categoriesResult.Data.Items != null ? categoriesResult.Data.Items.Count() : 0;
            loggingDelegate?.Invoke($"Fetched {numCategories} categories");

            if (numCategories > 0)
            {
                var remoteCategories = categoriesResult.Data.Items
                    .Select(i => new Category
                    {
                        Id = Guid.NewGuid(),
                        Code = i.Code,
                        Description = i.Description,
                        DefaultOpen = i.DefaultOpen?.Value ?? false,
                        AllowUserInitiatedSubscriptions = i.AllowSubscriptions?.Value ?? false,
                        Name = i.Name,
                        ShortName = i.ShortName,
                    }).ToList();

                var localCategories = ctx.Categories.ToList();

                // Add new categories:

                var newCategories = remoteCategories
                    .Where(rc => !localCategories.Any(lc => lc.Code == rc.Code))
                    .ToList();

                ctx.Categories.AddRange(newCategories);
                ctx.SaveChanges();


                // Update existing categories:

                var existingCategories = localCategories
                    .Where(lc => remoteCategories.Any(rc => rc.Code == lc.Code))
                    .ToList();

                foreach (var localCategory in existingCategories)
                {
                    var remoteCategory = remoteCategories.First(rc => rc.Code == localCategory.Code);

                    // update category info:

                    var fullCategoryResponse = await service.ReadCategoryAsync(remoteCategory.Code);

                    var categoryInfo = fullCategoryResponse.Data;

                    remoteCategory.AllowUserInitiatedSubscriptions = categoryInfo.AllowSubscriptions.Value;
                    remoteCategory.DefaultOpen = categoryInfo.DefaultOpen.Value;
                    remoteCategory.Description = categoryInfo.Description;
                    remoteCategory.Name = categoryInfo.Name;
                    remoteCategory.ShortName = categoryInfo.ShortName;

                    var parentCategoryCode = categoryInfo?.Parent?.CategoryCode; 
                    if (parentCategoryCode != null) {
                        var parentCategory = ctx.Categories.FirstOrDefault(c => c.Code == parentCategoryCode);
                        remoteCategory.ParentCategory = parentCategory;
                    }

                    ctx.SaveChanges();
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

        public async static Task UpdateSubscribersAsync<T>(
            IGovDeliveryApiService service, 
            IGovDeliveryContextFactory<T> factory, 
            Action<string> loggingDelegate = null
        )
            where T: AbstractGovDeliveryContext
        {
            var ctx = factory.CreateDbContext();

            var localSubscribers = ctx.Subscribers.ToList();

            loggingDelegate?.Invoke($"Found {localSubscribers.Count} subscribers to update...");

            var subscriberEnumerator = localSubscribers.GetEnumerator();

            // pull x subscribers and request their data
            var updateTasks = Enumerable.Range(0, 10)
                .Select(n =>
                {
                    subscriberEnumerator.MoveNext();
                    var subscriberEntity = subscriberEnumerator.Current;

                    return UpdateSingleSubscriberAsync(subscriberEntity.Id, service, factory, loggingDelegate);
                })
                .ToList();

            var taskCounter = 0;
            // after each request comes back, save data, pick next eligible subscriber until none are left.
            while (updateTasks.Count() > 0)
            {
                if (taskCounter % 100 == 0)
                    loggingDelegate?.Invoke($"Updated {taskCounter} subscribers of {localSubscribers.Count}...");

                var t = await Task.WhenAny(updateTasks); // get latest finished task

                updateTasks.Remove(t); // remove it from the queue

                if (subscriberEnumerator.MoveNext())
                {
                    var subscriber = subscriberEnumerator.Current;
                    updateTasks.Add(UpdateSingleSubscriberAsync(subscriber.Id, service, factory, loggingDelegate));
                }
                taskCounter++;
            }

            await Task.WhenAll(updateTasks);
        }

        protected async static Task UpdateSingleSubscriberAsync<T>(
            Guid subscriberId,
            IGovDeliveryApiService service,
            IGovDeliveryContextFactory<T> factory,
            Action<string> loggingDelegte
        )
            where T : AbstractGovDeliveryContext
        {
            var ctx = factory.CreateDbContext();
            var subscriber = ctx.Subscribers.First(s => s.Id == subscriberId);

            if (subscriber == null || service == null || ctx == null)
                return;

            var subscriberInfoTask = service.ReadSubscriberAsync(subscriber.Email);
            var subscriberTopicsTask = service.ListSubscriberTopicsAsync(subscriber.Email);
            var subscriberCategoriesTask = service.ListSubscriberCategoriesAsync(subscriber.Email);

            await Task.WhenAll(subscriberInfoTask, subscriberTopicsTask, subscriberCategoriesTask);

            var subscriberInfo = (await subscriberInfoTask).Data;

            // Update detailed subscriber info
            subscriber.BulletinFrequency = (BulletinFrequency)subscriberInfo.DigestFor.Value;
            subscriber.GovDeliveryId = subscriberInfo.Id.Value;
            subscriber.Phone = subscriberInfo.Phone;

            if (subscriberInfo.SendSubscriberUpdateNotifications != null)
                subscriber.SendSubscriberUpdateNotifications = subscriberInfo.SendSubscriberUpdateNotifications.Value;

            try { await ctx.SaveChangesAsync(); }
            catch (Exception e) { loggingDelegte?.Invoke($@"{e.Message} {e.TargetSite}"); }

            // Update Category Subscriptions

            var subscriberCategories = ctx.CategorySubscriptions
                .Where(sc => sc.SubscriberId == subscriber.Id)
                .Select(sc => sc.Category)
                .ToList();

            var subscriberCategoryInfo = (await subscriberCategoriesTask).Data.Items;

            // existing category subscriptions - do nothing.

            var newCategorySubscriptions = subscriberCategoryInfo
                .Where(sci => !subscriberCategories.Any(sc => sc.Code == sci.CategoryCode));

            foreach (var nCS in newCategorySubscriptions)
            {
                var cat = ctx.Categories.First(c => c.Code == nCS.CategoryCode);
                ctx.CategorySubscriptions.Add(new CategorySubscription
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
                ctx.CategorySubscriptions.Remove(categorySub);
            }

            await ctx.SaveChangesAsync();

            // Find new Topic subscriptions

            var subscriberTopics = ctx.TopicSubscriptions
                .Where(ts => ts.SubscriberId == subscriber.Id)
                .Select(ts => ts.Topic)
                .ToList();

            var subscriberTopicInfo = (await subscriberTopicsTask).Data.Items;

            var newTopicSubscriptions = subscriberTopicInfo
                .Where(sti => !subscriberTopics.Any(st => st.Code == sti.TopicCode))
                .ToList();

            foreach (var nTS in newTopicSubscriptions)
            {
                var topic = ctx.Topics.First(t => t.Code == nTS.TopicCode);
                ctx.TopicSubscriptions.Add(new TopicSubscription
                {
                    Subscriber = subscriber,
                    SubscriberId = subscriber.Id,
                    Topic = topic,
                    TopicId = topic.Id,
                });
            }

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                loggingDelegte?.Invoke($@"{e.Message} {e.TargetSite}");
            }

            // deleteable topic subscriptions
            var deleteableTopicSubscriptions = subscriberTopics
                .Where(st => !subscriberTopicInfo.Any(sti => sti.TopicCode == st.Code))
                .ToList();

            foreach (var dts in deleteableTopicSubscriptions)
            {
                var topicSub = ctx.TopicSubscriptions.First(ts => ts.TopicId == dts.Id);
                ctx.TopicSubscriptions.Remove(topicSub);
            }

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                loggingDelegte?.Invoke($@"{e.Message} {e.TargetSite}");
            }
        }
    }
}
