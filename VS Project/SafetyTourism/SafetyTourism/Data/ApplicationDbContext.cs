using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafetyTourism.Models;

namespace SafetyTourism.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SafetyTourism.Models.Destino> Destinos { get; set; }
        public DbSet<SafetyTourism.Models.Doenca> Doencas { get; set; }
        public DbSet<SafetyTourism.Models.Funcionario> Funcionarios { get; set; }
        public DbSet<SafetyTourism.Models.Surto> Surtos { get; set; }
        public DbSet<SafetyTourism.Models.Utilizador> Utilizadores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
            modelBuilder.Entity<Utilizador>().ToTable("Utilizador");
            modelBuilder.Entity<Destino>().ToTable("Destino");
            modelBuilder.Entity<Doenca>().ToTable("Doenca");
            modelBuilder.Entity<Surto>().ToTable("Surto");
        }
    }
}
