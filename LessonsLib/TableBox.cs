 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tripous.Data
{
 

    using Tripous.Extensions;

    public class TableBox : Form
    {
        public TableBox()
        {
            InitializeComponent();
        }

        #region Windows Form Designer generated code

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Grid = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Location = new System.Drawing.Point(0, 2);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(291, 244);
            this.Grid.TabIndex = 0;
            this.Grid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Grid_CellMouseDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(135, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // TableBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Grid);
            this.MinimizeBox = false;
            this.Name = "TableBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TableBox";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;

        #endregion
        
        /// <summary>
        /// handles the Grid double click 
        /// </summary>
        private void Grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btnOK.Enabled)
                btnOK.PerformClick();
        }
        /// <summary>
        /// Initializes the dialog box
        /// </summary>
        private void Initialize(DataTable Table)
        {
            if (!string.IsNullOrEmpty(Table.TableName))
                this.Text = Table.TableName;

            Grid.DataSource = Table;
            Grid.ReadOnly = true;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // ColumnHeader DisplayedCells Fill AllCells
            Grid.AllowUserToDeleteRows = false;
            Grid.AllowUserToAddRows = false;
            Grid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  


            foreach (DataColumn column in Table.Columns)
            {
                Grid.Columns[column.ColumnName].HeaderText = column.Caption;
                Grid.Columns[column.ColumnName].Visible = column.ExtendedProperties.AsBoolean("Visible", true); //Getter.AsBool(column.ExtendedProperties, "Visible", true);
            }

            btnOK.Enabled = Grid.RowCount > 0;
        }

        /// <summary>
        /// Displays the dialog box. Returns true if the user selects a row.
        /// Use the Column.ExtendedProperties to pass a Visible property for each column.
        /// </summary>
        static public bool Display(DataTable Table, out DataRow Row)
        {
            bool Result = false;
            Row = null;

            if (Table == null)
                throw new ArgumentNullException("Table", "TableBox can not display a null table");

            using (TableBox Dlg = new TableBox())
            {
                Dlg.Initialize(Table);
                Result = Dlg.ShowDialog() == DialogResult.OK;
                if (Result)
                {
                    Row = ((DataRowView)Dlg.Grid.CurrentRow.DataBoundItem).Row;
                }
            }

            return Result;
        }
        /// <summary>
        /// Displays the dialog box. 
        /// Use the Column.ExtendedProperties to pass a Visible property for each column.
        /// </summary>
        static public void Display(DataTable Table)
        {
            DataRow Row;
            Display(Table, out Row);
        }
        /// <summary>
        /// Displays the dialog box. 
        /// </summary>
        static public void DisplayRow(DataRow SourceRow)
        {
            string TableName = "Row";
            if (!string.IsNullOrEmpty(SourceRow.Table.TableName))
                TableName = string.Format("Row: {0}", SourceRow.Table.TableName);

            DataTable Table = new DataTable(TableName);
            Table.Columns.Add("Ordinal", typeof(int));
            Table.Columns.Add("Column", typeof(string));
            Table.Columns.Add("Caption", typeof(string));
            Table.Columns.Add("Value", typeof(string));
            Table.Columns.Add("Type", typeof(string));

            DataRow row;

            foreach (DataColumn column in SourceRow.Table.Columns)
            {
                row = Table.NewRow();

                row["Ordinal"] = column.Ordinal;
                row["Column"] = column.ColumnName;
                row["Caption"] = column.Caption;
                row["Type"] = column.DataType.ToString();
                if (!SourceRow.IsNull(column))
                    row["Value"] = SourceRow[column].ToString();

                Table.Rows.Add(row);
            }

            Display(Table);
        }
    }

 

}
