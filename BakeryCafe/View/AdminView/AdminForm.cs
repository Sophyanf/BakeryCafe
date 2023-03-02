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
           // MessageBox.Show("ср. стоимость " + data.AveragePriceAsync("", "Добрый"));
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            categoryComboBox.Items.Clear();
            manufComboBox.Items.Clear();
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
          if (new AddNewProductForm().ShowDialog()== DialogResult.OK)
            {
                AdminForm_Load(sender, e);
            }
        }

        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
               if (categoryComboBox.SelectedIndex == 0 && manufComboBox.SelectedIndex == 0)
                {
                    listBox1.Items.Clear();
                    (await dataProduct.GetProductAsync("")).ForEach(c => listBox1.Items.Add(c + "производитель: " + c.Manufacturers.FirstOrDefault().ManufacturerName));
            }
              
                else
                {
                    listBox1.Items.Clear();

                listBox1.Items.Clear();
                var products = await filter(categoryComboBox.Text, manufComboBox.Text);
                foreach (var product in products)
                {
                    listBox1.Items.Add(product);
                }
            }
        }

     
        private async void manufComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedIndex == 0 && manufComboBox.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                (await dataProduct.GetProductAsync("")).ForEach(c => listBox1.Items.Add(c + "производитель: " +  c.Manufacturers.FirstOrDefault().ManufacturerName));
            }
            else
            {
               
                listBox1.Items.Clear();
                
                var  products = await filter(categoryComboBox.Text, manufComboBox.Text);
                foreach (var product in products)
                {
                    listBox1.Items.Add(product);
                }
              //  if (manufComboBox.SelectedIndex != 0) { MessageBox.Show("ср. стоимость" + (await data.AveragePriceAsync("", manufComboBox.SelectedItem.ToString()))); }
            }
        }

        private async Task<List<Product>> filter(String category, String manuf)  // не получилось асинхронно + load
        {
            if (manuf == "Все производители") manuf= "";                       //подумать правильное условие если успею
            if (category == "Все категории") category = "";
            return dataProduct.load(category, manuf);
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (new EditProductForm(listBox1.SelectedItem as Product).ShowDialog() == DialogResult.OK)
                {
                    AdminForm_Load(sender, e);
                }
            }
            catch { MessageBox.Show("Выберите продукт из списка"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            removeProduct();
            AdminForm_Load(sender, e);
        }

        private void removeProduct ()
        {
            dataProduct.RemoveProduct(listBox1.SelectedItem as Product);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private async void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 4)
            {
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                label.AutoSize = true;
                label.Text="Выберите производителя";
                tabPage2.Controls.Add(label);
                label.Location = new System.Drawing.Point(380, 350);

                System.Windows.Forms.ComboBox comboBoxManuf = new System.Windows.Forms.ComboBox();
                (await data.GetManufAsync()).ForEach(c => comboBoxManuf.Items.Add(c.ManufacturerName));
                comboBoxManuf.Location = new System.Drawing.Point(380, 370);
                manufComboBox.Items.Add("Все производители");
                comboBoxManuf.SelectedIndex = 0;
                tabPage2.Controls.Add(comboBoxManuf);
                comboBoxManuf.Visible = true ;
            }
        }
    }
}

