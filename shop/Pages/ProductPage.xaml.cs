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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public static ProductPage pp { get; set; }
        public ProductPage()
        {
            InitializeComponent();
            pp = this;
            mainlist.ItemsSource = shopbdEntities.GetContext().products.ToList();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddProduct().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                products data = (products)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditProduct(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                products data = (products)mainlist.SelectedItem;
                if (data != null)
                {
                    shopbdEntities.GetContext().products.Remove(data);
                    shopbdEntities.GetContext().SaveChanges();
                    mainlist.ItemsSource = shopbdEntities.GetContext().products.ToList();
                }
            }
        }
    }
}
