using SQLite;

namespace MinhasCompras.Models
{
  public class Produto
  {
    string _descricao;
    // string _categoria;
    decimal _quantidade;
    decimal _preco;

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Descricao
    {
      get => _descricao;
      set
      {
        if (value == null)
        {
          throw new Exception("Por favor, preencha a descriçao.");
        }
        _descricao = value;
      }
    }
    public string Categoria { get; set; }
    public decimal Quantidade
    {
      get => _quantidade;
      set
      {
        if (value <= 0)
        {
          throw new Exception("A quantidade deve ser um valor maior que 0");
        }
        _quantidade = value;
      }
    }
    public decimal Preco
    {
      get => _preco;
      set
      {
        if (value <= 0)
        {
          throw new Exception("O preço deve ser um valor maior que zero");
        }
        _preco = value;
      }
    }
    public decimal Total => _quantidade * _preco;
  }
}
