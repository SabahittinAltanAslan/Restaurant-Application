using ProgMaui.Services;

namespace ProgMaui.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ApiService _apiService;

    public ProductsPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        LoadProducts();
    }

    private async void LoadProducts()
    {
        var products = await _apiService.GetProductsAsync();
        ProductsListView.ItemsSource = products;
    }
}
