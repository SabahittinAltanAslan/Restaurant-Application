using Microsoft.Maui.Controls;
using ProgMaui.Entities;
using ProgMaui.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgMaui.Views
{
    public partial class AddProductPage : ContentPage
    {
        public event EventHandler<Product> ProductAdded;
        private readonly ApiService _apiService;
        private List<Category> _categories;

        public AddProductPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                _categories = await _apiService.GetCategoriesAsync();

                // Populate Picker with categories
                foreach (var category in _categories)
                {
                    ProductCategoryPicker.Items.Add(category.Name);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate inputs if needed

            var newProduct = new Product
            {
                Name = ProductNameEntry.Text,
                Price = Convert.ToDecimal(ProductPriceEntry.Text),
                CategoryId = GetSelectedCategoryId()
            };

            // Call API service to add product
            await _apiService.AddProductAsync(newProduct);

            // Notify subscribers that a new product has been added
            ProductAdded?.Invoke(this, newProduct);

            // Navigate back
            await Navigation.PopAsync();
        }

        private int GetSelectedCategoryId()
        {
            var selectedIndex = ProductCategoryPicker.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _categories.Count)
            {
                return _categories[selectedIndex].Id;
            }
            return -1; // Handle case where no category is selected
        }
    }
}
