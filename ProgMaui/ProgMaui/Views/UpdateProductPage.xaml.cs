using Microsoft.Maui.Controls;
using ProgMaui.Entities;
using ProgMaui.Services;

namespace ProgMaui.Views
{
    public partial class UpdateProductPage : ContentPage
    {
        public event EventHandler<Product> ProductUpdated;
        private Product _product;

        public UpdateProductPage(Product product)
        {
            InitializeComponent();
            _product = product;
            PopulateFields();
        }

        private void PopulateFields()
        {
            ProductNameEntry.Text = _product.Name;
            ProductPriceEntry.Text = _product.Price.ToString();
        }

        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            // Validate inputs if needed

            _product.Name = ProductNameEntry.Text;
            _product.Price = Convert.ToDecimal(ProductPriceEntry.Text);

            // Call API service to update product
            var apiService = new ApiService();
            await apiService.UpdateProductAsync(_product.Id, _product);

            // Notify subscribers that the product has been updated
            ProductUpdated?.Invoke(this, _product);

            // Navigate back
            await Navigation.PopAsync();
        }
    }
}
