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
using MySql.Data.MySqlClient;


namespace main
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }  

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (input_text.Text == "0")
                input_text.Clear();
            input_text.Text = input_text.Text + button.Content;
            Postorder();
            Preorder();
        }

        private void Operate_Click(object sender, RoutedEventArgs e)
        {
            if (input_text.Text.Length > 0 )
            {
                Button button = (Button)sender;
                input_text.Text = input_text.Text + button.Content;
                Postorder();
                Preorder();
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        { 
            if(input_text.Text.Length > 0)
            {
                if (input_text.Text != "0")
                {
                    input_text.Undo();
                    if(input_text.Text.Length == 0)
                        input_text.Text = "0";
                    Postorder();
                    Preorder();
                }
            }
            
        }

        private void Ac_Click(object sender, RoutedEventArgs e)
        {
            input_text.Clear();
            input_text.Text = "0";
            Postorder();
            Preorder();
        }

        private void Postorder()
        {
            string[] stack = new string[100];
            int top = -1;
            if (postorder_result.Text == null)
                postorder_result.Text = "0";
            else
            {
                postorder_result.Clear();
                string inorder = input_text.Text;
                char[] inorder_array = inorder.ToArray();
                for (var i = 0; i < inorder.Length; i++)
                {
                    if (inorder_array[i] == '+')
                    {
                        postorder_result.Text = postorder_result.Text + " ";
                        for(var j=top; j>-1; j--)
                        {
                            if((stack[j] == "*") | (stack[j] == "/") | (stack[j] == "+") | (stack[j] == "-"))
                            {
                                postorder_result.Text = postorder_result.Text + stack[j] + " ";
                                top--;
                            }
                        }
                        stack[++top] = "+";                   
                    }
                    else if (inorder_array[i] == '-')
                    {
                        postorder_result.Text = postorder_result.Text + " ";
                        for (var j = top; j > -1; j--)
                        {
                            if ((stack[j] == "*") | (stack[j] == "/") | (stack[j] == "+") | (stack[j] == "-"))
                            {
                                postorder_result.Text = postorder_result.Text + stack[j] + " ";
                                top--;
                            }
                        }
                        stack[++top] = "-";
                    }
                    else if (inorder_array[i] == '*')
                    {
                        postorder_result.Text = postorder_result.Text + " ";
                        for (var j = top; j > -1; j--)
                        {
                            if ((stack[j] == "*") | (stack[j] == "/"))
                            {
                                postorder_result.Text = postorder_result.Text + stack[j] + " ";
                                top--;
                            }
                        }
                        stack[++top] = "*";
                    }
                    else if (inorder_array[i] == '/')
                    {
                        postorder_result.Text = postorder_result.Text + " ";
                        for (var j = top; j > -1; j--)
                        {
                            if ((stack[j] == "*") | (stack[j] == "/"))
                            {
                                postorder_result.Text = postorder_result.Text + stack[j] + " ";
                                top--;
                            }
                        }
                        stack[++top] = "/";
                    }
                    else
                        postorder_result.Text = postorder_result.Text + inorder_array[i];
                }
                for (var i = top; i > -1; i--)
                {
                    postorder_result.Text = postorder_result.Text + " " + stack[i] ;
                    top--;
                }
            }
            Calculate();
        }

        private void Calculate()
        {
            double[] stack = new double[100];
            int top = -1;
            string postorder = postorder_result.Text;
            string[] postorder_array = postorder.Split(' ');
            foreach (var word in postorder_array)
            {
                if ((word == "+") | (word == "-") | (word == "*") | (word == "/") | (word == ""))
                { 
                    if ((word == "+") & (top > 0))
                    {
                        stack[top - 1] += stack[top--];
                    }
                    else if ((word == "-") & (top > 0))
                    {
                        stack[top - 1] -= stack[top--];
                    }
                    else if ((word == "*") & (top > 0))
                    {
                        stack[top - 1] *= stack[top--];
                    }
                    else if ((word == "/") & (top > 0))
                    {
                        //stack[top - 1] = Convert.ToInt32(stack[top - 1] / stack[top--]);
                        stack[top - 1] /= stack[top--];
                    }
                }
                else
                {
                    if(word.Length<10)
                        stack[++top] = int.Parse(word);
                }
            }

            decimal_result.Text = Math.Round(stack[0], 3).ToString();
            int integer = Convert.ToInt16(Math.Floor(stack[0]));
            binary_result.Text = Convert.ToString(integer, 2);
            double not_int = stack[0] - Math.Floor(stack[0]);
            if(not_int != 0){
                binary_result.Text = binary_result.Text + ".";
                deTobi(not_int);
            }
                
            //binary_result.Text = Convert.ToString(stack[0], 2);
            
        }

        private void deTobi(double not_int)
        {
            for(var i=0 ; (i<5) & (not_int!=0) ; i++)
            {
                not_int *= 2;
                if(not_int >= 1){
                    binary_result.Text = binary_result.Text + "1";
                    not_int-=1;
                }
                else
                    binary_result.Text = binary_result.Text + "0";
            }
        }

        private void Preorder()
        {
            if (preorder_result.Text == null)
                preorder_result.Text = "0";
            else
            {
                preorder_result.Clear();
                string postorder = postorder_result.Text;
                string[] postorder_array = postorder.Split(' ');
                for(var i=postorder_array.Length-1; i>-1; i--)
                    preorder_result.Text += (postorder_array[i] + " ");
            }
        }

        private void Save_Data(object sender, RoutedEventArgs e)
        {
            string connStr = "server=127.0.0.1;" +
                            "port=3306;" +
                            "user id=root;" +
                            "password=0000;" +
                            "database=mini_calculation;" +
                            "charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            String sql_find = "Select * from result where postorder='" + postorder_result.Text.ToString() + "'";
            //String sql_find = "Select * from result";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql_find, conn);
                MySqlDataReader myData = cmd.ExecuteReader();
                if (!myData.HasRows)
                {
                    myData.Close();
                    // 如果沒有資料,顯示沒有資料的訊息 
                    Console.WriteLine("No data.");
                    String sql_insert = "Insert into result(inorder, postorder, preorder, deci, bi) values" +
                    "('" + input_text.Text.ToString() + "','" + postorder_result.Text.ToString() +
                    "','" + preorder_result.Text.ToString() + "','" + decimal_result.Text.ToString() +
                    "','" + binary_result.Text.ToString() + "');";

                    cmd = new MySqlCommand(sql_insert, conn);
                    myData = cmd.ExecuteReader();
                    MessageBox.Show("Insert Success");
                }
                else
                {
                    // 讀取資料並且顯示出來 
                    /*while (myData.Read())
                    {
                        Console.WriteLine("Text={0}", myData.GetString(0));
                    }*/
                    MessageBox.Show("Data Exist");
                    myData.Close();
                }
                myData.Close();
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }

            conn.Close();
        }

        private void Show_Data(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DataShow w_data = new DataShow();
            w_data.Show();

            /*string connStr = "server=127.0.0.1;" +
                            "port=3306;" +
                            "user id=root;" +
                            "password=0000;" +
                            "database=mini_calculation;" +
                            "charset=utf8;";
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
                        Console.WriteLine("id={0}", myData.GetString(0));
                        Console.WriteLine("inorder={0}", myData.GetString(1));
                        Console.WriteLine("postorder={0}", myData.GetString(2));
                        Console.WriteLine("preorder={0}", myData.GetString(3));
                        Console.WriteLine("decimal={0}", myData.GetString(4));
                        Console.WriteLine("binary={0}", myData.GetString(5));
                    }
                }
                myData.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }

            conn.Close();*/
        }
    }

}
