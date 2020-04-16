using System;

namespace apifilmes.Models.Request
{
    public class FilmeAtorItemRequest
    {
        public string Ator { get; set; }
        public string Personagem { get; set; }
        public decimal Altura { get; set; }
        public DateTime Nascimento { get; set; }
    }
}