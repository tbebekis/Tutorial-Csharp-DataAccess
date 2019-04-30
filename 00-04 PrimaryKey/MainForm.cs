using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Tripous.Data;

/*  
    DataTable.PrimaryKey and primary key constraints
    DataTable.Find() and Select() methods
    DataTable.Compute() method
    saving/loading a data table to/from xml
    DataTable.Copy() and Clone() methods

 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PrepareData();
        }

        DataTable table = new DataTable("Demo");

        /* creates a DataTable and populates its rows with sample data.  */
        void PrepareData()
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            table.PrimaryKey = new DataColumn[] { table.Columns["ID"], table.Columns["Name"] };
            // or  table.Constraints.Add("PK_DEMO", new DataColumn[] { table.Columns["ID"], table.Columns["Name"] }, true);

            for (int i = 1; i < 11; i++)
                table.Rows.Add(new object[] { i, "Name_" + i.ToString(), DateTime.Today.AddDays(i) });

            Grid.DataSource = table;
        }

        /*  the Rows.Find() method.
            The DataRowCollection.Contains() and DataRowCollection.Find() methods
            work only if a primary key is defined in the table */
        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row = table.Rows.Find(new object[] { 5, "Name_5" }); // Find() searches based on the PrimaryKey
            int i = table.Rows.IndexOf(row);

            MessageBox.Show("Row found at index : " + i.ToString());
        }


        /* converts a DataRow to a string representation */
        string RowToText(DataRow row)
        {
            string Result = "";

            foreach (DataColumn column in row.Table.Columns)
            {
                Result += column.ColumnName + " = " + row[column].ToString() + "  ";
            }

            return Result;
        }

        /*  using the DataTable.Select() method 
            see also: DataColumn.Expression property in online help */
        private void button2_Click(object sender, EventArgs e)
        {
            string expression = "Date >= '{0}' and Date <= '{1}' ";
            expression = string.Format(expression, DateTime.Today.AddDays(5).ToShortDateString(), DateTime.Today.AddDays(7).ToShortDateString());

            DataRow[] rows = table.Select(expression);

            string S = "";

            foreach (DataRow row in rows)
            {
                S += RowToText(row) + Environment.NewLine;
            }

            MessageBox.Show(S);
        }


        /* using the DataTable.Compute() method */
        private void button3_Click(object sender, EventArgs e)
        {
            object Result = table.Compute("Sum(ID)", "ID > 5");

            MessageBox.Show(Result.GetType().Name + ": " + Result.ToString());

        }

        /* saving and loading a table to/from an xml file */
        private void button4_Click(object sender, EventArgs e)
        {
            string FileName = Path.GetFullPath(@"..\..\Table.XML");
            table.WriteXml(FileName, XmlWriteMode.WriteSchema);

            DataTable temp = new DataTable();
            temp.ReadXml(FileName);

            TableBox.Display(temp);

        }

        /* the DataTable.Copy() and Clone() methods */
        private void button5_Click(object sender, EventArgs e)
        {
            DataTable temp = table.Copy();
            TableBox.Display(temp);

            temp = table.Clone();
            TableBox.Display(temp);

        }
    }
}
