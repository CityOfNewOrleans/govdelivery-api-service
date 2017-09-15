using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity
{
    public interface IGovDeliveryContextFactory<T> : IDesignTimeDbContextFactory<T>
        where T: DbContext, IGovDeliveryContext
    {
        T CreateDbContext();
    }
}
