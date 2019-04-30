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


/* Reading database schema information: DbConnection.GetSchema() */
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

        void Initialize()
        {
            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DataTable table = Con.GetSchema();

                foreach (DataRow Row in table.Rows)
                    cboCollections.Items.Add(Row["CollectionName"].ToString());

                cboCollections.SelectedIndex = 0;
            }
        } 

        private void cboCollections_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCollections.SelectedItem != null)
            {
                string Collection = cboCollections.SelectedItem.ToString();

                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    Grid.DataSource = Con.GetSchema(Collection);
                }
            }            
        }


    }
}
