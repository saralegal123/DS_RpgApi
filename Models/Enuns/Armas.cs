using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RpgApi.Models.Enuns;

namespace RpgApi.Models
{
    public class Arma
    {
        public int Id {get; set;}
        public string Nome { get; set; } = string.Empty;      
        public int Dano {get;set;}
        public ArmaEnum Classe { get; set; }
        public Personagem? Personagem {get; set;} = null!; //null! --> ignora os warnings
        public int? PersonagemId {get; set;}
        
        internal static void Add(Arma novaArma)
        {
            throw new NotImplementedException();
        }
    }

}
