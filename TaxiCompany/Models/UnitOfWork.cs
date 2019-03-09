using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiCompany.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public TaxiCompanyContext Context { get; }

        public UnitOfWork(TaxiCompanyContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }
    }
}
