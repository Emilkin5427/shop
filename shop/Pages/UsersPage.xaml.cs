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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public static UsersPage up {  get; set; }
        public UsersPage()
        {
            InitializeComponent();
            up = this;
            mainlist.ItemsSource = shopbdEntities.GetContext().users.Where(p => p.role==0).ToList();
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            new AddUser().Show();
        }

        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            if (mainlist.SelectedIndex != -1)
            {
                users data = (users)mainlist.SelectedItem;
                if (data != null)
                {
                    new EditUser(data).Show();
                }
            }
        }

        private void Dell_Button(object sender, RoutedEventArgs e)
        {

            if (mainlist.SelectedIndex != -1)
            {
                users data = (users)mainlist.SelectedItem;
                if (data != null)
                {
                    shopbdEntities.GetContext().users.Remove(data);
                    shopbdEntities.GetContext().SaveChanges();
                    mainlist.ItemsSource = shopbdEntities.GetContext().users.Where(p => p.role == 0).ToList();
                }
            }
        }
    }
}
