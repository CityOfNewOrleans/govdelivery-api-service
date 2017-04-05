using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Models.Rest.Topic
{
    [XmlRoot(ElementName = "topic")]
    public class CreateTopicRequestModel
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "short-name")]
        public string ShortName { get; set; }

        [XmlElement(ElementName = "description", IsNullable = true)]
        public string Description { get; set; }

        [XmlElement(ElementName = "send-by-email-enabled", DataType = "boolean")]
        public bool SendByEmailEnabled { get; set; }

        [XmlElement(ElementName = "wireless-enabled", DataType = "boolean")]
        public bool SmsEnabled { get; set; }

        [XmlElement(ElementName = "rss-feed-url", IsNullable = true)]
        public string RssFeedUrl { get; set; }

        [XmlElement(ElementName = "rss-feed-name", IsNullable = true)]
        public string RssFeedTitle { get; set; }

        [XmlElement(ElementName = "rss-feed-description", IsNullable = true)]
        public string RssFeedDescription { get; set; }

        [XmlElement(ElementName = "pagewatch-enabled", DataType = "boolean")]
        public bool PagewatchEnabled { get; set; }

        [XmlElement(ElementName = "pagewatch-suspended", DataType = "boolean")]
        public bool PagewatchSuspended { get; set; }

        [XmlElement(ElementName = "default-pagewatch-results", DataType = "integer", IsNullable = true)]
        public int DefaultPagewatchResults { get; set; }

        [XmlElement(ElementName = "pagewatch-autosend", DataType = "boolean")]
        public bool PagewatchAutosend { get; set; }

        [XmlElement(ElementName = "pagewatch-type", DataType = "integer")]
        public PagewatchType PagewatchType { get; set; }

        [XmlElement(ElementName = "watch-tagged-content", DataType = "boolean")]
        public bool WatchTaggedContent { get; set; }

        [XmlElement(ElementName = "pages", DataType = "array")]
        public IEnumerable<Page> Pages { get; set; }

        [XmlElement(ElementName = "categories", DataType = "array")]
        public IEnumerable<Category> Categories { get; set; }

        [XmlElement(ElementName = "visibility")]
        public TopicVisibility Visibility { get; set; }

        [XmlRoot(ElementName = "page")]
        public class Page
        {
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
        }

        [XmlRoot(ElementName = "category")]
        public class Category
        {
            [XmlElement(ElementName = "code")]
            public string Code { get; set; }
        }

    }

    [Serializable]
    public enum TopicVisibility
    {
        [XmlEnum(Name = "Listed")]
        Listed,
        [XmlEnum(Name = "Unlisted")]
        Unlisted,
        [XmlEnum(Name = "Restricted")]
        Restricted
    }

    public enum PagewatchType
    {
        HtmlPage = 1,
        RssAtomFeed = 2,
        File = 3,
    }
}
