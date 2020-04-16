using System;


namespace apifilmes.Models.Request
{
    public class DiretorPorFilmeNomeRequest
    {
        public string NmDiretor { get; set; }
        public DateTime DtNascimento { get; set; }
        public string NmFilme { get; set; }
    }
}