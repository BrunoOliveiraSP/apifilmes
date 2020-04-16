using System;
using System.Collections;
using System.Collections.Generic;


namespace apifilmes.Models.Response
{
    public class FilmeAtorResponse
    {
        public FilmeAtorItemFilmeResponse Filme { get; set; }
        public List<FilmeAtorItemAtorResponse> Atores { get; set; }
    }
}