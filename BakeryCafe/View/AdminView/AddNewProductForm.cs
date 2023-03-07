using BakeryCafe.Controllers;
using BakeryCafe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
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
            dateTimePicker1.MaxDate = DateTime.Today;
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
            (await data.GetListCategoryAsync()).ForEach(c => categoryComboBox.Items.Add(c.CategoryName)); // заменить на GetDataProductAsync(string dataType) ЕСЛИ ПОЛУЧИТСЯ
            (await data.GetListManufAsync()).ForEach(c => manyfComboBox.Items.Add(c.ManufacturerName)); // заменить на GetDataProductAsync(string dataType) ЕСЛИ ПОЛУЧИТСЯ
        }

        protected async void button1_Click(object sender, EventArgs e)
        {
            
            
            Product newProduct = new Product()  // создаем новый продукт из текстбоксов и пр.
            {
                productName = textBox1.Text,
                weight = (int)numericUpDown1.Value,
                price = numericUpDown2.Value,
                dateOfManuf = dateTimePicker1.Value
        };

            CategoryBakery category = await selectCategoryAsinc();
            Manufacturer manufacturer = await selectManufAsinc();

            var res = await dataProduct.AddProductAsync(newProduct, category, manufacturer);
           
            if (res == false)
            {
                MessageBox.Show("Ошибка!!! Проверьте продукт");
                return;
            }
            data.fillAverPrice(categoryData, categoryComboBox.Text);
            data.fillAverPrice(manufData, manyfComboBox.Text);
            DialogResult = DialogResult.OK;
        }

        protected async Task<CategoryBakery> selectCategoryAsinc ()  // перенести в DataProductController если успею
        {
            CategoryBakery category = (CategoryBakery)await data.CheckDataProductAsync(categoryComboBox.Text, categoryData); // проверяем есть ли уже категория
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
                }
            }
            
            return category;
        }

        protected async Task<Manufacturer> selectManufAsinc() // перенести в DataProductController если успею
        {
            Manufacturer manufacturer = (Manufacturer)await data.CheckDataProductAsync(manyfComboBox.Text, manufData);
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
                }
            }
            
            return manufacturer;
        }
    }
}