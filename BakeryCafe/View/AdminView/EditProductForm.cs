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
        public EditProductForm(Product product)
        {
            InitializeComponent();
            this.product = product;
        }

        private void EditProductForm_Load(object sender, EventArgs e)
        {
            categoryComboBox.Text = product.CategoryBakerys.CategoryName;
            /*foreach (var item in categoryComboBox.Items)
            {
                if (item.ToString() == product.CategoryBakerys.CategoryName)
                {
                    categoryComboBox.SelectedItem = item;
                }
            }*/
           
            //   categoryComboBox.SelectedItem = categoryComboBox.Items.Equals(product.CategoryBakerys.CategoryName);

        }
    }
}
