using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            foreach(var data in shopbdEntities.GetContext().ProductTypes.ToList())
            {
                boxtype.Items.Add(data.name);
            }
            
            foreach (var data in shopbdEntities.GetContext().suppliers.ToList())
            {
                boxsuppliers.Items.Add(data.name);
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] imgdata = System.IO.File.ReadAllBytes(boxpik.Text);
                int typeid = shopbdEntities.GetContext().ProductTypes.Where(p => p.name == boxtype.Text).ToList()[0].id;
                int suppliersidstat = shopbdEntities.GetContext().suppliers.Where(p => p.name == boxsuppliers.Text).ToList()[0].id;
                shopbdEntities.GetContext().products.Add(new products() { img = imgdata, name = boxname.Text, weight = Convert.ToInt32(boxw.Text), purchasePrice = Convert.ToInt32(boxpricepur.Text), salePrice = Convert.ToInt32(boxprice.Text), quantity = Convert.ToInt32(boxq.Text), typeid = typeid, suppliersid = suppliersidstat });
                shopbdEntities.GetContext().SaveChanges();
                ProductPage.pp.mainlist.ItemsSource = shopbdEntities.GetContext().products.ToList();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных");
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
