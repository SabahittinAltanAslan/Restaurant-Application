using ProgMaui.Entities;
using ProgMaui.Services;
using System.Collections.ObjectModel;

namespace ProgMaui.Views
{
    public partial class ManageProduct : ContentPage
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Product> _products;

        public ManageProduct()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await _apiService.GetProductsAsync();
            _products = new ObservableCollection<Product>(products);
            ProductsCollectionView.ItemsSource = _products;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var productId = (int)button.CommandParameter;
            await _apiService.DeleteProductAsync(productId);
            LoadProducts();
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            var newProductPage = new AddProductPage();
            newProductPage.ProductAdded += (source, product) =>
            {
                _products.Add(product);
            };
            await Navigation.PushAsync(newProductPage);
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Product selectedProduct)
            {
                var updateProductPage = new UpdateProductPage(selectedProduct);
                updateProductPage.ProductUpdated += (source, product) =>
                {
                    var index = _products.IndexOf(selectedProduct);
                    _products[index] = product;
                };
                await Navigation.PushAsync(updateProductPage);
            }
        }
    }
}
