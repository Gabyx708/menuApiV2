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


        public DbSet<Personal> Personales { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<Descuento> Descuentos { get; set; }
        public DbSet<PedidoPorMenuPlatillo> PedidosPorMenuPlatillo { get; set; }
        public DbSet<MenuPlatillo> MenuPlatillos { get; set; }
        public DbSet<Platillo> Platillos { get; set; }
        public DbSet<Menu> Menues { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<AutorizacionPedido> AutorizacionPedidos { get; set; }


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
