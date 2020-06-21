using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace apifilmes.Database
{
    public class FilmeDatabase
    {
        Models.apidbContext ctx = new Models.apidbContext();

        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            ctx.TbFilme.Add(filme);
            ctx.SaveChanges();

            return filme;
        }

        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = ctx.TbFilme.ToList();
            return filmes;
        }


        public List<Models.TbFilme> Consultar(string genero)
        {
            List<Models.TbFilme> filmes = ctx.TbFilme.Where(x => x.DsGenero == genero)
                                                     .ToList();
            return filmes;
        }

        public bool FilmeExistente(string nome)
        {
            bool existe = ctx.TbFilme.Any(x => x.NmFilme == nome);
            return existe;
        }


        public void Alterar(Models.TbFilme filme)
        {
            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            atual.NmFilme = filme.NmFilme;
            atual.DsGenero = filme.DsGenero;
            atual.NrDuracao = filme.NrDuracao;
            atual.VlAvaliacao = filme.VlAvaliacao;
            atual.BtDisponivel = filme.BtDisponivel;
            atual.DtLancamento = filme.DtLancamento;

            ctx.SaveChanges();
        }

        public void AlterarDisponibilidade(Models.TbFilme filme)
        {
            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            atual.BtDisponivel = filme.BtDisponivel;
            
            ctx.SaveChanges();
        }


        public void Remover(Models.TbFilme filme)
        {
            Models.TbFilme atual = ctx.TbFilme.First(x => x.IdFilme == filme.IdFilme);
            ctx.TbFilme.Remove(atual);

            ctx.SaveChanges();
        }


        public void RemoverPorGenero(Models.TbFilme filme)
        {
            List<Models.TbFilme> filmes = ctx.TbFilme.Where(x => x.DsGenero == filme.DsGenero)
                                                     .ToList();

            ctx.TbFilme.RemoveRange(filmes);
            ctx.SaveChanges();
        }





    }
}