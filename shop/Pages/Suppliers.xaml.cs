using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для Suppliers.xaml
    /// </summary>
    public partial class Suppliers : Page
    {
        public static Suppliers sup {  get; set; }
        public Suppliers()
        {
            InitializeComponent();
            sup = this;
            suppliers.ItemsSource = shopbdEntities.GetContext().suppliers.ToList();
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {
            if (suppliers.SelectedIndex != -1)
            {
                suppliers data = (suppliers)suppliers.SelectedItem;
                if (data != null)
                {
                    shopbdEntities.GetContext().suppliers.Remove(data);
                    shopbdEntities.GetContext().SaveChanges();
                    suppliers.ItemsSource = shopbdEntities.GetContext().suppliers.ToList();
                }
            }
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            if (suppliers.SelectedIndex != -1)
            {
                suppliers data = (suppliers)suppliers.SelectedItem;
                if (data != null)
                {
                    new EditSuppliers(data).Show();
                }
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddSuppliers().Show();
        }
    }
}
