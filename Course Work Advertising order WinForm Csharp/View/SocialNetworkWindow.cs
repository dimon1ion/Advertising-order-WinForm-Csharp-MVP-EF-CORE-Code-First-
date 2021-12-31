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
    public partial class SocialNetworkWindow : Form
    {
        private DataTable data;
        private AdvertisingOrderContext context;
        private List<SocialNetwork> socialNetworks;
        public SocialNetworkWindow()
        {
            InitializeComponent();
            context = new AdvertisingOrderContext();
            ShowTable();
        }

        private void ShowTable()
        {
            data = new DataTable();
            socialNetworks = context.SocialNetworks.ToList();
            data.Columns.Add("Id");
            data.Columns.Add("Name");
            foreach (SocialNetwork socialNetwork in socialNetworks)
            {
                DataRow row = data.NewRow();
                row["Id"] = socialNetwork.Id;
                row["Name"] = socialNetwork.Name;
                data.Rows.Add(row);
            }
            dataGridView1.DataSource = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (textBox1.Text != String.Empty)
                {
                    SocialNetwork tmp = context.SocialNetworks.Where(c => c.Id == Int32.Parse((dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Id"].ToString())).FirstOrDefault();

                    tmp.Name = textBox1.Text;
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
            if (textBox1.Text != String.Empty)
            {
                context.SocialNetworks.Add(new SocialNetwork() { Name = textBox1.Text });
                context.SaveChanges();
                ShowTable();
            }
            else
            {
                MessageBox.Show("Fill in the data", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = (dataGridView1.SelectedRows[0].DataBoundItem as DataRowView)["Name"]?.ToString() ?? String.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    context.SocialNetworks.Remove(context.SocialNetworks.Where(id => id.Id == Int32.Parse((dataGridView1.SelectedRows[i].DataBoundItem as DataRowView)["Id"].ToString())).FirstOrDefault());
                    context.SaveChanges();
                    ShowTable();
                }
            }
            else
            {
                MessageBox.Show("Row was not selected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
