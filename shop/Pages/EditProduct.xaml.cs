using Microsoft.Win32;
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
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        public products dt {  get; set; }
        public EditProduct(products dt)
        {
            InitializeComponent();
            this.dt = dt;
            foreach (var data in shopbdEntities.GetContext().ProductTypes.ToList())
            {
                boxtype.Items.Add(data.name);
            }

            foreach (var data in shopbdEntities.GetContext().suppliers.ToList())
            {
                boxsuppliers.Items.Add(data.name);
            }
            boxname.Text = dt.name;
            boxw.Text = dt.weight.ToString();
            boxpricepur.Text = dt.purchasePrice.ToString();
            boxprice.Text = dt.salePrice.ToString();
            boxq.Text = dt.quantity.ToString();
            boxsuppliers.Text = dt.suppliers.name;
            boxtype.Text = dt.ProductTypes.name;

        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                products data = shopbdEntities.GetContext().products.Where(p => p.id == dt.id).ToList()[0];

                int typeid = shopbdEntities.GetContext().ProductTypes.Where(p => p.name == boxtype.Text).ToList()[0].id;
                int suppliersidstat = shopbdEntities.GetContext().suppliers.Where(p => p.name == boxsuppliers.Text).ToList()[0].id;
                data.typeid = typeid;
                data.suppliersid = suppliersidstat;

                data.name = boxname.Text;
                data.weight = Convert.ToInt32(boxw.Text);
                data.purchasePrice = Convert.ToInt32(boxpricepur.Text);
                data.salePrice = Convert.ToInt32(boxprice.Text);
                data.quantity = Convert.ToInt32(boxq.Text);
                if (boxpik.Text == "")
                {

                }
                else
                {
                    data.img = System.IO.File.ReadAllBytes(boxpik.Text);
                }

                shopbdEntities.GetContext().SaveChanges();
                ProductPage.pp.mainlist.ItemsSource = shopbdEntities.GetContext().products.ToList();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Картинка (*.png)|*.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if ((bool)openFileDialog1.ShowDialog())
            {
                boxpik.Text = openFileDialog1.FileName;
            }
        }
    }
}
