using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace DZ_4._5
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Items> Products { get; set; }
        public ObservableCollection<Items> Books { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Products = new ObservableCollection<Items>();
            Books = new ObservableCollection<Items>();

            ProductsListView.ItemsSource = Products;
            BooksListView.ItemsSource = Books;
        }

        private async void AddItemButton_Clicked(object sender, EventArgs e)
        {
            var addItemPage = new AddItemPage(Products, Books);
            await Navigation.PushAsync(addItemPage);
        }

        private async void RemoveItemButton_Clicked(object sender, EventArgs e)
        {
            var removeItemPage = new RemoveItemPage(Products, Books);
            await Navigation.PushAsync(removeItemPage);
        }
    }
}