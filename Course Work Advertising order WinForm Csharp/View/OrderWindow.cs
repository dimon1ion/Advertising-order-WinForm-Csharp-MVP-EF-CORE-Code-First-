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
    public partial class OrderWindow : Form
    {
        private DataTable data;
        private AdvertisingOrderContext context;
        private IEnumerable<Order> orders;
        private IEnumerable<Customer> customers;
        private IEnumerable<AdType> adTypes;
        private IEnumerable<SocialNetwork> socialNetworks;
        public OrderWindow()
        {
            InitializeComponent();
            context = new AdvertisingOrderContext();
            ShowTable();
            customers = context.Customers.ToList();
            foreach (var item in customers)
            {
                CustomerComboBox.Items.Add($"{item.Name} {item.LastName} (Id:{item.Id})");
            }
            adTypes = context.AdTypes.ToList();
            foreach (var item in adTypes)
            {
                AdTypeComboBox.Items.Add($"{item.Type} (Id:{item.Id})");
            }
            socialNetworks = context.SocialNetworks.ToList();
            foreach (var item in socialNetworks)
            {
                NetworkComboBox.Items.Add($"{item.Name} (Id:{item.Id})");
            }
            PriceTextbox.Text = "0";
        }

        private void ShowTable()
        {
            data = new DataTable();
            orders = context.Orders.ToList();
            data.Columns.Add("Id");
            data.Columns.Add("AdTypeId");
            data.Columns.Add("CustomerId");
            data.Columns.Add("SocialNetworkId");
            data.Columns.Add("Date");
            data.Columns.Add("Price");
            data.Columns.Add("Text");
            foreach (Order order in orders)
            {
                DataRow row = data.NewRow();
                row["Id"] = order.Id;
                row["AdTypeId"] = order.AdTypeId;
                row["CustomerId"] = order.CustomerId;
                row["SocialNetworkId"] = order.SocialNetworkId;
                row["Date"] = order.Date;
                row["Price"] = order.Price;
                row["Text"] = order.Text;
                data.Rows.Add(row);
            }
            dataGridView1.DataSource = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CustomerComboBox.SelectedItem = null;
            AdTypeComboBox.SelectedItem = null;
            NetworkComboBox.SelectedItem = null;
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = String.Empty;
            PriceTextbox.Text = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (textBox1.Text != String.Empty && CustomerComboBox.SelectedItem != null && AdTypeComboBox.SelectedItem != null
                    && NetworkComboBox.SelectedItem != null && PriceTextbox.Text != String.Empty)
                {
                    Order tmp = context.Orders.Where(c => c.Id == Int32.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Id"].ToString())).FirstOrDefault();

                    tmp.CustomerId = customers.Where(x => $"{x.Name} {x.LastName} (Id:{x.Id})" == CustomerComboBox.SelectedItem.ToString()).First().Id;
                    tmp.AdTypeId = adTypes.Where(x => $"{x.Type} (Id:{x.Id})" == AdTypeComboBox.SelectedItem.ToString()).First().Id;
                    tmp.SocialNetworkId = socialNetworks.Where(x => $"{x.Name} (Id:{x.Id})" == NetworkComboBox.SelectedItem.ToString()).First().Id;
                    tmp.Date = dateTimePicker1.Value;
                    tmp.Price = Decimal.Parse(PriceTextbox.Text);
                    tmp.Text = textBox1.Text;
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != String.Empty && CustomerComboBox.SelectedItem != null && AdTypeComboBox.SelectedItem != null
                    && NetworkComboBox.SelectedItem != null && PriceTextbox.Text != String.Empty)
                {
                    int custId = customers.Where(x => $"{x.Name} {x.LastName} (Id:{x.Id})" == CustomerComboBox.SelectedItem.ToString()).First().Id;
                    int typeId = adTypes.Where(x => $"{x.Type} (Id:{x.Id})" == AdTypeComboBox.SelectedItem.ToString()).First().Id;
                    int socId = socialNetworks.Where(x => $"{x.Name} (Id:{x.Id})" == NetworkComboBox.SelectedItem.ToString()).First().Id;
                    context.Orders.Add(new Order()
                    {
                        CustomerId = custId,
                        AdTypeId = typeId,
                        SocialNetworkId = socId,
                        Date = dateTimePicker1.Value,
                        Price = Decimal.Parse(PriceTextbox.Text),
                        Text = textBox1.Text
                    });
                    context.SaveChanges();
                    ShowTable();
                }
                else
                {
                    MessageBox.Show("Fill in the data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter the price correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    context.Orders.Remove(context.Orders.Where(id => id.Id == Int32.Parse((dataGridView1.SelectedRows[i].DataBoundItem as DataRowView)["Id"].ToString())).FirstOrDefault());
                    context.SaveChanges();
                    ShowTable();
                }
            }
            else
            {
                MessageBox.Show("Row was not selected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Customer tmpCust = customers
                    .Where(c => c.Id == (Int32.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["CustomerId"]?.ToString())))
                    .FirstOrDefault();
                if (tmpCust != null)
                {
                    foreach (var item in CustomerComboBox.Items)
                    {
                        if (item.ToString() == $"{tmpCust.Name} {tmpCust.LastName} (Id:{tmpCust.Id})")
                        {
                            CustomerComboBox.SelectedItem = item;
                        }
                    }
                }
                AdType tmpAdType = adTypes
                    .Where(c => c.Id == (Int32.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["AdTypeId"]?.ToString())))
                    .FirstOrDefault();
                if (tmpAdType != null)
                {
                    foreach (var item in AdTypeComboBox.Items)
                    {
                        if (item.ToString() == $"{tmpAdType.Type} (Id:{tmpAdType.Id})")
                        {
                            AdTypeComboBox.SelectedItem = item;
                        }
                    }
                }
                SocialNetwork tmpSocNet = socialNetworks
                    .Where(c => c.Id == (Int32.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["SocialNetworkId"]?.ToString())))
                    .FirstOrDefault();
                if (tmpSocNet != null)
                {
                    foreach (var item in NetworkComboBox.Items)
                    {
                        if (item.ToString() == $"{tmpSocNet.Name} (Id:{tmpSocNet.Id})")
                        {
                            NetworkComboBox.SelectedItem = item;
                        }
                    }
                }
                textBox1.Text = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Text"]?.ToString() ?? String.Empty;
                dateTimePicker1.Value = DateTime.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Date"]?.ToString());
                PriceTextbox.Text = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Price"]?.ToString() ?? "0";
            }
        }
    }
}
