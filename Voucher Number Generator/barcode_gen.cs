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
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
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

    class variables
    {

        #region Variables...

        private static string _generation_type;
        public static string generation_type
        {
            get { return _generation_type; }
            set { _generation_type = value; }
        }
        private static string _email_list;
        public static string email_list
        {
            get { return _email_list; }
            set { _email_list = value; }
        }
        private static string _voucher_list;
        public static string voucher_list
        {
            get { return _voucher_list; }
            set { _voucher_list = value; }
        }
        private static string _voucher_num;
        public static string Voucher_num
        {
            get { return _voucher_num; }
            set { _voucher_num = value; }
        }
        private static string _success;
        public static string success
        {
            get { return _success; }
            set { _success = value; }
        }
        private static int _number_generated;
        public static int number_Generated
        {
            get { return _number_generated; }
            set { _number_generated = value; }
        }
        private static string _attachments;
        public static string attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }
        private static DateTime _manual_date;
        public static DateTime manual_date
        {
            get { return _manual_date; }
            set { _manual_date = value; }
        }

        #endregion

    }

    class barcode_gen
    {

        public static string voucher_num(int loop_number)
        {

            StringBuilder ordinal_list = new StringBuilder();
            int cnt = 0;
            while (cnt < loop_number)
            {

                // Get new voucher number
                string new_voucher_num = "";

                // If the month was changed
                string manual_month = variables.manual_date.Month.ToString();
                // Pad the month with a zero if single digit
                if (manual_month.Length == 1)
                    manual_month = "0" + manual_month;

                string manual_year = variables.manual_date.Year.ToString();

                new_voucher_num = Db.GetScalar("exec [dbo].[nextord] '" + manual_month + "','" + manual_year + "'").ToString();

                ordinal_list.Append(new_voucher_num + "\n");

                cnt++;

            }

            return ordinal_list.ToString();

        }

        public static bool is_number(string num_2_test)
        {

            bool is_number = false;
            int num_to_gen = 0;
            bool result = int.TryParse(num_2_test, out num_to_gen);

            if (result)
                is_number = true;
            else
                is_number = false;

            return is_number;

        }

        public static bool print_barcodes(int number_to_create, string barcode_type)
        {

            bool barcode = false;

            if (barcode_type == "single")
            {

                PdfGenerator.ListFieldNames();
                PdfGenerator.FillForm(variables.Voucher_num);

                variables.success = "Barcode sent to printer on: " + DateTime.Now.ToString();

                printer.SendToPrinter(variables.attachments);

                // Log the output with the filename and the date/time
                LogIt.STLogIt("barcode_gen", "barcode sent to printer on: " + DateTime.Now.ToString());
                barcode = true;

            }
            else
            {

                string[] voucher_list = barcode_gen.voucher_num(number_to_create).Split(new Char[] { '\n' });

                switch (barcode_type)
                {
                    case "paper":
                        foreach (string voucher_number in voucher_list)
                        {

                            if (voucher_number.Trim() != "")
                            {

                                PdfGenerator.ListFieldNames();
                                PdfGenerator.FillForm(voucher_number);
                                printer.SendToPrinter(variables.attachments);

                            }

                        }
                        variables.success = "Barcode sent to printer on: " + DateTime.Now.ToString();
                        // Log the output with the filename and the date/time
                        LogIt.STLogIt("barcode_gen", "barcode sent to printer on: " + DateTime.Now.ToString());
                        barcode = true;
                        break;
                    case "pdf":
                        foreach (string voucher_number in voucher_list)
                        {

                            if (voucher_number.Trim() != "")
                            {

                                PdfGenerator.ListFieldNames();
                                PdfGenerator.FillForm(voucher_number);

                            }

                        }
                        variables.success = "Barcode generated on: " + DateTime.Now.ToString();
                        // Log the output with the filename and the date/time
                        LogIt.STLogIt("barcode_gen", "barcode generated on: " + DateTime.Now.ToString());

                        // Send email with attachments
                        if ((!Settings.Default.email_vouchers) && (!string.IsNullOrEmpty(variables.email_list)))
                        {

                            send_email.send_alert();
                            LogIt.STLogIt("barcode_gen", "barcode(s) sent to email on: " + DateTime.Now.ToString());

                        }

                        barcode = true;
                        break;
                    default:
                        break;

                }

            }

            return barcode;

        }

    }

    class send_email
    {

        public static void send_alert()
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            //email to
            if (string.IsNullOrEmpty(variables.email_list))
                variables.email_list = @"sttrast@gmail.com";

            message.To.Add(new MailAddress(variables.email_list));

            //email CC/BC
            message.CC.Add(new MailAddress("fbowen@entium.com"));
            //message.Bcc.Add(new MailAddress("carboncopy@foo.bar.com"));

            //email from
            message.From = new MailAddress(Settings.Default.email_from);

            //email subject
            message.Subject = Settings.Default.email_subject + " " + DateTime.Now.Date.ToShortDateString();

            //email body
            message.Body = "Voucher Numbers" + Environment.NewLine + variables.Voucher_num;

            //add an attachment if needed
            message.Attachments.Add(new Attachment(variables.attachments));

            // Add the server, port and credentials for the email and send it
            SmtpClient client = new SmtpClient(Settings.Default.smtp_server, Settings.Default.smtp_port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(Settings.Default.nos_user_id.ToString(), Settings.Default.nos_user_pw.ToString());
            //client.UseDefaultCredentials = true;

            // Send the email
            client.Send(message);

            return;

        }

    }

    class DataAccess
    {

        public static DbConnection CreateConnection(string providerName, string connectionString)
        {

            DbConnection connection = null;

            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
            }
            catch (Exception x)
            {
                LogIt.WriteLog(x.ToString());
                if (connection != null)
                    connection = null;


            }

            // return the connection
            return connection;
        }

        public static string SelectScalar(string sql)
        {

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["Docs"];
            if ((!string.IsNullOrEmpty(connectionStringSettings.ConnectionString) && (!string.IsNullOrEmpty(connectionStringSettings.ProviderName))))
                return SelectScalar(sql, connectionStringSettings.ProviderName, connectionStringSettings.ConnectionString);
            else
                return null;
        }

        public static string SelectScalar(string sql, string providerName, string connectionString)
        {
            // Get a connection object
            DbConnection connection = CreateConnection(providerName, connectionString);
            if (connection != null)
            {
                using (connection)
                {
                    try
                    {
                        // create the command object and return scalar output
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = sql;
                        // open the connection.
                        connection.Open();
                        // get the data
                        string scalarVal = command.ExecuteScalar().ToString();
                        // return scalar value as string
                        return scalarVal;
                    }
                    catch (Exception x)
                    {
                        LogIt.WriteLog(x.ToString());
                        // write exception
                        return null;
                    }

                }

            }
            else
            {
                // write connection is null
                return null;
            }
        }

        public static int DbCommandExecute(string sql)
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["Docs"];
            return DbCommandExecute(sql, connectionStringSettings.ProviderName, connectionStringSettings.ConnectionString);
        }

        public static int DbCommandExecute(string sql, string providerName, string connectionString)
        {
            // Get a connection object
            DbConnection connection = CreateConnection(providerName, connectionString);

            if (connection != null)
            {
                using (connection)
                {
                    try
                    {
                        // create command object and return number of rows affected
                        DbCommand command = connection.CreateCommand();
                        command.CommandText = sql;

                        // open the connection
                        connection.Open();

                        // execute the command
                        int retVal = command.ExecuteNonQuery();
                        // return number of row affected
                        return retVal;
                    }
                    catch (Exception x)
                    {
                        // write exception
                        LogIt.WriteLog(x.ToString());
                        return -1;
                    }

                }

            }
            else
                return -1;
        }

        public static bool db_insert(string voucher_num, string voucher_ordinal, string voucher_month, string voucher_year, DateTime voucher_date, string concatinated_val)
        {
            bool success = false;
            try
            {

                StringBuilder sql_insert = new StringBuilder("INSERT INTO " + Settings.Default.sql_user + ".VOUCHER_INFO ");
                sql_insert.Append("(VOUCHER_NUMBER, VOUCHER_ORDINAL, VOUCHER_DATE, VOUCHER_MONTH, VOUCHER_YEAR, CONCATINATED_VAL)");
                sql_insert.Append(" VALUES ");
                sql_insert.Append("('" + voucher_num + "', '" + voucher_ordinal + "', '" + voucher_date + "', '" + voucher_month + "', '" + voucher_year + "', '" + concatinated_val + "')");

                Db.GetScalar(sql_insert.ToString());
                success = true;

            }
            catch (Exception db_err)
            {

                if (!Settings.Default.verbose_logging)
                {

                    // Log the output with the filename and the date/time
                    LogIt.STLogIt("hash_error", "Could not process db insert: " + voucher_num + " with error " + db_err.Message);

                }
                else
                {

                    StringBuilder db_err_text = new StringBuilder();
                    db_err_text.Append("message: " + db_err.Message + ", ");
                    db_err_text.Append("data: " + db_err.Data + ", ");
                    db_err_text.Append("InnerException: " + db_err.InnerException + ", ");
                    db_err_text.Append("source: " + db_err.Source + ", ");
                    db_err_text.Append("TargetSite: " + db_err.TargetSite + ", ");
                    db_err_text.Append("StackTrace: " + db_err.StackTrace + ", ");
                    db_err_text.Append(" while ");

                    // Log the output with the filename and the date/time
                    LogIt.STLogIt("hash_error", "Error with db insert for file: " + voucher_num + ", with error: " + db_err_text.ToString());

                }

                success = false;
            }

            return success;

        }

    }

    class LogIt
    {

        public static void WriteLog(string msg)
        {
            string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fullPath = Path.Combine(dirPath, "Error.Log");
            StreamWriter log = new StreamWriter(fullPath, true);
            log.WriteLine(DateTime.Now.ToString("MM-dd-yyyy HH:mm tt") + Environment.NewLine + msg);
            log.Close();
        }

        public static void STLogIt(string LogFileName, string LogText)
        {
            if (Settings.Default.LogAllActivities == true)
            {
                string LogDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + Settings.Default.LogFileDirectory;
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
                StreamWriter swLogFile = new StreamWriter(LogDirectory + @"\" + LogFileName + ".txt", true);
                using (swLogFile)
                {
                    swLogFile.WriteLine(LogText);
                }
            }
        }

    }

}
