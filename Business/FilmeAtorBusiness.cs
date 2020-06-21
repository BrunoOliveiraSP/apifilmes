using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace apifilmes.Business
{
    public class FilmeAtorBusiness
    {
        Database.FilmeAtorDatabase filmeAtorDb = new Database.FilmeAtorDatabase();
        
        public List<Models.Response.FilmeAtorResponse> Consultar(string genero, string personagem, string ator)
        {
            if (string.IsNullOrEmpty(genero))
                throw new ArgumentException("Gênero é obrigatório.");
                

            List<Models.TbFilme> filmes = filmeAtorDb.Consultar(genero, personagem, ator);

            Utils.FilmeAtorResponseConverter faConverter = new Utils.FilmeAtorResponseConverter();
            List<Models.Response.FilmeAtorResponse> response = faConverter.Converter(filmes);

            return response;
        }

    }
}