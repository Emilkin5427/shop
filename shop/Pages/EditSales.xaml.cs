using Newtonsoft.Json;
using shop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для EditSales.xaml
    /// </summary>
    public partial class EditSales : Window
    {
        private sales dt {  get; set; }
        public EditSales(sales data)
        {
            InitializeComponent();
            if(data.status== "Возврат") vstat.IsEnabled = false;
            boxuser.Text = data.users.name;
            dt = data;
            mainlist.ItemsSource = JsonConvert.DeserializeObject<List<SalesModel>>(data.allproducts);
            lbprice.Content = "Цена: "+ data.price+" руб.";
        }

        private void GetCh_Button(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            double sum = 0;
            var builder = new StringBuilder();
            var buyerList = new List<Goods>();
            foreach (var item in JsonConvert.DeserializeObject<List<SalesModel>>(dt.allproducts))
            {
                buyerList.Add(new Goods(item.product, item.id, item.price, item.quantity));
            }
            builder.AppendLine($"{"".PadRight(25, ' ')}Касса");
            foreach (var product in buyerList)
            {
                counter++;
                sum += product.price;
                builder.AppendLine($"{counter}.{product.name}");
                builder.AppendLine($"  Код:{product.code}");
                builder.AppendLine($"  Количество:{product.quantity}");
                builder.AppendLine($"  Стоимость{"".PadRight(40 - product.price.ToString().Length, '.')}{product.price}");
            }
            builder.AppendLine("".PadRight(51, '='));
            builder.AppendLine($"Итог{"".PadRight(46 - sum.ToString().Length, '.')}{sum}");
            File.WriteAllText("cheque.txt", builder.ToString());
            MessageBox.Show("Файл создан cheque.txt");
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {
            sales data = shopbdEntities.GetContext().sales.Where(p=>p.id==dt.id).ToList()[0];
            data.status = "Возврат";
            shopbdEntities.GetContext().SaveChanges();

            foreach (var dt in JsonConvert.DeserializeObject<List<SalesModel>>(data.allproducts))
            {

                products product = shopbdEntities.GetContext().products.Where(p => p.id == dt.id).ToList()[0];
                product.quantity = product.quantity + dt.quantity;
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
    }
    class Goods
    {
        public string name;
        public int code;
        public double price;
        public int quantity;
        public Goods(string name, int code, double price,int quantity)
        {
            this.name = name;
            this.code = code;
            this.price = price;
            this.quantity = quantity;
        }
    }
}
