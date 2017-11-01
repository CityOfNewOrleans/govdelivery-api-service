using System.Xml.Serialization;

namespace GovDelivery.Rest.Models {

    [XmlRoot("errors")]
    public class ErrorModel {
        
        [XmlElement("code")]
        public string Code {get;set;}

        [XmlElement("error")]
        public string Error { get; set; }

    }

}