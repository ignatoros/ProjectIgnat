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
    
    public partial class EditService : Window
    {
        CBClass cB;
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public EditService()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new ServiceAdmin().ShowDialog();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Request] " +
                    $"Set ClientName='{NameTb.Text}'," +
                    $"ClientSecondName='{SecondNameTb.Text}'," +
                    $"ClientLastName='{LastNameTb.Text}'," +
                    $"ClientPhoneNumber='{NumberTb.Text}'," +
                    $"ProblemDescription='{DescriptionTb.Text}'," +
                    $"IdStatus='{StatusСb.SelectedValue.ToString()}'," +
                    $"IdEmployee='{EmployeeCb.SelectedValue.ToString()}' " +
                    $"Where IdRequest='{VarialbleClass.UserId}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные менеджера " +
                    $"успешно отредактированы");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cB.LoadCB(StatusСb, CBClass.CBType.Status);
            cB.LoadCB(EmployeeCb, CBClass.CBType.Employee);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[Request] " +
                 $"Where IdRequest='{VarialbleClass.UserId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                NameTb.Text = dataReader[1].ToString();
                SecondNameTb.Text = dataReader[2].ToString();
                LastNameTb.Text = dataReader[3].ToString();
                NumberTb.Text = dataReader[4].ToString();
                DescriptionTb.Text = dataReader[5].ToString();
                StatusСb.SelectedValue = dataReader[6].ToString();
                EmployeeCb.SelectedValue = dataReader[7].ToString();
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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new ServiceAdmin().ShowDialog();
        }
    }
}
