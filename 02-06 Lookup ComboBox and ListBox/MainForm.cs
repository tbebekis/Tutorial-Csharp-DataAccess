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



/* Lookup ComboBox and ListBox */
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
            /* select the two tables */
            SelectTables(new string[] { "COUNTRY", "CITY" });
            Grid.DataSource = ds.Tables["CITY"];


            /* define the "source" of the data for the control */
            cboCountry.DataSource = ds.Tables["COUNTRY"];
            cboCountry.DisplayMember = "NAME";
            cboCountry.ValueMember = "ID";

            /* define where to "put" the ValueMember value */
            cboCountry.DataBindings.Add("SelectedValue", ds.Tables["CITY"], "COUNTRY_ID");

            /* Here is a trick: BindingContexts create binging managers based on datasources. 
               A binding manager per datasource. So passing here a "different" datasource
               to the ListBox than the one passed to the ComboBox, creates two distinct 
               binding managers, although both controls display the same table  */
            /* define the "source" of the data for the control */
            lboCountry.DataSource = ds;
            lboCountry.DisplayMember = "COUNTRY.NAME";
            lboCountry.ValueMember = "COUNTRY.ID";

            /* define where to "put" the ValueMember value */
            lboCountry.DataBindings.Add("SelectedValue", ds.Tables["CITY"], "COUNTRY_ID"); 
        } 

        private void cboCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCountry.SelectedIndex >= 0)
                edtComboBoxValue.Text = cboCountry.SelectedValue.ToString();
        }

        private void lboCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lboCountry.SelectedIndex >= 0)
                edtListBoxValue.Text = lboCountry.SelectedValue.ToString();
        }
    }
}
