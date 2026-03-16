using MinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
  ObservableCollection<Produto> produtos = [];

  public ListaProduto()
  {
    InitializeComponent();

    lista_produtos.ItemsSource = produtos;
  }

  protected override async void OnAppearing()
  {
    try
    {
      produtos.Clear();
      List<Produto> tmp = await App.Database.GetAll();
      tmp.ForEach(produto => produtos.Add(produto));
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }

  private void ToolbarItem_Clicked(object sender, EventArgs e)
  {
    try
    {
      Navigation.PushAsync(new Views.NovoProduto());
    }
    catch (Exception ex)
    {
      DisplayAlert("Ops", ex.Message, "OK");
    }
  }

  private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
  {
    try
    {
      string filter = e.NewTextValue;

      produtos.Clear();
      List<Produto> tmp = await App.Database.Search(filter);
      tmp.ForEach(produto => produtos.Add(produto));
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }

  private void OnSum(object sender, EventArgs e)
  {
    decimal sum = produtos.Sum(produto => produto.Total);

    string msg = $"Total: {sum:C}";
    DisplayAlert("Total dos produtos", msg, "OK");
  }

  private async void OnMenuItemClicked(object sender, EventArgs e)
  {
    try
    {
      MenuItem selectedItem = sender as MenuItem;
      Produto produto = selectedItem.BindingContext as Produto;

      bool confirm = await DisplayAlert("Remover item", "Tem certeza que deseja excluir o produto?", "Sim", "Não");
      if (!confirm) return;

      await App.Database.Delete(produto.Id);
      produtos.Remove(produto);
    }
    catch (Exception ex)
    {
      await DisplayAlert("Ops", ex.Message, "OK");
    }
  }

  private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
  {
    try
    {
      Produto produto = e.SelectedItem as Produto;

      Navigation.PushAsync(new Views.EditarProduto
      {
        BindingContext = produto,
      });
    }
    catch (Exception ex)
    {
      DisplayAlert("Ops", ex.Message, "OK");
    }
  }
}
