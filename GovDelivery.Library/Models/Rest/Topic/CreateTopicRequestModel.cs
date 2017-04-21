using GovDelivery.Library.Models.Rest.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Topic
{
    [XmlRoot("topic")]
    public class CreateTopicRequestModel
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("short-name")]
        public string ShortName { get; set; }

        [XmlElement("description")]
        public NillableSerializableString Description { get; set; }

        [XmlElement("send-by-email-enabled")]
        public SerializableBool SendByEmailEnabled { get; set; }

        [XmlElement("wireless-enabled")]
        public SerializableBool SmsEnabled { get; set; }

        [XmlElement("rss-feed-url")]
        public NillableSerializableString RssFeedUrl { get; set; }

        [XmlElement("rss-feed-name")]
        public NillableSerializableString RssFeedTitle { get; set; }

        [XmlElement("rss-feed-description")]
        public NillableSerializableString RssFeedDescription { get; set; }

        [XmlElement("pagewatch-enabled")]
        public SerializableBool PagewatchEnabled { get; set; }

        [XmlElement("pagewatch-suspended")]
        public SerializableBool PagewatchSuspended { get; set; }

        [XmlElement("default-pagewatch-results")]
        public NillableSerializableString DefaultPagewatchResults { get; set; }

        [XmlElement("pagewatch-autosend")]
        public SerializableBool PagewatchAutosend { get; set; }

        /// <summary>
        /// Use PagewatchType enum to set acceptable values.
        /// </summary>
        [XmlElement("pagewatch-type")]
        public SerializableInt PagewatchType { get; set; }

        [XmlElement("watch-tagged-content")]
        public SerializableBool WatchTaggedContent { get; set; }

        [XmlElement("pages")]
        public SerializablePageArray Pages { get; set; }

        [XmlElement("categories")]
        public SerializableCategoryArray Categories { get; set; }

        /// <summary>
        /// Use TopicVisibility enum to set acceptable values.
        /// </summary>
        [XmlElement("visibility")]
        public SerializableInt Visibility { get; set; }

        public class SerializablePageArray : BaseSerializableArray<Page>
        {
            [XmlElement("page")]
            public override List<Page> Items { get; set; }
        }

        public class Page
        {
            [XmlElement("url")]
            public string Url { get; set; }
        }

        public class SerializableCategoryArray : BaseSerializableArray<Category>
        {
            [XmlElement("category")]
            public override List<Category> Items { get; set; }
        }

        [XmlRoot("category")]
        public class Category
        {
            [XmlElement("code")]
            public string Code { get; set; }
        }

    }

    [Serializable]
    public enum TopicVisibility
    {
        [XmlEnum("Listed")]
        Listed,
        [XmlEnum("Unlisted")]
        Unlisted,
        [XmlEnum("Restricted")]
        Restricted
    }

    public enum PagewatchType
    {
        HtmlPage = 1,
        RssAtomFeed = 2,
        File = 3,
    }
}
