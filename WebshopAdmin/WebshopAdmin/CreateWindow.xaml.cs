﻿using System;
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

            InitializeComponent();
            this.grid = grid;
            this.service = service;
            this.grid.IsReadOnly = true;

            List<String> categories = new List<String>();
            categories.Add("Discount pris");
            categories.Add("Hverdags oste");
            categories.Add("Luxus oste");
            categories.Add("Eksklusive oste");
            categories.Add("Styk ost");
            categories.Add("Osteborde");

            comboBoxCategory.ItemsSource = categories.ToList();

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            bool new1 = false; 
            if(checkBoxNew.IsChecked == true){
                new1 = true;
            }
            if (txtCountAvailable.Text == "" || txtCountry.Text == "" || TxtName.Text == "" || txtUnitprice.Text == "" || comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show("Fill name, country, unit price, count available and select a category");
            }
            else
            {
                decimal d;
                if (decimal.TryParse(txtCountAvailable.Text, out d) && decimal.TryParse(txtUnitprice.Text, out d))
                {
                    service.createProduct(TxtName.Text, Convert.ToDecimal(txtUnitprice.Text), Convert.ToInt32(txtCountAvailable.Text), txtPicture.Text, txtCountry.Text, comboBoxCategory.SelectedItem.ToString(), txtDescription.Text, new1);
                    grid.ItemsSource = null;
                    grid.ItemsSource = service.getProducts();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Count Available and Unit price must be a number");
                }
                
            }
        }

    
    }
}