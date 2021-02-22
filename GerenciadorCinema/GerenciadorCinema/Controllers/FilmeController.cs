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
    [Route("api/filme")]
    public class FilmeController : ControllerBase
    {

        private readonly GerenciadorCinemaContext _context;

        public FilmeController(GerenciadorCinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar-filmes")]
        public async Task<IActionResult> GetFilmes()
        {
            try
            {
                var listfilmes = await Task.Run(() => GetListaFilmes(_context));

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Listagem ok",
                    Erro = null,
                    Validation = null,
                    Data = listfilmes,
                };

                return Ok(result);
            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os filmes",
                    Erro = e.Message,
                    Validation = null,
                    Data = null,
                };

                return BadRequest(result);
            }

        }

        [HttpPost]
        [Route("registrar-filme")]
        public async Task<IActionResult> Save([FromBody] FilmeViewEntity filmevew)
        {
            try
            {
                var result = new Result();

                var filme = new FilmeEntity
                {
                    Titulo = filmevew.Titulo,
                    Descricao = filmevew.Descricao,
                    Duracao = filmevew.Duracao,
                    Imagem = filmevew.Imagem,
                };

                var countTitulo = (from f in _context.Filme
                                   where f.Titulo == filme.Titulo
                                   select f
                                   ).ToList().Count();

                if (countTitulo > 0)
                {

                    result = new Result
                    {
                        Fail = true,
                        OK = false,
                        Msg = "Esse Título de Filme já está cadastrado!",
                        Erro = null, 
                        Validation = null,

                    };

                    return Ok(result);
                }

                _context.Add<FilmeEntity>(filme);
                _context.SaveChanges();

                result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Filme adicionado com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = filme,
                };


                return Ok(result);

            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os filmes",
                    Erro = e.Message,
                    Validation = null,
                };
                

                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("deletar-filme")]
        public async Task<IActionResult> Delete([FromBody] FilmeViewEntity filmeView)
        {
            try
            {
                var result = new Result();

                var filme = new FilmeEntity
                {
                    Id = Convert.ToInt32(filmeView.Id)
                };

                var countSessoes = (from s in _context.Sessao
                                    join f in _context.Filme on s.Filme.Id equals f.Id
                                    where s.Filme.Id == filme.Id
                                    select s
                    ).ToList().Count();

                if (countSessoes > 0)
                {

                    result = new Result
                    {
                        Fail = true,
                        OK = false,
                        Msg = "Este Filme já está vinculado a uma sessão",
                        Erro = null,
                        Validation = null,

                    };

                    return Ok(result);
                }

                _context.Remove<FilmeEntity>(filme);
                _context.SaveChanges();

                result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Filme excluido com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = filme,
                };

                return Ok(result);
            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao excluir o filmes",
                    Erro = e.Message,
                    Validation = null,
                };


                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("alterar-filme")]
        public async Task<IActionResult> Update([FromBody] FilmeViewEntity filmevew)
        {
            try
            {
                var filme = _context.Filme.Find(Convert.ToInt32(filmevew.Id));


                filme.Titulo = filmevew.Titulo;
                filme.Descricao = filmevew.Descricao;
                filme.Imagem = filmevew.Imagem;
                filme.Duracao = filmevew.Duracao;

                _context.Update<FilmeEntity>(filme);
                _context.SaveChanges();

                var result = new Result
                {
                    Fail = false,
                    OK = true,
                    Msg = "Filme atualizado com sucesso",
                    Erro = null,
                    Validation = null,
                    Data = filme,
                };


                return Ok(result);

            }
            catch (Exception e)
            {

                var result = new Result
                {
                    Fail = true,
                    OK = false,
                    Msg = "Ocorreru um erro ao listar os filmes",
                    Erro = e.Message,
                    Validation = null,
                };


                return BadRequest(result);
            }
        }
        public static List<FilmeEntity> GetListaFilmes(GerenciadorCinemaContext context)
        {
            return (from b in context.Filme select b).ToList();
        }

        public static FilmeEntity GetFilmeById(GerenciadorCinemaContext context, int Id)
        {
            return (from b in context.Filme where b.Id == Id select b).FirstOrDefault();
        }


    }
}
