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
                            if((stack[j] == "*")|(stack[j] == "/"))
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
                            if ((stack[j] == "*") | (stack[j] == "/"))
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
                        stack[++top] = "*";
                    }
                    else if (inorder_array[i] == '/')
                    {
                        postorder_result.Text = postorder_result.Text + " ";
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
            int[] stack = new int[100];
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
                        stack[top - 1] = Convert.ToInt32(stack[top - 1] / stack[top--]);
                    }
                }
                else
                {
                    if(word.Length<10)
                        stack[++top] = int.Parse(word);
                }
            }
            decimal_result.Text = stack[0].ToString();
            binary_result.Text = Convert.ToString(stack[0], 2);
        }

        private void Preorder()
        {
            if (preorder_result.Text == null)
                preorder_result.Text = "0";
            else{
                preorder_result.Clear();
                string postorder = postorder_result.Text;
                string[] postorder_array = postorder.Split(' ');
                for(var i=postorder_array.Length-1; i>-1; i--)
                    preorder_result.Text += (postorder_array[i] + " ");
            }
        }
    }

}
