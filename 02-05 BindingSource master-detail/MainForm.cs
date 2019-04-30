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


/* BindingSource: a master-detail example */
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
        BindingSource bsMaster = new BindingSource();
        BindingSource bsDetail = new BindingSource();


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
            /* select the two tables */
            SelectTables(new string[] {"COUNTRY", "CITY"});

            /* create a relation between those two tables */
            ds.Relations.Add("CountryCities", ds.Tables["COUNTRY"].Columns["ID"], ds.Tables["CITY"].Columns["COUNTRY_ID"]);

            /* the top master BindingSource. It gets as DataSource the ds DataSet 
               and as DataMember the name of the COUNTRY table */
            bsMaster.DataSource = ds;
            bsMaster.DataMember = "COUNTRY"; 

            /* the detail BindingSource. It gets as DataSource a master BindingSource 
               and as DataMember the name of a proper Relation object */
            bsDetail.DataSource = bsMaster;
            bsDetail.DataMember = "CountryCities";

            /* set DataGridView datasources  */
            gridMaster.DataSource = bsMaster;
            gridDetail.DataSource = bsDetail;

        }

    }
}
