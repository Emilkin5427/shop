using shop.Model;
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
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
    {
        public static SalesPage sp { get; set; }
        public SalesPage()
        {
            InitializeComponent();
            sp = this;
            List<SalesListModel> list = new List<SalesListModel>();
            foreach(var dt in shopbdEntities.GetContext().sales.ToList())
            {
                list.Add(new SalesListModel() { id=dt.id,user =dt.users.name,datetime=dt.datetime.ToString("g"),price=dt.price,status=dt.status});
            }
            mainlist.ItemsSource = list;
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {

            new AddSales().Show();
        }

        private void Get_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                SalesListModel data = (SalesListModel)mainlist.SelectedItem;
                if (data != null)
                {
                    sales sal = shopbdEntities.GetContext().sales.Where(p => p.id == data.id).ToList()[0];
                    new EditSales(sal).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                SalesListModel data = (SalesListModel)mainlist.SelectedItem;
                if (data != null)
                {

                    if (data.status == "Возврат")
                    {
                        MessageBox.Show("Товары возвращены, удаление невозможно");
                        return;
                    }
                    sales sal = shopbdEntities.GetContext().sales.Where(p => p.id == data.id).ToList()[0];
                    shopbdEntities.GetContext().sales.Remove(sal);
                    shopbdEntities.GetContext().SaveChanges();

                    List<SalesListModel> list = new List<SalesListModel>();
                    foreach (var dt in shopbdEntities.GetContext().sales.ToList())
                    {
                        list.Add(new SalesListModel() { id = dt.id, user = dt.users.name, datetime = dt.datetime.ToString("g"), price = dt.price, status = dt.status });
                    }
                    mainlist.ItemsSource = list;
                }
            }
        }
    }
}
