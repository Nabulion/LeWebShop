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

namespace WebshopAdmin
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Service.Service service;
        private int rowIndex;
        private object selectedItem;
        private DataGrid grid;
        public EditWindow(Service.Service service, int rowIndex, object selectedItem, DataGrid grid)
        {

            InitializeComponent();
            this.selectedItem = selectedItem;
            this.service = service;
            this.rowIndex = rowIndex;
            this.grid = grid;

            List<String> categories = new List<String>();
            categories.Add("Discount pris");
            categories.Add("Hverdags oste");
            categories.Add("Luxus oste");
            categories.Add("Eksklusive oste");
            categories.Add("Styk ost");
            categories.Add("Osteborde");

            comboBoxCategoryEdit.ItemsSource = categories.ToList();

            

            TxtName.Text = service.filldata(service.getProducts()).Rows[rowIndex]["name"].ToString();
            txtUnitprice.Text = service.filldata(service.getProducts()).Rows[rowIndex]["unitPrice"].ToString();
            txtCountAvailable.Text = service.filldata(service.getProducts()).Rows[rowIndex]["countAvailable"].ToString();
            txtCountry.Text = service.filldata(service.getProducts()).Rows[rowIndex]["country"].ToString();
           
            

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {   
            bool new1 = false; 
            if(checkBoxNew.IsChecked == true){
                new1 = true;
            }
            decimal d;
            if (selectedItem != null && TxtName.Text != "" && decimal.TryParse(txtUnitprice.Text, out d) && decimal.TryParse(txtCountAvailable.Text, out d) && txtCountry.Text != "" && comboBoxCategoryEdit.SelectedItem != null)
            {
                service.updateProduct((Product)selectedItem, TxtName.Text, Convert.ToDecimal(txtUnitprice.Text), Convert.ToInt32(txtCountAvailable.Text), txtPicture.Text, 0, txtCountry.Text, comboBoxCategoryEdit.SelectedItem.ToString(), new1);
                grid.ItemsSource = null;
                grid.ItemsSource = service.getProducts();
                this.Close();
            }
            else
            {
                MessageBox.Show("Udfyld alle felter, på nær picture og new product");
            }
        }

    }
}