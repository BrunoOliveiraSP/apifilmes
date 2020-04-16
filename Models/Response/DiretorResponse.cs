namespace apifilmes.Models.Response
{
    public class DiretorResponse
    {
        public int IdDiretor { get; set; }
        public int? IdFilme { get; set; }
        public string Diretor { get; set; }
        public string Filme { get; set; }
        public string Genero { get; set; }
        public decimal? Avaliacao { get; set; }
        public bool? Disponivel { get; set; }
    }
}