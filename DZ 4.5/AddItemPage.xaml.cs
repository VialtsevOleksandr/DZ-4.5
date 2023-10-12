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
                "����� ��������",
                "���� ������",
                "���� �������",
                "������� ������������",
                "������ ��������",
                "��������� ��������",
                "����� ������",
                "������ ���������",
                "��������� ������",
                "˳�� ��������",
                "������ ���������",
                "���� ����",
                "��� ����������",
                "������ �������",
                "����� �����",
                "��� ��������",
                "����� ������",
                "���� ��������",
                "����� ����������",
                "���� �������"
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
            // ���� CheckBox ��������, ������ ������ �� ������ �������� ������
            selectedAuthors.Add(author);
        }
        else
        {
            // ���� CheckBox �� ��������, ��������� ������ � ������ �������� ������
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
            errorMessage += "�� �� ���� ��������. �������� ����.\n";
        }
        else
        {
            if (!(double.TryParse(PriceEntry.Text, out double price) && price > 0))
            {
                inputValid = false;
                errorMessage += "���������� ����. ������ ������ ����� ����� �� 0.\n";
            }
            else
            {
                newItem.Price = price;
                if (ItemTypePicker.SelectedItem != null && ItemTypePicker.SelectedItem.ToString() == "�����")
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
                        errorMessage += "���������� ������� ������� ��� �� �� ���� ��������. ������ ������ ���� ����� ����� �� 0 ��� �������� ����.\n";
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
                        errorMessage += "���������� ������� ��� �� �� ���� ��������. ������ ������ ����� ����� �� 0 ��� �������� ����.\n";
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
            DisplayAlert("�������", errorMessage, "OK");
        }
    }

    private void ItemTypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = ItemTypePicker.SelectedItem?.ToString();

        // ���������, �� ���� ������� ������� �������� ��� ����������
        switch (selectedItem)
        {
            case "�������":
                ExpirationDateEntryText.IsVisible = true;
                ExpirationDateEntryPicker.IsVisible = true;
                QuantityEntry.IsVisible = true;
                UnitOfMeasureEntry.IsVisible = true;
                PageCountEntry.IsVisible = false;
                PublisherEntry.IsVisible = false;
                AuthorsStackLayout.IsVisible = false;
                AddButton.IsEnabled = true;
                break;

            case "�����":
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