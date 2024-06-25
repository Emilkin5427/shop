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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSuppliers.xaml
    /// </summary>
    public partial class AddSuppliers : Window
    {
        public AddSuppliers()
        {
            InitializeComponent();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            shopbdEntities.GetContext().suppliers.Add(new suppliers() {name=boxname.Text,address=boxaddres.Text,telephone=boxnum.Text });
            shopbdEntities.GetContext().SaveChanges();
            Suppliers.sup.suppliers.ItemsSource = shopbdEntities.GetContext().suppliers.ToList();
            this.Close();
        }
    }
}
