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
        public DataGrid gridFAQ = new DataGrid();
        private ListBox boxProducts = new ListBox();
        private Service.Service service;
        private Image img = new Image();
        
        lewebshopEntities db = Dao.Database.db;
        public MainWindow()
        {
           
            service = new Service.Service();

            grid.ItemsSource = service.getProducts();
            grid.CanUserAddRows = false;


            gridPackage.ItemsSource = db.Packages.ToList();
            gridPackage.CanUserAddRows = false;
            gridPackage.IsReadOnly = true;


            boxProducts.ItemsSource = service.getProducts();

            gridFAQ.ItemsSource = db.FAQs.ToList();
            gridFAQ.CanUserAddRows = false;

            InitializeComponent();
        }

        //Følgende button_click metoder er til tab-knapperne øverst i gui'et
        //Denne metode sætter Products
        private void Button_Click_Products(object sender, RoutedEventArgs e)
        {
            clear();

            string[] buttons = { "Create", "Edit", "Delete" };

            grid.Width = 275;
            grid.Height = 200;
            grid.SelectionChanged += new SelectionChangedEventHandler(Event_SelectionChanged);
            
            sp_view.Children.Add(grid);



            sp_middle.Children.Add(img);


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

                b.Click += new RoutedEventHandler(Button_Clicked_Product);
                sp_Options.Children.Add(b);
            }
        }

        private void Event_SelectionChanged(object sender, RoutedEventArgs e)
        {
            List<Product> l = new List<Product>();
            l = db.Products.ToList();
            Product pe = new Product();
            if (grid.SelectedItem != null)
            {
                pe = (Product)grid.SelectedItem;
                img.Source = service.getImage(pe.name);
            }
            else
            {
                grid.SelectedItem = l.ElementAt(0);
            }
            
        }

        //Denne metode sætter package
        private void Button_Click_Package(object sender, RoutedEventArgs e)
        {
            clear();
            string[] buttons = { "Create", "Edit", "Delete" };

            boxProducts.Width = 200;
            boxProducts.Height = 100;
            boxProducts.DataContext = service.getProducts();
            boxProducts.SelectionMode = SelectionMode.Multiple;
            sp_middle.Children.Add(boxProducts);

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
            removeColumn();
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

        //denne metode sætter FAQ
        private void Button_Click_FAQ(object sender, RoutedEventArgs e)
        {
            clear();

            string[] buttons = { "Create", "Edit", "Delete" };


            Label l1 = new Label();
            l1.Content = "Question";
            l1.Width = 175;
            sp_middle.Children.Add(l1);

            TextBox t1 = new TextBox();
            t1.Text = "";
            sp_middle.Children.Add(t1);


            Label l2 = new Label();
            l2.Content = "Answer";
            sp_middle.Children.Add(l2);

            TextBox t2 = new TextBox();
            t2.Text = "";
            sp_middle.Children.Add(t2);

            gridFAQ.Width = 275;
            gridFAQ.Height = 200;
            sp_view.Children.Add(gridFAQ);


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

                b.Click += b_Click_FAQ;
                sp_Options.Children.Add(b);
            }


        }

        //Følgende Button_Clicked sætter CRUD options for de forskellige tabs
        //Product CRUD
        void Button_Clicked_Product(object sender, EventArgs e)
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

        // Package CRUD
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

                        service.createPackage(l, textBox.Text, Convert.ToDecimal(textBox1.Text));
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

                            service.editpackage(list, p, textboxname.Text, Convert.ToDecimal(textboxprice.Text));
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


        //FAQ crud
        void b_Click_FAQ(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "Create":
                        var textQuestion = sp_middle.Children.OfType<TextBox>().First();
                        var textAnswer = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                        service.createFAQ(textQuestion.Text, textAnswer.Text);
                        gridFAQ.ItemsSource = null;
                        gridFAQ.ItemsSource = db.FAQs.ToList();


                        break;

                    case "Delete":
                        {
                            service.deleteFAQ((FAQ)gridFAQ.SelectedItem);
                            gridFAQ.ItemsSource = null;
                            gridFAQ.ItemsSource = db.FAQs.ToList();

                        }
                        break;

                    case "Edit":
                        if (gridFAQ.SelectedItem != null)
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            var textboxQuestion = sp_middle.Children.OfType<TextBox>().First();
                            var textboxAnswer = sp_middle.Children.OfType<TextBox>().ElementAt(1);

                            FAQ faq = (FAQ)datatable.SelectedItem;
                            service.editFAQ(faq, textboxQuestion.Text, textboxAnswer.Text);
                            gridFAQ.ItemsSource = null;
                            gridFAQ.ItemsSource = db.FAQs.ToList();
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

        private void removeColumn()
        {
            foreach (DataGridColumn column in gridPackage.Columns)
            {
                if (column.Header.ToString() == "Products")
                {
                    gridPackage.Columns.Remove(column);
                    break;
                }
            }
        }

    }
}

