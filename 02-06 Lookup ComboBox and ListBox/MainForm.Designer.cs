namespace Lessons
{
    partial class MainForm
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
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.lboCountry = new System.Windows.Forms.ListBox();
            this.edtComboBoxValue = new System.Windows.Forms.TextBox();
            this.edtListBoxValue = new System.Windows.Forms.TextBox();
            this.Grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCountry
            // 
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Location = new System.Drawing.Point(12, 12);
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(167, 21);
            this.cboCountry.TabIndex = 1;
            this.cboCountry.SelectedValueChanged += new System.EventHandler(this.cboCountry_SelectedValueChanged);
            // 
            // lboCountry
            // 
            this.lboCountry.FormattingEnabled = true;
            this.lboCountry.Location = new System.Drawing.Point(13, 45);
            this.lboCountry.Name = "lboCountry";
            this.lboCountry.Size = new System.Drawing.Size(166, 121);
            this.lboCountry.TabIndex = 2;
            this.lboCountry.SelectedValueChanged += new System.EventHandler(this.lboCountry_SelectedValueChanged);
            // 
            // edtComboBoxValue
            // 
            this.edtComboBoxValue.Location = new System.Drawing.Point(195, 12);
            this.edtComboBoxValue.Name = "edtComboBoxValue";
            this.edtComboBoxValue.Size = new System.Drawing.Size(76, 20);
            this.edtComboBoxValue.TabIndex = 3;
            // 
            // edtListBoxValue
            // 
            this.edtListBoxValue.Location = new System.Drawing.Point(195, 44);
            this.edtListBoxValue.Name = "edtListBoxValue";
            this.edtListBoxValue.Size = new System.Drawing.Size(76, 20);
            this.edtListBoxValue.TabIndex = 4;
            // 
            // Grid
            // 
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Location = new System.Drawing.Point(321, 12);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.Size = new System.Drawing.Size(386, 144);
            this.Grid.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 200);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.edtListBoxValue);
            this.Controls.Add(this.edtComboBoxValue);
            this.Controls.Add(this.lboCountry);
            this.Controls.Add(this.cboCountry);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.ListBox lboCountry;
        private System.Windows.Forms.TextBox edtComboBoxValue;
        private System.Windows.Forms.TextBox edtListBoxValue;
        private System.Windows.Forms.DataGridView Grid;
    }
}

