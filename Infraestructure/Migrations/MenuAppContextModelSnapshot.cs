﻿// <auto-generated />
using System;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestructure.Migrations
{
    [DbContext(typeof(MenuAppContext))]
    partial class MenuAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.Authorization", b =>
                {
                    b.Property<string>("IdUser")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("IdOrder")
                        .HasColumnType("char(36)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdUser", "IdOrder");

                    b.HasIndex("IdOrder")
                        .IsUnique();

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdDiscount");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            IdDiscount = new Guid("270e2cbf-b6bb-4a7f-854a-ef67b8447888"),
                            Percentage = 50m,
                            StartDate = new DateTime(2024, 4, 19, 14, 25, 19, 935, DateTimeKind.Local).AddTicks(8915)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Dish", b =>
                {
                    b.Property<int>("IdDish")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Activated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDish");

                    b.ToTable("Dishes");

                    b.HasData(
                        new
                        {
                            IdDish = 1,
                            Activated = true,
                            Description = "Ravioli de ricotta y espinacas con salsa de tomate",
                            Price = 1000m
                        },
                        new
                        {
                            IdDish = 2,
                            Activated = true,
                            Description = "milanesa a la napolitana",
                            Price = 3000m
                        },
                        new
                        {
                            IdDish = 3,
                            Activated = true,
                            Description = "Ceviche de camarón y pescado",
                            Price = 2800m
                        },
                        new
                        {
                            IdDish = 4,
                            Activated = true,
                            Description = "Costillas de cerdo a la barbacoa con salsa ahumada",
                            Price = 357m
                        },
                        new
                        {
                            IdDish = 5,
                            Activated = true,
                            Description = "Paella mixta de mariscos y pollo",
                            Price = 1890m
                        },
                        new
                        {
                            IdDish = 6,
                            Activated = true,
                            Description = "Salmón con verduras salteadas y arroz jazmín",
                            Price = 100m
                        },
                        new
                        {
                            IdDish = 7,
                            Activated = true,
                            Description = "Lasaña de carne y verduras con capas de pasta",
                            Price = 1200m
                        },
                        new
                        {
                            IdDish = 8,
                            Activated = true,
                            Description = "Pechuga de pollo rellena de queso de cabra ",
                            Price = 1500m
                        });
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Property<Guid>("IdMenu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CloseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EatingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdMenu");

                    b.ToTable("Menues");
                });

            modelBuilder.Entity("Domain.Entities.MenuOption", b =>
                {
                    b.Property<Guid>("IdMenu")
                        .HasColumnType("char(36)");

                    b.Property<int>("IdDish")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Requested")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("IdMenu", "IdDish");

                    b.HasIndex("IdDish");

                    b.ToTable("MenuOptions");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StateCode")
                        .HasColumnType("int");

                    b.HasKey("IdOrder");

                    b.HasIndex("IdUser");

                    b.HasIndex("StateCode");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("IdOrder")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdMenu")
                        .HasColumnType("char(36)");

                    b.Property<int>("IdDish")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("IdOrder", "IdMenu", "IdDish");

                    b.HasIndex("IdMenu", "IdDish");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Receipt", b =>
                {
                    b.Property<Guid>("IdReceipt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdDiscount")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdOrder")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdReceipt");

                    b.HasIndex("IdDiscount");

                    b.HasIndex("IdOrder")
                        .IsUnique();

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.Property<int>("StateCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("StateCode");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Domain.Entities.Transition", b =>
                {
                    b.Property<Guid>("IdOrder")
                        .HasColumnType("char(36)");

                    b.Property<int>("InitialStateCode")
                        .HasColumnType("int");

                    b.Property<int>("FinalStateCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdOrder", "InitialStateCode", "FinalStateCode");

                    b.HasIndex("FinalStateCode");

                    b.HasIndex("InitialStateCode");

                    b.ToTable("Transitions");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("IdUser")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Privilege")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Authorization", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithOne("Authorization")
                        .HasForeignKey("Domain.Entities.Authorization", "IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.MenuOption", b =>
                {
                    b.HasOne("Domain.Entities.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("IdDish")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Menu", "Menu")
                        .WithMany("Options")
                        .HasForeignKey("IdMenu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.State", "State")
                        .WithMany("Orders")
                        .HasForeignKey("StateCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.MenuOption", "MenuOption")
                        .WithMany("OrderItems")
                        .HasForeignKey("IdMenu", "IdDish")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuOption");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.Receipt", b =>
                {
                    b.HasOne("Domain.Entities.Discount", "Discount")
                        .WithMany("Receipts")
                        .HasForeignKey("IdDiscount")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithOne("Receipt")
                        .HasForeignKey("Domain.Entities.Receipt", "IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.Transition", b =>
                {
                    b.HasOne("Domain.Entities.State", "FinalSate")
                        .WithMany("FinalTransitions")
                        .HasForeignKey("FinalStateCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("Transitions")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.State", "InitialState")
                        .WithMany("InitialTransitions")
                        .HasForeignKey("InitialStateCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FinalSate");

                    b.Navigation("InitialState");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.Discount", b =>
                {
                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Domain.Entities.MenuOption", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("Authorization");

                    b.Navigation("Items");

                    b.Navigation("Receipt");

                    b.Navigation("Transitions");
                });

            modelBuilder.Entity("Domain.Entities.State", b =>
                {
                    b.Navigation("FinalTransitions");

                    b.Navigation("InitialTransitions");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
