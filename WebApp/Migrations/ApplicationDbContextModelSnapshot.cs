﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CouponDatabase.Models.AwardChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AwardChannel");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "POS"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "SelfCare"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Telesales"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Webshop"
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AquireFrom");

                    b.Property<DateTime>("AquireTo");

                    b.Property<DateTime>("AwardFrom");

                    b.Property<DateTime>("AwardTo");

                    b.Property<string>("Code");

                    b.Property<int>("CouponSeries");

                    b.Property<string>("Holder");

                    b.Property<long>("PromotionId");

                    b.Property<int>("Status");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("PromotionId");

                    b.ToTable("Coupon");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            AquireFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AquireTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "EASTER1234567892",
                            CouponSeries = 0,
                            Holder = "38640440481",
                            PromotionId = 1L,
                            Status = 4,
                            User = "38640440481"
                        },
                        new
                        {
                            Id = 3L,
                            AquireFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AquireTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "EASTER1234567893",
                            CouponSeries = 0,
                            Holder = "38640440482",
                            PromotionId = 1L,
                            Status = 2,
                            User = "38640440482"
                        },
                        new
                        {
                            Id = 4L,
                            AquireFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AquireTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "EASTER1234567894",
                            CouponSeries = 0,
                            Holder = "38640440483",
                            PromotionId = 1L,
                            Status = 3,
                            User = "38640440483"
                        },
                        new
                        {
                            Id = 1L,
                            AquireFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AquireTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardFrom = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AwardTo = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "EASTER1234567891",
                            CouponSeries = 0,
                            Holder = "",
                            PromotionId = 1L,
                            Status = 1,
                            User = ""
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponHistory", b =>
                {
                    b.Property<long>("CouponHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CouponId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("CouponHistoryId");

                    b.HasIndex("CouponId");

                    b.ToTable("CouponHistory");
                });

            modelBuilder.Entity("CouponDatabase.Models.IssuerChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("IssuerChannel");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "POS"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Salesforce"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "SelfCare"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Telesales"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Webshop"
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.Promotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Promotion");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Code = "Spring",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            Code = "Easter",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            Code = "Summer",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4L,
                            Code = "Spring2",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5L,
                            Code = "Easter2",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6L,
                            Code = "Summer2",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7L,
                            Code = "Spring3",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8L,
                            Code = "Easter3",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9L,
                            Code = "Summer3",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10L,
                            Code = "Spring4",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 11L,
                            Code = "Easter4",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 12L,
                            Code = "Summer4",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 13L,
                            Code = "Spring5",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 14L,
                            Code = "Easter5",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 15L,
                            Code = "Summer5",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 16L,
                            Code = "Spring6",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 17L,
                            Code = "Easter6",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 18L,
                            Code = "Summer6",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 19L,
                            Code = "Spring7",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 20L,
                            Code = "Easter7",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 21L,
                            Code = "Summer7",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 22L,
                            Code = "Spring8",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 23L,
                            Code = "Easter8",
                            Enabled = false,
                            ValidFrom = new DateTime(2020, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 24L,
                            Code = "Summer8",
                            Enabled = true,
                            ValidFrom = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValidTo = new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionAwardChannel", b =>
                {
                    b.Property<long>("PromotionId");

                    b.Property<long>("AwardChannelId");

                    b.HasKey("PromotionId", "AwardChannelId");

                    b.HasIndex("AwardChannelId");

                    b.ToTable("PromotionAwardChannel");

                    b.HasData(
                        new
                        {
                            PromotionId = 1L,
                            AwardChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 1L,
                            AwardChannelId = 3L
                        },
                        new
                        {
                            PromotionId = 1L,
                            AwardChannelId = 5L
                        },
                        new
                        {
                            PromotionId = 2L,
                            AwardChannelId = 3L
                        },
                        new
                        {
                            PromotionId = 2L,
                            AwardChannelId = 4L
                        },
                        new
                        {
                            PromotionId = 2L,
                            AwardChannelId = 5L
                        },
                        new
                        {
                            PromotionId = 11L,
                            AwardChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 3L,
                            AwardChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 3L,
                            AwardChannelId = 4L
                        },
                        new
                        {
                            PromotionId = 3L,
                            AwardChannelId = 3L
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionIssuerChannel", b =>
                {
                    b.Property<long>("PromotionId");

                    b.Property<long>("IssuerChannelId");

                    b.HasKey("PromotionId", "IssuerChannelId");

                    b.HasIndex("IssuerChannelId");

                    b.ToTable("PromotionIssuerChannel");

                    b.HasData(
                        new
                        {
                            PromotionId = 1L,
                            IssuerChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 1L,
                            IssuerChannelId = 2L
                        },
                        new
                        {
                            PromotionId = 1L,
                            IssuerChannelId = 5L
                        },
                        new
                        {
                            PromotionId = 2L,
                            IssuerChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 2L,
                            IssuerChannelId = 4L
                        },
                        new
                        {
                            PromotionId = 2L,
                            IssuerChannelId = 5L
                        },
                        new
                        {
                            PromotionId = 11L,
                            IssuerChannelId = 2L
                        },
                        new
                        {
                            PromotionId = 3L,
                            IssuerChannelId = 1L
                        },
                        new
                        {
                            PromotionId = 3L,
                            IssuerChannelId = 4L
                        },
                        new
                        {
                            PromotionId = 3L,
                            IssuerChannelId = 5L
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionProperty", b =>
                {
                    b.Property<long>("PromotionId");

                    b.Property<long>("PropertyId");

                    b.HasKey("PromotionId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PromotionProperty");

                    b.HasData(
                        new
                        {
                            PromotionId = 1L,
                            PropertyId = 2L
                        },
                        new
                        {
                            PromotionId = 1L,
                            PropertyId = 4L
                        },
                        new
                        {
                            PromotionId = 2L,
                            PropertyId = 6L
                        },
                        new
                        {
                            PromotionId = 2L,
                            PropertyId = 1L
                        },
                        new
                        {
                            PromotionId = 7L,
                            PropertyId = 2L
                        },
                        new
                        {
                            PromotionId = 5L,
                            PropertyId = 4L
                        },
                        new
                        {
                            PromotionId = 3L,
                            PropertyId = 6L
                        },
                        new
                        {
                            PromotionId = 11L,
                            PropertyId = 1L
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.Property", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Property");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "UniqueCoupons"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "NamedHolders"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "NamedConsumers"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "HolderIsOnlyConsumer"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "AlloweMultipleRedeems"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "AllowCouponSeries"
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.Coupon", b =>
                {
                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("Coupons")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponHistory", b =>
                {
                    b.HasOne("CouponDatabase.Models.Coupon", "Coupon")
                        .WithMany("CouponHistories")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionAwardChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.AwardChannel", "AwardChannel")
                        .WithMany()
                        .HasForeignKey("AwardChannelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionAwardChannels")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionIssuerChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.IssuerChannel", "IssuerChannel")
                        .WithMany()
                        .HasForeignKey("IssuerChannelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionIssuerChannels")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionProperty", b =>
                {
                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionProperties")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
