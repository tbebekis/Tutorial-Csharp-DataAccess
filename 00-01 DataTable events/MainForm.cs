using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tripous.Extensions;
using Tripous.Data;


/* DataTable events 
 
    DataColumnChangeEventArgs
    DataRowChangeEventArgs
    DataTableNewRowEventArgs
 
    NOTE: this example references the LessonsLib assembly which contains
          extension methods for the DataRow class 
 
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PrepareTable();
        }

        DataTable table = new DataTable("Events");

        /* creates the schema of the table and links to its events */
        void PrepareTable()
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));

            for (int i = 1; i < 6; i++)
                table.Rows.Add(new object[] {i, "Name_" + i.ToString() });

            Grid.DataSource = table;

            table.ColumnChanging += new DataColumnChangeEventHandler(Table_ColumnChanging);
            table.ColumnChanged += new DataColumnChangeEventHandler(Table_ColumnChanged);

            table.RowChanging += new DataRowChangeEventHandler(Table_RowChanging);
            table.RowChanged += new DataRowChangeEventHandler(Table_RowChanged);

            table.RowDeleting += new DataRowChangeEventHandler(Table_RowDeleting);
            table.RowDeleted += new DataRowChangeEventHandler(Table_RowDeleted);

            table.TableNewRow += new DataTableNewRowEventHandler(Table_NewRow);
        
        }

        /* converts a DataRow to a string representation */
        string RowToText(DataRow row)
        {
            string Result = "";

            foreach (DataColumn column in row.Table.Columns)
            {
                Result += column.ColumnName + " = " +  row.AsString(column.ColumnName, "") + "  ";
            }

            return Result;
        }
 

        void Table_ColumnChanging(object sender, DataColumnChangeEventArgs args)
        {
            lboLog.Items.Add(string.Format("ColumnChanging ({0}): Value = {1}, ProposedValue = {2} ", 
                                args.Column.ColumnName,  args.Row[args.Column], args.ProposedValue)) ;
        }

        void Table_ColumnChanged(object sender, DataColumnChangeEventArgs args)
        {
            lboLog.Items.Add(string.Format("ColumnChanged ({0}): Value = {1}, ProposedValue = {2} ",
                    args.Column.ColumnName, args.Row[args.Column], args.ProposedValue));
        }

        void Table_RowChanging(object sender, DataRowChangeEventArgs args)
        {
            lboLog.Items.Add(string.Format("RowChanging (Action : {0})  {1}  ", args.Action, RowToText(args.Row)));
        }

        void Table_RowChanged(object sender, DataRowChangeEventArgs args)
        {
            lboLog.Items.Add(string.Format("RowChanged (Action : {0})  {1}  ", args.Action, RowToText(args.Row)));
        }

        void Table_RowDeleting(object sender, DataRowChangeEventArgs args)
        {
            lboLog.Items.Add(string.Format("RowDeleting (Action : {0})  {1}  ", args.Action, RowToText(args.Row)));
        }

        void Table_RowDeleted(object sender, DataRowChangeEventArgs args)
        {
            //lboLog.Items.Add(string.Format("RowDeleted (Action : {0})  {1}  ", args.Action, RowToText(args.Row)));
        }

        void Table_NewRow(object sender, DataTableNewRowEventArgs args)
        {
            lboLog.Items.Add(string.Format("NewRow : {0} ",  RowToText(args.Row)));
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lboLog.Items.Clear();
        }
 
 

    }
}
