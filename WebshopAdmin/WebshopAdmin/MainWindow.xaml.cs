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

namespace WebshopAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int dummy = 0;
        DataGrid grid = new DataGrid();
        private Service.Service service;
        public MainWindow()
        {
            service = new Service.Service();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string[] buttons = { "Create", "Edit", "Delete" };

            if (dummy == 0)
            {
                dummy = +1;
                
                grid.Width = 275;
                grid.Height = 200;         
                sp_view.Children.Add(grid);
                

                for (int i = 0; i < buttons.Count(); i++)
                {
                    string name = buttons[i];
                    Button b = new Button();
                    b.Name = name;
                    b.Content = buttons[i];
                    var margin = b.Margin;
                    margin.Left = 5;
                    margin.Top = 10;
                    margin.Right = 5;
                    b.Margin = margin;

                    b.Click += new RoutedEventHandler(Button_Clicked);
                    sp_Options.Children.Add(b);
                }
            }
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "Create":
                        CreateWindow cw = new CreateWindow(service);
                        cw.Show();
                        break;

                    case "Edit":
                        if (grid.SelectedItem != null)
                        {
                            EditWindow ew = new EditWindow(service, grid.SelectedIndex);
                            ew.Show();
                        }
                        else
                        {
                            MessageBox.Show("Vælg venligst en række fra datagrid");
                        }

                        break;
                    
                }
            }
        }
    }
}
