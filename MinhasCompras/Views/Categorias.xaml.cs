using MinhasCompras.Models;
using System.Linq;

namespace MinhasCompras.Views;

public partial class Categorias : ContentPage
{
	public Categorias()
	{
		InitializeComponent();
	}

  protected override async void OnAppearing()
  {
    base.OnAppearing();
    await CarregarCategorias();
  }

  class CategoriaTotal
  {
    public string Categoria { get; set; }
    public decimal Total { get; set; }
  }

  private async Task CarregarCategorias()
  {
    try
    {
      IEnumerable<Produto> produtos = null;
      produtos = (BindingContext as ListView).ItemsSource as IEnumerable<Produto>;

      if (produtos == null)
      {
        await DisplayAlert("Ops!", "Não foi possível localizar os produtos.", "OK");
      }

      var agrupados = produtos
        .GroupBy(p => string.IsNullOrWhiteSpace(p.Categoria) ? "Sem catagoria" : p.Categoria)
        .Select(g => new CategoriaTotal
        {
          Categoria = g.Key,
          Total = g.Sum(p => p.Total)
        })
        .OrderByDescending(ct => ct.Total)
        .ToList();

        lista_categorias.ItemsSource = agrupados;
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops!", ex.Message, "OK");
    }
  }

  private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
  {
    try
    {
      string filter = (e.NewTextValue ?? "").Trim().ToLower();

      if (string.IsNullOrEmpty(filter))
      {
        await CarregarCategorias();
        return;
      }

      IEnumerable<CategoriaTotal> categorias = (IEnumerable<CategoriaTotal>)lista_categorias.ItemsSource;

      var lista = categorias.Where(c => (c.Categoria ?? "").ToLower().Contains(filter)).ToList();

      lista_categorias.ItemsSource = lista;
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }
}
