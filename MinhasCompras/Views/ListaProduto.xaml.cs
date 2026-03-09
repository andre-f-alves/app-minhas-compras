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
		List<Produto> tmp = await App.Database.GetAll();
		tmp.ForEach(produto => produtos.Add(produto));
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
		string filter = e.NewTextValue;
        
		produtos.Clear();
        List<Produto> tmp = await App.Database.Search(filter);
        tmp.ForEach(produto => produtos.Add(produto));
    }

	private void OnSum(object sender, EventArgs e)
	{
		decimal sum = produtos.Sum(produto => produto.Total);

		string msg = $"Total: {sum:C}";
		DisplayAlert("Total dos produtos", msg, "OK");
	}

	private async void OnRemoveItem(object sender, EventArgs e)
	{
		if (lista_produtos.SelectedItem == null) return;

		Produto produto = (Produto)lista_produtos.SelectedItem;
        await App.Database.Delete(produto.Id);

		produtos.Remove(produto);
	}
}