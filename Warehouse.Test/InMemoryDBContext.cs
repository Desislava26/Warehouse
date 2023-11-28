using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Test
{
    public class InMemoryDBContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public InMemoryDBContext()
        {
            //will become in memory database
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            //this dbContext is IDisposable
            using var context = new ApplicationDbContext(dbContextOptions);
            context.Database.EnsureCreated();
        }


        public ApplicationDbContext CreateContext() => new ApplicationDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();


    }
}
