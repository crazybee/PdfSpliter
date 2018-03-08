using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using PdfSpliter.Properties;
using iTextSharp.text.pdf;

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
            OpenFileDialog fdlg = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = @"Pdf Files (.pdf)|*.pdf|All Files (*.*)|*.*",
                RestoreDirectory = true
            };
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
            progressBar1.Text = @"Succeed";
            outputinformation.Text = @"Succeed";
            Showoutput(outputfolder);
        }

        private void OutPutPdfJob(IProgress<int> progress)
        {
            // Get a fresh copy of the sample PDF file
            var filename = inputfilepath.Text;
            PdfReader reader = null;
            string name = Path.GetFileNameWithoutExtension(filename);
            if (string.IsNullOrEmpty(filename))
            {
                outputinformation.Text = @"input file name is empty";
                return;
            }
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
                    var total = GetPageCount(filename);
                    reader = new PdfReader(filename);
                    reader.RemoveUsageRights();
                    reader.RemoveUnusedObjects();
                    reader.Tampered = true;
                    if (reader.IsEncrypted())
                    {
                        reader = unlockPdf(reader);
                    }
                    var sourceDocument = new Document(reader.GetPageSizeWithRotation(1));
                    for (int idx = 1; idx <= total; idx++)
                    {

                        var outputfilename = $"{name} - Page {idx}_tempfile.pdf";
                        outputfilename = Path.Combine(outputFolder, outputfilename);
                        var pdfCopyProvider = new PdfCopy(sourceDocument,
                            new FileStream(outputfilename, System.IO.FileMode.Create));
                        sourceDocument.Open();
                        var importedPage = pdfCopyProvider.GetImportedPage(reader, idx);
                        pdfCopyProvider.AddPage(importedPage);
                        progress?.Report((idx) * 100 / total);
                    }
                    sourceDocument.Close();
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    outputinformation.Text = @"Exception throws";
                    throw;
                }

                outputfolder = outputFolder;
            }
        }

        private static int GetPageCount(string path)
        {
            PdfReader pdfReader = new PdfReader(path);
            return pdfReader.NumberOfPages;
        }

        private static void Showoutput(string outputFolder)
        {
            OpenFileDialog outputDialog = new OpenFileDialog();
            outputDialog.InitialDirectory = outputFolder;
            outputDialog.ShowDialog();
        }

        //private void progressBar1_Click(object sender, EventArgs e)
        //{
        //}
        private static PdfReader unlockPdf(PdfReader reader)
        {
            if (reader == null)
                return reader;
            try
            {
                //var propertyInfo = reader.GetType().GetProperty("encrypted", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                //propertyInfo.SetValue(reader, false, null);
                FieldInfo info = reader.GetType().GetTypeInfo().GetField("unethicalreading");
                info.SetValue(reader, true);

            }
            catch (Exception e)
            { // ignore 
                Console.WriteLine(e.Message);
            }
            return reader;
        }

    }
}

