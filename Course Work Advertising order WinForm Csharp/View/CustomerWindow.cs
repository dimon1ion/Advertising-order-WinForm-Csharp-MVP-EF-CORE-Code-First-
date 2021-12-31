using Course_Work_Advertising_order_WinForm_Csharp.DbContextDir;
using Course_Work_Advertising_order_WinForm_Csharp.DbObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Course_Work_Advertising_order_WinForm_Csharp.View
{
    public partial class CustomerWindow : Form
    {
        private DataTable data;
        private AdvertisingOrderContext context;
        private List<Customer> customers;
        public CustomerWindow()
        {
            InitializeComponent();
            context = new AdvertisingOrderContext();
            ShowTable();
        }

        private void ShowTable()
        {
            data = new DataTable();
            customers = context.Customers.ToList();
            data.Columns.Add("Id");
            data.Columns.Add("First name");
            data.Columns.Add("Last name");
            foreach (Customer customer in customers)
            {
                DataRow row = data.NewRow();
                row["Id"] = customer.Id;
                row["First name"] = customer.Name;
                row["Last name"] = customer.LastName;
                data.Rows.Add(row);
            }
            dataGridView1.DataSource = data;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty)
            {
                context.Customers.Add(new Customer() { Name = textBox1.Text, LastName = textBox2.Text });
                context.SaveChanges();
                ShowTable();
            }
            else
            {
                MessageBox.Show("Fill in the data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    context.Customers
                        .Remove(context.Customers
                        .Where(id => id.Id == Int32
                        .Parse((dataGridView1.SelectedRows[i].DataBoundItem as DataRowView)["Id"]
                        .ToString()))
                        .FirstOrDefault());
                    context.SaveChanges();
                    ShowTable();
                }
            }
            else
            {
                MessageBox.Show("Row was not selected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["First name"]?
                    .ToString() ?? String.Empty;
                textBox2.Text = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Last name"]?
                    .ToString() ?? String.Empty;
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (textBox1.Text != String.Empty && textBox2.Text != String.Empty)
                {
                    Customer tmp = context.Customers
                        .Where(c => c.Id == Int32
                        .Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Id"]
                        .ToString())).FirstOrDefault();

                    tmp.Name = textBox1.Text;
                    tmp.LastName = textBox2.Text;
                    context.SaveChanges();
                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Input field cannot be empty", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Row was not selected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
