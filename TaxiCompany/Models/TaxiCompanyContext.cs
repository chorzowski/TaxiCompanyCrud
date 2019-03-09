using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiCompany.Models
{
    public class TaxiCompanyContext : DbContext
    {
        public TaxiCompanyContext(DbContextOptions<TaxiCompanyContext>options) : base(options)
        {

        }
        public DbSet<Driver> Drivers { get; set; }
    }
}
