using SQLite;

namespace MinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
