using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace apifilmes.Business
{
    public class FilmeBusiness
    {
        Database.FilmeDatabase filmeDB = new Database.FilmeDatabase();

        public Models.TbFilme Salvar(Models.TbFilme filme)
        {
            if (string.IsNullOrEmpty(filme.NmFilme))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Gênero é obrigatório");

            if (filme.VlAvaliacao < 0)
                throw new ArgumentException("Avaliação inválida.");
            
            if (filme.NrDuracao < 0)
                throw new ArgumentException("Duração inválida.");

            if (filmeDB.FilmeExistente(filme.NmFilme))
                throw new ArgumentException("Filme já cadastrado.");


            Models.TbFilme f = filmeDB.Salvar(filme);
            return f;
        }


        public List<Models.TbFilme> Listar()
        {
            List<Models.TbFilme> filmes = filmeDB.Listar();
            return filmes;
        }


        public List<Models.TbFilme> Consultar(string genero)
        {
            List<Models.TbFilme> filmes = filmeDB.Consultar(genero);
            return filmes;
        }


        public void Alterar(Models.TbFilme filme)
        {
            if (filme.IdFilme <= 0)
                throw new ArgumentException("Filme inválido");

            if (string.IsNullOrEmpty(filme.NmFilme))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Gênero é obrigatório");

            if (filme.VlAvaliacao < 0)
                throw new ArgumentException("Avaliação inválida.");
            
            if (filme.NrDuracao < 0)
                throw new ArgumentException("Duração inválida.");


            filmeDB.Alterar(filme);
        }

        public void AlterarDisponibilidade(Models.TbFilme filme)
        {
            filmeDB.AlterarDisponibilidade(filme);
        }


        public void Remover(Models.TbFilme filme)
        {
            if (filme.IdFilme <= 0)
                throw new ArgumentException("Filme inválido");

            filmeDB.Remover(filme);
        }


        public void RemoverPorGenero(Models.TbFilme filme)
        {
            if (string.IsNullOrEmpty(filme.DsGenero))
                throw new ArgumentException("Gênero é obrigatório");

            filmeDB.RemoverPorGenero(filme);
        }

    }
}