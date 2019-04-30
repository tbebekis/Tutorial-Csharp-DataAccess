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
 


/* DataGrid control */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        const string MsAccessFileName = @"..\..\..\Lessons.MDB";
        readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

        DataSet ds = new DataSet("Lessons");
        string[] tableNames = new string[] { "COUNTRY", "CITY", "TRADER", "MATERIAL", "TRADE", "TRADE_LINES" };

        /* selects TableNames tables from the database into the ds DataSet object. */
        void SelectTables(string[] TableNames)
        {
            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();

                using (DbDataAdapter Adapter = factory.CreateDataAdapter())
                {
                    Adapter.SelectCommand = Cmd;

                    foreach (string TableName in TableNames)
                    {
                        Cmd.CommandText = string.Format("select * from {0}", TableName);
                        Adapter.FillSchema(ds, SchemaType.Source, TableName);
                        Adapter.Fill(ds, TableName);
                    }
                }
            }
        }

        void Initialize()
        {
            SelectTables(tableNames);

            ds.Relations.Add("", ds.Tables["COUNTRY"].Columns["ID"], ds.Tables["CITY"].Columns["COUNTRY_ID"]);
            ds.Relations.Add("", ds.Tables["TRADER"].Columns["ID"], ds.Tables["TRADE"].Columns["TRADER_ID"]);
 
            ds.Relations.Add("", ds.Tables["TRADE"].Columns["ID"], ds.Tables["TRADE_LINES"].Columns["TRADE_ID"]);
            ds.Relations.Add("", ds.Tables["MATERIAL"].Columns["ID"], ds.Tables["TRADE_LINES"].Columns["MATERIAL_ID"]);

            Grid.DataSource = ds;
        }
    }
}
