using Domain.Entities;
using Infraestructure.Config;
using Infraestructure.Config.TestData;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class MenuAppContext : DbContext
    {
        public MenuAppContext(DbContextOptions<MenuAppContext> options)
        : base(options)
        {

        }


        public DbSet<User> Personales { get; set; }
        public DbSet<Order> Pedidos { get; set; }
        public DbSet<Receipt> Recibos { get; set; }
        public DbSet<Discount> Descuentos { get; set; }
        public DbSet<PedidoPorMenuPlatillo> PedidosPorMenuPlatillo { get; set; }
        public DbSet<MenuPlatillo> MenuPlatillos { get; set; }
        public DbSet<Dish> Platillos { get; set; }
        public DbSet<Menu> Menues { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Authorization> AutorizacionPedidos { get; set; }


        //config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonalConfig());
            modelBuilder.ApplyConfiguration(new PedidosConfig());
            modelBuilder.ApplyConfiguration(new MenuPlatilloConfig());
            modelBuilder.ApplyConfiguration(new PedidoPorMenuPlatilloConfig());
            modelBuilder.ApplyConfiguration(new ReciboConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new PlatilloConfig());
            modelBuilder.ApplyConfiguration(new PagoConfig());
            modelBuilder.ApplyConfiguration(new DescuentoConfig());
            modelBuilder.ApplyConfiguration(new AutorizacionPedidoConfig());


            modelBuilder.ApplyConfiguration(new AdministradorTest());
            modelBuilder.ApplyConfiguration(new DescuentoTest());

            //test data
            //TODO delete for production enviroment
            modelBuilder.ApplyConfiguration(new PlatilloTest());
            modelBuilder.ApplyConfiguration(new PersonalConfig());
            modelBuilder.ApplyConfiguration(new PersonalTest());
        }
    }
}
