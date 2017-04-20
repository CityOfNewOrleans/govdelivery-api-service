using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GovDelivery.Library.Models
{
    public abstract class BaseConvertibleFromXDocument {
        
        protected abstract void AbsorbValuesFromXDocument(XDocument xDoc);

    }
}
