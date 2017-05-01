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
        /// <summary>
        /// For serialization purposes. Always returns true.
        /// </summary>
        [XmlAttribute("nil")]
        public bool Nil { get { return true; } set { } }
    }

    [Serializable()]
    public class NillableSerializableString : Nillable
    {
        public NillableSerializableString() { }

        public NillableSerializableString(string value)
        {
            Value = value;
        }

        [XmlText()]
        public string Value { get; set; }
    }

    [Serializable()]
    public class NillableSerializableBool : Nillable
    {
        public NillableSerializableBool() { }

        public NillableSerializableBool(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// For serialization purposes. Always returns "boolean".
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get { return "boolean"; } set { } }

        [XmlText()]
        public bool Value { get; set; }
    }

    [Serializable()]
    public class SerializableBool
    {
        public SerializableBool() { }

        public SerializableBool(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// For serialization purposes. Always returns "boolean".
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get { return "boolean"; } set { } }

        [XmlText()]
        public bool Value { get; set; }
    }

    [Serializable()]
    public class NillableSerializableInt : Nillable
    {
        public NillableSerializableInt() { }

        public NillableSerializableInt(int value)
        {
            Value = value;
        }

        /// <summary>
        /// For serialization purposes. Always returns "integer".
        /// </summary>
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
        public SerializableInt() { }

        public SerializableInt(int value)
        {
            Value = value;
        }

        /// <summary>
        /// For serialization purposes. Always returns "integer".
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get { return "integer"; } set { } }

        [XmlText()]
        public int Value { get; set; }

    }

    [Serializable()]
    public abstract class BaseSerializableArray<T>
    {
        /// <summary>
        /// For serialization purposes. Always returns "array".
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get { return "array"; } set { } }

        [XmlIgnore]
        public abstract List<T> Items { get; set; }
    }
}
