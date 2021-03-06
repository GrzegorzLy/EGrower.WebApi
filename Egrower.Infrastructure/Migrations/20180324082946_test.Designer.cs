﻿// <auto-generated />
using Egrower.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Egrower.Infrastructure.Migrations
{
    [DbContext(typeof(EGrowerContext))]
    [Migration("20180324082946_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EGrower.Core.Domain.Atachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedAt");

                    b.Property<byte[]>("Data");

                    b.Property<int>("EmailMessageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EmailMessageId");

                    b.ToTable("Atachments");
                });

            modelBuilder.Entity("EGrower.Core.Domain.EmailMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedAt");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("From");

                    b.Property<string>("Subject");

                    b.Property<string>("TextHTMLBody");

                    b.Property<string>("To");

                    b.HasKey("Id");

                    b.ToTable("EmailMessages");
                });

            modelBuilder.Entity("EGrower.Core.Domain.Atachment", b =>
                {
                    b.HasOne("EGrower.Core.Domain.EmailMessage", "EmailMessage")
                        .WithMany("Atachments")
                        .HasForeignKey("EmailMessageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
