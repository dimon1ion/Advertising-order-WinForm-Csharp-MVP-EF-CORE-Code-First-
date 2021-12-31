using Course_Work_Advertising_order_WinForm_Csharp.DbObjects;
using Microsoft.EntityFrameworkCore;

namespace Course_Work_Advertising_order_WinForm_Csharp.DbContextDir
{
    public class AdvertisingOrderContext : DbContext
    {
        public DbSet<AdType> AdTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdvertisingOrder;Integrated Security=true;");
        }
    }
}
