﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Signalr.Models;

#nullable disable

namespace Signalr.Data
{
    public partial class MagazynContext : DbContext
    {
        public MagazynContext()
        {
        }

        public MagazynContext(DbContextOptions<MagazynContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Zamowienia> Zamowienia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Zamowienia>(entity =>
            {
                entity.HasKey(e => e.Za_Id);

                entity.Property(e => e.Za_Nazwa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Za_Nr_Zamowienia)
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Za_Odbiorca)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}