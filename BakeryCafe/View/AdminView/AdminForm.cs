using BakeryCafe.Controllers;
using BakeryCafe.Model;
using BakeryCafe.View.AdminView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.View
{
    public partial class AdminForm : Form
    {
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        private ReportsController dataReports = ReportsController.Instance;
        List<PictureBox> pictureBoxes;
        ImageBlink imageBlink;
        public static string manufData = "manufacturer";
        public static string categoryData = "categoryBakery";

        public AdminForm()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox> { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
            listBox1.TopIndex = listBox1.TopIndex + (vScrollBar1.Value - 5);
            imageBlink = new ImageBlink(imageList1);
        }

        private async void AdminForm_Load(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                imageBlink.BlincIm(pictureBox);
            }
            categoryComboBox.Items.Clear();
            manufComboBox.Items.Clear();
            categoryComboBox.Items.Add("Все категории");
            manufComboBox.Items.Add("Все производители");
            //(await data.GetDataProductAsync(categoryData)).Cast<List<CategoryBakery>>().ToList().ForEach(c => categoryComboBox.Items.Add((CategoryBakery)c.CategoryName));
            (await data.GetListCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName));
            (await data.GetListManufAsync()).ForEach(c => manufComboBox.Items.Add(c.ManufacturerName));
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
                    (await dataProduct.GetListProductAsync("")).ForEach(c => listBox1.Items.Add(c + "производитель: " + c.Manufacturers.FirstOrDefault().ManufacturerName));
            }
              
                else
                {
                    listBox1.Items.Clear();

                listBox1.Items.Clear();
                (await filter(categoryComboBox.Text, manufComboBox.Text)).ForEach(c => listBox1.Items.Add(c)); 
                //if (categoryComboBox.SelectedIndex != 0) { MessageBox.Show("ср. стоимость" + (await data.AveragePriceAsync(categoryData, categoryComboBox.SelectedItem.ToString()))); }
            }
        }

     
        private async void manufComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryComboBox.SelectedIndex == 0 && manufComboBox.SelectedIndex == 0)
            {
                listBox1.Items.Clear();
                (await dataProduct.GetListProductAsync("")).ForEach(c => listBox1.Items.Add(c/* + "производитель: " +  c.Manufacturers.FirstOrDefault().ManufacturerName*/));
            }
            else
            {
               
                listBox1.Items.Clear();
                
                var  products = await filter(categoryComboBox.Text, manufComboBox.Text);
                foreach (var product in products)
                {
                    listBox1.Items.Add(product);
                }
                //if (manufComboBox.SelectedIndex != 0) { MessageBox.Show("ср. стоимость" + (await data.AveragePriceAsync(manufData, manufComboBox.SelectedItem.ToString()))); }
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
            RezultLabel.Text = "";
             
            if (listBox2.SelectedIndex < 4 )
            {
                groupBox1.Visible = true;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                comboBoxManuf.Items.Clear();
                (await data.GetListManufAsync()).ForEach(c => comboBoxManuf.Items.Add(c.ManufacturerName));
                comboBoxManuf.SelectedIndex = 0;
            }

            if (listBox2.SelectedIndex <= 1 )
            {
                numericUpDownPrice.Visible = false;
                label5.Visible = false;
                comboBoxManuf.SelectedIndexChanged += ComboBoxManuf_SelectedIndexChanged;
            }

            if (listBox2.SelectedIndex > 1 && listBox2.SelectedIndex < 4)
            {
                comboBoxManuf.SelectedIndexChanged += ComboBoxManuf_SelectedIndexChanged;
                label5.Visible = true;
                numericUpDownPrice.Visible = true;
            }

            if (listBox2.SelectedIndex == 4 )
            {
                groupBox1.Visible = false;
                groupBox3.Visible = false;
                groupBox2.Location = new System.Drawing.Point(310, 320);
                tabPage2.Controls.Add(groupBox2);
                groupBox2.Visible = true;
                decimal min= await data.GetCMinPriceAsync("");
                decimal max = await data.GetCMaxPriceAsync("");
                numericUpDownStart.Minimum = min;
                numericUpDownStart.Maximum = max;
                numericUpDownFinish.Minimum = min;
                numericUpDownFinish.Maximum = max;
                numericUpDownFinish.Value = numericUpDownFinish.Maximum;
            }

            if (listBox2.SelectedIndex == 5)
            {
                groupBox1.Visible = false;
                groupBox3.Visible = false;
                groupBox2.Location = new System.Drawing.Point(310, 320);
                tabPage2.Controls.Add(groupBox2);
                groupBox2.Visible = true;
                numericUpDownStart.Minimum = await data.GetCMinPriceAsync("");
                numericUpDownStart.Maximum = await data.GetCMaxPriceAsync("");
                label7.Text = "Введите стоимость";
                label6.Visible = false;
                numericUpDownFinish.Visible = false;
            }
          
        }

        private async void ComboBoxManuf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (numericUpDownPrice != null)
                try { 
                    numericUpDownPrice.Minimum = await data.GetCMinPriceAsync(comboBoxManuf.SelectedItem.ToString());
                    numericUpDownPrice.Maximum = await data.GetCMaxPriceAsync(comboBoxManuf.SelectedItem.ToString());
                }
                catch { MessageBox.Show("Нет продуктов выбранного производителя"); }
           
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            RezultLabel.Text = "";
            switch(listBox2.SelectedIndex)
            {
                case 0:
                    RezultLabel.Text += "Результат: " + Environment.NewLine +(await dataReports.ManufFromAllProducts(comboBoxManuf.SelectedItem.ToString())).ToString();
                    break;
                case 1:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + (await dataReports.ManufFromOtherManuf(comboBoxManuf.SelectedItem.ToString()));
                    break;
                case 2:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + "Доля более дорогих товаров: " + (await dataReports.expensiveManufPrice(comboBoxManuf.SelectedItem.ToString(),numericUpDownPrice.Value));
                    break;
                case 3:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + "Доля более дешевых товаров: " + (await dataReports.cheaperManufPrice(comboBoxManuf.SelectedItem.ToString(), numericUpDownPrice.Value));
                    break;
                case 4:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + "Доля товаров в интервале цен: " + (await dataReports.getPriceWithinLimits(numericUpDownStart.Value, numericUpDownFinish.Value));
                    break;
                case 5:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + "Доля товаров дешевле заданой: " + (await dataReports.getPriceChipper(numericUpDownStart.Value));
                    break;
                case 6:
                    RezultLabel.Text += "Результат: " + Environment.NewLine + "Доля товаров в интервале дат изготовления: " + (await dataReports.getDateWithinLimits(dateTimePicker1.Value, dateTimePicker2.Value));
                    break;
              

            }
        }
    }
}

