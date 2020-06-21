using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Database
{
    public class FilmeAtorDatabase
    {
        Models.apidbContext ctx = new Models.apidbContext();


        public List<Models.TbFilme> Consultar(string genero, string personagem, string ator)
        {
            List<Models.TbFilme> filmes = 
                ctx.TbFilme
                      .Include(x => x.TbFilmeAtor)
                      .ThenInclude(x => x.IdAtorNavigation)
                   .Where(x => x.DsGenero == genero 
                            && x.TbFilmeAtor.Any(f => f.NmPersonagem.Contains(personagem)
                                                   && f.IdAtorNavigation.NmAtor.Contains(ator)))
                   .ToList();

            return filmes;
        }

    }
}