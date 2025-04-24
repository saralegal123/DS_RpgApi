using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RpgApi.Controllers;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using RpgApi.Utils;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        //ctor + enter --> atalho pra criação do construtor
        // : --> significa herança, ent estou herdando algo de outra classe
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        public DbSet<Personagem> TB_PERSONAGENS { get; set; }
        public DbSet<Arma> TB_ARMAS { get;set; }
        public DbSet<Usuario> TB_USUARIOS { get; set; }

        //oveerride --> sobreescrevendo algo que ja existe
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS"); 
           modelBuilder.Entity<Arma>(). ToTable("TB_ARMAS");
           modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");

           modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Personagens)
            .WithOne(e => e.Usuario)
            .HasForeignKey(e => e.UsuarioId)
            .IsRequired(false);

           modelBuilder.Entity<Personagem>().HasData
           (
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1},
            new Personagem() { Id = 2, Nome = "Felipe", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Barbaro, UsuarioId = 1},
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago, UsuarioId = 1},
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro, UsuarioId = 1 },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Barbaro, UsuarioId = 1},
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago, UsuarioId = 1}
           );
           
            
            modelBuilder.Entity<Arma>().HasData
           (
            new Arma() { Id = 1, Nome = "Espada", Dano=17, Classe=ArmaEnum.Espada},
            new Arma() { Id = 2, Nome = "Adaga", Dano=15, Classe=ArmaEnum.Adaga},
            new Arma() { Id = 3, Nome = "Besta", Dano=18, Classe=ArmaEnum.Besta },
            new Arma() { Id = 4, Nome = "Mangual", Dano=18,  Classe=ArmaEnum.Mangual },
            new Arma() { Id = 5, Nome = "Cajado", Dano=20, Classe=ArmaEnum.Cajado },
            new Arma() { Id = 6, Nome = "Pistola", Dano=21,  Classe=ArmaEnum.Pistola },
            new Arma() { Id = 7, Nome = "Fêmur", Dano=25,  Classe=ArmaEnum.Femur }
           );

           //criação de um usuario padrao
           Usuario user = new Usuario();
           Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[]salt);
           user.Id = 1;
           user.Username = "UsuarioAdmin"; 
           user.PasswordString = string.Empty;
           user.PasswordHash = hash;
           user.PasswordSalt = salt;
           user.Perfil = "Admin";
           user.Email = "seuEmail@gmail.com";
           user.Latitude = -23.5200241;
           user.Longitude = -46.596498;

           modelBuilder.Entity<Usuario>().HasData(user);
           //fim da criação do usuario padrao

           //define qeu se o perfil nao form informado, o valor padrao será jogador
           modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                    .HaveColumnType("varchar").HaveMaxLength(200);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => 
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
       
       
    }
}