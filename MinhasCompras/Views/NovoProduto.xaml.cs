using MinhasCompras.Models;

namespace MinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
  public NovoProduto()
  {
    InitializeComponent();
  }

  private async void ToolbarItem_Clicked(object sender, EventArgs e)
  {
    try
    {
      Produto produto = new()
      {
        Descricao = descricao.Text,
        Quantidade = Convert.ToDecimal(quantidade.Text),
        Preco = Convert.ToDecimal(preco.Text)
      };

      await App.Database.Insert(produto);
      await DisplayAlert("Sucesso!", "Produto registrado", "OK");
      await Navigation.PopAsync();
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }
}
