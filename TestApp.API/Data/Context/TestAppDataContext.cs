

namespace TestApp.API.Data.Context
{
    using Microsoft.EntityFrameworkCore;
    using TestApp.API.Models;

    public class TestAppDataContext : DbContext
    {
        public TestAppDataContext(DbContextOptions<TestAppDataContext> options) : base(options)
        {
        }

        public virtual DbSet<Value> Values {get;set;}
    }
}