using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GovDelivery.Library.Utils
{
    public static class XElementConversionExtenstions
    {
        public static XElement ToXElement(this int x, string elementName) => 
            new XElement(elementName, new XAttribute("type", "integer"), x.ToString());

        public static XElement ToXElement(this bool x, string elementName) => 
            new XElement(elementName, new XAttribute("type", "boolean"), x.ToString());

        public static XElement ToXElement(this string x, string elementName) =>
            new XElement(elementName, x);

        public static XElement ToXElement<T>(this IEnumerable<T> x, string elementName, Func<T, XElement> itemConversion) =>
            new XElement(elementName, new XAttribute("type", "array"), x.Select(itemConversion));

    }
}
