﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Petscao.Data;

#nullable disable

namespace Petscao.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("Petscao.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CEP")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressId");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("Petscao.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Breed")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("AnimalId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Petscao.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.HasIndex("AddressId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Petscao.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeId");

                    b.HasIndex("AddressId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Petscao.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Petscao.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Petscao.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("REAL");

                    b.HasKey("SaleId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Petscao.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Petscao.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<string>("CorporateReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FantasyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("SupplierId");

                    b.HasIndex("AddressId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Petscao.Models.Timeline", b =>
                {
                    b.Property<int>("TimelineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("ServiceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TimelineId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Timeline");
                });

            modelBuilder.Entity("Petscao.Models.Animal", b =>
                {
                    b.HasOne("Petscao.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Petscao.Models.Customer", b =>
                {
                    b.HasOne("Petscao.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Petscao.Models.Employee", b =>
                {
                    b.HasOne("Petscao.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Petscao.Models.Product", b =>
                {
                    b.HasOne("Petscao.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Petscao.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Petscao.Models.Sale", b =>
                {
                    b.HasOne("Petscao.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Petscao.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Petscao.Models.Supplier", b =>
                {
                    b.HasOne("Petscao.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Petscao.Models.Timeline", b =>
                {
                    b.HasOne("Petscao.Models.Animal", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Petscao.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Petscao.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Customer");

                    b.Navigation("Service");
                });
#pragma warning restore 612, 618
        }
    }
}
