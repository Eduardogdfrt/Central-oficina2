using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Ellp.Api.Infra.SqlServer
{
    public class SqlServerDbContextFactory : IDbContextFactory<SqlServerDbContext>
    {
        private readonly string _connectionString;

        public SqlServerDbContextFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString")
                                   ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                   ?? throw new InvalidOperationException("Connection string 'DbConnectionString' not found.");
        }

        public SqlServerDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SqlServerDbContext>()
                            .UseSqlServer(_connectionString)
                            .Options;

            return new SqlServerDbContext(options);
        }
    }
}
