//Name : Tasvir Dineshbhai Rupareliya
//Student ID : 8829633

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace _8829633_FinalExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Inventory> inventoryitems = new List<Inventory>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void additem_Click(object sender, RoutedEventArgs e)
        {
            //THIS IS EMPTY VALIDATION CODE
            if (itemnumber.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Item Number");
            }
            else if (itemname.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Item Name");
            }
            else if (itemcost.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Item Cost");
            }
            else if (itemqty.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Item Quntity");
            }
            else
            {
                var invitem = new Inventory();

                try
                {
                    invitem.itemName = itemname.Text;

                    invitem.itemNumber = Int16.Parse(itemnumber.Text);

                    invitem.Cost = Double.Parse(itemcost.Text);

                    invitem.Quantity = Int16.Parse(itemqty.Text);

                    inventoryitems.Add(invitem);

                    loadgriddata(inventoryitems);
                }
                catch (Exception)
                {
                    //IF USER ENTER INVALID DATA
                    MessageBox.Show("Please Enter Valid Data");
                }
            }
         }

        private void loadgriddata(List<Inventory> items)
        {
            //LOAD THE DATA IN THE GRID
            datagrid.ItemsSource = items;

            datagrid.Items.Refresh();
        }

        private void SaveData(string itemsJson)
        {
            //SAVE THE DATA IN JSON FILE
            File.WriteAllText("inventorydata.json", itemsJson);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //SAVE DATA BY BUTTON CLICK
            string itemsJson = JsonConvert.SerializeObject(inventoryitems);
            SaveData(itemsJson);
        }

        private List<Inventory> LoadData()
        {
             //LOADING DATA BY JSON TO DATA GRID
             string itemsJson = File.ReadAllText("inventorydata.json");
             return JsonConvert.DeserializeObject<List<Inventory>>(itemsJson);                       
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            //LOAD DATA BY CLICKING BUTTON            
            inventoryitems = LoadData();            
            loadgriddata(inventoryitems);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (delete_field.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Item No You Want To Delete");
            }
            else
            {
                try
                {
                    //DELETE DATA FROM GRID BY NUMBER
                    var number = Int16.Parse(delete_field.Text);
                    var deleteitem = inventoryitems.Find(item => item.itemNumber == number);
                    inventoryitems.Remove(deleteitem);
                    loadgriddata(inventoryitems);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Enter Valid Item Number");
                }
            }
        }
    }
}
