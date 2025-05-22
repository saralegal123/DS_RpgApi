using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgApi.Models.Enuns
{
    public class Usuario
    {
        public int Id {get; set;} //atalho para propriedade: PROP + TAB
        public String Username {get; set;} = string.Empty;
        public byte[]? PasswordHash {get; set;}
        public byte[]? PasswordSalt {get; set;} 
        public byte[]? Foto {get; set;} 
        public double ? Latitude {get; set;} 
        public double? Longitude {get; set;} 
        public DateTime? DataAcesso {get; set;} 
        
        public List<Personagem> Personagens { get; set; } = new List<Personagem>();

        [NotMapped]
        public string PasswordString {get; set;}  = string.Empty;
        
        [NotMapped]
        public string Token {get; set;} = string.Empty;
                
        public string? Perfil {get; set;} 
        public string? Email {get; set;} 
    }
}
    