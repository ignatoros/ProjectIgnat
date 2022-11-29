using ProjectIgnat.ClassFolder;
using ProjectIgnat.WindowFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ProjectIgnat.WindowFolder.ServiceFolder
{
   
    public partial class AddService : Window
    {
        CBClass cBClass;
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        public AddService()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert Into dbo.[Request] " +
                "(ClientName,ClientSecondName,ClientLastName," +
                "ClientPhoneNumber,ProblemDescription,IdStatus,IdEmployee) " +
                $"Values ('{NameTb.Text}'," +
                $"'{SecondNameTb.Text}'," +
                $"'{LastNameTb.Text}'," +
                $"'{NumberTb.Text}'," +
                $"'{DescriptionTb.Text}'," +
                $"'{StatusСb.SelectedValue.ToString()}'," +
                $"'{EmployeeCb.SelectedValue.ToString()}')",
                sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Услуга " +
                $"успешно добавлена");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }          
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new ServiceAdmin().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.LoadCB(StatusСb, CBClass.CBType.Status);
            cBClass.LoadCB(EmployeeCb, CBClass.CBType.Employee);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new ServiceAdmin().ShowDialog();
        }
    }
}
