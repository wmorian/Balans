﻿// <auto-generated />
using Balans.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Balans.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20190327195731_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("Balans.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Balans.Models.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<float>("Amount");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("Balans.Models.Entity", b =>
                {
                    b.HasOne("Balans.Models.Account")
                        .WithMany("Entities")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
