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
        public EditWindow(Service.Service service, int rowIndex)
        {
            this.service = service;
            this.rowIndex = rowIndex;
            InitializeComponent();
            //TxtName.Text = service.getDataTable().Rows[rowIndex]["name"].ToString();
            //txtUnitprice.Text = service.getDataTable().Rows[rowIndex]["unitPrice"].ToString();
            //txtCountAvailable = service.getDataTable().Rows[rowIndex]["countAvailable"].ToString();
            //txtCountry.Text = service.getDataTable().Rows[rowIndex]["country"].ToString();
            //txtPicture.Text = service.getDataTable().Rows[rowIndex]["pic"].ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //service.updateRow(rowIndex, TxtName.Text, txtUnitprice.Text, txtCountAvailable, txtCountry.Text, txtPicture.Text);
            this.Close();
        }

    }
}