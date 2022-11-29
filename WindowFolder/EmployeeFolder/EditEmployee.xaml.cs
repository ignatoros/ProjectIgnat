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

namespace ProjectIgnat.WindowFolder.EmployeeFolder
{

    public partial class EditEmployee : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cB;
        SqlDataReader dataReader;
        private string idUser;
        public EditEmployee()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cB.LoadCB(PostСb, CBClass.CBType.Post);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.Employee " +
                    $"Where IdEmployee='{VarialbleClass.UserId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                NameTb.Text = dataReader[1].ToString();
                SecondNameTb.Text = dataReader[2].ToString();
                LastNameTb.Text = dataReader[3].ToString();
                NumberTb.Text = dataReader[4].ToString();
                EmailTb.Text = dataReader[5].ToString();
                PostСb.SelectedValue = dataReader[6].ToString();
                idUser = dataReader[7].ToString();
                sqlCommand = new SqlCommand("Select * From dbo.[User] " +
                    $"Where IdUser='{idUser}'", sqlConnection);
                dataReader.Close();
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
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

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string znak = "!@#$%^&";
            string cif = "1234567890";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string bol = "QWERTYUIOPASDFGHJKLZXCVBNM";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MBClass.ErrorMB("Введите имя");
                NameTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(znak.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " !@#$%^&");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(cif.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " цифру");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(mal.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " строчную букву");
                PasswordTb.Focus();
            }
            else if (PasswordTb.Text.IndexOfAny(bol.ToCharArray()) < 0)
            {
                MBClass.ErrorMB("Пароль должен содержать" +
                    " заглавную букву");
                PasswordTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.[User] " +
                        $"Set UserName ='{LoginTb.Text}'," +
                        $"UserPassword='{PasswordTb.Text}'," +
                        $"IdRole=2 " +
                        $"Where IdUser='{idUser}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.Employee " +
                        $"Set EmployeeName='{NameTb.Text}'," +
                        $"EmployeeSecondName='{SecondNameTb.Text}'," +
                        $"EmployeeLastName='{LastNameTb.Text}'," +
                        $"EmployeeNumber='{NumberTb.Text}'," +
                        $"EmployeeEmail='{EmailTb.Text}'," +
                        $"IdPost='{PostСb.SelectedValue.ToString()}'," +
                        $"IdUser='{idUser}'" +
                        $"Where IdEmployee='{VarialbleClass.UserId}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Данные работника " +
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
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeAdmin().ShowDialog();
        }
    }
}
