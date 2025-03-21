﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SyncStyle.DbContexts;

#nullable disable

namespace SyncStyle.Migrations
{
    [DbContext(typeof(StyleSyncContext))]
    partial class StyleSyncContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("contact_mechanism_id_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("style_sync_prod_id_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("user_id_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("SyncStyle.Model.ContactMechanism", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "contact_mechanism_id_hilo");

                    b.Property<string>("ContactMechanismInfo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ContactMechanismType")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ContactMechanism", (string)null);
                });

            modelBuilder.Entity("SyncStyle.Model.StyleSyncProd", b =>
                {
                    b.Property<int>("StyleSyncProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("StyleSyncProdId"), "style_sync_prod_id_hilo");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("varchar(160000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("StyleSyncProdId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("StyleSyncProd", (string)null);
                });

            modelBuilder.Entity("SyncStyle.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("UserId"), "user_id_hilo");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FailedLoginAttempts")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastFailedLoginAttempt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("SyncStyle.Model.ContactMechanism", b =>
                {
                    b.HasOne("SyncStyle.Model.User", null)
                        .WithMany("ContactMechanisms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SyncStyle.Model.StyleSyncProd", b =>
                {
                    b.HasOne("SyncStyle.Model.User", null)
                        .WithMany("StyleSyncProds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SyncStyle.Model.User", b =>
                {
                    b.Navigation("ContactMechanisms");

                    b.Navigation("StyleSyncProds");
                });
#pragma warning restore 612, 618
        }
    }
}
