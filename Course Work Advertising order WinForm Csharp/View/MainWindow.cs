using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_Work_Advertising_order_WinForm_Csharp.DbContextDir;
using Course_Work_Advertising_order_WinForm_Csharp.DbObjects;
using Course_Work_Advertising_order_WinForm_Csharp.View;

namespace Course_Work_Advertising_order_WinForm_Csharp
{
    public partial class MainWindow : Form
    {
        public AdvertisingOrderContext context { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            context = new AdvertisingOrderContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            this.Visible = false;
            customerWindow.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SocialNetworkWindow networkWindow = new SocialNetworkWindow();
            this.Visible = false;
            networkWindow.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdTypeWindow adTypeWindow = new AdTypeWindow();
            this.Visible = false;
            adTypeWindow.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            this.Visible = false;
            orderWindow.ShowDialog();
            this.Visible = true;
        }
    }
}
