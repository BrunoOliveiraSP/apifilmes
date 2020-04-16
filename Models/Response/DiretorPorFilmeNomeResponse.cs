using System;

namespace apifilmes.Models.Response
{
    public class DiretorPorFilmeNomeResponse
    {
        public int IdDiretor {get;set;}
        public int IdFilme { get; set; }
        
        public string NmDiretor { get; set; }
        public DateTime? DtNascimento { get; set; }
        public string NmFilme { get; set; }
    }
}