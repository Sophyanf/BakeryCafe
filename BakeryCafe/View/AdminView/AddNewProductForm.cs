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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BakeryCafe.View.AdminView
{
    public partial class AddNewProductForm : Form
    {
        private DataProductController data = DataProductController.Instance;
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
    }
}
