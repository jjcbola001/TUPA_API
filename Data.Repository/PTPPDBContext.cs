using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using SharedModels;
using SharedUtilities;

namespace Data.Repository
{
    public partial class PTPPDBContext : DbContext
    {
        public PTPPDBContext()
        {
        }

        private readonly string _connectionString;

        public PTPPDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PTPPDBContext(DbContextOptions<PTPPDBContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                ConnectionStrings connectionStrings = ConfigurationUtility.GetConnectionStrings();
                var con = connectionStrings.PrimaryDatabaseConnectionString;

                optionsBuilder.UseSqlServer(con);
            }
        }

 
    }
}
