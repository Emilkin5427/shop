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
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public MainAdminPage()
        {
            InitializeComponent();
        }

        private void Ex_Button(object sender, RoutedEventArgs e)
        {

            MainWindow.mw.mainframe.Content = new Pages.AuthPage();
        }

        private void GetPageSuppliers_Button(object sender, RoutedEventArgs e)
        {
            mainframe.Content = new Pages.Suppliers();
        }

        private void GetUsers_Button(object sender, RoutedEventArgs e)
        {

            mainframe.Content = new Pages.UsersPage();
        }

        private void GetProduct_Button(object sender, RoutedEventArgs e)
        {

            mainframe.Content = new Pages.ProductPage();
        }

        private void GetSales_Button(object sender, RoutedEventArgs e)
        {

            mainframe.Content = new Pages.SalesPage();
        }
    }
}
