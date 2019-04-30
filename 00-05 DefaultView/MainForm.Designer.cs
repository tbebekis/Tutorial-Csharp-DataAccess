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
            this.btnSort = new System.Windows.Forms.Button();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.btnRowFilter = new System.Windows.Forms.Button();
            this.edtSort = new System.Windows.Forms.TextBox();
            this.edtRowFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(-1, 2);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 0;
            this.btnSort.Text = "btnSort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Location = new System.Drawing.Point(-1, 31);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(500, 167);
            this.Grid.TabIndex = 1;
            // 
            // btnRowFilter
            // 
            this.btnRowFilter.Location = new System.Drawing.Point(257, -1);
            this.btnRowFilter.Name = "btnRowFilter";
            this.btnRowFilter.Size = new System.Drawing.Size(75, 23);
            this.btnRowFilter.TabIndex = 2;
            this.btnRowFilter.Text = "btnRowFilter";
            this.btnRowFilter.UseVisualStyleBackColor = true;
            this.btnRowFilter.Click += new System.EventHandler(this.btnRowFilter_Click);
            // 
            // edtSort
            // 
            this.edtSort.Location = new System.Drawing.Point(80, 2);
            this.edtSort.Name = "edtSort";
            this.edtSort.Size = new System.Drawing.Size(171, 20);
            this.edtSort.TabIndex = 4;
            this.edtSort.Text = "Name DESC";
            // 
            // edtRowFilter
            // 
            this.edtRowFilter.Location = new System.Drawing.Point(338, 1);
            this.edtRowFilter.Name = "edtRowFilter";
            this.edtRowFilter.Size = new System.Drawing.Size(144, 20);
            this.edtRowFilter.TabIndex = 5;
            this.edtRowFilter.Text = "ID >= 2 AND ID <= 4";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 199);
            this.Controls.Add(this.edtRowFilter);
            this.Controls.Add(this.edtSort);
            this.Controls.Add(this.btnRowFilter);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.btnSort);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button btnRowFilter;
        private System.Windows.Forms.TextBox edtSort;
        private System.Windows.Forms.TextBox edtRowFilter;
    }
}

