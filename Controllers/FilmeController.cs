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


        [HttpPost]
        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();
            
            ctx.TbFilme.Add(filme);
            ctx.SaveChanges();

            return filme;
        }


        [HttpGet]
        public List<Models.TbFilme> Listar()
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            List<Models.TbFilme> filmes = ctx.TbFilme.ToList();
            return filmes;
        }



        [HttpGet("consultar")]
        public List<Models.TbFilme> Consultar(string genero)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            List<Models.TbFilme> filmes = ctx.TbFilme.Where(x => x.DsGenero == genero)
                                                     .ToList();
            return filmes;
        }



        [HttpPut]
        public void Alterar(Models.TbFilme filme)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            atual.NmFilme = filme.NmFilme;
            atual.DsGenero = filme.DsGenero;
            atual.NrDuracao = filme.NrDuracao;
            atual.VlAvaliacao = filme.VlAvaliacao;
            atual.BtDisponivel = filme.BtDisponivel;
            atual.DtLancamento = filme.DtLancamento;

            ctx.SaveChanges();
        }


        [HttpPut("disponibilidade")]
        public void AlterarDisponibilidade(Models.TbFilme filme)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            atual.BtDisponivel = filme.BtDisponivel;
            
            ctx.SaveChanges();
        }



        [HttpDelete]
        public void Remover(Models.TbFilme filme)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            ctx.TbFilme.Remove(atual);

            ctx.SaveChanges();
        }

        
        [HttpDelete("genero")]
        public void RemoverPorGenero(Models.TbFilme filme)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            List<Models.TbFilme> filmes = ctx.TbFilme.Where(x => x.DsGenero == filme.DsGenero)
                                                     .ToList();

            ctx.TbFilme.RemoveRange(filmes);
            ctx.SaveChanges();
        }



        
    }
}