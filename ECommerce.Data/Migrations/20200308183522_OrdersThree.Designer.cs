﻿// <auto-generated />
using System;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Data.Migrations
{
    [DbContext(typeof(CommerceContext))]
    [Migration("20200308183522_OrdersThree")]
    partial class OrdersThree
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECommerce.Data.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ECommerce.Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CardNumber");

                    b.Property<string>("City");

                    b.Property<string>("Expiration");

                    b.Property<bool?>("Filled");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("NameOnCard");

                    b.Property<string>("SecurityCode");

                    b.Property<int>("ShoppingCartId");

                    b.Property<string>("State");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ECommerce.Data.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("ImageFileName");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ECommerce.Data.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("ECommerce.Data.ShoppingCartProducts", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("ShoppingCartId");

                    b.Property<int>("Quantity");

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartsProducts");
                });

            modelBuilder.Entity("ECommerce.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("AuthorizedUsers");
                });

            modelBuilder.Entity("ECommerce.Data.Order", b =>
                {
                    b.HasOne("ECommerce.Data.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Data.Product", b =>
                {
                    b.HasOne("ECommerce.Data.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ECommerce.Data.ShoppingCartProducts", b =>
                {
                    b.HasOne("ECommerce.Data.Product", "Product")
                        .WithMany("ShoppingCartProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ECommerce.Data.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartProducts")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
