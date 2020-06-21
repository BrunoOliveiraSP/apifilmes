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
    public class FilmeAtorController  : ControllerBase
    {
        Business.FilmeAtorBusiness filmeAtorBusiness = new Business.FilmeAtorBusiness();


        [HttpPost]
        public void Salvar(Models.Request.FilmeAtorRequest request)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            foreach (Models.Request.FilmeAtorItemRequest item in request.Atores)
            {
                Models.TbAtor ator = new Models.TbAtor();
                ator.NmAtor = item.Ator;
                ator.VlAltura = item.Altura;
                ator.DtNascimento = item.Nascimento;

                ctx.TbAtor.Add(ator);
                ctx.SaveChanges();

                Models.TbFilmeAtor fa = new Models.TbFilmeAtor();
                fa.IdFilme = request.IdFilme;
                fa.IdAtor = ator.IdAtor;
                fa.NmPersonagem = item.Personagem;

                ctx.TbFilmeAtor.Add(fa);
                ctx.SaveChanges();
            }
        }


        [HttpPost("encadeado")]
        public void SalvarEncadeado(List<Models.TbAtor> atores)
        {
            Models.apidbContext ctx = new Models.apidbContext();
            ctx.TbAtor.AddRange(atores);
            ctx.SaveChanges();
        }



        [HttpPost("mix")]
        public void Mix(Models.Request.FilmeAtorRequest request)
        {
            List<Models.TbAtor> atores = 
                request.Atores.Select(x => new Models.TbAtor()
                {
                    NmAtor = x.Ator,
                    VlAltura = x.Altura,
                    DtNascimento = x.Nascimento,
                    TbFilmeAtor = new List<Models.TbFilmeAtor>() { 
                        new Models.TbFilmeAtor() 
                        {
                            NmPersonagem = x.Personagem,
                            IdFilme = request.IdFilme
                        }
                    }
                }).ToList();

            
            Models.apidbContext ctx = new Models.apidbContext();
            ctx.TbAtor.AddRange(atores);
            ctx.SaveChanges();
        }




        [HttpPost("juntos")]
        public void SalvarJuntos(Models.Request.FilmeAtorJuntosRequest req)
        {
            Models.TbFilme filme= new Models.TbFilme();
            filme.NmFilme= req.NmFilme;
            filme.DsGenero = req.DsGenero;
            filme.NrDuracao = req.NrDuracao;
            filme.VlAvaliacao = req.VlAvaliacao;
            filme.BtDisponivel = req.BtDisponivel;
            filme.DtLancamento = req.DtLancamento;
            
            filme.TbFilmeAtor =
                req.Atores.Select(x => new Models.TbFilmeAtor()
                {
                    NmPersonagem = x.Personagem,
                    IdAtorNavigation = new Models.TbAtor() 
                    {
                        NmAtor = x.Ator,
                        DtNascimento = x.Nascimento,
                        VlAltura = x.Altura
                    }
                }).ToList();


            
            Models.apidbContext ctx = new Models.apidbContext();
            ctx.TbFilme.Add(filme);
            ctx.SaveChanges();
        }





        [HttpGet]
        public List<Models.Response.FilmeAtorResponse> ListarFilmes()
        {
            Models.apidbContext ctx = new Models.apidbContext();

            List<Models.TbFilme> filmes = 
                ctx.TbFilme
                      .Include(x => x.TbFilmeAtor)
                      .ThenInclude(x => x.IdAtorNavigation)
                   .ToList();


            List<Models.Response.FilmeAtorResponse> response =
                filmes.Select(x => new Models.Response.FilmeAtorResponse()
                {
                    Filme = new Models.Response.FilmeAtorItemFilmeResponse()
                    {
                        Id = x.IdFilme,
                        Filme = x.NmFilme,
                        Genero = x.DsGenero,
                        Avaliacao = x.VlAvaliacao,
                        Duracao = x.NrDuracao,
                        Disponivel = x.BtDisponivel,
                        Lancamento = x.DtLancamento
                    },
                    Atores = x.TbFilmeAtor.Select(f => new Models.Response.FilmeAtorItemAtorResponse()
                    {
                        IdAtor = f.IdAtorNavigation.IdAtor,
                        IdFilmeAtor = f.IdFilmeAtor,
                        Ator = f.IdAtorNavigation.NmAtor,
                        Personagem = f.NmPersonagem
                    }).ToList()
                }).ToList();

            return response;
        }



        [HttpGet("consultar")]
        public List<Models.Response.FilmeAtorResponse> Consultar(string genero, string personagem, string ator)
        {
            List<Models.Response.FilmeAtorResponse> response = 
                filmeAtorBusiness.Consultar(genero, personagem, ator);
            return response;
        }
        



        [HttpPut]
        public void Alterar(Models.TbFilmeAtor filmeAtor)
        {
            Models.apidbContext ctx = new Models.apidbContext();

            Models.TbFilmeAtor atual = ctx.TbFilmeAtor.First(x => x.IdFilmeAtor == filmeAtor.IdFilmeAtor);
            atual.NmPersonagem = filmeAtor.NmPersonagem;

            ctx.SaveChanges();
        }



        [HttpDelete]
        public void Remover(Models.TbFilmeAtor filmeAtor)
        {
            Models.apidbContext ctx = new Models.apidbContext();
            
            Models.TbFilmeAtor atual = ctx.TbFilmeAtor.First(x => x.IdFilmeAtor == filmeAtor.IdFilmeAtor);
            
            ctx.TbFilmeAtor.Remove(atual);
            ctx.SaveChanges();
        }






    }
}