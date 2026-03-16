using MinhasCompras.Models;

namespace MinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
  public EditarProduto()
  {
    InitializeComponent();
  }

  private async void OnToolbarItemClicked(object sender, EventArgs e)
  {
    try
    {
      Produto binding = BindingContext as Produto;
      Produto produto = new()
      {
        Id = binding.Id,
        Descricao = descricao.Text,
        Quantidade = Convert.ToDecimal(quantidade.Text),
        Preco = Convert.ToDecimal(preco.Text)
      };

      await App.Database.Update(produto);
      await DisplayAlert("Sucesso!", "Produto atualizado", "OK");
      await Navigation.PopAsync();
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }
}
