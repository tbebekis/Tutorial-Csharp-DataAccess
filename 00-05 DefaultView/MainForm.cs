using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.SqlClient;

/*  
    DataView class: DataTable.DefaultView property
 
 */
namespace Lessons
{
 

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PrepareDataTable();
        }

        DataTable table = new DataTable("City"); 
 
        void PrepareDataTable()
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add(new object[] {1, "Paris" });
            table.Rows.Add(new object[] {2, "London" });
            table.Rows.Add(new object[] {3, "Athens" });
            table.Rows.Add(new object[] {4, "Berlin" });
            table.Rows.Add(new object[] {5, "Rome" });

            Grid.DataSource = table.DefaultView;            
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            table.DefaultView.Sort = edtSort.Text;
        }

        private void btnRowFilter_Click(object sender, EventArgs e)
        {
            table.DefaultView.RowFilter = edtRowFilter.Text;
        } 
    }
}
