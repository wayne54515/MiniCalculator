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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace main
{
    /// <summary>
    /// DataShow.xaml 的互動邏輯
    /// </summary>
    public partial class DataShow : Window
    {
        string connStr = "server=127.0.0.1;" +
                            "port=3306;" +
                            "user id=root;" +
                            "password=0000;" +
                            "database=mini_calculation;" +
                            "charset=utf8;";

        public DataShow()
        {
            InitializeComponent();
            init();

        }

        private void init()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            String sql_find = "Select * from result;";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql_find, conn);
                MySqlDataReader myData = cmd.ExecuteReader();
                if (!myData.HasRows)
                {
                    // 如果沒有資料,顯示沒有資料的訊息 
                    Console.WriteLine("No data.");
                }
                else
                {
                    // 讀取資料並且顯示出來 
                    while (myData.Read())
                    {
                        String d_id = myData.GetString(0);
                        String d_inorder = myData.GetString(1);
                        String d_postorder = myData.GetString(2);
                        String d_preorder = myData.GetString(3);
                        String d_deci = myData.GetString(4);
                        String d_bi = myData.GetString(5);

                        result_table.Items.Add(new { id = d_id, inorder = d_inorder, postorder = d_postorder, preorder = d_preorder, deci = d_deci, bi = d_bi});
                    }
                }
                myData.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }

            conn.Close();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = result_table.SelectedItem;
            string id = (result_table.SelectedCells[0].Column.GetCellContent(selectedItem) as TextBlock).Text;
            //Console.WriteLine();
            if (selectedItem != null)
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                String sql_del = "Delete from result where id=" + id;
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql_del, conn);
                    MySqlDataReader myData = cmd.ExecuteReader();
                
                    myData.Close();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
                }

                conn.Close();
                result_table.Items.Remove(selectedItem);
            }
        }

        public class table_form{
            public string id { get; set; }
            public string inorder { get; set; }
            public string postorder { get; set; }
            public string preorder { get; set; }
            public string deci { get; set; }
            public string bi { get; set; }
        }

    }
}
