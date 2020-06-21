using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        Business.FilmeBusiness filmeBusiness = new Business.FilmeBusiness();


        [HttpPost]
        public ActionResult<Models.TbFilme> Salvar(Models.TbFilme filme)
        {
            try
            {
                Models.TbFilme f = filmeBusiness.Salvar(filme);
                return f;    
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(
                    new Models.Response.ErroResponse(ex, 400)
                );
            }
        }









        [HttpGet]
        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = filmeBusiness.Listar();
            return filmes; 
        }



        [HttpGet("consultar")]
        public List<Models.TbFilme> Consultar(string genero)
        {
            List<Models.TbFilme> filmes = filmeBusiness.Consultar(genero);
            return filmes;
        }



        [HttpPut]
        public void Alterar(Models.TbFilme filme)
        {
            filmeBusiness.Alterar(filme);
        }


        [HttpPut("disponibilidade")]
        public void AlterarDisponibilidade(Models.TbFilme filme)
        {
            filmeBusiness.AlterarDisponibilidade(filme);
        }



        [HttpDelete]
        public void Remover(Models.TbFilme filme)
        {
            filmeBusiness.Remover(filme);
        }

        
        [HttpDelete("genero")]
        public void RemoverPorGenero(Models.TbFilme filme)
        {
            filmeBusiness.RemoverPorGenero(filme);
        }



        
    }
}