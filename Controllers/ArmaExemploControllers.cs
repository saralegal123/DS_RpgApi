using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    //[Route("api/[controller]")] retirar o "api/"
    // para localizar no postman nao é necessário o "controller"
    [Route("[controller]")]
    public class ArmaExemploControllers : ControllerBase
    {
         private static List<Arma> armas = new List<Arma>()
        {
            new Arma() { Id = 1, Nome = "Espada", Dano=17, Classe=ArmaEnum.Espada},
            new Arma() { Id = 2, Nome = "Adaga", Dano=15, Classe=ArmaEnum.Adaga},
            new Arma() { Id = 3, Nome = "Besta", Dano=18, Classe=ArmaEnum.Besta },
            new Arma() { Id = 4, Nome = "Mangual", Dano=18,  Classe=ArmaEnum.Mangual },
            new Arma() { Id = 5, Nome = "Cajado", Dano=20, Classe=ArmaEnum.Cajado },
            new Arma() { Id = 6, Nome = "Pistola", Dano=21,  Classe=ArmaEnum.Pistola },
            new Arma() { Id = 7, Nome = "Femur", Dano=25,  Classe=ArmaEnum.Femur }
        };

        [HttpGet("Get")]
        public IActionResult GetFirst(){
            Arma a = armas[0];
            return Ok(a);
        }         
        
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(armas);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(armas.FirstOrDefault(ar => ar.Id == id));
        }

        [HttpPost]
        public IActionResult AddArma(Arma novaArma)
        {
            Arma.Add(novaArma);
            return Ok(armas);
        }

        [HttpGet("GetOrdenado")]
         public IActionResult GetOrdem()
        {
            List<Arma> listaFinal = armas.OrderBy(a => a.Nome).ToList();
            return Ok(listaFinal);
        }

        
    }
}