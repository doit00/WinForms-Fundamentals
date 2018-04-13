using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18TAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "http://filestore.aqa.org.uk/resources/chemistry/specifications/AQA-7404-7405-SP-2015.PDF";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = textBox1.Text;
            var fileName = Path.GetFileName(url);

            WebClient client = new WebClient();
            client.DownloadFile(url, fileName);
            this.Text = "Download copleted";
            

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            var url = textBox1.Text;
            var fileName = Path.GetFileName(url);

            WebClient client = new WebClient();
            await client.DownloadFileTaskAsync(url, fileName);
            this.Text = "Download copleted";

        }
    }
}
