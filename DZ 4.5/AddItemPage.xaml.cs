using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DZ_4._5;
public partial class AddItemPage : ContentPage
{
    private ObservableCollection<Items> Products;
    private ObservableCollection<Items> Books;
    private List<string> selectedAuthors = new List<string>();

    public AddItemPage(ObservableCollection<Items> products, ObservableCollection<Items> books)
    {
        InitializeComponent();
        Products = products;
        Books = books;
        List<string> Authors = new List<string>()
            {
                "Тарас Шевченко",
                "Іван Франко",
                "Леся Українка",
                "Михайло Коцюбинський",
                "Василь Стефаник",
                "Олександр Довженко",
                "Павло Тичина",
                "Максим Рильський",
                "Володимир Сосюра",
                "Ліна Костенко",
                "Василь Симоненко",
                "Іван Драч",
                "Юрій Андрухович",
                "Оксана Забужко",
                "Сергій Жадан",
                "Юрій Винничук",
                "Андрій Курков",
                "Таня Малярчук",
                "Софія Андрухович",
                "Олег Коцарев"
            };
        foreach (var author in Authors)
        {
            var checkBox = new CheckBox();
            checkBox.CheckedChanged += OnCheckBoxCheckedChanged;
            var label = new Label { Text = author };
            var stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
            stackLayout.Children.Add(checkBox);
            stackLayout.Children.Add(label);
            AuthorsStackLayout.Children.Add(stackLayout);
        }
    }
    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var stackLayout = (StackLayout)checkBox.Parent;
        var label = (Label)stackLayout.Children[1];
        string author = label.Text;

        if (e.Value)
        {
            // Якщо CheckBox відмічений, додаємо автора до списку вибраних авторів
            selectedAuthors.Add(author);
        }
        else
        {
            // Якщо CheckBox не відмічений, видаляємо автора зі списку вибраних авторів
            selectedAuthors.Remove(author);
        }
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        bool inputValid = true;
        string errorMessage = "";

        var newItem = new Items
        {
            Name = NameEntry.Text,
            CountryOfOrigin = CountryEntry.Text,
            PackagingDate = PackagingDateDatePicker.Date,
            Description = DescriptionEntry.Text
        };

        if (string.IsNullOrWhiteSpace(newItem.Name) ||
        string.IsNullOrWhiteSpace(newItem.CountryOfOrigin) ||
        string.IsNullOrWhiteSpace(newItem.Description))
        {
            inputValid = false;
            errorMessage += "Не всі поля заповнені. Заповніть поля.\n";
        }
        else
        {
            if (!(double.TryParse(PriceEntry.Text, out double price) && price > 0))
            {
                inputValid = false;
                errorMessage += "Некоректна ціна. Введіть додатнє число більше за 0.\n";
            }
            else
            {
                newItem.Price = price;
                if (ItemTypePicker.SelectedItem != null && ItemTypePicker.SelectedItem.ToString() == "Книга")
                {
                    if (int.TryParse(PageCountEntry.Text, out int pageCount) && pageCount > 0 &&
                        !string.IsNullOrWhiteSpace(PublisherEntry.Text))
                    {
                        var newBook = new Books
                        {
                            Name = newItem.Name,
                            Price = newItem.Price,
                            CountryOfOrigin = newItem.CountryOfOrigin,
                            PackagingDate = newItem.PackagingDate,
                            Description = newItem.Description,
                            PageCount = pageCount,
                            Publisher = PublisherEntry.Text,
                            SelectedAuthors = string.Join(", ", selectedAuthors)
                        };
                        Books.Add(newBook);
                    }
                    else
                    {
                        inputValid = false;
                        errorMessage += "Некоректна кількість сторінок або не всі поля заповнені. Введіть додатнє ціле число більше за 0 або заповніть поля.\n";
                    }
                }
                else
                {
                    if (double.TryParse(QuantityEntry.Text, out double quantity) && quantity > 0 &&
                        !string.IsNullOrWhiteSpace(UnitOfMeasureEntry.Text))
                    {
                        var newProduct = new Products
                        {
                            Name = newItem.Name,
                            Price = newItem.Price,
                            CountryOfOrigin = newItem.CountryOfOrigin,
                            PackagingDate = newItem.PackagingDate,
                            Description = newItem.Description,
                            ExpirationDate = ExpirationDateEntryPicker.Date,
                            Quantity = quantity,
                            UnitOfMeasure = UnitOfMeasureEntry.Text
                        };
                        Products.Add(newProduct);
                    }
                    else
                    {
                        inputValid = false;
                        errorMessage += "Некоректна кількість або не всі поля заповнені. Введіть додатнє число більше за 0 або заповніть поля.\n";
                    }
                }
            }
        }

        if (inputValid)
        {
            Navigation.PopAsync();
        }
        else
        {
            DisplayAlert("Помилка", errorMessage, "OK");
        }
    }

    private void ItemTypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = ItemTypePicker.SelectedItem?.ToString();

        // Визначити, які поля потрібно зробити видимими або невидимими
        switch (selectedItem)
        {
            case "Продукт":
                ExpirationDateEntryText.IsVisible = true;
                ExpirationDateEntryPicker.IsVisible = true;
                QuantityEntry.IsVisible = true;
                UnitOfMeasureEntry.IsVisible = true;
                PageCountEntry.IsVisible = false;
                PublisherEntry.IsVisible = false;
                AuthorsStackLayout.IsVisible = false;
                AddButton.IsEnabled = true;
                break;

            case "Книга":
                ExpirationDateEntryText.IsVisible = false;
                ExpirationDateEntryPicker.IsVisible = false;
                QuantityEntry.IsVisible = false;
                UnitOfMeasureEntry.IsVisible = false;
                PageCountEntry.IsVisible = true;
                PublisherEntry.IsVisible = true;
                AuthorsStackLayout.IsVisible = true;
                AddButton.IsEnabled = true;
                break;

            default:
                ExpirationDateEntryPicker.IsVisible = false;
                QuantityEntry.IsVisible = false;
                UnitOfMeasureEntry.IsVisible = false;
                PageCountEntry.IsVisible = false;
                PublisherEntry.IsVisible = false;
                AuthorsStackLayout.IsVisible = false;
                AddButton.IsEnabled = false;
                break;
        }
    }
}