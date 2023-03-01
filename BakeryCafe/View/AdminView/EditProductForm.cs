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
        public EditProductForm(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private  async void EditProductForm_Load(object sender, EventArgs e)
        {
            await fillEditForm();
        }

        private async Task fillEditForm()
        {
            categoryComboBox.SelectedItem = await dataProduct.GetProductCategoryAsync(product);
            //product.Manufacturers.ToList().ForEach(m => manyfComboBox.Text = m.ManufacturerName);
            manyfComboBox.SelectedItem = await data.GetProdManufAsync(product);
            textBox1.Text = product.productName;
            numericUpDown1.Value = product.weight;
            numericUpDown2.Value = product.price;
            dateTimePicker1.MinDate = (await dataProduct.GetProductAsync("")).Min(p => p.dateOfManuf);
            dateTimePicker1.Value = product.dateOfManuf;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (
            categoryComboBox.SelectedItem = await dataProduct.GetProductCategoryAsync(product) 
            manyfComboBox.SelectedItem = await data.GetProdManufAsync(product);
        }

        private void fillProdukt ()
        {
            product.productName = textBox1.Text  ;
            product.weight = (int)numericUpDown1.Value ;
            product.price = numericUpDown2.Value;
            product.dateOfManuf = dateTimePicker1.Value;
        }
    }
}
