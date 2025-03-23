using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Company_API.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = "company.db",
                Cache = SqliteCacheMode.Shared
            }.ToString();

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
