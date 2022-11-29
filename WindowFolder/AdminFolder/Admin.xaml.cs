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

namespace ProjectIgnat.WindowFolder.AdminFolder
{
    
    public partial class Admin : Window
    {
        SqlConnection sqlConnection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        DGClass dG;
    
        public Admin()
        {
            InitializeComponent();
            dG = new DGClass(ListUserDG);
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().ShowDialog();
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VarialbleClass.UserId = dG.SelectId();
                    new EditUser().ShowDialog();
                    dG.LoadDG("Select * From dbo.[User]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.LoadDG("Select * From dbo.[User] " +
                $"Where UserName Like '%{SearchTb.Text}%'");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddUser().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dG.LoadDG("SELECT * From dbo.[User]");
        }

        private void MenuRabIm_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeFolder.EmployeeAdmin().ShowDialog();
        }
    }
}
