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

        private async void AddNewProduct_Load(object sender, EventArgs e)
        {
            (await data.GetCategoryBakeryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName));
        }

        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            (await data.GetCategoryBakeryAsync()).ForEach(c => categoryComboBox.Items.Add(c));
        }
        private async void manufCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            (await data.GetCategoryBakeryAsync()).ForEach(c => manyfComboBox.Items.Add(c));
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var category = await data.GetCategoryAsync(categoryComboBox.Text); // проверяем есть ли уже категория
            Product newProduct = new Product()  // создаем новый продукт из текстбоксов и пр.
            {
                productName = textBox1.Text,
                weight = (int)numericUpDown1.Value,
                price = numericUpDown2.Value
            };
            if (category == null) // если категории нет
            {
                CategoryBakery newCategory = new CategoryBakery()  // создаем категорию
                {
                    CategoryName = categoryComboBox.Text,
                    AveragePrice = 0
                };
                // await data.AddCategoryAsync(newCategory);
                //newProduct.CategoryBakerys = newCategory;
                newCategory.Products.Add(newProduct);
                var result = await data.AddCategoryAsync(newCategory);
                if (result == false)
                {
                    MessageBox.Show("Что-то пошло не так!!! Проверьте категорию или услугу");
                    return;
                }
            }
                else
                {
                category.Products.Add(newProduct);
                    var res = await dataProduct.AddProductAsync(newProduct);
                    if (res == false)
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
