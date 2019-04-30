using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*  DataTable, DataColumnCollection and DataColumn class: defining table schema
    DataRowCollection and DataRow class: adding data to a table
    Iterating a DataTable and reading data
    Iterating a table through a DataTableReader
    DBNull values

    DataGridView shortcut keys
    ------------------------------------------------
    Select a row:   Shift + Space 
    Delete a row:   Shift + Space + Delete
    Insert a row:   Arrow down at the end of the rows
 
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /* DataTable class: defining table schema */
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable("Person");
            DataColumn column;

            column = new DataColumn();                              // using the DataColumn constructor
            column.ColumnName = "ID";
            column.DataType = typeof(int);
            column.AutoIncrement = true;
            column.AutoIncrementSeed = -1;
            column.AutoIncrementStep = -1;
            table.Columns.Add(column);                              // a column  needs to be added to the columns            

            column = new DataColumn("FirstName", typeof(string));   // using a different constructor
            column.MaxLength = 12;
            column.AllowDBNull = true;
            column.DefaultValue = "<first name>";
            table.Columns.Add(column);

            column = table.Columns.Add("LastName", typeof(string)); // the Columns.Add() provides handy overloads
            column.AllowDBNull = false;
            column.Unique = true;

            column = table.Columns.Add("Married", typeof(bool));

            column = table.Columns.Add("Name", typeof(string), "LastName + ', ' + FirstName"); 

            Grid.DataSource = table;
        }

        /* DataRowCollection and DataRow class: adding data to a table */
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));

            DataRow row;
            for (int i = 1; i < 6; i++)
            {
                row = table.NewRow();                   // DataTable.NewRow() creates a new row. The row is not added to the rows.
                
                row[0] = i;                             // accessing a column by its index (DataColumn.Ordinal property)                 
                row["Name"] = "Name_" + i.ToString();   // accdessing a column by its ColumnName

                table.Rows.Add(row);                    // DataTable.Rows.Add() adds a row to the Rows.
            }

            Grid.DataSource = table;
        }


        /*  a variation of adding data to a table. 
            Creating and adding a row to a table in a single statement with DataTable.Rows.Add().
            
            NOTE: When schema of the table includes auto-computed columns, such as an auto-increment column
            or an expression column, use the null value in any positional ADO.NET call, such as
            the DataTable.Rows.Add() overload used below         
         */
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(int)).AutoIncrement = true;
            table.Columns.Add("Name", typeof(string));

            for (int i = 1; i < 6; i++)
            {
                table.Rows.Add( new object[] { null, "Name_" + i.ToString() } );
            }

            Grid.DataSource = table;
        }

        /* creates a demo table */
        DataTable CreateTempTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));

            for (int i = 1; i < 6; i++)
                table.Rows.Add(new object[] { i, "Name_" + i.ToString() });

            return table;
        }

        /* iterating a table and reading data by typecasting column values */
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable table = CreateTempTable();

            int ID;
            string Name;

            string S = "";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                ID = (int)table.Rows[i]["ID"];
                Name = (string)table.Rows[i]["Name"];

                S += ID.ToString() + ", " + Name + Environment.NewLine;
            }

            MessageBox.Show(S);

        }

        /* iterating a table and reading data using an Enumerator (foreach) and the DataRow.Field<T>() generic method */
        private void button5_Click(object sender, EventArgs e)
        {
            DataTable table = CreateTempTable();

            int ID;
            string Name;

            string S = ""; 

            foreach (DataRow row in table.Rows)
            {
                ID = row.Field<int>("ID");
                Name = row.Field<string>("Name");  

                S += ID.ToString() + ", " + Name + Environment.NewLine;
            }

            MessageBox.Show(S);
        }

        /* iterating by using a DataTableReader object and the Read() method */
        private void button6_Click(object sender, EventArgs e)
        {
            DataTable table = CreateTempTable();

            int ID;
            string Name;

            string S = "";

            using (DataTableReader reader = new DataTableReader(table))
            {
                while (reader.Read())
                {
                    ID = (int)reader["ID"];
                    Name = (string)reader["Name"];

                    S += ID.ToString() + ", " + Name + Environment.NewLine;
                }
            }

            MessageBox.Show(S);
        }

        /* iterating by using a DataTableReader object and a foreach statement */
        private void button7_Click(object sender, EventArgs e)
        {
            DataTable table = CreateTempTable();

            int ID;
            string Name;

            string S = "";

            using (DataTableReader reader = new DataTableReader(table))
            {
                foreach (DbDataRecord record in reader)
                {
                    ID = (int)record["ID"];
                    Name = (string)record["Name"];

                    S += ID.ToString() + ", " + Name + Environment.NewLine;
                }
            }

            MessageBox.Show(S);
        }

        /* DBNull values */
        private void button8_Click(object sender, EventArgs e)
        {
            DataTable table = CreateTempTable();

            table.Rows[0]["Name"] = DBNull.Value;

            if (table.Rows[0]["Name"] == DBNull.Value) // or if (DBNull.Value.Equals(table.Rows[0]["Name"]))
            {
                MessageBox.Show("NULL");
            }
        }
    }
}
