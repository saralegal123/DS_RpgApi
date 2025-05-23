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
        public DbSet<Arma> TB_ARMAS { get; set; }
        public DbSet<Usuario> TB_USUARIOS { get; set; }
        public DbSet<Habilidade> TB_HABILIDADES { get; set; }
        public DbSet<PersonagemHabilidade> TB_PERSONAGENS_HABILIDADES { get; set; }
        public DbSet<Disputa> TB_DISPUTAS { get; set;}

        //oveerride --> sobreescrevendo algo que ja existe
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS"); 
           modelBuilder.Entity<Arma>(). ToTable("TB_ARMAS");
           modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");
           modelBuilder.Entity<Habilidade>().ToTable("TB_HABILIDADES");
           modelBuilder.Entity<PersonagemHabilidade>().ToTable("TB_PERSONAGENS_HABILIDADES");
           modelBuilder.Entity<Disputa>().ToTable("TB_DISPUTAS");


           modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Personagens)
            .WithOne(e => e.Usuario)
            .HasForeignKey(e => e.UsuarioId)
            .IsRequired(false);

            //Relacionamento One to One (um pra um)
            modelBuilder.Entity<Personagem>()
            .HasOne(e => e.Arma)
            .WithOne(e => e.Personagem)
            .HasForeignKey<Arma>(e => e.PersonagemId)
            .IsRequired();

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
            new Arma() { Id = 1, Nome = "Espada", Dano=17, /*Classe=ArmaEnum.Espada,*/ PersonagemId = 1},
            new Arma() { Id = 2, Nome = "Adaga", Dano=15, /*Classe=ArmaEnum.Adaga,*/ PersonagemId = 2},
            new Arma() { Id = 3, Nome = "Besta", Dano=18, /*Classe=ArmaEnum.Besta ,*/ PersonagemId = 3},
            new Arma() { Id = 4, Nome = "Mangual", Dano=18,  /*Classe=ArmaEnum.Mangual,*/ PersonagemId = 4},
            new Arma() { Id = 5, Nome = "Cajado", Dano=20, /*Classe=ArmaEnum.Cajado,*/ PersonagemId = 5},
            new Arma() { Id = 6, Nome = "Pistola", Dano=21,  /*Classe=ArmaEnum.Pistola,*/ PersonagemId = 6},
            new Arma() { Id = 7, Nome = "Fêmur", Dano=25,  /*Classe=ArmaEnum.Femur,*/ PersonagemId = 7}
           );

            //Habilidades de Personagens 
            modelBuilder.Entity<PersonagemHabilidade>()
                .HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId});

            modelBuilder.Entity<Habilidade>().HasData
            (
                new Habilidade(){Id = 1, Nome = "Adormecer", Dano = 39},
                new Habilidade(){Id = 2, Nome = "Congelar", Dano = 41},
                new Habilidade(){Id = 3, Nome = "Hipnotizar", Dano = 37}
            );

            modelBuilder.Entity<PersonagemHabilidade>().HasData
            (
                new PersonagemHabilidade() {PersonagemId = 1, HabilidadeId = 1},
                new PersonagemHabilidade() {PersonagemId = 1, HabilidadeId = 2},
                new PersonagemHabilidade() {PersonagemId = 2, HabilidadeId = 2},
                new PersonagemHabilidade() {PersonagemId = 3, HabilidadeId = 2},
                new PersonagemHabilidade() {PersonagemId = 3, HabilidadeId = 3},  
                new PersonagemHabilidade() {PersonagemId = 4, HabilidadeId = 3},
                new PersonagemHabilidade() {PersonagemId = 5, HabilidadeId = 1},
                new PersonagemHabilidade() {PersonagemId = 6, HabilidadeId = 2},
                new PersonagemHabilidade() {PersonagemId = 7, HabilidadeId = 3}
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

            modelBuilder.Entity<Disputa>().HasKey(d => d.Id);//Indicação da chave primária da entidade.
            //Abaixo fica o mapeamento do nome das colunas da tabela para as propriedades da classe.
            modelBuilder.Entity<Disputa>().Property(d => d.DataDisputa).HasColumnName("Dt_Disputa");
            modelBuilder.Entity<Disputa>().Property(d => d.AtacanteId).HasColumnName("AtacanteId");
            modelBuilder.Entity<Disputa>().Property(d => d.OponenteId).HasColumnName("OponenteId");
            modelBuilder.Entity<Disputa>().Property(d => d.Narracao).HasColumnName("Tx_Narracao");            

            //modelBuilder.Entity<Personagem>().Navigation(p=>p.Arma).AutoInclude();
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