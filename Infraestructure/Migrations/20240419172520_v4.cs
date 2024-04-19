using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    IdDiscount = table.Column<Guid>(type: "char(36)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.IdDiscount);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    IdDish = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Activated = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.IdDish);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Menues",
                columns: table => new
                {
                    IdMenu = table.Column<Guid>(type: "char(36)", nullable: false),
                    EatingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menues", x => x.IdMenu);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateCode);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    NickName = table.Column<string>(type: "longtext", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Privilege = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuOptions",
                columns: table => new
                {
                    IdMenu = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdDish = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Requested = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuOptions", x => new { x.IdMenu, x.IdDish });
                    table.ForeignKey(
                        name: "FK_MenuOptions_Dishes_IdDish",
                        column: x => x.IdDish,
                        principalTable: "Dishes",
                        principalColumn: "IdDish",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuOptions_Menues_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "Menues",
                        principalColumn: "IdMenu",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StateCode = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.IdOrder);
                    table.ForeignKey(
                        name: "FK_Orders_States_StateCode",
                        column: x => x.StateCode,
                        principalTable: "States",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdUser = table.Column<string>(type: "varchar(255)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => new { x.IdUser, x.IdOrder });
                    table.ForeignKey(
                        name: "FK_Authorizations_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorizations_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdMenu = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdDish = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.IdOrder, x.IdMenu, x.IdDish });
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuOptions_IdMenu_IdDish",
                        columns: x => new { x.IdMenu, x.IdDish },
                        principalTable: "MenuOptions",
                        principalColumns: new[] { "IdMenu", "IdDish" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    IdReceipt = table.Column<Guid>(type: "char(36)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdDiscount = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.IdReceipt);
                    table.ForeignKey(
                        name: "FK_Receipts_Discounts_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "Discounts",
                        principalColumn: "IdDiscount",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transitions",
                columns: table => new
                {
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false),
                    InitialStateCode = table.Column<int>(type: "int", nullable: false),
                    FinalStateCode = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transitions", x => new { x.IdOrder, x.InitialStateCode, x.FinalStateCode });
                    table.ForeignKey(
                        name: "FK_Transitions_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "IdOrder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transitions_States_FinalStateCode",
                        column: x => x.FinalStateCode,
                        principalTable: "States",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transitions_States_InitialStateCode",
                        column: x => x.InitialStateCode,
                        principalTable: "States",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "Percentage", "StartDate" },
                values: new object[] { new Guid("270e2cbf-b6bb-4a7f-854a-ef67b8447888"), 50m, new DateTime(2024, 4, 19, 14, 25, 19, 935, DateTimeKind.Local).AddTicks(8915) });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "IdDish", "Activated", "Description", "Price" },
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
                name: "IX_Authorizations_IdOrder",
                table: "Authorizations",
                column: "IdOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuOptions_IdDish",
                table: "MenuOptions",
                column: "IdDish");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IdMenu_IdDish",
                table: "OrderItems",
                columns: new[] { "IdMenu", "IdDish" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdUser",
                table: "Orders",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StateCode",
                table: "Orders",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_IdDiscount",
                table: "Receipts",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_IdOrder",
                table: "Receipts",
                column: "IdOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transitions_FinalStateCode",
                table: "Transitions",
                column: "FinalStateCode");

            migrationBuilder.CreateIndex(
                name: "IX_Transitions_InitialStateCode",
                table: "Transitions",
                column: "InitialStateCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Transitions");

            migrationBuilder.DropTable(
                name: "MenuOptions");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Menues");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
