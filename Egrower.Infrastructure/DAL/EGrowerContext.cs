﻿using Egrower.Infrastructure.Factories;
using EGrower.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egrower.Infrastructure.DAL
{
    public class EGrowerContext : DbContext
    {
        public EGrowerContext(DbContextOptions<EGrowerContext> options)
            : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailMessage>()
                .HasMany(em => em.Atachments)
                .WithOne(a => a.EmailMessage)
                .HasForeignKey(a => a.EmailMessageId);
               
        }

        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<Atachment> Atachments{ get; set; }
    }
}
