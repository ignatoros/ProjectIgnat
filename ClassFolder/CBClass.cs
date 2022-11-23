using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectIgnat.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection = new SqlConnection(
              @"Data Source=(local)\SQLEXPRESS;" +
            "Initial Catalog=IgnatProject;" +
            "Integrated Security=True");
        DataTable dataTable;
        SqlDataAdapter sqlData;
        DataSet dataSet;

        public enum CBType
        {
            Role,
            Post,
            Employee,
            Status
        }

        private void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdRole, RoleName " +
                    "From dbo.[Role] Order by IdRole ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Role]");
                comboBox.ItemsSource = dataSet.Tables["[Role]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Role]"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Role]"].Columns["IdRole"].ToString();
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
        
        private void PostCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdPost, PostName " +
                    "From dbo.[Post] Order by IdPost ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Post]");
                comboBox.ItemsSource = dataSet.Tables["[Post]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Post]"].Columns["PostName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Post]"].Columns["IdPost"].ToString();
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

        private void EmployeeCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdEmployee, EmployeeName " +
                    "From dbo.[Employee] Order by IdEmployee ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Employee]");
                comboBox.ItemsSource = dataSet.Tables["[Employee]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Employee]"].Columns["EmployeeName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Employee]"].Columns["IdEmployee"].ToString();
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
        private void StatusCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select IdStatus, StatusName " +
                    "From dbo.[Status] Order by IdStatus ASC",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[Status]");
                comboBox.ItemsSource = dataSet.Tables["[Status]"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[Status]"].Columns["StatusName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["[Status]"].Columns["IdStatus"].ToString();
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

        public void LoadCB(ComboBox comboBox, CBType type)
        {
            switch (type)
            {
                case CBType.Role:
                    RoleCBLoad(comboBox);
                    break;
                case CBType.Post:
                    PostCBLoad(comboBox);
                    break;
                case CBType.Employee:
                    EmployeeCBLoad(comboBox);
                    break;
                case CBType.Status:
                    StatusCBLoad(comboBox);
                    break;
            }
        }
    }
}
