using System.Collections.ObjectModel;

namespace DZ_4._5;

public partial class RemoveItemPage : ContentPage
{
    private ObservableCollection<Items> Products;
    private ObservableCollection<Items> Books;

    public RemoveItemPage(ObservableCollection<Items> products, ObservableCollection<Items> books)
    {
        InitializeComponent();
        Products = products;
        Books = books;

        ItemTypePicker.SelectedIndexChanged += (sender, e) =>
        {
            var selectedItemType = ItemTypePicker.SelectedItem.ToString();

            // Встановлюємо відповідний джерело даних для пікера "Оберіть товар для видалення"
            if (selectedItemType == "Продукт")
            {
                // Отримуємо назви доданих продуктів
                var productNames = Products.Select(product => product.Name).ToList();
                ItemPicker.ItemsSource = productNames;
            }
            else if (selectedItemType == "Книга")
            {
                // Отримуємо назви доданих книг
                var bookNames = Books.Select(book => book.Name).ToList();
                ItemPicker.ItemsSource = bookNames;
            }
        };
    }

    private void RemoveButton_Clicked(object sender, EventArgs e)
    {
        var selectedItemName = ItemPicker.SelectedItem as string;

        if (selectedItemName != null)
        {
            // Отримуємо вибраний товар за іменем
            var selectedItem = Products.FirstOrDefault(product => product.Name == selectedItemName) ?? Books.FirstOrDefault(book => book.Name == selectedItemName);

            if (selectedItem != null)
            {
                if (Products.Contains(selectedItem))
                {
                    Products.Remove(selectedItem);
                }
                else if (Books.Contains(selectedItem))
                {
                    Books.Remove(selectedItem);
                }
            }
        }

        Navigation.PopAsync();
    }
}
