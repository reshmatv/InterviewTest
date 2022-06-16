using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BusinessLogicLayer;

namespace InterviewTest
{
    public partial class InventoryDetails : Form
    {
        public InventoryDetails()
        {
            InitializeComponent();
        }

        private void InventoryDetails_Load(object sender, EventArgs e)
        {
            LoadDatagrid();
            
        }

        private void LoadDatagrid()
        {
            dataGridView1.DataSource = Product.DisplayProduct();
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[5].HeaderText = "Total Price";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if( txtName.Text ==String.Empty|| txtPrice.Text ==String.Empty|| txtQuantity.Text ==String.Empty||
                richTxtDescription.Text == String.Empty) {
                MessageBox.Show("Fill mandatory fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var Prod=new Product();
            Prod.Prod_Name = txtName.Text.Trim();
            Prod.Description = richTxtDescription.Text.Trim();
            Prod.Price = Convert.ToDouble(txtPrice.Text);
            Prod.Quantity = Convert.ToInt32(txtQuantity.Text);
            if(Product.AddNewProduct(Prod))
            {
                LoadDatagrid();
                ClearFields();
                MessageBox.Show("New item added successfully");
            }
            else
            {
                MessageBox.Show("Found an error while adding new item");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataRow = (Product)dataGridView1.CurrentRow.DataBoundItem;
            txtProdNum.Text = dataRow.Prod_Num;
            txtName.Text = dataRow.Prod_Name;
            txtPrice.Text = dataRow.Price.ToString();
            txtQuantity.Text = dataRow.Quantity.ToString();
            richTxtDescription.Text = dataRow.Description;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (txtName.Text == String.Empty || txtPrice.Text == String.Empty || txtQuantity.Text == String.Empty ||
                richTxtDescription.Text == String.Empty)
            {
                MessageBox.Show("Fill mandatory fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            var Prod = new Product();
            Prod.Prod_Num = txtProdNum.Text.Trim();
            Prod.Prod_Name = txtName.Text.Trim();
            Prod.Description = richTxtDescription.Text.Trim();
            Prod.Price = Convert.ToDouble(txtPrice.Text);
            Prod.Quantity = Convert.ToInt32(txtQuantity.Text);
            if (Product.UpdateProduct(Prod))
            {
                LoadDatagrid();
                ClearFields();
                MessageBox.Show("Item updated successfully");
            }
            else
            {
                MessageBox.Show("Found an error while updating an item");
            }
        }

        private void ClearFields()
        {
            txtProdNum.Text=txtName.Text=txtPrice.Text=txtQuantity.Text=richTxtDescription.Text=String.Empty;
            dataGridView1.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Product.DeleteProduct(txtProdNum.Text.Trim()))
            {
                LoadDatagrid();
                ClearFields();
                MessageBox.Show("Item deleted successfully");
            }
            else
            {
                MessageBox.Show("Found an error while deleting an item");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var isStock = Convert.ToBoolean(row.Cells[6].Value);
                row.DefaultCellStyle.BackColor = isStock ? Color.Green : Color.Red;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
