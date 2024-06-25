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

namespace shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditSuppliers.xaml
    /// </summary>
    public partial class EditSuppliers : Window
    {
        private suppliers suppliers;
        public EditSuppliers( suppliers data )
        {
            InitializeComponent();
            suppliers=data;
            boxname.Text = data.name;
            boxnum.Text = data.telephone;
            boxaddres.Text = data.address;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            suppliers data = shopbdEntities.GetContext().suppliers.Where(p => p.id == suppliers.id).ToList()[0];
            data.telephone = boxnum.Text;
            data.address = boxaddres.Text;
            data.name = boxname.Text;
            shopbdEntities.GetContext().SaveChanges();
            Suppliers.sup.suppliers.ItemsSource = shopbdEntities.GetContext().suppliers.ToList();
            this.Close();
        }
    }
}
