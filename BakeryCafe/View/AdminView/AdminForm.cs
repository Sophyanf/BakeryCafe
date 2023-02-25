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
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.Add("Все категории");
            manufComboBox.Items.Add("Все производители");
            (await data.GetCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName));
            (await data.GetManufAsync()).ForEach(c => manufComboBox.Items.Add(c.ManufacturerName));
            categoryComboBox.SelectedIndex = 0;
           
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
                (await dataProduct.GetProductAsync("")).ForEach(c => listBox1.Items.Add(c.ToString()));
            }
            else
            {
                listBox1.Items.Clear();

                ((CategoryBakery)await data.GetProductCategoryAsync(categoryComboBox.Text, categoryData)).Products.ToList().ForEach(p => listBox1.Items.Add(p));
            }
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

