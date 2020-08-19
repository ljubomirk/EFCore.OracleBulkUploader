﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200730100631_AppDBUpdate")]
    partial class AppDBUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CouponDatabase.Models.AccessLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnName("ACTION")
                        .HasMaxLength(80);

                    b.Property<int>("ApplicationType")
                        .HasColumnName("APPLICATION_TYPE");

                    b.Property<string>("Channel")
                        .HasColumnName("CHANNEL")
                        .HasMaxLength(20);

                    b.Property<bool>("Granted")
                        .HasColumnName("GRANTED");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnName("ISSUED_DATE");

                    b.Property<string>("Username")
                        .HasColumnName("USERNAME")
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_ACCESS_LOG");

                    b.ToTable("ACCESS_LOG");
                });

            modelBuilder.Entity("CouponDatabase.Models.AwardChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_AWARD_CHANNEL");

                    b.ToTable("AWARD_CHANNEL");

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
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AquireFrom")
                        .HasColumnName("AQUIRE_FROM");

                    b.Property<DateTime?>("AquireTo")
                        .HasColumnName("AQUIRE_TO");

                    b.Property<DateTime?>("AwardFrom")
                        .HasColumnName("AWARD_FROM");

                    b.Property<DateTime?>("AwardTo")
                        .HasColumnName("AWARD_TO");

                    b.Property<string>("Code")
                        .HasColumnName("CODE")
                        .HasMaxLength(40);

                    b.Property<int>("CouponSeries")
                        .HasColumnName("COUPON_SERIES");

                    b.Property<bool>("Enabled")
                        .HasColumnName("ENABLED");

                    b.Property<string>("Holder")
                        .HasColumnName("HOLDER")
                        .HasMaxLength(20);

                    b.Property<int>("MaxRedeemNo")
                        .HasColumnName("MAX_REDEEM_NO");

                    b.Property<long>("PromotionId")
                        .HasColumnName("PROMOTION_ID");

                    b.Property<int>("Status")
                        .HasColumnName("STATUS");

                    b.Property<string>("User")
                        .HasColumnName("USER")
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_COUPON");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[CODE] IS NOT NULL");

                    b.HasIndex("PromotionId")
                        .HasName("IX_COUPON_PROMOTION_ID");

                    b.ToTable("COUPON");
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponAwardChannel", b =>
                {
                    b.Property<long>("CouponId")
                        .HasColumnName("COUPON_ID");

                    b.Property<long>("AwardChannelId")
                        .HasColumnName("AWARD_CHANNEL_ID");

                    b.HasKey("CouponId", "AwardChannelId");

                    b.HasIndex("AwardChannelId")
                        .HasName("IX_COUPON_AWARD_CHANNEL_AWARD_CHANNEL_ID");

                    b.ToTable("COUPON_AWARD_CHANNEL");
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnName("ACTION");

                    b.Property<long>("CouponId")
                        .HasColumnName("COUPON_ID");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnName("ISSUED_DATE");

                    b.Property<int>("Status")
                        .HasColumnName("STATUS");

                    b.Property<string>("User")
                        .HasColumnName("USER");

                    b.HasKey("Id")
                        .HasName("PK_COUPON_HISTORY");

                    b.HasIndex("CouponId")
                        .HasName("IX_COUPON_HISTORY_COUPON_ID");

                    b.ToTable("COUPON_HISTORY");
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponIssuerChannel", b =>
                {
                    b.Property<long>("CouponId")
                        .HasColumnName("COUPON_ID");

                    b.Property<long>("IssuerChannelId")
                        .HasColumnName("ISSUER_CHANNEL_ID");

                    b.HasKey("CouponId", "IssuerChannelId");

                    b.HasIndex("IssuerChannelId")
                        .HasName("IX_COUPON_ISSUER_CHANNEL_ISSUER_CHANNEL_ID");

                    b.ToTable("COUPON_ISSUER_CHANNEL");
                });

            modelBuilder.Entity("CouponDatabase.Models.IssuerChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_ISSUER_CHANNEL");

                    b.ToTable("ISSUER_CHANNEL");

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

            modelBuilder.Entity("CouponDatabase.Models.NotifyList", b =>
                {
                    b.Property<long>("SystemId")
                        .HasColumnName("SYSTEM_ID");

                    b.Property<long>("ChannelId")
                        .HasColumnName("CHANNEL_ID");

                    b.Property<string>("Url")
                        .HasColumnName("URL");

                    b.HasKey("SystemId", "ChannelId");

                    b.HasIndex("ChannelId")
                        .HasName("IX_NOTIFY_LIST_CHANNEL_ID");

                    b.ToTable("NOTIFY_LIST");
                });

            modelBuilder.Entity("CouponDatabase.Models.Promotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("CODE")
                        .HasMaxLength(40);

                    b.Property<int>("CouponSeries")
                        .HasColumnName("COUPON_SERIES");

                    b.Property<bool>("Enabled")
                        .HasColumnName("ENABLED");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("NAME")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnName("VALID_FROM");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnName("VALID_TO");

                    b.HasKey("Id")
                        .HasName("PK_PROMOTION");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("PROMOTION");
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionAwardChannel", b =>
                {
                    b.Property<long>("PromotionId")
                        .HasColumnName("PROMOTION_ID");

                    b.Property<long>("AwardChannelId")
                        .HasColumnName("AWARD_CHANNEL_ID");

                    b.HasKey("PromotionId", "AwardChannelId");

                    b.HasIndex("AwardChannelId")
                        .HasName("IX_PROMOTION_AWARD_CHANNEL_AWARD_CHANNEL_ID");

                    b.ToTable("PROMOTION_AWARD_CHANNEL");
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionIssuerChannel", b =>
                {
                    b.Property<long>("PromotionId")
                        .HasColumnName("PROMOTION_ID");

                    b.Property<long>("IssuerChannelId")
                        .HasColumnName("ISSUER_CHANNEL_ID");

                    b.HasKey("PromotionId", "IssuerChannelId");

                    b.HasIndex("IssuerChannelId")
                        .HasName("IX_PROMOTION_ISSUER_CHANNEL_ISSUER_CHANNEL_ID");

                    b.ToTable("PROMOTION_ISSUER_CHANNEL");
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionProperty", b =>
                {
                    b.Property<long>("PromotionId")
                        .HasColumnName("PROMOTION_ID");

                    b.Property<long>("PropertyId")
                        .HasColumnName("PROPERTY_ID");

                    b.HasKey("PromotionId", "PropertyId");

                    b.HasIndex("PropertyId")
                        .HasName("IX_PROMOTION_PROPERTY_PROPERTY_ID");

                    b.ToTable("PROMOTION_PROPERTY");
                });

            modelBuilder.Entity("CouponDatabase.Models.Property", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(20);

                    b.HasKey("Id")
                        .HasName("PK_PROPERTY");

                    b.ToTable("PROPERTY");

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
                            Name = "AllowMultipleRedeems"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "AllowCouponSeries"
                        });
                });

            modelBuilder.Entity("CouponDatabase.Models.System", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .HasColumnName("LOGIN")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .HasColumnName("NAME")
                        .HasMaxLength(20);

                    b.Property<string>("PwdHash")
                        .HasColumnName("PWD_HASH")
                        .HasMaxLength(200);

                    b.HasKey("Id")
                        .HasName("PK_SYSTEM");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[NAME] IS NOT NULL");

                    b.ToTable("SYSTEM");
                });

            modelBuilder.Entity("CouponDatabase.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("USERNAME")
                        .HasMaxLength(20);

                    b.Property<int>("AccessType")
                        .HasColumnName("ACCESS_TYPE");

                    b.Property<string>("Domain")
                        .HasColumnName("DOMAIN")
                        .HasMaxLength(20);

                    b.Property<string>("Fullname")
                        .HasColumnName("FULLNAME")
                        .HasMaxLength(80);

                    b.HasKey("Username");

                    b.ToTable("APPL_USER");
                });

            modelBuilder.Entity("CouponDatabase.Models.Coupon", b =>
                {
                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("Coupons")
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_COUPON_PROMOTION_PROMOTION_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponAwardChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.AwardChannel", "AwardChannel")
                        .WithMany()
                        .HasForeignKey("AwardChannelId")
                        .HasConstraintName("FK_COUPON_AWARD_CHANNEL_AWARD_CHANNEL_AWARD_CHANNEL_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Coupon", "Coupon")
                        .WithMany()
                        .HasForeignKey("CouponId")
                        .HasConstraintName("FK_COUPON_AWARD_CHANNEL_COUPON_COUPON_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponHistory", b =>
                {
                    b.HasOne("CouponDatabase.Models.Coupon", "Coupon")
                        .WithMany("CouponHistories")
                        .HasForeignKey("CouponId")
                        .HasConstraintName("FK_COUPON_HISTORY_COUPON_COUPON_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.CouponIssuerChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.Coupon", "Coupon")
                        .WithMany()
                        .HasForeignKey("CouponId")
                        .HasConstraintName("FK_COUPON_ISSUER_CHANNEL_COUPON_COUPON_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.IssuerChannel", "IssuerChannel")
                        .WithMany()
                        .HasForeignKey("IssuerChannelId")
                        .HasConstraintName("FK_COUPON_ISSUER_CHANNEL_ISSUER_CHANNEL_ISSUER_CHANNEL_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.NotifyList", b =>
                {
                    b.HasOne("CouponDatabase.Models.IssuerChannel", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .HasConstraintName("FK_NOTIFY_LIST_ISSUER_CHANNEL_CHANNEL_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.System", "System")
                        .WithMany()
                        .HasForeignKey("SystemId")
                        .HasConstraintName("FK_NOTIFY_LIST_SYSTEM_SYSTEM_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionAwardChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.AwardChannel", "AwardChannel")
                        .WithMany()
                        .HasForeignKey("AwardChannelId")
                        .HasConstraintName("FK_PROMOTION_AWARD_CHANNEL_AWARD_CHANNEL_AWARD_CHANNEL_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionAwardChannels")
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_PROMOTION_AWARD_CHANNEL_PROMOTION_PROMOTION_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionIssuerChannel", b =>
                {
                    b.HasOne("CouponDatabase.Models.IssuerChannel", "IssuerChannel")
                        .WithMany()
                        .HasForeignKey("IssuerChannelId")
                        .HasConstraintName("FK_PROMOTION_ISSUER_CHANNEL_ISSUER_CHANNEL_ISSUER_CHANNEL_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionIssuerChannels")
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_PROMOTION_ISSUER_CHANNEL_PROMOTION_PROMOTION_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CouponDatabase.Models.PromotionProperty", b =>
                {
                    b.HasOne("CouponDatabase.Models.Promotion", "Promotion")
                        .WithMany("PromotionProperties")
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_PROMOTION_PROPERTY_PROMOTION_PROMOTION_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CouponDatabase.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .HasConstraintName("FK_PROMOTION_PROPERTY_PROPERTY_PROPERTY_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
