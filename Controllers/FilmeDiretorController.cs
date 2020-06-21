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
    public class FilmeDiretorController : ControllerBase
    {
        
        
        [HttpPost]
        public Models.Request.FilmeDiretorRequest Salvar(Models.Request.FilmeDiretorRequest request)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            ctx.TbFilme.Add(request.Filme);
            ctx.SaveChanges();

            request.Diretor.IdFilme = request.Filme.IdFilme;
            
            ctx.TbDiretor.Add(request.Diretor);
            ctx.SaveChanges();

            return request;
        }
        
        
        [HttpPost("encadeado")]
        public Models.TbFilme SalvarEncadeado(Models.TbFilme filme)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            ctx.TbFilme.Add(filme);
            ctx.SaveChanges();

            return filme;
        }



        [HttpGet("consultar/diretor")]
        public List<Models.TbDiretor> Consultar(string diretor, string genero)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            List<Models.TbDiretor> diretores = 
                ctx.TbDiretor
                     // .Include(x => x.IdFilmeNavigation)
                   .Where(x => x.NmDiretor.Contains(diretor) 
                            && x.IdFilmeNavigation.DsGenero == genero)
                   .ToList();

            return diretores;
        }



        [HttpGet("consultar/filme")]
        public List<Models.TbFilme> ConsultarFilmes(string genero, string diretor)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            List<Models.TbFilme> filmes = 
                ctx.TbFilme
                     .Include(x => x.TbDiretor)
                   .Where(x => x.DsGenero == genero 
                            && x.TbDiretor.All(d => d.NmDiretor.StartsWith(diretor)))
                   .ToList();

            return filmes;
        }




    }
}