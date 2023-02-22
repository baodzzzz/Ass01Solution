using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers : Window
    {
        public ASS1_DBContext db = new ASS1_DBContext();
        private IMemberRepository memberRepository;
        public WindowMembers()
        {
            InitializeComponent();
            memberRepository = new MemberDAO();
        }

        private void LoadData()
        {
            var members = db.Members.Select(m => new
                {
                    id = m.MemberId,
                    email = m.Email,
                    companyName = m.CompanyName,
                    city = m.City,
                    country = m.Country,
                }).ToList();

            lvMember.ItemsSource = members;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public bool CheckEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Member member = new Member();
            if (!CheckEmail(txtEmail.Text))
            {
                MessageBox.Show("Bạn phải đúng định dạng email", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtEmail.Focus();
                return;
            }
            else
            {
                member.Email = txtEmail.Text;
                member.CompanyName = txtCompanyName.Text;
                member.City = txtCity.Text;
                member.Country = txtCountry.Text;
                member.Password = txtPassword.Text;
                memberRepository.addMember(member);
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmail(txtEmail.Text))
            {
                MessageBox.Show("Bạn phải đúng định dạng email", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtEmail.Focus();
                return;
            }
            else
            {
                Member member = new Member
                {
                    MemberId = int.Parse(txtMemId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
                memberRepository.editMember(member);
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Member member = db.Members.FirstOrDefault(x => x.MemberId == int.Parse(txtMemId.Text));
            memberRepository.deleteMember(member);
            LoadData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtMemId.Text = "";
            txtEmail.Text = "";
            txtCompanyName.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
            txtPassword.Text = "";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WindowMembers windowMembers = new WindowMembers();
            windowMembers.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            WindowOrders windowOrders = new WindowOrders();
            windowOrders.Show();
            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WindowProducts windowProducts = new WindowProducts();
            windowProducts.Show();
            this.Close();
        }
    }
}
