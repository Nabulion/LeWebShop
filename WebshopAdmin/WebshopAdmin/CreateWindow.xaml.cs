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
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        private Service.Service service;
        private DataGrid grid;
        public CreateWindow(Service.Service service, DataGrid grid)
        {
            this.grid = grid;
            this.service = service;
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            service.createProduct(TxtName.Text, Convert.ToDecimal(txtUnitprice.Text),Convert.ToInt32(txtCountAvailable.Text),txtPicture.Text,0,txtCountry.Text);
            grid.ItemsSource = null;
            grid.ItemsSource = service.getProducts();
            this.Close();
        }
    }
}