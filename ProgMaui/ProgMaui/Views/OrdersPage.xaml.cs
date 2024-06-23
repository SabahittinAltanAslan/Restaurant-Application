using ProgMaui.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace ProgMaui.Views
{
    public partial class OrdersPage : ContentPage
    {
        private readonly ApiService _apiService;

        public OrdersPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            LoadOrders();
        }

        private async void LoadOrders()
        {
            var orders = await _apiService.GetOrdersAsync();
            OrdersListView.ItemsSource = orders;
        }
    }
}
