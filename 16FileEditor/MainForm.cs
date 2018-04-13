using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Demos
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "CS-Editor";
            tsCurrentDate.Text =
                DateTime.Today.ToShortDateString();
            tsCurrentFile.Text = "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutBox1();
            aboutForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtEditor.Text = "";
            var result = ofDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var selectedFile = ofDialog.FileName;
                tsStatus.Text = "Opening...";
                if (!bgFile.IsBusy)
                {
                    bgFile.RunWorkerAsync(selectedFile);
                }
                tsCurrentFile.Text = selectedFile;
            }

        }

        private void bgFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                tsStatus.Text = "Cancelled";
                tsProgress.Value = 0;
                return;
            }
            var fileContent = (string[])e.Result;
            txtEditor.Lines = fileContent;
            tsProgress.Value = 100;
            tsStatus.Text = "Completed";
        }

        private void bgFile_DoWork(object sender, DoWorkEventArgs e)
        {
            
            var fileContent = File.ReadAllLines(e.Argument.ToString());
            if (e.Cancel)
            {
                return;
            }

            for (int i = 1; i <= 99; i++)
            {
                if (bgFile.CancellationPending) {
                    bgFile.ReportProgress(0);
                    e.Cancel = true;
                }

                Thread.Sleep(50);
                bgFile.ReportProgress(i);
            }
            //Thread.Sleep(TimeSpan.FromSeconds(5));
            e.Result = fileContent;
        }

        private void cancelOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bgFile.CancelAsync();
        }

        private void bgFile_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tsProgress.Value = e.ProgressPercentage;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aboutToolStripMenuItem_Click(sender, e);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
