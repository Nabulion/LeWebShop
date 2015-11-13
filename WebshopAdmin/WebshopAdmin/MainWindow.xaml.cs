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
        public DataGrid grid = new DataGrid();
        public DataGrid gridPackage = new DataGrid();
        private ListBox box = new ListBox();
        private Service.Service service;
      
        public MainWindow()
        {
            service = new Service.Service();
            grid.ItemsSource = service.getProducts();
            grid.CanUserAddRows = false;
            gridPackage.CanUserAddRows = false;
            box.ItemsSource = service.getProducts();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clear();

            string[] buttons = { "Create", "Edit", "Delete" };

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

        private void Button_Click_Package(object sender, RoutedEventArgs e)
        {
            clear();
            string[] buttons = { "Create", "Edit", "Delete" };

            box.Width = 200;
            box.Height = 100;
            box.DataContext = service.getProducts();
            box.SelectionMode = SelectionMode.Multiple;
            sp_middle.Children.Add(box);

            Label l1 = new Label();
            l1.Content = "Name";
            sp_middle.Children.Add(l1);

            TextBox t1 = new TextBox();
            t1.Text = "";
            sp_middle.Children.Add(t1);


            Label l2 = new Label();
            l2.Content = "Price";
            sp_middle.Children.Add(l2);

            TextBox t2 = new TextBox();
            t2.Text = "";
            sp_middle.Children.Add(t2);

            gridPackage.Width = 275;
            gridPackage.Height = 200;
            sp_view.Children.Add(gridPackage);


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

                b.Click += new RoutedEventHandler(Button_Clicked_Package);
                sp_Options.Children.Add(b);
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
                        CreateWindow cw = new CreateWindow(service, grid);
                        cw.Show();
                        break;

                    case "Delete":
                        {
                            service.deleteProduct((Product)grid.SelectedItem);
                            grid.ItemsSource = null;
                            grid.ItemsSource = service.getProducts();
                        }
                        break;
                    case "Edit":
                        if (grid.SelectedItem != null)
                        {
                            EditWindow ew = new EditWindow(service, grid.SelectedIndex, grid.SelectedItem, grid);

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
        // Package functions.
        void Button_Clicked_Package(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "Create":

                        Package pac = new Package();
                        var textBox = sp_middle.Children.OfType<TextBox>().FirstOrDefault();
                        var textBox1 = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                        var listbox = sp_middle.Children.OfType<ListBox>().First();
                        pac.price = Convert.ToDecimal(textBox1.Text);
                        pac.name = textBox.Text;
                        List<Product> l = new List<Product>();
                        listbox.SelectionMode = SelectionMode.Multiple;

                        foreach (var i in listbox.SelectedItems)
                        {
                            l.Add((Product)i);
                        }
                        
                        service.createPackage(l,textBox.Text,Convert.ToDecimal(textBox1.Text));

                        gridPackage.ItemsSource = null;
                        gridPackage.ItemsSource = db.Packages.ToList();
                        
                        break;

                    case "Delete":
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            Package p = (Package)datatable.SelectedItem;
                            service.deletePackage(p);
                            gridPackage.ItemsSource = null;
                            gridPackage.ItemsSource = db.Packages.ToList();
                        }
                        break;
                    case "Edit":
                        if (gridPackage.SelectedItem != null)
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            var textboxname = sp_middle.Children.OfType<TextBox>().First();
                            var textboxprice = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                            var listboxproducts = sp_middle.Children.OfType<ListBox>().First();
                            List<Product> list = new List<Product>();
                            Package p = (Package)datatable.SelectedItem;
                            listboxproducts.SelectionMode = SelectionMode.Multiple;
                            foreach (var select in listboxproducts.SelectedItems)
                            {
                                list.Add((Product)select);
                            }

                            service.editpackage(list,p, textboxname.Text, Convert.ToDecimal(textboxprice.Text));
                            gridPackage.ItemsSource = null;
                            gridPackage.ItemsSource = db.Packages.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Vælg venligst en række fra datagrid");
                        }
                        break;
                }
            }
        }

        private void clear()
        {
            sp_Options.Children.Clear();
            sp_view.Children.Clear();
            sp_middle.Children.Clear();
        }

    }
}

