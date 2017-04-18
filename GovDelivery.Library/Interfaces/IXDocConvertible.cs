using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GovDelivery.Library.Interfaces
{
    public interface IXDocConvertible<T>
    {
        XDocument ToXDoc(T model);

        T FromXDoc(XDocument xDoc);
    }
}
