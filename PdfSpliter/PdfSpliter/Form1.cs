using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSpliter.Properties;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfSpliter
{
    public partial class Form1 : Form
    {
        private static string outputfolder;

        public Form1()
        {
            InitializeComponent();
            this.Text = Resources.Form1_Form1_PDF_spliter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void fileselect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.InitialDirectory = Environment.CurrentDirectory;
            fdlg.Filter = "Pdf Files (.pdf)|*.pdf|All Files (*.*)|*.*";
            fdlg.RestoreDirectory = true;
            DialogResult result = fdlg.ShowDialog(); // Show the dialog.

            if (result == DialogResult.OK) // Test result.
            {
                string file = fdlg.FileName;
                try
                {
                    inputfilepath.Text = file;
                }
                catch (IOException)
                {
                    outputinformation.Text = Resources.Form1_fileselect_Click_file_io_error;
                }
            }
        }

        private async void run_Click(object sender, EventArgs e)
        {
            outputinformation.Text = "";
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            var progress = new Progress<int>(v =>
            {
                // This lambda is executed in context of UI thread,
                // so it can safely update form controls
                progressBar1.Value = v;
            });

            await Task.Run(() => OutPutPdfJob(progress));
            progressBar1.Text = "Succeed";
            outputinformation.Text = "Succeed";
            Showoutput(outputfolder);
        }

        private void OutPutPdfJob(IProgress<int> progress)
        {
            // Get a fresh copy of the sample PDF file
            var filename = inputfilepath.Text;

            if (string.IsNullOrEmpty(filename))
            {
                outputinformation.Text = "input file name is empty";
                return;
            }
            // Open the file

            PdfDocument inputDocument = PdfReader.Open(filename, PdfDocumentOpenMode.Import);
            string name = Path.GetFileNameWithoutExtension(filename);
            string outFolder = Path.GetDirectoryName(filename);
            if (outFolder != null)
            {
                var outputFolder = Path.Combine(outFolder, name);
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                try
                {
                    var total = inputDocument.PageCount;

                    for (int idx = 0; idx < total; idx++)
                    {
                        // Create new document
                        PdfDocument outputDocument = new PdfDocument();
                        outputDocument.Options.CompressContentStreams = true;
                        // Add the page and save it
                        outputDocument.AddPage(inputDocument.Pages[idx]);
                        var outputfilename = String.Format("{0} - Page {1}_tempfile.pdf", name, idx + 1);
                        outputfilename = Path.Combine(outputFolder, outputfilename);
                        outputDocument.Save(outputfilename);
                        if (progress != null)
                            progress.Report((idx + 1)*100/total);
                    }
                }
                catch (Exception)
                {
                    outputinformation.Text = "Exception throws";
                    throw;
                }

                outputfolder = outputFolder;
            }
        }

        private static void Showoutput(string outputFolder)
        {
            OpenFileDialog outputDialog = new OpenFileDialog();
            outputDialog.InitialDirectory = outputFolder;
            outputDialog.ShowDialog();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }
    }
}

