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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private users data {  get; set; }
        public EditUser(users data)
        {
            InitializeComponent();
            this.data = data;
            boxlogin.Text = data.login;
            boxname.Text = data.name;
            boxnum.Text = data.telephone;
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {

            users dt = shopbdEntities.GetContext().users.Where(p => p.id == data.id).ToList()[0];
            dt.telephone = boxnum.Text;
            dt.login = boxlogin.Text;
            dt.name = boxname.Text;
            shopbdEntities.GetContext().SaveChanges();
            UsersPage.up.mainlist.ItemsSource = shopbdEntities.GetContext().users.Where(p => p.role == 0).ToList();
            this.Close();
        }
    }
}
