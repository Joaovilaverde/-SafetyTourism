using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourism.Models;
using Microsoft.EntityFrameworkCore;

namespace SafetyTourism.Data
{
    public class SafetyContext : DbContext
    {
        public SafetyContext(DbContextOptions<SafetyContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Doenca> Doencas { get; set; }
        public DbSet<AfectadoPor> Afectados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
            modelBuilder.Entity<Utilizador>().ToTable("Utilizador");
            modelBuilder.Entity<Destino>().ToTable("Destino");
            modelBuilder.Entity<Doenca>().ToTable("Doenca");
            modelBuilder.Entity<AfectadoPor>().ToTable("AfectadoPor");
        }
    }
}
