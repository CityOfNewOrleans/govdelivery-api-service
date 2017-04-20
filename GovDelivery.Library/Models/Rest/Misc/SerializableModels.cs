using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GovDelivery.Library.Models.Rest.Misc
{
    public abstract class Nillable
    {
        [XmlAttribute("nil")]
        public bool Nil { get { return true; } set { } }
    }

    [Serializable()]
    public class NillableSerializableString : Nillable
    {
        [XmlText()]
        public string Value { get; set; }
    }

    [Serializable()]
    public class NillableSerializableBool : Nillable
    {
        [XmlAttribute("type")]
        public string Type { get { return "boolean"; } set { } }

        [XmlText()]
        public bool Value { get; set; }
    }

    [Serializable()]
    public class SerializableBool
    {
        [XmlAttribute("type")]
        public string Type { get { return "boolean"; } set { } }

        [XmlText()]
        public bool Value { get; set; }
    }

    [Serializable()]
    public class NillableSerializableInt : Nillable
    {
        [XmlAttribute("type")]
        public string Type { get { return "integer"; } set { } }

        [XmlIgnore()]
        public int Value { get; set; }

        [XmlText()]
        public string OuterValue { get { return Value.ToString(); } set { } }

    }

    [Serializable()]
    public class SerializableInt
    {
        [XmlAttribute("type")]
        public string Type { get { return "integer"; } set { } }

        [XmlText()]
        public int Value { get; set; }

    }

    [Serializable()]
    public abstract class BaseSerializableArray<T>
    {
        /// <remarks/>
        [XmlAttribute("type")]
        public string Type { get { return "array"; } set { } }

        [XmlIgnore]
        public abstract List<T> Items { get; set; }
    }
}
