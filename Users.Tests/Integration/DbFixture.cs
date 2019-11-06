using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Users.Persistence;

namespace Users.Tests.Integration
{
    public class DbFixture : IDisposable
    {
        private readonly DbContextOptions<UsersDbContext> _options;

        public string ConnectionString { get; }
        public string EnvironmentName { get; }

        public DbFixture()
        {            
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json");

            this.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(this.EnvironmentName))
                builder.AddJsonFile($"appsettings.{this.EnvironmentName}.json", true);

            var config = builder.Build();

            var connString = config.GetConnectionString("db");
            this.ConnectionString = string.Format(connString, Guid.NewGuid());

            _options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseSqlServer(this.ConnectionString)
                .Options;

            Console.Write($"running integration tests on {this.EnvironmentName} ...");
        }

        public UsersDbContext BuildDbContext()
        {
            try
            {
                var ctx = new UsersDbContext(_options);
                ctx?.Database?.EnsureCreated();
                return ctx;
            }
            catch (Exception ex)
            {
                var builder = new SqlConnectionStringBuilder(this.ConnectionString);

                throw new Exception($"unable to connect to db {builder.InitialCatalog} on {builder.DataSource}", ex);
            }
        }

        public virtual void Dispose()
        {
            try
            {
                var dbCtx = BuildDbContext();
                dbCtx.Database.EnsureDeleted();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
