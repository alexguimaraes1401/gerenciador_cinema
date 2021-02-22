using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCinema.Models;
using GerenciadorCinema.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Controllers
{
    [ApiController]
    [Route("api/sala")]
    public class SalaController : ControllerBase
    {
        [HttpGet]
        [Route("listar-salas")]
        public async Task<IActionResult> GetAsync([FromServices] GerenciadorCinemaContext context)
        {

            Result result = new Result();

            try
            {
                var listSalas = await Task.Run(() => GetListaSalas(context));

                result.OK = true;
                result.Fail = false;
                result.Msg = "Listagem ok";
                result.Data = listSalas;

                return Ok(result);
            }catch(Exception e)
            {
                
                result.Fail = true;
                result.OK = false;
                result.Msg = "Ocorreru um erro ao listar as salas";
                result.Erro = e.Message;

                return BadRequest(result);
            }
        }

        public static List<SalaEntity> GetListaSalas(GerenciadorCinemaContext context)
        {
            return (from b in context.Sala select b).ToList();
        }
        public static SalaEntity GetSalaById(GerenciadorCinemaContext context, int Id)
        {
            return (from b in context.Sala where b.Id == Id select b).FirstOrDefault();
        }


    }
}
