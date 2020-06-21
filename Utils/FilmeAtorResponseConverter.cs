using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace apifilmes.Utils
{
    public class FilmeAtorResponseConverter
    {
        
        public List<Models.Response.FilmeAtorResponse> Converter(List<Models.TbFilme> filmes)
        {
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


    }
}