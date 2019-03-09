using System;
using System.Threading.Tasks;

namespace TaxiCompany.Models
{
    public interface IUnitOfWork : IDisposable
    {
        TaxiCompanyContext Context { get; }
        void Commit();
    }
}