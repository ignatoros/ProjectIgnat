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
    
    public partial class AddEmployee : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cB;
        SqlDataReader dataReader;

        public AddEmployee()
        {
            InitializeComponent();
            cB = new CBClass();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeAdmin().ShowDialog();
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
            else if (string.IsNullOrWhiteSpace(LastNameTb.Text))
            {
                MBClass.ErrorMB("Введите фамилию");
                LastNameTb.Focus();
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
                int? id = null;
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.[User] " +
                        "(UserName,UserPassword,IdRole) " +
                        $"Values ('{LoginTb.Text}'," +
                        $"'{PasswordTb.Text}'," +
                        $"2)",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand = new SqlCommand(
                    "SELECT * From dbo.[User] " +
                    $"Where UserName='{LoginTb.Text}'",
                    sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();
                    id = int.Parse(dataReader[0].ToString());
                    dataReader.Close();
                    sqlCommand = new SqlCommand(
                        "Insert Into dbo.[Employee] " +
                        "(EmployeeName,EmployeeSecondName,EmployeeLastName," +
                        "EmployeeNumber,EmployeeEmail,IdPost,IdUser) " +
                        $"Values ('{NameTb.Text}'," +
                        $"'{SecondNameTb.Text}'," +
                        $"'{LastNameTb.Text}'," +
                        $"'{NumberTb.Text}'," +
                         $"'{EmailTb.Text}'," +
                        $"'{PostСb.SelectedValue.ToString()}'," +
                        $"'{id.ToString()}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB("Успешно добавлен");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cB.LoadCB(PostСb, CBClass.CBType.Post);
        }
    }
}
