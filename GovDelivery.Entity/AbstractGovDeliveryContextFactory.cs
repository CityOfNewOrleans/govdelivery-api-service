using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovDelivery.Entity
{
    public abstract class AbstractGovDeliveryContextFactory<T> : IDesignTimeDbContextFactory<T>, IGovDeliveryContextFactory<T>
        where T : AbstractGovDeliveryContext, IGovDeliveryContext, new()
    {
        protected DbContextOptionsBuilder optionsBuilder { get; set; }

        public AbstractGovDeliveryContextFactory()
        {
            throw new NotImplementedException("Override me in concrete implentation!");
        }

        public AbstractGovDeliveryContextFactory(DbContextOptionsBuilder optionsBuilder)
        {
            this.optionsBuilder = optionsBuilder;
        }

        public T CreateDbContext() => CreateDbContext(new string[] { "" });

        public T CreateDbContext(string[] args) => (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);

    }
}
