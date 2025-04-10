using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RpgApi.Data;
using RpgApi.Models;

//Sara Marcely Andrade de Oliveira e Isabelle
namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ArmasController : ControllerBase
    {
        private readonly DataContext _context;

        public ArmasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Arma>> Get()
        {
            return _context.Armas.ToList();
        }
    
        [HttpPost]
        public IActionResult Post([FromBody] Arma arma)
        {
            _context.Armas.Add(arma);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new {id = arma.Id}, arma);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Arma novaArma)
        {
            var  arma = _context.Armas.Find(id);
            if (arma == null) return NotFound();


            arma.Nome = novaArma.Nome;
            arma.Dano = novaArma.Dano;
            _context.SaveChanges();

            return Ok(arma);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var arma =  _context.Armas.Find();
            if (arma == null) return NotFound();

            _context.Armas.Remove(arma);
            _context.SaveChanges();

            return NoContent();
        }
    }
}