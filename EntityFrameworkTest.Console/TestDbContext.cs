using System.Data.Entity;
using EntityFrameworkTest.Console.Models;

namespace EntityFrameworkTest.Console
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
            : base("Data Source=localhost; Initial Catalog=EntityFrameworkTest; Integrated Security=True; Connection Reset=False; MultipleActiveResultSets=True")
        {
            Configure();
        }
        
        protected void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Root> Roots { get; set; }

        public virtual DbSet<AnyChild> AnyChildren { get; set; }

        public virtual DbSet<AnyEntity> AnyEntities { get; set; }

        public virtual DbSet<DateChild> DateChildren { get; set; }

        public virtual DbSet<AnyEntityParent> AnyEntityParents { get; set; }
    }
}