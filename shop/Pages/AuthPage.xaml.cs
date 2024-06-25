using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void Auth_Button(object sender, RoutedEventArgs e)
        {
            List<users> user = shopbdEntities.GetContext().users.Where(p => p.login == loginbox.Text && p.password == passwordbox.Text).ToList();
            if (user.Count == 1)
            {
                if (user[0].role==1)
                {
                    MainWindow.mw.mainframe.Content = new Pages.MainAdminPage();
                }
                else
                {
                    MessageBox.Show("Нет прав");

                }
                
                return;
            }
            MessageBox.Show("Не верный логин или пароль");
        }
    }
}
