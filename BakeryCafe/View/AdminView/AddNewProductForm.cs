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
    }
}
