﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taller1_WebMovil.Src.Data;

#nullable disable

namespace Taller1_WebMovil.Src.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241003193655_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Gender", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("categoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("stock")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Purchase", b =>
                {
                    b.Property<int>("productId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("purchaseReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("totalPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("productId", "userId", "purchaseReceiptId");

                    b.HasIndex("purchaseReceiptId");

                    b.HasIndex("userId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.PurchaseReceipt", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("district")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("purchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("totalPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("PurchaseReceipts");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Role", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("birthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("enable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("genderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("roleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("rut")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("genderId");

                    b.HasIndex("roleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Product", b =>
                {
                    b.HasOne("Taller1_WebMovil.Src.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.Purchase", b =>
                {
                    b.HasOne("Taller1_WebMovil.Src.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taller1_WebMovil.Src.Models.PurchaseReceipt", "purchaseReceipt")
                        .WithMany()
                        .HasForeignKey("purchaseReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taller1_WebMovil.Src.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("purchaseReceipt");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Taller1_WebMovil.Src.Models.User", b =>
                {
                    b.HasOne("Taller1_WebMovil.Src.Models.Gender", "gender")
                        .WithMany()
                        .HasForeignKey("genderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taller1_WebMovil.Src.Models.Role", "role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("gender");

                    b.Navigation("role");
                });
#pragma warning restore 612, 618
        }
    }
}
