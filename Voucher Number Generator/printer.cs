using System;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using Voucher_Number_Generator.Properties;
using Voucher_Number_Generator;
using iTextSharp.text.pdf;

namespace Voucher_Number_Generator
{

    class printer
    {

        public static void SendToPrinter(string filePath)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = filePath;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();
        }

        //public static void print_pdf(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        public static bool print_pdf(string pdf_path, PrintPageEventArgs e)
        {

            bool results = false;
            //string printPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            StreamReader fileToPrint = new StreamReader(pdf_path);
            Font printFont = new Font("Calibri", 10);
            float yPos = 0f;
            int count = 0;
            //Rectangle.FromLTRB(0, 0, 1000, 1000);
            
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;
            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (count < linesPerPage)
            {
                line = fileToPrint.ReadLine();
                if (line == null)
                {
                    break;
                }
                yPos = topMargin + count * printFont.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }
            if (line != null)
            {
                e.HasMorePages = true;
            }

            //printDocument1.Print();
            fileToPrint.Close();

            results = true;
            return results;

        }

    }
}
