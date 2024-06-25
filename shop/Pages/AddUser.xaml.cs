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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            shopbdEntities.GetContext().users.Add(new users() { login=boxlogin.Text,name=boxname.Text,telephone=boxnum.Text});
            shopbdEntities.GetContext().SaveChanges();

            UsersPage.up.mainlist.ItemsSource = shopbdEntities.GetContext().users.Where(p => p.role == 0).ToList();
            this.Close();
        }
    }
}
