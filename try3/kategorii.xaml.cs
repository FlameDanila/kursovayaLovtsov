using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace try3
{
    /// <summary>
    /// Логика взаимодействия для kategorii.xaml
    /// </summary>
    public partial class kategorii : Window
    {
        public kategorii()
        {
            InitializeComponent();
            update();
        }
        public void update()
        {
            var List = App.bd.Category.ToList();
            if (SearchText != null)
            {
                var poisk = SearchText.Text;
                List = App.bd.Category.Where(p => p.name.Contains(poisk)).ToList();
            }
            listView.ItemsSource = List;
        }

        private void AppendClick(object sender, RoutedEventArgs e)
        {
            Category fil = new Category
            {
                name = SearchText.Text,
            };
            App.bd.Category.Add(fil);
            App.bd.SaveChanges();

            update();
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            update();
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            var Body = App.bd.Category.Where(p => p.id == App.categoryChange.id).FirstOrDefault();

            Body.name = NameText.Text;

            App.bd.SaveChanges();

            update();
        }

        public void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string f = SearchText.Text;
            var listBook = App.bd.Category.Where(Name => Name.name.Contains(f)).ToList();

            listView.ItemsSource = listBook;
        }


        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Button button1 = (Button)sender;
            Category udal = (Category)button1.DataContext;

            App.bd.Category.Remove(udal);
            App.bd.SaveChanges();

            update();

            MessageBox.Show($"Удалён: {udal.name}");
        }

        private void RedactClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Category Body = (Category)button.DataContext;

            NameText.Text = Body.name;

            App.categoryChange = Body;
        }

        private void CategoryClick(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
