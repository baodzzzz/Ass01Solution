using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private readonly string adminEmail;
        private readonly string adminPassword;
        private IMemberRepository memberRepository;
        private ASS1_DBContext db = new ASS1_DBContext();
        public WindowLogin()
        {
            memberRepository = new MemberDAO();
            InitializeComponent();
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
            adminEmail = configuration["Admin:Email"];
            adminPassword = configuration["Admin:Password"];
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Password;
            if (email == adminEmail && password == adminPassword)
            {
                WindowMembers windowMembers = new WindowMembers();
                windowMembers.Show();

                // Close the current window
                this.Close();
            }
            else
            {
                bool authenLogin = memberRepository.AuthenticateUser(email, password);
                if (authenLogin)
                {
                    MessageBox.Show("Login Success!");
                    WindowOrders windowOrders = new WindowOrders();
                    windowOrders.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect email or password!");
                }
            }
           
        }
    }
}
