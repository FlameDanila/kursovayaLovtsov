using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace try3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            update();
            List<Category> categories = App.bd.Category.ToList();
            cat.ItemsSource = categories.Select(n => n.name).ToList();
        }
        public void update()
        {
            var List = App.bd.Product.ToList();
            if (SearchText != null)
            {
                var poisk = SearchText.Text;
                List = App.bd.Product.Where(p => p.Name.Contains(poisk)).ToList();
            }
            listView.ItemsSource = List;
        }

        private void AppendClick(object sender, RoutedEventArgs e)
        {
            Product fil = new Product
            {
                Name = SearchText.Text,
                Date = DateText.Text,
                Manufacturer = ManufacturerText.Text,
                Cost = Convert.ToInt32(CostText.Text),
                Category_id = cat.SelectedIndex + 1
            };
            App.bd.Product.Add(fil);
            App.bd.SaveChanges();

            update();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            var Body = App.bd.Product.Where(p => p.id == App.izmenenie.id).FirstOrDefault();

            Body.Name = NameText.Text;
            Body.Date = DateText.Text;
            Body.Manufacturer = ManufacturerText.Text;
            Body.Cost = Convert.ToInt32(CostText.Text);
            Body.Category_id = cat.SelectedIndex + 1;

            App.bd.SaveChanges();

            update();
        }

        public void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Combo.Text == "Название")
            {
                string f = SearchText.Text;
                var listBook = App.bd.Product.Where(Name => Name.Name.Contains(f)).ToList();

                listView.ItemsSource = listBook;
            }
            if (Combo.Text == "Стоимость")
            {
                if (SearchText.Text != "")
                {
                    try
                    {
                        int f = Convert.ToInt32(SearchText.Text);
                        var listBook = App.bd.Product.Where(m => m.Cost == f).ToList();

                        listView.ItemsSource = listBook;
                    }
                    catch(Exception) 
                    {
                        MessageBox.Show("Введите число");
                    }
                }
            }
            if (Combo.Text == "Категория")
            {
                string f = SearchText.Text;
                var listBook = App.bd.Product.Where(date => date.Manufacturer.Contains(f)).ToList();

                listView.ItemsSource = listBook;
            }
            if (Combo.Text == "Дата")
            {
                string f = SearchText.Text;
                var listBook = App.bd.Product.Where(date => date.Date.Contains(f)).ToList();

                listView.ItemsSource = listBook;
            }
        }
    

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Button button1 = (Button)sender;
            Product udal = (Product)button1.DataContext;

            App.bd.Product.Remove(udal);
            App.bd.SaveChanges();

            update();

            MessageBox.Show($"Удалён: {udal.Name}");
        }

        private void RedactClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product Body = (Product)button.DataContext;

            if (Body.Category_id == 1)
            {
                cat.SelectedItem = cat.Items[0];
            }
            else if (Body.Category_id == 2)
            {
                cat.SelectedItem = cat.Items[1];
            }
            else if (Body.Category_id == 3)
            {
                cat.SelectedItem = cat.Items[2];
            }

            NameText.Text = Body.Name;
            DateText.Text = Body.Date.ToString();
            ManufacturerText.Text = Body.Manufacturer;
            CostText.Text = Body.Cost.ToString();

            App.izmenenie = Body;
        }

        private void CategoryClick(object sender, RoutedEventArgs e)
        {
            kategorii window = new kategorii();
            window.Show();
            Close();
        }
        public new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void DigitsTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
