using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Demos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.SelectedText))
            {
                Clipboard.SetText(textBox1.SelectedText);
            }
            else
            {
                textBox2.Text = "No text selected in textBox1";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //Clipboard.GetText()

            IDataObject data = Clipboard.GetDataObject();

            if (data.GetDataPresent(DataFormats.Text))
            {
                textBox2.Text = data.GetData(DataFormats.Text).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var wavFiles = Directory.GetFiles(@"C:\Windows\media\","*.wav");
            this.Text = $"Found {wavFiles.Length} wav files";
            var sound = @"C:\Windows\media\Windows Logoff Sound.wav";
            SoundPlayer player = new SoundPlayer();
            
            foreach (var file in wavFiles)
            {
                var msg = String.Format("now playing:{0}", file);
                this.Text = msg;
                player.SoundLocation = file;
                player.PlaySync();

            }
            //using WMP10SDK
            //WindowMediaPlayer p = new WindowsMediaPlayer
            //p.Url = "path to mp3";
            //p.Controls.Play;


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.X)
            {
                MessageBox.Show("Ctrl + Alt + X");
            }
        }
    }
}
