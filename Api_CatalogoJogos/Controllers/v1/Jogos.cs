using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Api_CatalogoJogos.InputModel;
using Api_CatalogoJogos.ViewModel;
using Api_CatalogoJogos.Services;
using Api_CatalogoJogos.Exceptions;


namespace Api_CatalogoJogos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    // garantia de versionamento para evitar falhas
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }
 
        [HttpGet]

        //async - assíncrono. Retornará um ActionResult do tipo lista.
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);
            if (jogos.Count() == 0)
            {
                return NoContent();
            }
            return Ok(jogos);
            //estado 200 (status code)
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogos = await _jogoService.Obter(idJogo);

            if (jogos == null)
                return NoContent();

            return Ok(jogos);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogo)
        {
            try
            {
                var jogos = await _jogoService.Inserir(jogo);
                return Ok(jogos);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
                //estado 422
            }

        }

        [HttpPut("{idJogo:guid}")]
        public  async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogo)
        {
            try
            {
                var jogos = _jogoService.Atualizar(idJogo, jogo);
                return Ok(jogos);
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não encontrado");
                //estado 404
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não encontrado");
                //estado 404
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> Excluir([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não encontrado");
            }

        }

    }
}
