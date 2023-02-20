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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BakeryCafe.View.AdminView
{
    public partial class AddNewProductForm : Form
    {
        private DataProductController data = DataProductController.Instance;
        private ProductController dataProduct = ProductController.Instance;

        public AddNewProductForm()
        {
            InitializeComponent();
            toolTip1.SetToolTip(categoryComboBox, "Если в списке нет нужной категории, введите наименование новой категории");
            toolTip1.SetToolTip(manyfComboBox, "Если в списке нет нужной категории, введите наименование новой категории");

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void AddNewProduct_Load(object sender, EventArgs e)
        {

        }

        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           (await data.GetCategoryBakeryAsync()).ForEach(c => categoryComboBox.Items.Add(c));
        }
        private async void manufCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            (await data.GetCategoryBakeryAsync()).ForEach(c => categoryComboBox.Items.Add(c));
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var category = await data.GetCategoryBakeryAsync();
            Product newProduct = new Product()
            {
                productName = textBox1.Text,
                weight = (int)numericUpDown1.Value,
                price = numericUpDown2.Value
            };
            if (category == null)
            {
                CategoryBakery newCategory = new CategoryBakery()
                {
                    CategoryName = categoryComboBox.Text,
                };
                newCategory.Products.Add(newProduct);
                var result = await data.AddServiceCategoryAsync(newCategory);
                if (result == false)
                {
                    MessageBox.Show("Что-то пошло не так!!! Проверьте категорию или услугу");
                    return;
                }
            }
            else
            {
                var result = await dataProduct.AddProductAsync(newProduct);
                if (result == false)
                {
                    MessageBox.Show("Что-то пошло не так!!! Проверьте услугу");
                    return;
                }
            }
            categoryComboBox.Items.Add(await data.GetCategoryBakeryAsync());
            categoryComboBox.Text = "";
        }
    }
}
