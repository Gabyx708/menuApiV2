using Application.Helpers.Logger;
using Application.Interfaces.I;
using Application.Interfaces.IAuthentication;
using Application.Interfaces.IBills;
using Application.Interfaces.IDiscount;
using Application.Interfaces.IDish;
using Application.Interfaces.IMenu;
using Application.Interfaces.IMenuOption;
using Application.Interfaces.IOrder;
using Application.Interfaces.IReceipt;
using Application.Interfaces.ISession;
using Application.Interfaces.IUnitOfWork;
using Application.Interfaces.IUser;
using Application.UseCase.V2.Bills;
using Application.UseCase.V2.Dish.Create;
using Application.UseCase.V2.Dish.GetByDescription;
using Application.UseCase.V2.Dish.GetById;
using Application.UseCase.V2.Dish.UpdatePrices;
using Application.UseCase.V2.Menu.Create;
using Application.UseCase.V2.Menu.GetById;
using Application.UseCase.V2.Menu.GetFilter;
using Application.UseCase.V2.Menu.GetNextAvailable;
using Application.UseCase.V2.Menu.GetUserOrderFromMenu;
using Application.UseCase.V2.Menu.GetWithOrders;
using Application.UseCase.V2.Order.Cancel;
using Application.UseCase.V2.Order.Create;
using Application.UseCase.V2.Order.Finished;
using Application.UseCase.V2.Order.GetById;
using Application.UseCase.V2.User.ChangePassword;
using Application.UseCase.V2.User.Create;
using Application.UseCase.V2.User.GetAll;
using Application.UseCase.V2.User.GetBills;
using Application.UseCase.V2.User.GetById;
using Application.UseCase.V2.User.GetOrders;
using Application.UseCase.V2.User.SignIn;
using Infraestructure.Commands;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Infraestructure.Querys.Authentication;
using Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
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
                Logger.LogError("FAIL: LOG PATH FOR LOGS NULL...");
                return;

            }

            Logger.InitializeLogger(logPath);

            //secreto
            string? secret = builder.Configuration.GetSection("AppSettings")["secreto"];

            if (secret == null) { throw new ArgumentNullException(nameof(secret)); }

            //Database
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                Logger.LogError("connection string not detected");
                return;
            }

            builder.Services.AddDbContext<MenuAppContext>(options => options.UseMySQL(connectionString));
            var builderConnectionString = new MySqlConnectionStringBuilder(connectionString);
            string host = builderConnectionString.Server;


            Logger.LogInformation("Attempting to connect to the database at host: {Host}", host);

            // Intentar probar la conexión
            try
            {
                using var scope = builder.Services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MenuAppContext>();

                if (context.Database.CanConnect())
                {
                    Logger.LogInformation("Successfully connected to the database at host: {Host}", host);
                }
                else
                {
                    Logger.LogInformation("Failed to connect to the database at host: {Host}", host);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while attempting to connect to the database at host: {Host}", host);
            }


            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            //Query
            builder.Services.AddScoped<IDishQuery, DishQuery>();
            builder.Services.AddScoped<IMenuQuery, MenuQuery>();
            builder.Services.AddScoped<IOrderQuery, OrderQuery>();
            builder.Services.AddScoped<IMenuOptionQuery, MenuOptionQuery>();
            builder.Services.AddScoped<IDiscountQuery, DiscountQuery>();
            builder.Services.AddScoped<IUserQuery, UserQuery>();
            builder.Services.AddScoped<IAuthenticationQuery, AuthenticacionQuery>();


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
            builder.Services.AddScoped<IGetUserOrdersFromMenu, GetUserOrderFromMenu>();

            //Order
            builder.Services.AddScoped<ICreateOrderCommand, CreateOrderCommand>();
            builder.Services.AddScoped<ICancelOrderCommand, CancelOrder>();
            builder.Services.AddScoped<IGetOrderByIdQuery, GetOrderById>();
            builder.Services.AddScoped<IFinishedOrderCommand, FinishedOrderCommand>();

            //User
            builder.Services.AddScoped<ICreateUserCommand, CreateUser>();
            builder.Services.AddScoped<IGetUsers, GetAllUsersQuery>();
            builder.Services.AddScoped<IGetUserOrdersQuery, GetUserOrders>();
            builder.Services.AddScoped<IGetUserByIdQuery, GetById>();
            builder.Services.AddScoped<IChangePassword, ChangePasswordCommand>();
            builder.Services.AddScoped<IGetUserBills,GetUserBills>();
            builder.Services.AddScoped<ISignIn>(
                            provider =>
                            {
                                return new UserSignInCommand(
                                provider.GetRequiredService<IAuthenticationQuery>(),
                                secret);
                             });


            //Bills
            builder.Services.AddScoped<IGetBillsMonth,GetBillsMonth>();



            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWorkCreateOrder, UnitOfWorkCreateOrder>();
            builder.Services.AddScoped<IUnitOfWorkFinishedOrder, UnitOfWorkFinishedOrder>();

            //jwt config
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
             {
                 jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(secret)
                 ),
                     ValidIssuer = "menu-service",
                     ValidAudience = "app-frontend",
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


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MENU API", Version = "v2" });

                // Configuración de Swagger para incluir JWT
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Esquema del token
                    BearerFormat = "JWT" // Formato del token
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                { securityScheme, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
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

            Logger.LogInformation("app running..... OK\n");


            app.Run();
        }
    }
}