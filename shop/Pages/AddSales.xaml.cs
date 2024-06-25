using Newtonsoft.Json;
using shop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AddSales.xaml
    /// </summary>
    public partial class AddSales : Window
    {
        private static List<SalesModel> sales { get; set; }
        private int price = 0;
        public AddSales()
        {
            InitializeComponent();
            foreach (var data in shopbdEntities.GetContext().users.ToList())
            {
                boxuser.Items.Add(data.name);
            }
            foreach (var data in shopbdEntities.GetContext().products.ToList())
            {
                boxprod.Items.Add(data.name);
            }
            sales = new List<SalesModel>();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            if (boxuser.SelectedIndex == -1) return;
            users data = shopbdEntities.GetContext().users.Where(p => p.name == boxuser.Text).ToList()[0];
            shopbdEntities.GetContext().sales.Add(new sales() { userid = data.id, price = price,datetime=DateTime.Now,allproducts=JsonConvert.SerializeObject(sales),status="Выдано" });
            shopbdEntities.GetContext().SaveChanges();

            foreach(var dt in sales)
            {

                products product = shopbdEntities.GetContext().products.Where(p => p.id == dt.id).ToList()[0];
                product.quantity = product.quantity-dt.quantity;
                shopbdEntities.GetContext().SaveChanges();
            }


            List<SalesListModel> list = new List<SalesListModel>();
            foreach (var dt in shopbdEntities.GetContext().sales.ToList())
            {
                list.Add(new SalesListModel() { id = dt.id, user = dt.users.name, datetime = dt.datetime.ToString("g"), price = dt.price, status = dt.status });
            }
            SalesPage.sp.mainlist.ItemsSource = list;
            this.Close();
        }

        private void AddProduct_Button(object sender, RoutedEventArgs e)
        {
            products data = shopbdEntities.GetContext().products.Where(p => p.name== boxprod.Text).ToList()[0];
            if (data != null)
            {
                int i = 0;
                if(int.TryParse(boxq.Text, out i))
                {
                    if (i <= 0) return;
                    foreach(var dt in sales)
                    {
                        if(dt.id == data.id)
                        {
                            MessageBox.Show("Товар уже в корзине");
                            return;
                        }
                    }
                    if (data.quantity < i)
                    {
                        MessageBox.Show("В наличии осталось: "+ data.quantity+"шт.");

                        return;
                    }
                    sales.Add(new SalesModel() { id = data.id, product = data.name, price=data.salePrice*i, quantity =i});
                    mainlist.ItemsSource = null;
                    mainlist.ItemsSource = sales;
                    mainlist.Items.Refresh();
                    price += data.salePrice * i;
                    lbprice.Content = "Цена: " + price+" руб.";
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void DellProduct_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                SalesModel data = (SalesModel)mainlist.SelectedItem;
                if (data != null)
                {
                    price -= data.price;
                    lbprice.Content = "Цена: " + price + " руб.";
                    sales.Remove(data);
                    mainlist.ItemsSource = null;
                    mainlist.ItemsSource = sales;
                    mainlist.Items.Refresh();
                }
            }
        }
    }
}
