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
    public class DiretorController : ControllerBase
    {


        [HttpPost]
        public Models.TbDiretor Salvar(Models.TbDiretor diretor)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            ctx.TbDiretor.Add(diretor);
            ctx.SaveChanges();

            return diretor;
        }




        [HttpPost("filme")]
        public Models.Response.DiretorPorFilmeNomeResponse SalvarPorFilmeNome(Models.Request.DiretorPorFilmeNomeRequest diretorReq)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();
            Models.TbFilme filme = ctx.TbFilme.First(x => x.NmFilme == diretorReq.NmFilme);
            
            Models.TbDiretor diretor = new Models.TbDiretor();
            diretor.NmDiretor = diretorReq.NmDiretor;
            diretor.DtNascimento = diretorReq.DtNascimento;
            diretor.IdFilme = filme.IdFilme;

            ctx.TbDiretor.Add(diretor);
            ctx.SaveChanges();

            Models.Response.DiretorPorFilmeNomeResponse resp = new Models.Response.DiretorPorFilmeNomeResponse();
            resp.IdDiretor = diretor.IdDiretor;
            resp.NmDiretor = diretor.NmDiretor;
            resp.DtNascimento = diretor.DtNascimento;
            resp.IdFilme = filme.IdFilme;
            resp.NmFilme = filme.NmFilme;
            
            return resp;
        }


        




        [HttpGet]
        public List<Models.Response.DiretorResponse> Listar()
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            List<Models.TbDiretor> diretores = 
                ctx.TbDiretor
                     .Include(x => x.IdFilmeNavigation)
                   .ToList();

            List<Models.Response.DiretorResponse> response =
                diretores.Select(x => new Models.Response.DiretorResponse {
                    IdDiretor = x.IdDiretor,
                    IdFilme = x.IdFilme,
                    Diretor = x.NmDiretor,
                    Filme = x.IdFilmeNavigation.NmFilme,
                    Genero = x.IdFilmeNavigation.DsGenero,
                    Avaliacao = x.IdFilmeNavigation.VlAvaliacao,
                    Disponivel = x.IdFilmeNavigation.BtDisponivel
                }).ToList();
            
            return response;
        }



        [HttpPut]
        public void Alterar(Models.TbDiretor diretor)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            Models.TbDiretor atual = ctx.TbDiretor.First(x => x.IdDiretor == diretor.IdDiretor);
            atual.NmDiretor = diretor.NmDiretor;
            atual.DtNascimento = diretor.DtNascimento;
            atual.IdFilme = diretor.IdFilme;

            ctx.SaveChanges();
        }


        [HttpDelete]
        public void Deletar(Models.TbDiretor diretor)
        {
            Models.apiDBContext ctx = new Models.apiDBContext();

            Models.TbDiretor atual = ctx.TbDiretor.First(x => x.IdDiretor == diretor.IdDiretor);

            ctx.Remove(atual);
            ctx.SaveChanges();
        }

        

        
    }
}