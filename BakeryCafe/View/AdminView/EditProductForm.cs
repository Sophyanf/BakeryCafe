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

namespace BakeryCafe.View.AdminView
{
    public partial class EditProductForm : AddNewProductForm
    {
        Product product;
        private ProductController dataProduct = ProductController.Instance;
        private DataProductController data = DataProductController.Instance;
        string productCategory = "";
        string productManuf = "";
        public EditProductForm(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private  async void EditProductForm_Load(object sender, EventArgs e)
        {
            productCategory = await dataProduct.GetProductCategoryAsync(product);
            productManuf = await dataProduct.GetProdManufAsync(product);
            await fillEditForm();
        }

        private async Task fillEditForm()
        {
           
            categoryComboBox.SelectedItem = productCategory;
            manyfComboBox.SelectedItem = productManuf;
            textBox1.Text = product.productName;
            numericUpDown1.Value = product.weight;
            numericUpDown2.Value = product.price;
            dateTimePicker1.MinDate = (await dataProduct.GetProductAsync("")).Min(p => p.dateOfManuf);
            dateTimePicker1.Value = product.dateOfManuf;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            fillProdukt();
        }

        private async void fillProdukt ()
        {
            product.productName = textBox1.Text;
            product.weight = (int)numericUpDown1.Value;
            product.price = numericUpDown2.Value;
            product.dateOfManuf = dateTimePicker1.Value;

            CategoryBakery category = null;
            Manufacturer manufacturer = null;

            if (productCategory != categoryComboBox.SelectedItem.ToString() ) { category = await selectCategoryAsinc(); }
                
                    
             if  (productManuf != manyfComboBox?.SelectedItem.ToString()) { manufacturer = await selectManufAsinc(); }
        
                var res = await dataProduct.EditProductAsync(product, category, manufacturer);
                if (res == false)
                {
                    MessageBox.Show("Ошибка!!! Проверьте продукт");
                    return;
                }
                DialogResult = DialogResult.OK;
        }
    }
}
