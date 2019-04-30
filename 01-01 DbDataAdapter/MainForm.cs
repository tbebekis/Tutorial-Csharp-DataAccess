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



/*   
    Retrieving data: the DbDataAdapter class 
    Using a DbDataAdapter to Fill() a DataSet with multiple tables. 
    A DataSet object is eventually a collection of DataTable objects 
 
    Table and column mapping: DataTableMapping and DataColumnMapping class
    The DbDataAdapter.FillSchema() method
 
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();      
      
            /* add table names to the combo box */
            cboTableNames.Items.AddRange(tableNames);            
        }


        const string MsAccessFileName = @"..\..\..\Lessons.MDB";
        readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");


        DataSet ds = new DataSet();
        string[] tableNames = new string[] { "COUNTRY", "CITY", "TRADER", "MATERIAL", "TRADE", "TRADE_LINES" };




        /*  DbDataAdapter class
            DbDataAdapter.Fill()  
         
            This example fills a single DataTable. No DataSet is involved
         */   
        private void button1_Click(object sender, EventArgs e)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from COUNTRY";

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;

                    /*  The DbDataAdapter class is used in placing a result set into a DataTable or DataSet object.
                        The DbDataAdapter.Fill() overloaded method executes the DbCommand passed to its SelectCommand
                        property and fills either a DataTable or a DataSet by executing the query against the database.  */
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    Grid.DataSource = table;
                }

            }
        }


        /*  selects all tables from the database into a single DataSet object.
            This example fills multiple DataTable objects which added to a DataSet.
            */
        void SelectTables(string[] TableNames)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();                

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;

                    /*  The DbDataAdapter.Fill() uses the DataSet.Tables property.
                        It creates a new DataTable object for each selected table.
                        That new table is added to the Tables of the DataSet.*/
                    foreach (string TableName in TableNames)
                    {
                        cmd.CommandText = string.Format("select * from {0}", TableName);
                        adapter.Fill(ds, TableName);
                    }
                } 

                Grid.DataSource = ds.Tables[0];
            }
        }


        /* Event handler for the cboTableNames combo box SelectedValueChanged event */
        private void cboTableNames_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ds.Tables.Count == 0)
                SelectTables(tableNames);

            Grid.DataSource = ds.Tables[cboTableNames.SelectedIndex];
        }


        /*  DbDataAdapter.FillSchema() method.
            FillSchema() returns schema information based on the DbDataAdapter.SelectCommand.
            
            Also the FillSchema() configures the following DataColumn properties
                * AllowDBNull
                * AutoIncrement (AutoIncrementStep and AutoIncrementSeed must by set manually)
                * MaxLength
                * ReadOnly
                * Unique
                
            and the following DataTable properties
                * PrimaryKey
                * Constraints

            FillSchema() preserves any schema already defined in the DataTable objects. 
         */
        void SelectFillSchema(string SQL, DataTable table)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = SQL;

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
       
                    adapter.SelectCommand = cmd;
                    adapter.FillSchema(table, SchemaType.Source);
                    adapter.Fill(table);
                    Grid.DataSource = table;
                }
            }
        }

        /* displays the constraints of the table */
        void DisplayConstraints(DataTable table)
        {
            string S = "";

            foreach (Constraint constraint in table.Constraints)
                S += "(" + constraint.GetType().Name + ") " + constraint.ToString() + Environment.NewLine;

            if (S != "")
                MessageBox.Show(S);
        }


        /* FillSchema() with a query with a join. No constraints are returned */
        private void button2_Click(object sender, EventArgs e)
        {
            string SQL = @"select CITY.ID as ID, CITY.NAME as NAME, COUNTRY.NAME as COUNTRY " +
                         @"from CITY left join COUNTRY on CITY.COUNTRY_ID = COUNTRY.ID ";

            
            DataTable table = new DataTable("City");            
            table.Columns.Add("Flag", typeof(System.Boolean));

            SelectFillSchema(SQL, table);
            DisplayConstraints(table);
        }

        /* FillSchema() with a simple query on a table */
        private void button3_Click(object sender, EventArgs e)
        {
            string SQL = @"select * from CITY";

            DataTable table = new DataTable("City");
            table.Columns.Add("Flag", typeof(System.Boolean));

            SelectFillSchema(SQL, table);
            DisplayConstraints(table);
        }


    }
}
