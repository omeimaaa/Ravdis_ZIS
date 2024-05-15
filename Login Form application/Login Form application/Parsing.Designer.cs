namespace Login_Form_application
{
    partial class Parsing
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
            this.openfile = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Colo = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openfile
            // 
            this.openfile.Location = new System.Drawing.Point(1055, 308);
            this.openfile.Name = "openfile";
            this.openfile.Size = new System.Drawing.Size(174, 43);
            this.openfile.TabIndex = 0;
            this.openfile.Text = "Открыть файл";
            this.openfile.UseVisualStyleBackColor = true;
            this.openfile.Click += new System.EventHandler(this.openfile_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(1083, 496);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(120, 30);
            this.close.TabIndex = 1;
            this.close.Text = "Закрыть";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(12, 12);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(995, 534);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.RichTextBoxText_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1055, 139);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(174, 24);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSignatures_SelectedIndexChanged);
            // 
            // Colo
            // 
            this.Colo.Location = new System.Drawing.Point(1055, 220);
            this.Colo.Name = "Colo";
            this.Colo.Size = new System.Drawing.Size(174, 43);
            this.Colo.TabIndex = 4;
            this.Colo.Text = "Выделить";
            this.Colo.UseVisualStyleBackColor = true;
            this.Colo.Click += new System.EventHandler(this.Colo_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1055, 47);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(174, 44);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Искать";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // Parsing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 686);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.Colo);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.close);
            this.Controls.Add(this.openfile);
            this.Name = "Parsing";
            this.Text = "Parsing";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openfile;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Colo;
        private System.Windows.Forms.Button buttonSearch;
    }
}