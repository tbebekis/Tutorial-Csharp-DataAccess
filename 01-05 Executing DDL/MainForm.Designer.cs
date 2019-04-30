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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Pager = new System.Windows.Forms.TabControl();
            this.tabEditor = new System.Windows.Forms.TabPage();
            this.btnExecStoreProc = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.Editor = new System.Windows.Forms.TextBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.Pager.SuspendLayout();
            this.tabEditor.SuspendLayout();
            this.tabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Pager
            // 
            this.Pager.Controls.Add(this.tabEditor);
            this.Pager.Controls.Add(this.tabGrid);
            this.Pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pager.Location = new System.Drawing.Point(0, 0);
            this.Pager.Name = "Pager";
            this.Pager.SelectedIndex = 0;
            this.Pager.Size = new System.Drawing.Size(587, 515);
            this.Pager.TabIndex = 2;
            // 
            // tabEditor
            // 
            this.tabEditor.Controls.Add(this.btnExecStoreProc);
            this.tabEditor.Controls.Add(this.btnSelect);
            this.tabEditor.Controls.Add(this.Editor);
            this.tabEditor.Controls.Add(this.btnExec);
            this.tabEditor.Location = new System.Drawing.Point(4, 22);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditor.Size = new System.Drawing.Size(579, 489);
            this.tabEditor.TabIndex = 0;
            this.tabEditor.Text = "Editor";
            this.tabEditor.UseVisualStyleBackColor = true;
            // 
            // btnExecStoreProc
            // 
            this.btnExecStoreProc.Location = new System.Drawing.Point(290, 6);
            this.btnExecStoreProc.Name = "btnExecStoreProc";
            this.btnExecStoreProc.Size = new System.Drawing.Size(113, 23);
            this.btnExecStoreProc.TabIndex = 5;
            this.btnExecStoreProc.Text = "btnExecStoreProc";
            this.btnExecStoreProc.UseVisualStyleBackColor = true;
            this.btnExecStoreProc.Click += new System.EventHandler(this.btnExecStoreProc_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(87, 6);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // Editor
            // 
            this.Editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Editor.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Editor.Location = new System.Drawing.Point(9, 33);
            this.Editor.Multiline = true;
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(562, 448);
            this.Editor.TabIndex = 3;
            this.Editor.Text = resources.GetString("Editor.Text");
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(6, 6);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 23);
            this.btnExec.TabIndex = 2;
            this.btnExec.Text = "btnExec";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.Grid);
            this.tabGrid.Location = new System.Drawing.Point(4, 22);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrid.Size = new System.Drawing.Size(599, 429);
            this.tabGrid.TabIndex = 1;
            this.tabGrid.Text = "Grid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // Grid
            // 
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(3, 3);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(593, 423);
            this.Grid.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 515);
            this.Controls.Add(this.Pager);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Pager.ResumeLayout(false);
            this.tabEditor.ResumeLayout(false);
            this.tabEditor.PerformLayout();
            this.tabGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Pager;
        private System.Windows.Forms.TabPage tabEditor;
        private System.Windows.Forms.TextBox Editor;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.TabPage tabGrid;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnExecStoreProc;

    }
}

