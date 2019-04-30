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



/* DataGridView control 
 
   Defining a lookup column 
 */
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
            SelectTables(new string[] { "COUNTRY", "CITY" } );

            Grid.DataSource = ds.Tables["CITY"];

            /* add a lookup column  */
            DataGridViewComboBoxColumn colLookUp = new DataGridViewComboBoxColumn();
            colLookUp.HeaderText = "Country";
            colLookUp.DataSource = ds.Tables["COUNTRY"];
            colLookUp.DataPropertyName = "COUNTRY_ID";
            
            colLookUp.DisplayMember = "NAME";
            colLookUp.ValueMember = "ID";
            colLookUp.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            Grid.Columns.Insert(2, colLookUp);

        }
    }
}
