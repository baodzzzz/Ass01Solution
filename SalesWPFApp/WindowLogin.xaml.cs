using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private IMemberRepository memberRepository;
        private ASS1_DBContext db = new ASS1_DBContext();
        public WindowLogin()
        {
            memberRepository = new MemberDAO();
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Password;

            bool authenLogin = memberRepository.AuthenticateUser(email, password);
            if (authenLogin)
            {
                MessageBox.Show("Login Success!");
                WindowOrders windowOrders = new WindowOrders();
                windowOrders.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Incorrect email or password!");
            }
        }
    }
}
