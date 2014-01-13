using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Printing;
using System.Web;

// Acrobat/MS Office references
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.text.pdf.parser;
using Acrobat;

// Non-system references
using Voucher_Number_Generator.Properties;
using Voucher_Number_Generator;
using TCI.DataLayer.DataObjects;
using TCI.BusinessLayer;

namespace Voucher_Number_Generator
{
    class PdfGenerator
    {

        // List all of the form fields into a textbox. The
        // form fields identified can be used to map each of the
        // fields in a PDF.
        public static void ListFieldNames()
        {

            string pdfTemplate = Environment.CurrentDirectory + @"\" + Settings.Default.pdfTemplate;
            // title the form

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            // create and populate a string builder with each of the 
            // field names available in the subject PDF

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, AcroFields.Item> de in pdfReader.AcroFields.Fields)
            {

                sb.Append(de.Key.ToString() + Environment.NewLine);

            }

        }

        public static void FillForm(string voucher_num)
        {

            string pdfTemplate = Environment.CurrentDirectory.ToString() + @"\" + Settings.Default.pdfTemplate;
            string temp_directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + @"\" + Settings.Default.temporary_dir + @"\" + DateTime.Now.Year.ToString();
            if (!Directory.Exists(temp_directory))
                Directory.CreateDirectory(temp_directory);

            string newFile = temp_directory + @"\" + voucher_num + ".pdf";
            // If the file already exists, delete it
            if (File.Exists(newFile))
                File.Delete(newFile);

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            pdfFormFields.SetField("Year", voucher_num.Substring(2, 2));
            pdfFormFields.SetField("Month", voucher_num.Substring(0, 2));
            pdfFormFields.SetField("Ordinal", voucher_num.Substring(4, voucher_num.Length - 4));
            pdfFormFields.SetField("Voucher", voucher_num);
            pdfFormFields.SetField("STVBarcode", @"*" + Settings.Default.unique_string + voucher_num + @"*");
            pdfFormFields.SetField("BarcodeText", @"*" + Settings.Default.unique_string + voucher_num + @"*");

            pdfStamper.FormFlattening = true;
            if (Settings.Default.LogAllActivities)
            {

                if (pdfStamper.FullCompression)
                    LogIt.STLogIt("FullCompression", "Temp file: " + newFile + " is compressed");

            }

            //if (pdfStamper.FullCompression)
            //    MessageBox.Show("True");

            pdfStamper.Close();
            pdfReader.Close();

            variables.attachments = newFile;

        }

    }

}


