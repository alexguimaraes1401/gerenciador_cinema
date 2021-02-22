using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCinema.Models;
using GerenciadorCinema.Models.View;
using GerenciadorCinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;


namespace GerenciadorCinema.Controllers
{
    [ApiController]
    [Route("api/sessao")]
    public class SessaoController : ControllerBase
    {

        private readonly GerenciadorCinemaContext _context;

        public SessaoController(GerenciadorCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar-sessoes")]
        public async Task<IActionResult> GetSessoes()
        {
            try
            {
                var listsessoes = await Task.Run(() => GetListaSessoes(_context));

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Listagem ok",
                    Erro = null,
                    Validation = null,
                    Data = listsessoes,
                };

                return Ok(result);
            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os sessoes",
                    Erro = e.Message,
                    Validation = null,
                    Data = null,
                };

                return BadRequest(result);
            }

        }

        [HttpGet]
        [Route("get-sessao")]
        public async Task<IActionResult> GetSessao([FromQuery] int Id)
        {
            try
            {
                var sessao = await Task.Run(() => GetSessaoById(_context, Id));

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Listagem ok",
                    Erro = null,
                    Validation = null,
                    Data = sessao,
                };

                return Ok(result);
            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os sessoes",
                    Erro = e.Message,
                    Validation = null,
                    Data = null,
                };

                return BadRequest(result);
            }

        }

        [HttpPost]
        [Route("registrar-sessao")]
        public async Task<IActionResult> Save([FromBody] SessaoViewEntity sessaovew)
        {
            try
            {
                var sessao = new SessaoEntity
                {
                    Data = Convert.ToDateTime(sessaovew.Data),
                    Sala = SalaController.GetSalaById(_context, Convert.ToInt32(sessaovew.Sala)),
                    Filme = FilmeController.GetFilmeById(_context, Convert.ToInt32(sessaovew.Filme)),
                    Horario = sessaovew.Horario,
                    TipoAnimacao = Convert.ToInt32(sessaovew.TipoAnimacao),
                    TipoAudio = Convert.ToInt32(sessaovew.TipoAudio),
                    ValorIngresso = Convert.ToDecimal(sessaovew.ValorIngresso)

                };

                _context.Add<SessaoEntity>(sessao);
                _context.SaveChanges();

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Sessao adicionado com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = sessao,
                };


                return Ok(result);

            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os sessoes",
                    Erro = e.Message,
                    Validation = null,
                };
                

                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("deletar-sessao")]
        public async Task<IActionResult> Delete([FromBody] SessaoViewEntity sessaoView)
        {
            try
            {
                var sessao = new SessaoEntity
                {
                    Id = Convert.ToInt32(sessaoView.Id)
                };

                _context.Remove<SessaoEntity>(sessao);
                _context.SaveChanges();

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Sessao excluido com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = sessao,
                };

                return Ok(result);
            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao excluir o sessoes",
                    Erro = e.Message,
                    Validation = null,
                };


                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("alterar-sessao")]
        public async Task<IActionResult> Update([FromBody] SessaoViewEntity sessaovew)
        {
            try
            {
                var sessao = _context.Sessao.Find(Convert.ToInt32(sessaovew.Id));

                sessao.Data = Convert.ToDateTime(sessaovew.Data);
                sessao.Sala = SalaController.GetSalaById(_context, Convert.ToInt32(sessaovew.Sala));
                sessao.Filme = FilmeController.GetFilmeById(_context, Convert.ToInt32(sessaovew.Filme));
                sessao.Horario = sessaovew.Horario;
                sessao.TipoAnimacao = Convert.ToInt32(sessaovew.TipoAnimacao);
                sessao.TipoAudio = Convert.ToInt32(sessaovew.TipoAudio);
                sessao.ValorIngresso = Convert.ToDecimal(sessaovew.ValorIngresso);

                _context.Update<SessaoEntity>(sessao);
                _context.SaveChanges();

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Sessao atualizado com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = sessao,
                };


                return Ok(result);

            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os sessoes",
                    Erro = e.Message,
                    Validation = null,
                };


                return BadRequest(result);
            }
        }
        public static List<SessaoViewEntity> GetListaSessoes(GerenciadorCinemaContext context)
        {
            return (from ses in context.Sessao
                    join filme in context.Filme on ses.Filme.Id equals filme.Id
                    join sala in context.Sala on ses.Sala.Id equals sala.Id
                    select new SessaoViewEntity{ Id = ses.Id.ToString(), Data = ses.Data.ToString("dd/mm/yyyy"), Horario = ses.Horario, ValorIngresso = "R$ " + ses.ValorIngresso.ToString(), TipoAnimacao = ses.TipoAnimacao == 0 ? "2D" : "3D" , TipoAudio = ses.TipoAudio == 0 ? "Original" : "Dublado", Filme = filme.Descricao, Sala = sala.Descricao }).ToList();
        }

        public static SessaoViewEntity GetSessaoById(GerenciadorCinemaContext context, int Id)
        {
            return (from ses in context.Sessao
                    join filme in context.Filme on ses.Filme.Id equals filme.Id
                    join sala in context.Sala on ses.Sala.Id equals sala.Id
                    where ses.Id == Id
                    select new SessaoViewEntity { Id = ses.Id.ToString(), Data = ses.Data.ToString("yyyy-MM-dd"), Horario = ses.Horario, ValorIngresso = ses.ValorIngresso.ToString(), TipoAnimacao = ses.TipoAnimacao.ToString(), TipoAudio = ses.TipoAudio.ToString(), Filme = filme.Id.ToString(), Sala = sala.Id.ToString() }).FirstOrDefault();
        }

    }
}
