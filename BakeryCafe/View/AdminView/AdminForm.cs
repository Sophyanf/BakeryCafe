using BakeryCafe.Controllers;
using BakeryCafe.Model;
using BakeryCafe.View.AdminView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BakeryCafe.View
{
    public partial class AdminForm : Form
    {
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        public static string manufData = "manufacturer";
        public static string categoryData = "categoryBakery";

        public AdminForm()
        {
            InitializeComponent();
            listBox1.TopIndex = listBox1.TopIndex + (vScrollBar1.Value - 5);
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.Add("Все категории");
            manufComboBox.Items.Add("Все производители");
            (await data.GetCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName));
            (await data.GetManufAsync()).ForEach(c => manufComboBox.Items.Add(c.ManufacturerName));
            categoryComboBox.SelectedIndex = 0;
            manufComboBox.SelectedIndex = 0;
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           new AddNewProductForm().ShowDialog(); 
        }

        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
               if (categoryComboBox.SelectedIndex == 0)
                {
                    listBox1.Items.Clear();
                    (await dataProduct.GetProductAsync("")).ForEach(c => listBox1.Items.Add(c));
                }
                else
                {
                    listBox1.Items.Clear();

                listBox1.Items.Clear();
                var products = filter(categoryComboBox.Text, manufComboBox.Text);
                foreach (var product in products)
                {
                    listBox1.Items.Add(product);
                }
            }
        }

     
        private void manufComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manufComboBox.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                manufComboBox.SelectedIndexChanged += categoryComboBox_SelectedIndexChanged;
            }
            else
            {
                listBox1.Items.Clear();
                var products = filter(categoryComboBox.Text, manufComboBox.Text);
                foreach (var product in products)
                {
                    listBox1.Items.Add(product);
                }
                /* listBox1.Items.Clear();
                 var products = filter(categoryComboBox.Text, manufComboBox.Text);
                 foreach (var product in products)
                 {
                     listBox1.Items.Add(product.ToString());
                 }*/
            }
        }

        private List<Product> filter(String category, String manuf)
        {
            if (manuf == "Все производители") manuf= "";
            return dataProduct.load(category, manuf);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
             new EditProductForm(listBox1.SelectedItem as Product).ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

