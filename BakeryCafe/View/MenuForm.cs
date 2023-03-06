using BakeryCafe.Controllers;
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
            if (categoryComboBox.SelectedIndex == 0)
            {
                label1.Text = "";
                label1.Text = await dataMenu.GetListCategoryStringAsync();
                listBox1.Items.Clear();
                dataMenu.GetSortProductsList("", radioButtonChange).ForEach(c => listBox1.Items.Add(c));
            }
            else
            {
                listBox1.Items.Clear();
                label1.Text = "";
                string chiperCategory = "Самая дешевая категоря: "  +Environment.NewLine + 
                    await dataMenu.GetCheaperCategoryStringAsync() ;
                string expenciveCategorye = "Самая дорогая категоря: " + Environment.NewLine 
                    + await dataMenu.GetExpenciveCategoryStringAsync();
                label1.Text = await dataMenu.GetCategoryStringAsync(categoryComboBox.SelectedItem.ToString()) + Environment.NewLine + chiperCategory + expenciveCategorye;
                 dataMenu.GetSortProductsList(categoryComboBox.SelectedItem.ToString(), radioButtonChange).ForEach(c => listBox1.Items.Add(c));

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked ==true) { radioButtonChange = 0; }
            categoryComboBox_SelectedIndexChanged(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) { radioButtonChange = 1; }
            categoryComboBox_SelectedIndexChanged(sender, e);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true) { radioButtonChange = 2; }
            categoryComboBox.SelectedIndexChanged += categoryComboBox_SelectedIndexChanged;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true) { radioButtonChange = 3; }
            categoryComboBox.SelectedIndexChanged += categoryComboBox_SelectedIndexChanged;
        }
    }
}
