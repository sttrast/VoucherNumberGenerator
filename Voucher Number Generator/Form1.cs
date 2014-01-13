using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;

// Acrobat/MS Office references
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

// Non-system references
using Voucher_Number_Generator.Properties;
using Voucher_Number_Generator;
using TCI.DataLayer.DataObjects;
using TCI.BusinessLayer;

namespace Voucher_Number_Generator
{

    public partial class ETP_Voucher_Generator : Form
    {

        public ETP_Voucher_Generator()
        {
            InitializeComponent();
        }

        public void reset_main_form()
        {

            number_warning.Text = "Select one option:";
            number_warning.Visible = true;
            number_generated.Text = "";
            number_generated.Enabled = false;
            number_generated_2.Text = "";
            number_generated_2.Enabled = false;
            gen_barcodes_paper.Checked = false;
            gen_barcodes_PDFs.Checked = false;
            gen_single_barcode.Checked = false;
            gen_barcodes_paper.Enabled = true;
            gen_barcodes_PDFs.Enabled = true;
            number_generated.Enabled = true;
            single_voucher_num.Text = "";
            single_voucher_num.Enabled = false;

            // Reset the "Go" button
            gen_voucher_num.Enabled = false;

            // Reset the email combo box
            cmb_email_addresses.Enabled = false;
            cmb_email_addresses.Visible = false;
            cmb_email_addresses.SelectedItem = "";
            email_label.Text = "voucher number(s) to PDF*.";

            voucher_date.Value = DateTime.Now;
            voucher_date.Format = DateTimePickerFormat.Custom;
            voucher_date.CustomFormat = "MM/yyyy";
            voucher_date.ShowUpDown = true; // to prevent the calendar from being displayed

            //voucher_date.Value = (DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString());
            gen_voucher_label.Text = "* Max #: " + Settings.Default.max_vouchers.ToString();

            // Variables reset
            variables.generation_type = "";
            variables.attachments = "";
            variables.email_list = "";
            variables.number_Generated = 0;
            variables.voucher_list = "";
            variables.Voucher_num = "";

            // Stop any Acrobat processes still active
            Process[] processes = Process.GetProcessesByName("AcroRd32");
            foreach (var process in processes)
            {

                process.Kill();

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region SQL tables, procedures and text files...

            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO

            //--make sure the text file identified in Settings.Default.email_text_file is in that directory
            //--******************* log table for voucher numbers - changed 12dec2013 **************************
            //-- DROP TABLE [DBO].[VOUCHER_INFO]        
            //-- CREATE TABLE [DBO].[VOUCHER_INFO] (    
            //--  [SYSTEM_ID] [INT]	IDENTITY(1,1),          
            //--  [VOUCHER_NUMBER] [VARCHAR] (15) NULL,
            //--  [VOUCHER_ORDINAL] [INT] NULL,
            //--  [BREAKER_PG_DATE] [DATETIME] NULL,          
            //--  [VOUCHER_MONTH] [VARCHAR] (5) NULL,       
            //--  [VOUCHER_YEAR] [VARCHAR] (4) NULL      
            //-- ) ON [PRIMARY]    
            //--CREATE UNIQUE CLUSTERED INDEX VOUCHER_INFO_C1 ON DBO.[VOUCHER_INFO](SYSTEM_ID, VOUCHER_NUMBER)

            //--*************************** table tracking voucher ordinal *************************************
            //-- DROP TABLE [DBO].[VOUCHER_ORDINAL]
            //-- CREATE TABLE [DBO].[VOUCHER_ORDINAL] (    
            //-- [SYSTEM_ID] [INT]	IDENTITY(1,1),          
            //-- [ORDINAL] [INT] NULL,     
            //-- [YEAR] [INT] NULL,
            //-- [LAST_ORD] [INT] NULL,
            //-- [MONTH] [INT] NULL
            //-- ) ON [PRIMARY]    
            //--INSERT INTO VOUCHER_ORDINAL (ORDINAL, MONTH, YEAR) VALUES (0, month(GETDATE()), year(GETDATE()))
            //--INSERT INTO VOUCHER_ORDINAL (ORDINAL, MONTH, YEAR) VALUES (0, month(GETDATE()), '2013')
            //--ALTER TABLE [DBO].[VOUCHER_ORDINAL] ADD [LAST_ORD] [INT] NULL


            ///****** Object:  StoredProcedure [dbo].[insert_voucher]    Script Date: 01/07/2014 22:47:46 ******/
            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO
            //ALTER PROCEDURE [dbo].[insert_voucher] 
            //@month varchar(2), @year varchar(4), @ordinal varchar (5)
            //AS
            //BEGIN

            //SET NOCOUNT ON;

            //declare @voucher_number varchar(9), @2_digit_year varchar(2)

            //if (len(@year) = 4)
            //    set @2_digit_year = substring(@year, 3, 2)
            //else
            //    set @2_digit_year = @year

            //print @year + '  ' + @2_digit_year

            //set @voucher_number = @month + @2_digit_year + @ordinal

            //print @voucher_number

            //    insert into dbo.VOUCHER_INFO 
            //    (VOUCHER_NUMBER, BREAKER_PG_DATE, VOUCHER_MONTH, VOUCHER_YEAR, VOUCHER_ORDINAL) 
            //    values 
            //    (@month + @2_digit_year + @ordinal, GETDATE(), @month, @year, @ordinal)

            //END

            //-- exec [dbo].[nextord] '01','2014'
            //-- [dbo].[insert_voucher] '01','2014','99999'
            //-- select * from VOUCHER_INFO where voucher_year = '2012'
            //-- select * from VOUCHER_ORDINAL


            ///****** Object:  StoredProcedure [dbo].[nextord]    Script Date: 01/07/2014 21:59:17 ******/
            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO
            //ALTER procedure [dbo].[nextord]
            //@month varchar(2), @year varchar(4)
            //AS

            //SET NOCOUNT ON 

            //------------------------- if there are no rows -------------------------------
            //if(select COUNT(*) from dbo.VOUCHER_ORDINAL) = 0
            //    INSERT INTO VOUCHER_ORDINAL (ORDINAL, MONTH, YEAR) VALUES (0, month(GETDATE()), year(GETDATE()))
            //-------------------- if there is no row for year ----------------------------
            //if(select COUNT(*) from dbo.VOUCHER_ORDINAL where YEAR = @year) = 0
            //    INSERT INTO VOUCHER_ORDINAL (ORDINAL, MONTH, YEAR) VALUES (0, month(GETDATE()), @year)
            //-- Uncomment the ordinal counter for either year or month
            //-- If both are uncommented, the ordinal will always be based on month
            //-- Set the year or month to current and start ordinal over
            //------------------------------- by year --------------------------------------
            //if (select count(system_id) from dbo.[voucher_ordinal] where year = (select year(GETDATE()))) = 0
            //    INSERT INTO VOUCHER_ORDINAL (ORDINAL, MONTH, YEAR) VALUES (0, month(GETDATE()), year(GETDATE()))
            //------------------------------- by month -------------------------------------
            //--if (select [MONTH] from dbo.[voucher_ordinal]) != (select month(GETDATE())) 
            //--	update dbo.[voucher_ordinal] set [month] = month(GETDATE())

            //declare @ordinal varchar(30), @voucher_num varchar(30), @2_digit_month varchar(2), @2_digit_year varchar(2)

            //-------------------------- Increment the counter -----------------------------
            //update dbo.voucher_ordinal set ordinal = ordinal + 1 where YEAR = @year

            //-------------------------- pad the ordinal with zeros ------------------------
            //select @ordinal = CASE len(ordinal)
            //when 1 then '0000' + CAST(ordinal as varchar)
            //when 2 then '000' + CAST(ordinal as varchar)
            //when 3 then '00' + CAST(ordinal as varchar)
            //when 4 then '0' + CAST(ordinal as varchar)
            //else CAST(ordinal as varchar)
            //end from dbo.voucher_ordinal
            //where YEAR = @year

            //------------------------ Change year to 2 digits -----------------------------
            //set @2_digit_year = substring(@year,3,2)

            //-------------------------- insert the ordinal use data -----------------------
            //exec [dbo].[insert_voucher] @month, @year, @ordinal

            //--insert into dbo.VOUCHER_INFO 
            //--(VOUCHER_NUMBER, BREAKER_PG_DATE, VOUCHER_MONTH, VOUCHER_YEAR, VOUCHER_ORDINAL)
            //--values
            //--(@month + @year + @ordinal, GETDATE(), @2_digit_month, @year, @ordinal)

            //------------------------------ select returned data --------------------------
            //select @month + @2_digit_year + @ordinal

            //-- ******************************* call to stored procedure ***************************************
            //-- exec [dbo].[nextord] '01','2014'
            //-- [dbo].[insert_voucher] '01','2014','99999'
            //-- select * from VOUCHER_INFO where voucher_year = '2012'
            //-- select * from VOUCHER_ORDINAL

            #endregion

            reset_main_form();

            if ((!string.IsNullOrEmpty(Settings.Default.nos_user_id) && (!string.IsNullOrEmpty(Settings.Default.nos_user_pw)) && (Settings.Default.email_msgbox)))
            {

                MessageBox.Show("User ID and PW specified in Sendmail.\r\nRemove or respecify in the config file.\r\nOr disable this message box");

            }

            try
            {

                StreamReader email_addresses = new StreamReader(Environment.CurrentDirectory + @"\" + Settings.Default.email_text_file);
                while (!email_addresses.EndOfStream)
                {

                    cmb_email_addresses.Items.Add(email_addresses.ReadLine());

                }

            }
            catch (Exception err)
            {

                // Log the output with the filename and the date/time
                LogIt.STLogIt("barcode_error", "Text from: " + Settings.Default.email_text_file + " could not be read. Error: " + err.Message);

            }

            // For first launch
            gen_barcodes_paper.Checked = true;
            number_generated.Text = "1";
            gen_voucher_num.Enabled = true;

        }

        private void gen_barcodes_paper_CheckedChanged(object sender, EventArgs e)
        {

            gen_voucher_num.Enabled = true;
            gen_voucher_num.Text = "Go";
            number_warning.Visible = true;
            number_generated.Enabled = true;
            number_generated_2.Enabled = false;
            number_generated_2.Text = "";
            cmb_email_addresses.Visible = false;
            cmb_email_addresses.Enabled = false;
            email_label.Text = "voucher number(s) to PDF*. ";
            single_voucher_num.Enabled = false;
            variables.generation_type = "paper";

        }

        private void gen_barcodes_PDFs_CheckedChanged(object sender, EventArgs e)
        {

            gen_voucher_num.Enabled = false;
            gen_voucher_num.Text = "Go";
            number_warning.Visible = true;
            number_generated_2.Enabled = true;
            cmb_email_addresses.Visible = true;
            cmb_email_addresses.Enabled = true;
            email_label.Text = "voucher number(s) to PDF*. Select email address: ";
            number_generated.Enabled = false;
            number_generated.Text = "";
            single_voucher_num.Enabled = false;
            variables.generation_type = "pdf";

        }

        private void gen_single_barcode_CheckedChanged(object sender, EventArgs e)
        {

            number_generated_2.Enabled = false;
            number_generated_2.Text = "";
            gen_voucher_num.Enabled = false;
            gen_voucher_num.Text = "Go";
            single_voucher_num.Enabled = true;
            number_generated_2.Enabled = false;
            number_generated_2.Text = "";
            cmb_email_addresses.Visible = false;
            cmb_email_addresses.Enabled = false;
            email_label.Text = "voucher number(s) to PDF*. ";
            number_generated.Enabled = false;
            number_generated.Text = "";
            variables.generation_type = "single";

        }

        private void gen_voucher_num_Click(object sender, EventArgs e)
        {

            gen_voucher_num.Text = "Working";

            if ((string.IsNullOrEmpty(variables.generation_type) && (!string.IsNullOrEmpty(number_generated.Text))))
            {

                variables.generation_type = "paper";

            }

            if (variables.generation_type == "single")
            {

                variables.Voucher_num = single_voucher_num.Text;
                if (barcode_gen.is_number(variables.Voucher_num))
                {

                    //// Added 7jan2014 - inserts manually entered voucher numbers to the database
                    Db.GetScalar("exec [dbo].[insert_voucher] '" + variables.Voucher_num.Substring(0, 2) + "','" +
                        variables.Voucher_num.Substring(2, 2) + "','" + variables.Voucher_num.Substring(4, variables.Voucher_num.Length - 4) + "'");

                    variables.Voucher_num = single_voucher_num.Text;
                    variables.generation_type = "single";

                }


                if (!string.IsNullOrEmpty(single_voucher_num.ToString()))
                    barcode_gen.print_barcodes(1, variables.generation_type);
                else
                    number_warning.Text = "You must enter a voucher #";

            }
            else
            {

                try
                {

                    if (variables.generation_type == "paper")
                    {

                        if (!string.IsNullOrEmpty(number_generated.Text))
                        {

                            if (barcode_gen.is_number(number_generated.Text))
                            {


                                barcode_gen.print_barcodes(int.Parse(number_generated.Text), variables.generation_type);

                            }
                            else
                            {

                                // Log the output with the filename and the date/time
                                LogIt.STLogIt("barcode_error", "Text entered: " + number_generated.Text + " is not numeric.");
                                variables.success = "Text entered on: " + DateTime.Now.ToString() + " is not numeric.";

                            }

                        }
                        else
                        {

                            //number_warning.Text = "Please choose an option, enter a number, select an email, and click Generate.";
                            number_generated.Text = "";

                        }

                    }
                    if (variables.generation_type == "pdf")
                    {

                        if (!string.IsNullOrEmpty(number_generated_2.Text))
                        {

                            if (barcode_gen.is_number(number_generated_2.Text))
                            {

                                // Send email with attachments
                                if ((!Settings.Default.email_vouchers) && (!string.IsNullOrEmpty(variables.email_list)))
                                {

                                    if (int.Parse(number_generated_2.Text) <= Settings.Default.max_vouchers)
                                        barcode_gen.print_barcodes(int.Parse(number_generated_2.Text), variables.generation_type);
                                    else
                                        barcode_gen.print_barcodes(Settings.Default.max_vouchers, variables.generation_type);

                                }
                                else
                                {

                                    barcode_gen.print_barcodes(int.Parse(number_generated_2.Text), variables.generation_type);

                                }

                            }
                            else
                            {

                                // Log the output with the filename and the date/time
                                LogIt.STLogIt("barcode_error", "Text entered: " + number_generated.Text + " is not numeric.");
                                variables.success = "Text entered on: " + DateTime.Now.ToString() + " is not numeric.";

                            }

                        }
                        else
                        {

                            MessageBox.Show("Please enter the number of separator pages to generate and click Go.");
                            number_generated.Text = "";

                        }

                    }

                }
                catch (Exception err)
                {

                    // Log the output with the filename and the date/time
                    if (!Settings.Default.verbose_logging)
                    {

                        // Log the output with the filename and the date/time
                        LogIt.STLogIt("barcode_error", "Could not process voucher numbers with error " + err.Message);

                    }
                    else
                    {

                        StringBuilder db_err_text = new StringBuilder();
                        db_err_text.Append("message: " + err.Message + ", ");
                        db_err_text.Append("data: " + err.Data + ", ");
                        db_err_text.Append("InnerException: " + err.InnerException + ", ");
                        db_err_text.Append("source: " + err.Source + ", ");
                        db_err_text.Append("TargetSite: " + err.TargetSite + ", ");
                        db_err_text.Append("StackTrace: " + err.StackTrace + ", ");
                        db_err_text.Append(" while ");

                        // Log the output with the filename and the date/time
                        LogIt.STLogIt("barcode_error", "Could not process voucher numbers with error " + db_err_text.ToString());

                    }

                }

            }

            reset_main_form();
            number_generated.Text = "";
            gen_voucher_num.Text = "Done";

        }

        private void Cancel_Click(object sender, EventArgs e)
        {

            // Stop any Acrobat processes still active
            Process[] processes = Process.GetProcessesByName("AcroRd32");
            foreach (var process in processes)
            {
                process.Kill();
            }

            Form.ActiveForm.Close();
            Application.Exit();

        }

        private void reset_form_Click(object sender, EventArgs e)
        {

            reset_main_form();

        }

        private void number_generated_TextChanged(object sender, EventArgs e)
        {

            //number_generated.SelectAll();

            if (variables.generation_type == "pdf")
            {

                if (!gen_voucher_num.Enabled)
                {

                    if (number_generated.Text.Length > 0)
                    {

                        cmb_email_addresses.Enabled = true;

                    }

                }
                else if (number_generated.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;

                }

            }
            if (variables.generation_type == "paper")
            {

                if (!gen_voucher_num.Enabled)
                {

                    gen_voucher_num.Enabled = true;

                }
                else if (number_generated.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;

                }

            }
            else
            {

                if (!gen_voucher_num.Enabled)
                {

                    gen_voucher_num.Enabled = true;

                }
                else if (number_generated.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;

                }

            }

        }

        private void number_generated_2_TextChanged(object sender, EventArgs e)
        {

            if (variables.generation_type == "pdf")
            {

                if (!gen_voucher_num.Enabled)
                {

                    if (number_generated_2.Text.Length > 0)
                    {

                        cmb_email_addresses.Enabled = true;

                    }

                }
                else if (number_generated_2.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;
                    cmb_email_addresses.Enabled = false;
                    cmb_email_addresses.Visible = false;

                }

            }
            if (variables.generation_type == "paper")
            {

                if (!gen_voucher_num.Enabled)
                {

                    gen_voucher_num.Enabled = true;

                }
                else if (number_generated_2.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;

                }

            }
            else
            {

                if (!gen_voucher_num.Enabled)
                {

                    gen_voucher_num.Enabled = true;

                }
                else if (number_generated_2.Text.Length == 0)
                {

                    gen_voucher_num.Enabled = false;

                }

            }

        }

        private void voucher_date_ValueChanged(object sender, EventArgs e)
        {

            variables.manual_date = voucher_date.Value;

        }

        private void cmb_email_addresses_SelectedIndexChanged(object sender, EventArgs e)
        {

            variables.email_list = cmb_email_addresses.Text;

        }

        private void single_voucher_num_TextChanged(object sender, EventArgs e)
        {

            if (single_voucher_num.Text.Length == 8)
            {

                gen_voucher_num.Enabled = true;

            }

        }

    }

}

