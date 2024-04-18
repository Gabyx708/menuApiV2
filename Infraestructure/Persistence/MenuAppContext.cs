using Domain.Entities;
using Infraestructure.Config;
using Infraestructure.TestData;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class MenuAppContext : DbContext
    {
        public MenuAppContext(DbContextOptions<MenuAppContext> options)
        : base(options)
        {

        }

        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menues { get; set; }
        public DbSet<MenuOption> MenuOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<User> Users { get; set; }


        //config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorizationConfig());
            modelBuilder.ApplyConfiguration(new DiscountConfig());
            modelBuilder.ApplyConfiguration(new DishConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new MenuOptionConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new OrderItemConfig());
            modelBuilder.ApplyConfiguration(new ReceiptConfig());
            modelBuilder.ApplyConfiguration(new StateConfig());
            modelBuilder.ApplyConfiguration(new TransitionConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());



            //test data
            //TODO delete for production enviroment
            modelBuilder.ApplyConfiguration(new DishTest());
            modelBuilder.ApplyConfiguration(new DiscountTest());
            //modelBuilder.ApplyConfiguration(new AdministratorTest());
            //modelBuilder.ApplyConfiguration(new UserTest());
        }
    }
}
