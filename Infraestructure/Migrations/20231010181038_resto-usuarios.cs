using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class restousuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Descuento",
                columns: table => new
                {
                    IdDescuento = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaInicioVigencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuento", x => x.IdDescuento);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    IdMenu = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaConsumo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaCarga = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.IdMenu);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    IdPersonal = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Dni = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Mail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Privilegio = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    isAutomatico = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.IdPersonal);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Platillo",
                columns: table => new
                {
                    IdPlatillo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillo", x => x.IdPlatillo);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    NumeroPago = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    idPersonal = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAnulado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.NumeroPago);
                    table.ForeignKey(
                        name: "FK_Pagos_Personal_idPersonal",
                        column: x => x.idPersonal,
                        principalTable: "Personal",
                        principalColumn: "IdPersonal",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuPlatillo",
                columns: table => new
                {
                    IdMenuPlatillo = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdMenu = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdPlatillo = table.Column<int>(type: "int", nullable: false),
                    PrecioActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Solicitados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPlatillo", x => x.IdMenuPlatillo);
                    table.ForeignKey(
                        name: "FK_MenuPlatillo_Menu_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menu",
                        principalColumn: "IdMenu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPlatillo_Platillo_IdPlatillo",
                        column: x => x.IdPlatillo,
                        principalTable: "Platillo",
                        principalColumn: "IdPlatillo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recibo",
                columns: table => new
                {
                    IdRecibo = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdDescuento = table.Column<Guid>(type: "char(36)", nullable: false),
                    precioTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroPago = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recibo", x => x.IdRecibo);
                    table.ForeignKey(
                        name: "FK_Recibo_Descuento_IdDescuento",
                        column: x => x.IdDescuento,
                        principalTable: "Descuento",
                        principalColumn: "IdDescuento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recibo_Pagos_NumeroPago",
                        column: x => x.NumeroPago,
                        principalTable: "Pagos",
                        principalColumn: "NumeroPago");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    IdPedido = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdPersonal = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdRecibo = table.Column<Guid>(type: "char(36)", nullable: false),
                    FechaDePedido = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedido_Personal_IdPersonal",
                        column: x => x.IdPersonal,
                        principalTable: "Personal",
                        principalColumn: "IdPersonal",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedido_Recibo_IdRecibo",
                        column: x => x.IdRecibo,
                        principalTable: "Recibo",
                        principalColumn: "IdRecibo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutorizacionPedido",
                columns: table => new
                {
                    IdPedido = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdPersonal = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorizacionPedido", x => new { x.IdPedido, x.IdPersonal });
                    table.ForeignKey(
                        name: "FK_AutorizacionPedido_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorizacionPedido_Personal_IdPersonal",
                        column: x => x.IdPersonal,
                        principalTable: "Personal",
                        principalColumn: "IdPersonal",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PedidoPorMenuPlatillo",
                columns: table => new
                {
                    IdPedido = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdMenuPlatillo = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoPorMenuPlatillo", x => new { x.IdPedido, x.IdMenuPlatillo });
                    table.ForeignKey(
                        name: "FK_PedidoPorMenuPlatillo_MenuPlatillo_IdMenuPlatillo",
                        column: x => x.IdMenuPlatillo,
                        principalTable: "MenuPlatillo",
                        principalColumn: "IdMenuPlatillo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoPorMenuPlatillo_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Descuento",
                columns: new[] { "IdDescuento", "FechaInicioVigencia", "Porcentaje" },
                values: new object[] { new Guid("603c1e99-23d9-433f-a420-f22086e77dc4"), new DateTime(2023, 10, 10, 15, 10, 38, 675, DateTimeKind.Local).AddTicks(8045), 50m });

            migrationBuilder.InsertData(
                table: "Personal",
                columns: new[] { "IdPersonal", "Apellido", "Dni", "FechaAlta", "FechaIngreso", "FechaNac", "Mail", "Nombre", "Password", "Privilegio", "Telefono", "isAutomatico" },
                values: new object[,]
                {
                    { new Guid("1234744a-563d-4d1b-8843-feb9106d8817"), "BOT", "NOUSAR", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "no usar usuario", "BOT", "bf409362e443f1a3275d1c31e9adc6b8f35821220e3e1b637cbbf2e0a7dacbeb", 1, "00000", false },
                    { new Guid("1a371422-8bf5-4f2d-93f1-8a67424da47e"), "López", "456789123", new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "carloslopez@example.com", "Carlos", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "678912345", false },
                    { new Guid("49296257-6749-435b-b0c5-8ac9952d3cf5"), "González", "987654321", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1988, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "mariagonzalez@example.com", "María", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "0987654321", false },
                    { new Guid("74a4eae0-b44b-412a-a490-5aedaf37e45c"), "Pérez", "123456789", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "juanperez@example.com", "Juan", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "1234567890", false },
                    { new Guid("75c2993f-a877-4166-a0ac-84e15c403ef4"), "Aker", "administrador", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sistemas@tecnaingenieria.com", "Administrador", "99c1fcf52fc18a9417f60d0e6e7119957fc5638f4ee80ff04fe91bdd5763715d", 1, "1234567890", false },
                    { new Guid("82494643-e613-4332-93c7-2c916b59a7a6"), "Fernández", "258963147", new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1987, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "diegofernandez@example.com", "Diego", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "741852963", false },
                    { new Guid("8af2a1ec-ecfe-40f1-b57d-5d2d01bf016c"), "Martínez", "789456123", new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedromartinez@example.com", "Pedro", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "987654321", false },
                    { new Guid("98808cb4-01f0-496e-b3d2-0291cfe56990"), "López", "741852963", new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1993, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "sofialopez@example.com", "Sofía", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "369258147", false },
                    { new Guid("9e3bca69-3461-4e34-a598-d5ff30e811ea"), "Hernández", "654123987", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1991, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "laurahernandez@example.com", "Laura", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "876543219", false },
                    { new Guid("b97cf646-d8b0-47ca-ba53-c269864c1110"), "Ramírez", "321654987", new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1992, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "anaramirez@example.com", "Ana", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "543216549", false },
                    { new Guid("e09f70be-4c95-4bf8-840a-231871e158dd"), "Gómez", "963852741", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1986, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "javiergomez@example.com", "Javier", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "159357852", false },
                    { new Guid("e5e6ca2b-9aa3-47ec-b957-61669f861c9e"), "Díaz", "159357852", new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "isabelladiaz@example.com", "Isabella", "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5", 2, "852963741", false }
                });

            migrationBuilder.InsertData(
                table: "Platillo",
                columns: new[] { "IdPlatillo", "Activado", "Descripcion", "Precio" },
                values: new object[,]
                {
                    { 1, true, "Ravioli de ricotta y espinacas con salsa de tomate", 1000m },
                    { 2, true, "milanesa a la napolitana", 3000m },
                    { 3, true, "Ceviche de camarón y pescado", 2800m },
                    { 4, true, "Costillas de cerdo a la barbacoa con salsa ahumada", 357m },
                    { 5, true, "Paella mixta de mariscos y pollo", 1890m },
                    { 6, true, "Salmón con verduras salteadas y arroz jazmín", 100m },
                    { 7, true, "Lasaña de carne y verduras con capas de pasta", 1200m },
                    { 8, true, "Pechuga de pollo rellena de queso de cabra ", 1500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacionPedido_IdPedido",
                table: "AutorizacionPedido",
                column: "IdPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutorizacionPedido_IdPersonal",
                table: "AutorizacionPedido",
                column: "IdPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPlatillo_IdMenu",
                table: "MenuPlatillo",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPlatillo_IdPlatillo",
                table: "MenuPlatillo",
                column: "IdPlatillo");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_idPersonal",
                table: "Pagos",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdPersonal",
                table: "Pedido",
                column: "IdPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdRecibo",
                table: "Pedido",
                column: "IdRecibo");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPorMenuPlatillo_IdMenuPlatillo",
                table: "PedidoPorMenuPlatillo",
                column: "IdMenuPlatillo");

            migrationBuilder.CreateIndex(
                name: "IX_Recibo_IdDescuento",
                table: "Recibo",
                column: "IdDescuento");

            migrationBuilder.CreateIndex(
                name: "IX_Recibo_NumeroPago",
                table: "Recibo",
                column: "NumeroPago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorizacionPedido");

            migrationBuilder.DropTable(
                name: "PedidoPorMenuPlatillo");

            migrationBuilder.DropTable(
                name: "MenuPlatillo");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Platillo");

            migrationBuilder.DropTable(
                name: "Recibo");

            migrationBuilder.DropTable(
                name: "Descuento");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Personal");
        }
    }
}
