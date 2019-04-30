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
            this.edtName = new System.Windows.Forms.TextBox();
            this.edtName2 = new System.Windows.Forms.TextBox();
            this.edtAge = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtNameNotBound = new System.Windows.Forms.TextBox();
            this.edtAgeNotBound = new System.Windows.Forms.TextBox();
            this.btnSaveNotBound = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edtName
            // 
            this.edtName.Location = new System.Drawing.Point(57, 14);
            this.edtName.Name = "edtName";
            this.edtName.Size = new System.Drawing.Size(100, 20);
            this.edtName.TabIndex = 1;
            // 
            // edtName2
            // 
            this.edtName2.Location = new System.Drawing.Point(217, 14);
            this.edtName2.Name = "edtName2";
            this.edtName2.Size = new System.Drawing.Size(100, 20);
            this.edtName2.TabIndex = 2;
            // 
            // edtAge
            // 
            this.edtAge.Location = new System.Drawing.Point(57, 38);
            this.edtAge.Name = "edtAge";
            this.edtAge.Size = new System.Drawing.Size(100, 20);
            this.edtAge.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Age";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Name (not bound)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Age (not bound)";
            // 
            // edtNameNotBound
            // 
            this.edtNameNotBound.Location = new System.Drawing.Point(111, 98);
            this.edtNameNotBound.Name = "edtNameNotBound";
            this.edtNameNotBound.Size = new System.Drawing.Size(100, 20);
            this.edtNameNotBound.TabIndex = 9;
            this.edtNameNotBound.Text = "no name";
            // 
            // edtAgeNotBound
            // 
            this.edtAgeNotBound.Location = new System.Drawing.Point(111, 123);
            this.edtAgeNotBound.Name = "edtAgeNotBound";
            this.edtAgeNotBound.Size = new System.Drawing.Size(100, 20);
            this.edtAgeNotBound.TabIndex = 10;
            this.edtAgeNotBound.Text = "0";
            // 
            // btnSaveNotBound
            // 
            this.btnSaveNotBound.Location = new System.Drawing.Point(231, 106);
            this.btnSaveNotBound.Name = "btnSaveNotBound";
            this.btnSaveNotBound.Size = new System.Drawing.Size(113, 23);
            this.btnSaveNotBound.TabIndex = 11;
            this.btnSaveNotBound.Text = "btnSaveNotBound";
            this.btnSaveNotBound.UseVisualStyleBackColor = true;
            this.btnSaveNotBound.Click += new System.EventHandler(this.btnSaveNotBound_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 167);
            this.Controls.Add(this.btnSaveNotBound);
            this.Controls.Add(this.edtAgeNotBound);
            this.Controls.Add(this.edtNameNotBound);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtAge);
            this.Controls.Add(this.edtName2);
            this.Controls.Add(this.edtName);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtName;
        private System.Windows.Forms.TextBox edtName2;
        private System.Windows.Forms.TextBox edtAge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edtNameNotBound;
        private System.Windows.Forms.TextBox edtAgeNotBound;
        private System.Windows.Forms.Button btnSaveNotBound;

    }
}

