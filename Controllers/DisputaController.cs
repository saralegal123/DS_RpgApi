using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisputasController : ControllerBase
    {
        private readonly DataContext _context;

        public DisputasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Arma")]
        public async Task<IActionResult> AtaqueComArmaAsync(Disputa d)
        {
            try
            {
                Personagem? atacante = await _context.TB_PERSONAGENS
                   .Include(p => p.Arma)
                   .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem? oponente = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                int dano = atacante.Arma.Dano + new Random().Next(atacante.Forca);
                dano = dano - new Random().Next(oponente.Defesa);

                if (dano > 0)                
                    oponente.PontosVida = oponente.PontosVida - dano;                
                if (oponente.PontosVida <= 0)                
                    d.Narracao = $"{oponente.Nome} foi derrotado!";                

                _context.TB_PERSONAGENS.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat(" Atacante: {0}. ", atacante.Nome);
                dados.AppendFormat(" Oponente: {0}. ", oponente.Nome);
                dados.AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                dados.AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                dados.AppendFormat(" Arma Utilizada: {0}. ", atacante.Arma.Nome);
                dados.AppendFormat(" Dano: {0}. ", dano);

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.TB_DISPUTAS.Add(d);
                _context.SaveChanges();

                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost("Habilidade")]
        public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.TB_PERSONAGENS
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade ph = await _context.TB_PERSONAGENS_HABILIDADES
                    .Include(p => p.Habilidade).FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId
                     && phBusca.PersonagemId == d.AtacanteId);


                     //Verificar se essa linha acima não vai gerar falha                     

                if (ph == null)
                    d.Narracao = $"{atacante.Nome} não possui esta habilidade";
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                    dano = dano - new Random().Next(oponente.Defesa);

                    if (dano > 0)                    
                        oponente.PontosVida = oponente.PontosVida - dano;                                            
                    if (oponente.PontosVida <= 0)                    
                        d.Narracao += $"{oponente.Nome} foi derrotado!";                    

                    _context.TB_PERSONAGENS.Update(oponente);
                    await _context.SaveChangesAsync();

                    StringBuilder dados = new StringBuilder();
                    dados.AppendFormat(" Atacante: {0}. ", atacante.Nome);
                    dados.AppendFormat(" Oponente: {0}. ", oponente.Nome);
                    dados.AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                    dados.AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                    dados.AppendFormat(" Habilidade Utilizada: {0}. ", ph.Habilidade.Nome);
                    dados.AppendFormat(" Dano: {0}. ", dano);

                    d.Narracao += dados.ToString();
                    d.DataDisputa = DateTime.Now;
                    _context.TB_DISPUTAS.Add(d);
                    _context.SaveChanges();
                }
                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }        

        [HttpGet("PersonagemRandom")]
        public async Task<IActionResult> Sorteio()
        {
            List<Personagem> personagens = await _context.TB_PERSONAGENS.ToListAsync();

            //Sorteio com numero da quantidade de personagens
            int sorteio = new Random().Next(personagens.Count);

            //busca na lista pelo indice sorteado (não é o id)
            Personagem p = personagens [sorteio];

            string msg = string.Format("N° Sorteado {0}. Personagem: {1}", sorteio, p.Nome);

            return Ok(msg);
        }

        [HttpPost("DisputaEmGrupo")]
        public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
        {
            try
            {
                d.Resultados = new List<string>(); //instancia a lista re resultados
                
                //busca na base dos persongaens informados no parametros incluindo Armas e Hbailidadaes
                List<Personagem> personagens = await _context.Personagens
                .Include(p => p.Arma)
                .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                .Where(p => d.ListaIdPersonagens.Contains(p.Id)).ToListAsync();

                //contagem de persoangens vivos na lista obtida do banco de dados 
                int qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0 ).Count;

                //enquianto houver mais de um personagem vivo havera disputa
                while (qtdPersonagensVivos > 1)
                {
                    List<Personagem> atacantes = persoangens.Where(p => p.PontosVida > 0).ToList();
                    Personagem atacante = atacante[new Random().Next(atacante.Count)];
                    d.AtacanteId = atacante.Id;

                    //selecione personagens com potnos de vida positivos, exceto o atacante escolhido e dpois faz sorteio
                    List<Personagem> oponente = personagens.Where(p => p.Id != atacante.Id && p.PontosVida > 0).Tolist();
                    Personagem oponente = oponente[new Random().Next(oponente.Count)];
                    d.OponenteId = oponente.Id;

                    //declare e redefine a cada passagem do while o valor das variaveis que serão usadas
                    int dano = 0;
                    string ataqueUsado = string.Empty;
                    string resultado = string.Empty;

                    //sorteia entre 0 e 1: 0 é um ataqie com arma e 1 é um ataque com habilidades
                    bool ataqueUsaArma = (new Random().Next(1) == 0);

                    if (ataqueUsaArma && atacante.Arma != null)
                    {
                        dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                        dano = dano - new Random().Next(oponente.Defesa); //sorteio de defesa
                        ataqueUsado = atacante.Arma.Nome;

                        if(dano > 0)
                        oponente.PontosVida = oponente.PontosVida - (int)dano;

                        //formata mensagem
                        resultado =
                            string.Format("{0} atacou {1} com o dano {3}", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado; //concatena o resultado com as narraçoes existentes
                        d.Resultados.Add(resultado); //adiciona o resultado atual na lista de resultados
                    }

                    else if (atacante.PersonagemHabilidades.Count != 0) //verifica se o personagem tem habilidades
                    {
                        int sorteioHabilidadeId = new Random().Next(atacante.PersonagemHabilidades.Count);
                        Habilidade habilidadeEscolhida = atacante.PersonagemHabilidades[sorteioHabilidadeId].Habilidade;
                        ataqueUsado = habilidadeEscolhida.Nome;

                        dano = habilidadeEscolhida.Dano + (new Random().Next(atacante.Inteligencia));
                        dano = dano - new Random().Next(oponente.Defesa);

                        if (dano > 0)
                            oponente.PontosVida = oponente.PontosVida - (int)dano;

                        resultado =
                            string.Format("{0} atacou {1} usando {2} com o dano de {3}.", atacante.Nome, oponente.Nome, ataqueUsado, dano);
                        d.Narracao += resultado;
                        d.Resultados.Add(resultado); 
                    }
                    if (!string.IsNullOrEmpty(ataqueUsado)) 
                    {
                        //Incrementa os dados dos combates
                        atacante.Vitorias++;
                        oponente.Derrotas++;
                        atacante.Disputas++;           
                        oponente.Disputas++;

                        d.Id = 0; //Zera o Id para poder salvar os dados de disputa sem erro de chave.
                        d.DataDisputa = DateTime.Now;
                        _context.Disputas.Add(d);         
                        await _context.SaveChangesAsync();
                    } 
                        qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count; 
                       
                        if (qtdPersonagensVivos == 1) //Havendo só um personagem vivo, existe um CAMPEÃO! 
                        {
                            string resultadoFinal =
                                $"{atacante.Nome.ToUpper()} é CAMPEÃO com {atacante.PontosVida} pontos de vida restantes!";
                            d.Narracao += resultadoFinal; //Concatena o resultado final com as demais narrações.
                            d.Resultados.Add(resultadoFinal); //Concatena o resultado final com os demais resultados.
                            
                            break; //break vai parar o While.
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                
                //codigo apos o fechamento do while atualizara os pontpos de vida, 
                //disputas, vitorias e derrotas de todos personagens ao final das batalhas
                _context.Personagens.UpdateRange(persoangens);
                await _context.SaveChangesAsync();

                return Ok(d); //retorna os dados das disputas
            }
           
           [HttpDelete("ApagarDisputas")]
           public async Task<IActionResult> DeleteAsync()
           {
                try
                {
                    List<Disputa> disputas = await _context.TB_DISPUTAS.ToListAsync();

                    _context.TB_DISPUTAS.RemoveRange(disputas);
                    await _context.SaveChangesAsync();

                    return Ok("Disputas apagadas");
                }
                catch (System.Exception ex)
                { 
                    return BadRequest(ex.Message); 
                }
            }
        
            [HttpGet("Listar")]
            public async Task<IActionResult> ListarAsync()
            {
                try
                {
                    List<Disputa> disputas = await _context.TB_DISPUTAS.ToListAsync();

                    return Ok(disputas);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        
            [HttpPut("RestaurarPontosVida")]
            public async Task<IActionResult> RestaurarPontosVidaAsync(Personagem p)
            {
                try
                {
                    int linhasAfetadas = 0;
                    Personagem? pEncontrado = await _context.Personagens.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
                    pEncontrado.PontosVida = 100;

                    bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
                        pAtualizar => pAtualizar.PontosVida);
                    // EF vai detectar e atualizar apenas as colunas que foram alteradas.
                    if (atualizou)
                        linhasAfetadas = await _context.SaveChangesAsync();

                    return Ok(linhasAfetadas);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            //Método para alteração da foto
            [HttpPut("AtualizarFoto")]
            public async Task<IActionResult> AtualizarFotoAsync(Personagem p)
            {
                try
                {
                    Personagem personagem = await _context.TB_PERSONAGENS
                        .FirstOrDefaultAsync(x => x.Id == p.Id);
                    personagem.FotoPersonagem = p.FotoPersonagem;
                    var attach = _context.Attach(personagem);
                    attach.Property(x => x.Id).IsModified = false;
                    attach.Property(x => x.FotoPersonagem).IsModified = true;
                    int linhasAfetadas = await _context.SaveChangesAsync();
                    return Ok(linhasAfetadas);
                }
                catch (System.Exception ex)
                { 
                    return BadRequest(ex.Message);
                }
            }
        
            [HttpPut("ZerarRanking")]
            public async Task<IActionResult> ZerarRankingAsync(Personagem p)
            {
                try
                {
                     Personagem pEncontrado =
                        await _context.TB_PERSONAGENS.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
                pEncontrado.Disputas = 0;
                pEncontrado.Vitorias = 0;
                pEncontrado.Derrotas = 0;
                int linhasAfetadas = 0;

                bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
                    pAtualizar => pAtualizar.Disputas,
                    pAtualizar => pAtualizar.Vitorias,
                    pAtualizar => pAtualizar.Derrotas);
                // EF vai detectar e atualizar apenas as colunas que foram alteradas.
                if (atualizou)
                    linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        
            [HttpPut("ZerarRankingRestaurarVidas")]
            public async Task<IActionResult> ZerarRankingRestaurarVidasAsync()
            {
                try
                {
                    List<Personagem> lista =
                    await _context.TB_PERSONAGENS.ToListAsync();

                    foreach (Personagem p in lista)
                    {
                        await ZerarRankingAsync(p);
                        await RestaurarPontosVidaAsync(p);
                    }
                    return Ok();
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
    }
            
}

       

