using MinhasCompras.Models;
using SQLite;

namespace MinhasCompras.Helpers
{
  public class SQLiteDatabaseHelper
  {
    readonly SQLiteAsyncConnection _conn;

    public SQLiteDatabaseHelper(string dbPath)
    {
      _conn = new SQLiteAsyncConnection(dbPath);
      _conn.CreateTableAsync<Produto>().Wait();
    }

    public Task<int> Insert(Produto produto)
    {
      return _conn.InsertAsync(produto);
    }

    public Task<List<Produto>> Update(Produto produto)
    {
      string statement = "UPDATE Produto SET Descricao = ?, Categoria = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
      return _conn.QueryAsync<Produto>(statement, produto.Descricao, produto.Categoria, produto.Quantidade, produto.Preco, produto.Id);
    }

    public Task<int> Delete(int id)
    {
      return _conn.Table<Produto>().DeleteAsync(item => item.Id == id);
    }

    public Task<List<Produto>> GetAll()
    {
      return _conn.Table<Produto>().ToListAsync();
    }

    public Task<List<Produto>> Search(string query)
    {
      string statement = "SELECT * FROM Produto WHERE Descricao LIKE ?";
      return _conn.QueryAsync<Produto>(statement, $"%{query}%");
    }
    
    public Task<List<Produto>> SearchByCategory(string query)
    {
      string statement = "SELECT * FROM Produto WHERE Categoria = ?";
      return _conn.QueryAsync<Produto>(statement, query);
    }
  }
}
