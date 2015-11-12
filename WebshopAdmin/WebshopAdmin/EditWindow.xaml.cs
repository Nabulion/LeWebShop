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
            this.selectedItem = selectedItem;
            this.service = service;
            this.rowIndex = rowIndex;
            this.grid = grid;
            InitializeComponent();

            TxtName.Text = service.filldata(service.getProducts()).Rows[rowIndex]["name"].ToString();
            txtUnitprice.Text = service.filldata(service.getProducts()).Rows[rowIndex]["unitPrice"].ToString();
            txtCountAvailable.Text = service.filldata(service.getProducts()).Rows[rowIndex]["countAvailable"].ToString();
            txtCountry.Text = service.filldata(service.getProducts()).Rows[rowIndex]["country"].ToString();
            txtPicture.Text = service.filldata(service.getProducts()).Rows[rowIndex]["pic"].ToString();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //service.updateRow(rowIndex, TxtName.Text, txtUnitprice.Text, txtCountAvailable, txtCountry.Text, txtPicture.Text);
            this.Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            service.updateProduct((Product)selectedItem,TxtName.Text, Convert.ToDecimal(txtUnitprice.Text),Convert.ToInt32(txtCountAvailable.Text),txtPicture.Text,0,txtCountry.Text);
            grid.ItemsSource = null;
            grid.ItemsSource = service.getProducts();
            this.Close();
        }

    }
}