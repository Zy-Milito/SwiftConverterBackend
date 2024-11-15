﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241114234716_AddedCurrencyStatus")]
    partial class AddedCurrencyStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("CurrencyUser", b =>
                {
                    b.Property<int>("FavedCurrenciesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FavedCurrenciesId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CurrencyUser");
                });

            modelBuilder.Entity("Data.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ExchangeRate")
                        .HasColumnType("REAL");

                    b.Property<string>("ISOCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExchangeRate = 1.0,
                            ISOCode = "USD",
                            Name = "United States Dollar",
                            Status = true,
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 2,
                            ExchangeRate = 1.1200000000000001,
                            ISOCode = "EUR",
                            Name = "Euro",
                            Status = true,
                            Symbol = "€"
                        },
                        new
                        {
                            Id = 3,
                            ExchangeRate = 0.0091000000000000004,
                            ISOCode = "JPY",
                            Name = "Japanese Yen",
                            Status = true,
                            Symbol = "¥"
                        },
                        new
                        {
                            Id = 4,
                            ExchangeRate = 1.25,
                            ISOCode = "GBP",
                            Name = "Pound Sterling",
                            Status = true,
                            Symbol = "£"
                        },
                        new
                        {
                            Id = 5,
                            ExchangeRate = 0.79000000000000004,
                            ISOCode = "CAD",
                            Name = "Canadian Dollar",
                            Status = true,
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 6,
                            ExchangeRate = 0.71999999999999997,
                            ISOCode = "AUD",
                            Name = "Australian Dollar",
                            Status = true,
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 7,
                            ExchangeRate = 0.012999999999999999,
                            ISOCode = "INR",
                            Name = "Indian Rupee",
                            Status = true,
                            Symbol = "₹"
                        },
                        new
                        {
                            Id = 8,
                            ExchangeRate = 0.001,
                            ISOCode = "ARS",
                            Name = "Argentine Peso",
                            Status = true,
                            Symbol = "$"
                        },
                        new
                        {
                            Id = 9,
                            ExchangeRate = 0.00084000000000000003,
                            ISOCode = "KRW",
                            Name = "South Korean Won",
                            Status = true,
                            Symbol = "₩"
                        },
                        new
                        {
                            Id = 10,
                            ExchangeRate = 0.14999999999999999,
                            ISOCode = "CNY",
                            Name = "Chinese Yuan",
                            Status = true,
                            Symbol = "¥"
                        });
                });

            modelBuilder.Entity("Data.Entities.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Data.Entities.SubscriptionPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxConversions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxConversions = 10,
                            Name = "Free",
                            Price = 0.0
                        },
                        new
                        {
                            Id = 2,
                            MaxConversions = 100,
                            Name = "Basic",
                            Price = 1.9099999999999999
                        },
                        new
                        {
                            Id = 3,
                            Name = "Pro",
                            Price = 5.1500000000000004
                        },
                        new
                        {
                            Id = 4,
                            MaxConversions = 100,
                            Name = "Trial",
                            Price = 0.0
                        });
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AccountStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SubscriptionPlanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionPlanId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountStatus = true,
                            Email = "admin@mail.com",
                            IsAdmin = true,
                            Password = "admin",
                            SubscriptionPlanId = 3,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            AccountStatus = true,
                            Email = "zyther@mail.com",
                            IsAdmin = false,
                            Password = "010101",
                            SubscriptionPlanId = 1,
                            Username = "Zyther"
                        },
                        new
                        {
                            Id = 3,
                            AccountStatus = true,
                            Email = "caleb@mail.com",
                            IsAdmin = false,
                            Password = "calebthepro",
                            SubscriptionPlanId = 2,
                            Username = "Caleb"
                        },
                        new
                        {
                            Id = 4,
                            AccountStatus = true,
                            Email = "lumion@mail.com",
                            IsAdmin = false,
                            Password = "master",
                            SubscriptionPlanId = 3,
                            Username = "Lumion"
                        });
                });

            modelBuilder.Entity("CurrencyUser", b =>
                {
                    b.HasOne("Data.Entities.Currency", null)
                        .WithMany()
                        .HasForeignKey("FavedCurrenciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.History", b =>
                {
                    b.HasOne("Data.Entities.Currency", "FromCurrency")
                        .WithMany()
                        .HasForeignKey("FromCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Currency", "ToCurrency")
                        .WithMany()
                        .HasForeignKey("ToCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("ConversionHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.HasOne("Data.Entities.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionPlan");
                });

            modelBuilder.Entity("Data.Entities.SubscriptionPlan", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Navigation("ConversionHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
