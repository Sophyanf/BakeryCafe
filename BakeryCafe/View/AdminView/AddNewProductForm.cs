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
        public string manufData = "manufacturer";
        public string categoryData = "categoryBakery";

        public AddNewProductForm()
        {
            InitializeComponent();
        }

        private async void AddNewProduct_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(categoryComboBox, "Если в списке нет нужной категории, введите наименование новой категории");
            toolTip1.SetToolTip(manyfComboBox, "Если в списке нет нужной категории, введите наименование новой категории");
            (await data.GetDataProductAsync(categoryData)).ForEach(c => categoryComboBox.Items.Add(c));
            (await data.GetDataProductAsync(manufData)).ForEach(c => manyfComboBox.Items.Add(c));
        }
    
             private async void button1_Click(object sender, EventArgs e)
        {
            CategoryBakery category = (CategoryBakery)await data.CheckDataProductAsync(categoryComboBox.Text, categoryData); // проверяем есть ли уже категория
            Product newProduct = new Product()  // создаем новый продукт из текстбоксов и пр.
            {
                productName = textBox1.Text,
                weight = (int)numericUpDown1.Value,
                price = numericUpDown2.Value
            };

            if (category == null) // если категории нет
            {
                category = new CategoryBakery()  // создаем категорию
                {
                    CategoryName = categoryComboBox.Text,
                    AveragePrice = 0
                };
            }


                var result = await data.AddCategoryAsync(category);
               // await dataProduct.AddProductAsync(newProduct, category);
                if (result == false)
                {
                    MessageBox.Show("Ошибка!!! Проверьте категорию");
                    return;
                }
            var res = await dataProduct.AddProductAsync(newProduct, category) ;
            if (res == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте услугу");
                return;
            }
            this.AddNewProduct_Load(sender, e);
            }
        }
}
