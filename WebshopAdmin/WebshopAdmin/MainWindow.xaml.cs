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
        public DataGrid gridPackage;
        public DataGrid gridFAQ = new DataGrid();
        private ListBox boxProducts = new ListBox();
        private Service.Service service;
        private Image img = new Image();

        lewebshopEntities1 db = Dao.Database.db;
        public MainWindow()
        {
            InitializeComponent();
            service = new Service.Service();

            grid.ItemsSource = service.getProducts();
            grid.CanUserAddRows = false;
            grid.IsReadOnly = true;

            boxProducts.ItemsSource = service.getProducts();
            
            gridPackage = new DataGrid();
            gridPackage.ItemsSource = db.Packages.ToList();
            gridPackage.CanUserAddRows = false;
            gridPackage.IsReadOnly = true;
            
            

            gridFAQ.ItemsSource = db.FAQs.ToList();
            gridFAQ.CanUserAddRows = false;
            gridFAQ.IsReadOnly = true;

            
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
            boxProducts.ItemsSource = service.getProducts();
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

        private void btnMonthlySale_Click(object sender, RoutedEventArgs e)
        {
            clear();

            ComboBox boxProducts = new ComboBox();
            boxProducts.Width = 175;
            var margin1 = boxProducts.Margin;
            margin1.Bottom = 20;
            margin1.Left = 10;
            margin1.Top = 10;
            margin1.Right = 10;
            boxProducts.Margin = margin1;
            boxProducts.ItemsSource = service.getProducts();
            sp_middle.Children.Add(boxProducts);

            ComboBox boxPackages = new ComboBox();
            boxPackages.Width = 175;
            boxPackages.ItemsSource = db.Packages.ToList();
            sp_middle.Children.Add(boxPackages);

            Label labelProduct = new Label();
            labelProduct.Width = 200;
            labelProduct.Content = "Sale Products:";
            sp_view.Children.Add(labelProduct);

            ListBox saleProduct = new ListBox();
            saleProduct.Width = 200;
            saleProduct.Height = 90;
            sp_view.Children.Add(saleProduct);

            Label labelPackage = new Label();
            labelPackage.Width = 200;
            labelPackage.Content = "Sale Products:";
            sp_view.Children.Add(labelPackage);

            ListBox salePackage = new ListBox();
            salePackage.Width = 200;
            salePackage.Height = 90;
            sp_view.Children.Add(salePackage);



            string[] buttons = { "Add", "Remove"};

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

                b.Click += b_Click_MonthlySale;
                sp_Options.Children.Add(b);
            }


        }

        private void b_Click_MonthlySale(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "Add":
                        var comboProduct = sp_middle.Children.OfType<ComboBox>().First();
                        var comboPackage = sp_middle.Children.OfType<ComboBox>().ElementAt(1);
                        var listProduct = sp_view.Children.OfType<ListBox>().First();
                        var listPackage = sp_view.Children.OfType<ListBox>().ElementAt(1);

                        if (comboProduct.SelectedItem != null)
                        {
                            Product p = (Product)comboProduct.SelectedItem;
                            p.monthsale = true;
                            listProduct.ItemsSource = null;
                            listProduct.ItemsSource = service.getSaleProduct();
                        }

                        if (comboPackage.SelectedItem != null)
                        {
                            Package pac = (Package)comboPackage.SelectedItem;
                            pac.monthsale = true;
                            listPackage.ItemsSource = null;
                            listPackage.ItemsSource = service.getSalePackage();
                        }                  
                        else
                        {
                            MessageBox.Show("Select Products and/or Packages to go on sale from comboboxes");
                        }
                        break;

                    case "Remove":
                        {

                            if (gridFAQ.SelectedItem != null)
                            {
                                service.deleteFAQ((FAQ)gridFAQ.SelectedItem);
                                gridFAQ.ItemsSource = null;
                                gridFAQ.ItemsSource = db.FAQs.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Pick a FAQ");
                            }
                        }
                        break;

                    case "Edit":
                        if (gridFAQ.SelectedItem != null)
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            var textboxQuestion = sp_middle.Children.OfType<TextBox>().First();
                            var textboxAnswer = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                            FAQ faq = (FAQ)datatable.SelectedItem;
                            if (textboxQuestion.Text != "" && textboxAnswer.Text != "" && faq != null)
                            {
                                service.editFAQ(faq, textboxQuestion.Text, textboxAnswer.Text);
                                gridFAQ.ItemsSource = null;
                                gridFAQ.ItemsSource = db.FAQs.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Fill answer, question and pick a FAQ");
                            }
                        }
                        break;
                }
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
                            if (grid.SelectedItem != null)
                            {
                                grid.ItemsSource = null;
                                grid.ItemsSource = service.getProducts();
                            }
                            else
                            {
                                MessageBox.Show("Vælg et produkt");
                            }
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
                        if (textBox1.Text != null && textBox.Text != "") { 
                        pac.price = Convert.ToDecimal(textBox1.Text);
                        pac.name = textBox.Text;
                        }
                        List<Product> l = new List<Product>();
                        listbox.SelectionMode = SelectionMode.Multiple;

                        foreach (var i in listbox.SelectedItems)
                        {
                            l.Add((Product)i);
                        }


                        decimal d;
                        if (decimal.TryParse(textBox1.Text, out d) && textBox.Text != "")
                        {
                            service.createPackage(l, textBox.Text, Convert.ToDecimal(textBox1.Text));
                            gridPackage.ItemsSource = null;
                            gridPackage.ItemsSource = db.Packages.ToList();
                            
                        }
                        else
                        {
                            MessageBox.Show("Name must be letters and price must be a number");
                        }

                        break;

                    case "Delete":
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            Package p = (Package)datatable.SelectedItem;
                            if (p != null)
                            {
                                service.deletePackage(p);
                                gridPackage.ItemsSource = null;
                                gridPackage.ItemsSource = db.Packages.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Select a package");
                            }
                        }
                        break;
                    case "Edit":
                        {
                            
                                var datatable = sp_view.Children.OfType<DataGrid>().First();
                                var textboxname = sp_middle.Children.OfType<TextBox>().First();
                                var textboxprice = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                                var listboxproducts = sp_middle.Children.OfType<ListBox>().First();
                                List<Product> list = new List<Product>();

                                if (gridPackage.SelectedItem != null && decimal.TryParse(textboxprice.Text, out d) && !textboxname.Text.Equals("") && listboxproducts.SelectedItems != null)
                                {
                                    
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
                                    MessageBox.Show("Pick a row in the grid and insert name, price and pick the products");
                                }
                            
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
                        if (textAnswer.Text != "" && textQuestion.Text != "")
                        {
                            service.createFAQ(textQuestion.Text, textAnswer.Text);
                            gridFAQ.ItemsSource = null;
                            gridFAQ.ItemsSource = db.FAQs.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Fill answer and question");
                        }
                        break;

                    case "Delete":
                        {
                            
                            if (gridFAQ.SelectedItem != null)
                            {
                                service.deleteFAQ((FAQ)gridFAQ.SelectedItem);
                                gridFAQ.ItemsSource = null;
                                gridFAQ.ItemsSource = db.FAQs.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Pick a FAQ");
                            }
                        }
                        break;

                    case "Edit":
                        if (gridFAQ.SelectedItem != null)
                        {
                            var datatable = sp_view.Children.OfType<DataGrid>().First();
                            var textboxQuestion = sp_middle.Children.OfType<TextBox>().First();
                            var textboxAnswer = sp_middle.Children.OfType<TextBox>().ElementAt(1);
                            FAQ faq = (FAQ)datatable.SelectedItem;
                            if (textboxQuestion.Text != "" && textboxAnswer.Text != "" && faq != null)
                            {
                                service.editFAQ(faq, textboxQuestion.Text, textboxAnswer.Text);
                                gridFAQ.ItemsSource = null;
                                gridFAQ.ItemsSource = db.FAQs.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Fill answer, question and pick a FAQ");
                            }
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
        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            clear();
            string[] buttons = { "Hent" };

            Label l1 = new Label();
            l1.Content = "Start Dato";
            l1.Width = 175;
            sp_middle.Children.Add(l1);

            DatePicker dp = new DatePicker();
            sp_middle.Children.Add(dp);

            Label l2 = new Label();
            l2.Content = "Slut Dato";
            l2.Width = 175;
            sp_middle.Children.Add(l2);

            DatePicker dp2 = new DatePicker();
            sp_middle.Children.Add(dp2);

            Label l3 = new Label();
            l3.Content = "Gennemsnit Salg";
            l3.Width = 175;
            sp_view.Children.Add(l3);

            TextBox t1 = new TextBox();
            t1.Text = "";
            sp_view.Children.Add(t1);

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

                b.Click += b_Click_Stats;
                sp_Options.Children.Add(b);
            }


        }

        void b_Click_Stats(Object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "Hent":
                        var datepickerStart = sp_middle.Children.OfType<DatePicker>().First();
                        var datepickerEnd = sp_middle.Children.OfType<DatePicker>().ElementAt(1);
                        var textbox = sp_view.Children.OfType<TextBox>().First();
                        if (datepickerStart.SelectedDate != null && datepickerEnd != null)
                        {
                            int i = 0;
                            i = service.numberOfSales((DateTime)datepickerStart.SelectedDate, (DateTime)datepickerEnd.SelectedDate);
                            textbox.Text = i + "";
                        }

                        else
                        {
                            MessageBox.Show("Pick dates");
                        }
                        break;
                }
            }
        }
      

    }
}

