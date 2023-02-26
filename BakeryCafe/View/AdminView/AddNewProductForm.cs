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
        public static string manufData = "manufacturer";
        public static string categoryData = "categoryBakery";

        public AddNewProductForm()
        {
            InitializeComponent();
            dateTimePicker1.MinDate = DateTime.Today.AddDays(-5);
        }

        private async void AddNewProduct_Load(object sender, EventArgs e)
        {
            categoryComboBox.Items.Clear();
            manyfComboBox.Items.Clear();
            categoryComboBox.Text = "";
            manyfComboBox.Text = "";
            toolTip1.SetToolTip(categoryComboBox, "Если в списке нет нужной категории, введите наименование новой категории");
            toolTip1.SetToolTip(manyfComboBox, "Если в списке нет нужного производителя, введите наименование нового производителя");
            (await data.GetCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName)); // заменить на GetDataProductAsync(string dataType) ЕСЛИ ПОЛУЧИТСЯ
            (await data.GetManufAsync()).ForEach(c => manyfComboBox.Items.Add(c.ManufacturerName)); // заменить на GetDataProductAsync(string dataType) ЕСЛИ ПОЛУЧИТСЯ
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            CategoryBakery category = (CategoryBakery)await data.CheckDataProductAsync(categoryComboBox.Text, categoryData); // проверяем есть ли уже категория
            Manufacturer manufacturer = (Manufacturer)await data.CheckDataProductAsync(manyfComboBox.Text, manufData);
            Product newProduct = new Product()  // создаем новый продукт из текстбоксов и пр.
            {
                productName = textBox1.Text,
                weight = (int)numericUpDown1.Value,
                price = numericUpDown2.Value,
                dateOfManuf = dateTimePicker1.Value
        };

            if (category == null) // если категории нет
            {
                category = new CategoryBakery()  // создаем категорию
                {
                    CategoryName = categoryComboBox.Text,
                    AveragePrice = 0
                };
                if (await data.AddDataProdAsync(category) == false)
                {
                    MessageBox.Show("Ошибка!!! Проверьте категорию");
                    return;
                }   
            }

            if (manufacturer == null)
            {
                manufacturer = new Manufacturer()
                {
                    ManufacturerName = manyfComboBox.Text,
                    AveragePriceMan = 0
                };
                if (await data.AddDataProdAsync(manufacturer) == false)
                {
                    MessageBox.Show("Ошибка!!! Проверьте производителя");
                    return;
                }
            }

            var res = await dataProduct.AddProductAsync(newProduct, category, manufacturer);
            if (res == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте продукт");
                return;
            }
           
            this.AddNewProduct_Load(sender, e);
        }
        private async void CheckDataProduct(IDataProduct obj)
        {
            var result = await data.AddDataProdAsync(obj);
            if (result == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте категорию или производителя");
                return;
            }
        }
    }
}