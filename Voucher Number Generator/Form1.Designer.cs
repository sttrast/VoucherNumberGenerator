namespace Voucher_Number_Generator
{
    partial class ETP_Voucher_Generator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ETP_Voucher_Generator));
            this.gen_voucher_num = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.gen_barcodes_paper = new System.Windows.Forms.RadioButton();
            this.gen_barcodes_PDFs = new System.Windows.Forms.RadioButton();
            this.number_generated = new System.Windows.Forms.TextBox();
            this.gen_voucher_label = new System.Windows.Forms.Label();
            this.number_warning = new System.Windows.Forms.Label();
            this.email_label = new System.Windows.Forms.Label();
            this.reset_form = new System.Windows.Forms.Button();
            this.gen_single_barcode = new System.Windows.Forms.RadioButton();
            this.cmb_email_addresses = new System.Windows.Forms.ComboBox();
            this.single_voucher_num = new System.Windows.Forms.TextBox();
            this.number_generated_2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stv_image = new System.Windows.Forms.PictureBox();
            this.voucher_date = new System.Windows.Forms.DateTimePicker();
            this.date_picker_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.stv_image)).BeginInit();
            this.SuspendLayout();
            // 
            // gen_voucher_num
            // 
            this.gen_voucher_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_voucher_num.Location = new System.Drawing.Point(27, 257);
            this.gen_voucher_num.Name = "gen_voucher_num";
            this.gen_voucher_num.Size = new System.Drawing.Size(75, 32);
            this.gen_voucher_num.TabIndex = 3;
            this.gen_voucher_num.Text = "Go";
            this.gen_voucher_num.UseVisualStyleBackColor = true;
            this.gen_voucher_num.Click += new System.EventHandler(this.gen_voucher_num_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel.Location = new System.Drawing.Point(196, 258);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 31);
            this.Cancel.TabIndex = 5;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // gen_barcodes_paper
            // 
            this.gen_barcodes_paper.AutoSize = true;
            this.gen_barcodes_paper.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_barcodes_paper.Location = new System.Drawing.Point(27, 113);
            this.gen_barcodes_paper.Name = "gen_barcodes_paper";
            this.gen_barcodes_paper.Size = new System.Drawing.Size(99, 24);
            this.gen_barcodes_paper.TabIndex = 6;
            this.gen_barcodes_paper.TabStop = true;
            this.gen_barcodes_paper.Text = "Generate ";
            this.gen_barcodes_paper.UseVisualStyleBackColor = true;
            this.gen_barcodes_paper.CheckedChanged += new System.EventHandler(this.gen_barcodes_paper_CheckedChanged);
            // 
            // gen_barcodes_PDFs
            // 
            this.gen_barcodes_PDFs.AutoSize = true;
            this.gen_barcodes_PDFs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_barcodes_PDFs.Location = new System.Drawing.Point(27, 148);
            this.gen_barcodes_PDFs.Name = "gen_barcodes_PDFs";
            this.gen_barcodes_PDFs.Size = new System.Drawing.Size(95, 24);
            this.gen_barcodes_PDFs.TabIndex = 1;
            this.gen_barcodes_PDFs.TabStop = true;
            this.gen_barcodes_PDFs.Text = "Generate";
            this.gen_barcodes_PDFs.UseVisualStyleBackColor = true;
            this.gen_barcodes_PDFs.CheckedChanged += new System.EventHandler(this.gen_barcodes_PDFs_CheckedChanged);
            // 
            // number_generated
            // 
            this.number_generated.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.number_generated.Location = new System.Drawing.Point(126, 112);
            this.number_generated.Name = "number_generated";
            this.number_generated.Size = new System.Drawing.Size(47, 26);
            this.number_generated.TabIndex = 0;
            this.number_generated.TextChanged += new System.EventHandler(this.number_generated_TextChanged);
            // 
            // gen_voucher_label
            // 
            this.gen_voucher_label.AutoSize = true;
            this.gen_voucher_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_voucher_label.Location = new System.Drawing.Point(428, 263);
            this.gen_voucher_label.Name = "gen_voucher_label";
            this.gen_voucher_label.Size = new System.Drawing.Size(69, 20);
            this.gen_voucher_label.TabIndex = 6;
            this.gen_voucher_label.Text = "* Max #: ";
            // 
            // number_warning
            // 
            this.number_warning.AutoSize = true;
            this.number_warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.number_warning.Location = new System.Drawing.Point(23, 79);
            this.number_warning.Name = "number_warning";
            this.number_warning.Size = new System.Drawing.Size(137, 20);
            this.number_warning.TabIndex = 7;
            this.number_warning.Text = "Select one option:";
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.email_label.Location = new System.Drawing.Point(184, 150);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(209, 20);
            this.email_label.TabIndex = 10;
            this.email_label.Text = "voucher number(s) to PDF*. ";
            // 
            // reset_form
            // 
            this.reset_form.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reset_form.Location = new System.Drawing.Point(111, 258);
            this.reset_form.Name = "reset_form";
            this.reset_form.Size = new System.Drawing.Size(75, 31);
            this.reset_form.TabIndex = 4;
            this.reset_form.Text = "Reset";
            this.reset_form.UseVisualStyleBackColor = true;
            this.reset_form.Click += new System.EventHandler(this.reset_form_Click);
            // 
            // gen_single_barcode
            // 
            this.gen_single_barcode.AutoSize = true;
            this.gen_single_barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_single_barcode.Location = new System.Drawing.Point(27, 186);
            this.gen_single_barcode.Name = "gen_single_barcode";
            this.gen_single_barcode.Size = new System.Drawing.Size(482, 24);
            this.gen_single_barcode.TabIndex = 2;
            this.gen_single_barcode.TabStop = true;
            this.gen_single_barcode.Text = "Generate a single separator page by voucher #. Enter voucher #:";
            this.gen_single_barcode.UseVisualStyleBackColor = true;
            this.gen_single_barcode.CheckedChanged += new System.EventHandler(this.gen_single_barcode_CheckedChanged);
            // 
            // cmb_email_addresses
            // 
            this.cmb_email_addresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_email_addresses.FormattingEnabled = true;
            this.cmb_email_addresses.Location = new System.Drawing.Point(547, 147);
            this.cmb_email_addresses.Name = "cmb_email_addresses";
            this.cmb_email_addresses.Size = new System.Drawing.Size(209, 28);
            this.cmb_email_addresses.TabIndex = 16;
            this.cmb_email_addresses.SelectedIndexChanged += new System.EventHandler(this.cmb_email_addresses_SelectedIndexChanged);
            // 
            // single_voucher_num
            // 
            this.single_voucher_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.single_voucher_num.Location = new System.Drawing.Point(515, 185);
            this.single_voucher_num.Name = "single_voucher_num";
            this.single_voucher_num.Size = new System.Drawing.Size(90, 26);
            this.single_voucher_num.TabIndex = 120;
            this.single_voucher_num.TextChanged += new System.EventHandler(this.single_voucher_num_TextChanged);
            // 
            // number_generated_2
            // 
            this.number_generated_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.number_generated_2.Location = new System.Drawing.Point(126, 147);
            this.number_generated_2.Name = "number_generated_2";
            this.number_generated_2.Size = new System.Drawing.Size(47, 26);
            this.number_generated_2.TabIndex = 110;
            this.number_generated_2.TextChanged += new System.EventHandler(this.number_generated_2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(184, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "voucher number(s) to printer.  ";
            // 
            // stv_image
            // 
            this.stv_image.Image = ((System.Drawing.Image)(resources.GetObject("stv_image.Image")));
            this.stv_image.Location = new System.Drawing.Point(257, 11);
            this.stv_image.Name = "stv_image";
            this.stv_image.Size = new System.Drawing.Size(206, 73);
            this.stv_image.TabIndex = 20;
            this.stv_image.TabStop = false;
            // 
            // voucher_date
            // 
            this.voucher_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voucher_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.voucher_date.Location = new System.Drawing.Point(186, 215);
            this.voucher_date.Name = "voucher_date";
            this.voucher_date.Size = new System.Drawing.Size(200, 31);
            this.voucher_date.TabIndex = 21;
            this.voucher_date.Value = new System.DateTime(2013, 12, 23, 10, 50, 42, 0);
            this.voucher_date.ValueChanged += new System.EventHandler(this.voucher_date_ValueChanged);
            // 
            // date_picker_label
            // 
            this.date_picker_label.AutoSize = true;
            this.date_picker_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_picker_label.Location = new System.Drawing.Point(23, 224);
            this.date_picker_label.Name = "date_picker_label";
            this.date_picker_label.Size = new System.Drawing.Size(160, 20);
            this.date_picker_label.TabIndex = 22;
            this.date_picker_label.Text = "Voucher Month/Year:";
            // 
            // ETP_Voucher_Generator
            // 
            this.AcceptButton = this.gen_voucher_num;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(784, 312);
            this.Controls.Add(this.date_picker_label);
            this.Controls.Add(this.voucher_date);
            this.Controls.Add(this.stv_image);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.number_generated_2);
            this.Controls.Add(this.single_voucher_num);
            this.Controls.Add(this.cmb_email_addresses);
            this.Controls.Add(this.gen_single_barcode);
            this.Controls.Add(this.reset_form);
            this.Controls.Add(this.email_label);
            this.Controls.Add(this.number_warning);
            this.Controls.Add(this.gen_voucher_label);
            this.Controls.Add(this.number_generated);
            this.Controls.Add(this.gen_barcodes_PDFs);
            this.Controls.Add(this.gen_barcodes_paper);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.gen_voucher_num);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ETP_Voucher_Generator";
            this.Text = "AP Vouchering";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stv_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gen_voucher_num;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.RadioButton gen_barcodes_paper;
        private System.Windows.Forms.RadioButton gen_barcodes_PDFs;
        private System.Windows.Forms.TextBox number_generated;
        private System.Windows.Forms.Label gen_voucher_label;
        private System.Windows.Forms.Label number_warning;
        private System.Windows.Forms.Label email_label;
        private System.Windows.Forms.Button reset_form;
        private System.Windows.Forms.RadioButton gen_single_barcode;
        private System.Windows.Forms.ComboBox cmb_email_addresses;
        private System.Windows.Forms.TextBox single_voucher_num;
        private System.Windows.Forms.TextBox number_generated_2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox stv_image;
        private System.Windows.Forms.DateTimePicker voucher_date;
        private System.Windows.Forms.Label date_picker_label;
    }
}

