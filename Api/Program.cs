using Application.Helpers.Logger;
using Application.Interfaces.I;
using Application.Interfaces.IDiscount;
using Application.Interfaces.IDish;
using Application.Interfaces.IMenu;
using Application.Interfaces.IMenuOption;
using Application.Interfaces.IOrder;
using Application.Interfaces.IReceipt;
using Application.Interfaces.IUnitOfWork;
using Application.Interfaces.IUser;
using Application.UseCase.V2.Dish.Create;
using Application.UseCase.V2.Dish.GetByDescription;
using Application.UseCase.V2.Dish.GetById;
using Application.UseCase.V2.Dish.UpdatePrices;
using Application.UseCase.V2.Menu.Create;
using Application.UseCase.V2.Menu.GetById;
using Application.UseCase.V2.Menu.GetFilter;
using Application.UseCase.V2.Menu.GetNextAvailable;
using Application.UseCase.V2.Menu.GetWithOrders;
using Application.UseCase.V2.Order.Cancel;
using Application.UseCase.V2.Order.Create;
using Application.UseCase.V2.Order.Finished;
using Application.UseCase.V2.Order.GetById;
using Application.UseCase.V2.User.Create;
using Application.UseCase.V2.User.GetAll;
using Infraestructure.Commands;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //custom

            //log path file
            string logPath = builder.Configuration["AppSettings:logPath"];

            if (logPath == null)
            {
                Console.WriteLine("FAIL: LOG PATH FOR LOGS NULL...");
                return;

            }

            Logger.InitializeLogger(logPath);

            //secreto
            string? secret = builder.Configuration.GetSection("AppSettings")["secreto"];

            if (secret == null) { throw new ArgumentNullException(nameof(secret)); }

            //Database
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MenuAppContext>(options => options.UseMySQL(connectionString));


            if (connectionString == null)
            {
                Logger.LogError(null, "connection string not detected");
                return;
            }


            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            //Query
            builder.Services.AddScoped<IDishQuery, DishQuery>();
            builder.Services.AddScoped<IMenuQuery, MenuQuery>();
            builder.Services.AddScoped<IOrderQuery, OrderQuery>();
            builder.Services.AddScoped<IMenuOptionQuery, MenuOptionQuery>();
            builder.Services.AddScoped<IDiscountQuery, DiscountQuery>();
            builder.Services.AddScoped<IUserQuery, UserQuery>();


            //Command
            builder.Services.AddScoped<IDishCommand, DishCommand>();
            builder.Services.AddScoped<IMenuCommand, MenuCommand>();
            builder.Services.AddScoped<IMenuOptionCommand, MenuOptionCommand>();
            builder.Services.AddScoped<IOrderCommand, OrderCommand>();
            builder.Services.AddScoped<IReceiptCommand, ReceiptCommand>();
            builder.Services.AddScoped<IUserCommand, UserCommand>();

            //UseCase

            //Dish
            builder.Services.AddScoped<ICreateDishCommand, CreateDishCommand>();
            builder.Services.AddScoped<IGetDishesByDescription, GetDishByDescription>();
            builder.Services.AddScoped<IGetDishByIdQuery, GetDishById>();
            builder.Services.AddScoped<IDishesUpdatePrice, DishesUpdatePrice>();

            //Menu
            builder.Services.AddScoped<IGetMenuByIdQuery, GetMenuById>();
            builder.Services.AddScoped<IGetMenuFiltered, GetMenuByFiltered>();
            builder.Services.AddScoped<ICreateMenuCommand, CreateMenuCommand>();
            builder.Services.AddScoped<IGetNextMenuAvailable, GetNextMenuAvailable>();
            builder.Services.AddScoped<IGetMenuWithOrders, GetMenuWithOrders>();

            //Order
            builder.Services.AddScoped<ICreateOrderCommand, CreateOrderCommand>();
            builder.Services.AddScoped<ICancelOrderCommand, CancelOrder>();
            builder.Services.AddScoped<IGetOrderByIdQuery, GetOrderById>();
            builder.Services.AddScoped<IFinishedOrderCommand, FinishedOrderCommand>();

            //User
            builder.Services.AddScoped<ICreateUserCommand, CreateUser>();
            builder.Services.AddScoped<IGetUsers, GetAllUsersQuery>();

            ////Platillos
            //builder.Services.AddScoped<IPlatilloQuery, PlatilloQuery>();
            //builder.Services.AddScoped<IPlatilloCommand, PlatilloCommand>();
            //builder.Services.AddScoped<IPlatilloService, PlatilloService>();



            ////autenticacion
            //builder.Services.AddScoped<IAuthenticacionQuery, AutehenticationQuery>();
            //builder.Services.AddScoped<IAuthenticationService>(
            //    provider =>
            //    {
            //        return new AuthenticationService(
            //            secret,
            //            provider.GetRequiredService<IAuthenticacionQuery>(),
            //            provider.GetRequiredService<IPersonalService>(),
            //            provider.GetRequiredService<IPersonalQuery>(),
            //            provider.GetRequiredService<IPersonalCommand>());
            //    });



            ////Recibo
            //builder.Services.AddScoped<IReciboCommand, ReciboCommand>();
            //builder.Services.AddScoped<IReciboQuery, ReciboQuery>();
            //builder.Services.AddScoped<IReciboService, ReciboService>();

            ////autorizaciones pedido
            //builder.Services.AddScoped<IRepositoryAutorizacionPedido, RepositoryAutorizacionPedido>();


            ////Costos
            //builder.Services.AddScoped<ICostoService, CostoService>();


            //Automatizacion de pedidos
            //builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            //builder.Services.AddScoped<IAutomation, AutomationDelivery>();


            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWorkCreateOrder, UnitOfWorkCreateOrder>();
            builder.Services.AddScoped<IUnitOfWorkFinishedOrder, UnitOfWorkFinishedOrder>();

            //Autenticacion
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
             {
                 jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(secret)
                 ),
                     ValidIssuer = "menuServ",
                     ValidAudience = "menu",
                     ClockSkew = TimeSpan.FromHours(1)

                 };
             });

            //CORS deshabilitar
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            Logger.LogInformation("app runing...", null);
            app.Run();
        }
    }
}