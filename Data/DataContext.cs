using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RpgApi.Controllers;
using RpgApi.Models;
using RpgApi.Models.Enuns;

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

        //oveerride --> sobreescrevendo algo que ja existe
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS"); 
        
           modelBuilder.Entity<Personagem>().HasData
           (
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Felipe", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Barbaro },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Barbaro },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
           );
           
            modelBuilder.Entity<Arma>(). ToTable("TB_ARMAS");
            
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
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(200);
        }

        public DbSet<Arma> Armas {get;set;}
        public object Arma { get; internal set; }
    }
}