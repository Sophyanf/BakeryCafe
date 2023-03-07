using BakeryCafe.Controllers;
using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryCafe.View
{
    public partial class MenuForm : Form
    {
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;
        private ReportsController dataReports = ReportsController.Instance;
        private MenuController dataMenu = MenuController.Instance;
        List<PictureBox> pictureBoxes;
        ImageBlink imageBlink;
        public static string manufData = "manufacturer";
        public static string categoryData = "categoryBakery";
        int radioButtonChange = 0;
        public MenuForm()
        {
            InitializeComponent();
            pictureBoxes = new List<PictureBox> { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9 };
            listBox1.TopIndex = listBox1.TopIndex + (vScrollBar1.Value - 5);
            imageBlink = new ImageBlink(imageList1);
        }

        private async void MenuForm_Load(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                imageBlink.BlincIm(pictureBox);
            }
            categoryComboBox.Items.Clear();
           // manufComboBox.Items.Clear();
            categoryComboBox.Items.Add("Все категории");
          //  manufComboBox.Items.Add("Все производители");
            (await data.GetListCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName));
          //  (await data.GetListManufAsync()).ForEach(c => manufComboBox.Items.Add(c.ManufacturerName));
            categoryComboBox.SelectedIndex = 0;
          //  manufComboBox.SelectedIndex = 0;
        }
        
        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = "";
            if (categoryComboBox.SelectedIndex == 0)
            {
                label1.Text = await dataMenu.GetListCategoryStringAsync();
            }
            else
            {
                string chiperCategory = "Самая дешевая категоря: " + Environment.NewLine +
                    await dataMenu.GetCheaperCategoryStringAsync();
                string expenciveCategorye = "Самая дорогая категоря: " + Environment.NewLine
                    + await dataMenu.GetExpenciveCategoryStringAsync();
                label1.Text = await dataMenu.GetCategoryStringAsync(categoryComboBox.SelectedItem.ToString()) + Environment.NewLine + chiperCategory + expenciveCategorye;
            }
            ProductListboxFill(categoryComboBox.SelectedItem.ToString());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked ==true) { radioButtonChange = 0; }
            ProductListboxFill(categoryComboBox.SelectedItem.ToString());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) { radioButtonChange = 1; }
            ProductListboxFill(categoryComboBox.SelectedItem.ToString());
        }
       

      
        private void ProductListboxFill (string categoryname)
        {
            comboBox1.SelectedIndex = 0;
            listBox1.Items.Clear();
          if(categoryComboBox.SelectedIndex == 0) { categoryname = ""; }
            dataMenu.GetSortProductsList(categoryname, radioButtonChange).ForEach(c => listBox1.Items.Add(c));
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked == true) { radioButtonChange = 2; }
            ProductListboxFill(categoryComboBox.SelectedItem.ToString());
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton4.Checked == true) { radioButtonChange = 3; }
            ProductListboxFill(categoryComboBox.SelectedItem.ToString());
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label4.Text = "";
                numericUpDown1.Visible = false;
                dateTimePicker1.Visible = false;
                button1.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label4.Text = "Введите дату выпуска";
                numericUpDown1.Visible = false;
                dateTimePicker1.Visible = true;
                button1.Visible = true;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                label4.Text = "Введите стоимость";
                numericUpDown1.Visible = true;
                button1.Visible = true;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                label4.Text = "";
                numericUpDown1.Visible = false;
                dateTimePicker1.Visible = false;
                button1.Visible = false;
                groupBox2.Visible = true;
                comboBox2.Items.Add("Все производители");
                comboBox2.SelectedIndex = 0;
                (await data.GetListManufAsync()).ForEach(c => comboBox2.Items.Add(c.ManufacturerName));
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> prodList = new List<Product>();
            foreach (var item in listBox1.Items)
            {
                prodList.Add(item as Product);
            }
            if (prodList.Count != 0) { 
            listBox1.Items.Clear();
                if (comboBox1.SelectedIndex == 2)
                {
                    var prodListRez = prodList.Where(p => p.price > numericUpDown1.Value).ToList();
                    if (prodListRez != null) { prodListRez.ForEach(p => listBox1.Items.Add(p)); }
                    else MessageBox.Show("Нет таких продукто");
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    dateTimePicker1.MinDate = prodList.Min(p => p.dateOfManuf);
                    dateTimePicker1.MaxDate = prodList.Max(p => p.dateOfManuf);
                    var prodListRez = prodList.Where(p => p.dateOfManuf.Date == dateTimePicker1.Value.Date).ToList();
                    if (prodListRez != null) { prodListRez.ForEach(p => listBox1.Items.Add(p)); }
                    else MessageBox.Show("Нет таких продукто");
                }
                
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<Product> prodList = dataMenu.GetWeigt(comboBox2.SelectedItem.ToString(), (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            if (prodList != null) { prodList.ForEach(p => listBox1.Items.Add(p)); }
            else MessageBox.Show("Нет таких продукто");
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryComboBox.SelectedIndex = 0;
            string manuf = "";
            if (comboBox2.SelectedIndex != 0) { manuf = comboBox2.SelectedItem.ToString();}
            int min = (await dataProduct.GetListProductAsync("")).Min(p=> p.weight);
            int max = (await dataProduct.GetListProductAsync("")).Max(p => p.weight);
            numericUpDown2.Minimum = min;
            numericUpDown2.Maximum = max;
            numericUpDown3.Minimum = min;
            numericUpDown3.Maximum = max;
            numericUpDown2.Value = min;
            numericUpDown3.Value = max;
        }
    }
}
