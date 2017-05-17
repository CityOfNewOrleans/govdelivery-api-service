using GovDelivery.Rest.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Rest.Models.Topic
{
    [XmlRoot("topic")]
    public class UpdateTopicRequestModel
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
        public NillableSerializableInt DefaultPagewatchResults { get; set; }

        [XmlElement("pagewatch-autosend")]
        public SerializableBool PagewatchAutosend { get; set; }

        /// <summary>
        /// Use PagewatchType enum for acceptable values.
        /// </summary>
        [XmlElement("pagewatch-type")]
        public SerializableInt PagewatchType { get; set; }

        [XmlElement("watch-tagged-content")]
        public SerializableBool WatchTaggedContent { get; set; }

        [XmlElement("pages")]
        public SerializablePagesArray Pages { get; set; }

        [XmlElement("categories")]
        public SerializableCategoriesArray Categories { get; set; }

        [XmlElement("visibility")]
        public TopicVisibility Visibility { get; set; }

        public class SerializablePagesArray : BaseSerializableArray<Page>
        {
            [XmlElement("page")]
            public override List<Page> Items { get; set; }
        }

        public class Page
        {
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
        }

        public class SerializableCategoriesArray : BaseSerializableArray<Category>
        {
            [XmlElement("category")]
            public override List<Category> Items { get; set; }
        }

        public class Category
        {
            [XmlElement("code")]
            public string Code { get; set; }
        }
    }

    
}
