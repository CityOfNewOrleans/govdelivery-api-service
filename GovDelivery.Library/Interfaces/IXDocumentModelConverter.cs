using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GovDelivery.Library.Interfaces
{
    public interface IConvertXDocumentToModel<T>
    {
        T ToModel(XDocument xDoc);
    }

    public interface IConvertModelToXDocument<T>
    {
        XDocument ToXDocument(T model);

    }
}
