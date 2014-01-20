using drExplorer.Model;
using System;
using System.Windows.Forms;

namespace drExplorer
{
    public partial class frmMain : Form
    {
        private FtgReader reader;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                reader = new FtgReader();
                reader.Open(openFileDialog1.FileName);
                reader.Parse();
                lbFiles.DataSource = reader.Files;
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            var fileInfo = lbFiles.SelectedItem as drFileInfo;
            saveFileDialog1.FileName = fileInfo.Filename;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                reader.Extract(saveFileDialog1.FileName, fileInfo);
            }
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExtract.Enabled = lbFiles.SelectedIndex != -1;
            btnExtractAll.Enabled = lbFiles.SelectedIndex != -1;
            var fileInfo = lbFiles.SelectedItem as drFileInfo;
            statusStrip1.Items[0].Text = String.Format("Offset: {0}", fileInfo.Offset);
            statusStrip1.Items[1].Text = String.Format("Filesize: {0}", fileInfo.Size);
        }

        private void btnExtractAll_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                reader.ExtractAll(folderBrowserDialog1.SelectedPath);
            }
        }
    }
}