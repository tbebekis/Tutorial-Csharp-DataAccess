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


/* Posting changes back to the datasource with the DbDataAdapter.Update() method */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        const string MsAccessFileName = @"..\..\..\Lessons.MDB";
        readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

        
        DataSet ds;
        DataTable table;
        DbDataAdapter adapter;      

        private void MainForm_Load(object sender, EventArgs e)
        {
            ds = new DataSet("temp");
            table = ds.Tables.Add("COUNTRY");            

            adapter = factory.CreateDataAdapter();

            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from COUNTRY";
 
                adapter.SelectCommand = cmd;

                /* use FillSchema() to get any constraints from the table */
                adapter.FillSchema(table, SchemaType.Source);
                adapter.Fill(table);

                Grid.DataSource = table;        
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            adapter.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* create a DbCommandBuilder and link it to the adapter */
            DbCommandBuilder commandBuilder = factory.CreateCommandBuilder();
            commandBuilder.DataAdapter = adapter;

            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                /* The SelectCommand is already defined in the MainForm_Load */
                adapter.SelectCommand.Connection = con;

                /* link the auto-generated commands to the adapter */
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                
                /*  instruct adapter to refresh the autoincrement primary key back to the client 
                    NOTE: this will not work for the OleDb and MS Access. It should work with
                    other datasources such as MS SQL, Oracle etc, though.   */
                adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.Both;  
                adapter.InsertCommand.CommandText += Environment.NewLine + "SELECT @@IDENTITY as ID";

                MessageBox.Show(adapter.InsertCommand.CommandText);

                adapter.Update(table);
            }        

        }
    }
}
