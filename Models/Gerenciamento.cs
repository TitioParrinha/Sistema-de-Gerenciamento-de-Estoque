namespace Estoque.Models
{
    public class Gerenciamento
    {
        public int Id { get; set; } 
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int Quantidade { get; set; }

        public void RegistrarEntrada(int quantidade)
        {
            this.Quantidade += quantidade;
        }
    }

    
}