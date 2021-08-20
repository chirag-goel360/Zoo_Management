using System;
using System.Collections.Generic;
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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ZooManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;
        public MainWindow()
        {
            InitializeComponent();

            string connection = ConfigurationManager.ConnectionStrings["ZooManager.Properties.Settings.ConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connection);
            ShowZoos();
        }

        private void ShowZoos()
        {
            try
            {
                string query = "select * from Zoo";
                //SqlDataAdapter can be imagined like an Interface to make Tables usable by CSharp Objects.
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                using (sqlDataAdapter)
                {
                    DataTable zooTable = new DataTable();
                    sqlDataAdapter.Fill(zooTable);
                    //Which Information of the Table in Database should be Shown in our Listbox.
                    listZoos.DisplayMemberPath = "Location";
                    //Which Value of the Table in Database should be Delivered, When an Item from Listbox is Selected.
                    listZoos.SelectedValuePath = "Id";
                    //The Reference to the Data the Listbox should Populate.
                    listZoos.ItemsSource = zooTable.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
