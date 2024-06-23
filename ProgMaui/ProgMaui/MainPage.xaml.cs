using ProgMaui.Views;

namespace ProgMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnProductsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage());
        }

        private async void OnOrdersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrdersPage());
        }

        private async void OnManageProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageProduct());
        }

    }

}
