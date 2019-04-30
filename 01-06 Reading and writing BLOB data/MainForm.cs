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
using Tripous.Extensions;
using Tripous.Data;


/* Reading and writing BLOB data */

namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SelectTable();
        }

        const string MsAccessFileName = @"..\..\..\Lessons.MDB";
        readonly string cs = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");


        /* arranges the user interface */
        void EnableCommands()
        {
            DataTable Table = Grid.DataSource as DataTable;

            btnUpdate.Enabled = (Table != null) && (Grid.RowCount > 0);
            btnDelete.Enabled = btnUpdate.Enabled;
 
        }


        /* selects the PICTURES table and binds Grid and picBox to data */
        void SelectTable()
        {
            picBox.DataBindings.Clear();
            Grid.DataSource = null;

            DataTable Table = new DataTable("PICTURES");

            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();
                Cmd.CommandText = "select * from PICTURES";

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = Cmd;
                    adapter.FillSchema(Table, SchemaType.Source);
                    adapter.Fill(Table);

                    Grid.DataSource = Table;

                    /* add a proper DataBinding to picBox */
                    picBox.DataBindings.Add("Image", Table, "IMG", true, DataSourceUpdateMode.OnPropertyChanged);
                }
            }

            EnableCommands();
        }


        /* inserts a picture from disk to the database  */
        private void btnInsert_Click(object sender, EventArgs e)
        {    
            OpenFileDialog Dlg = new OpenFileDialog();
  
            Dlg.Filter = "bitmaps (*.bmp)|*.bmp|Jpegs (*.jpg)|*.jpg";       

            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                /* File.ReadAllBytes() reads the file and returns its data as an array of bytes */
                byte[] Bytes = File.ReadAllBytes(Dlg.FileName);

                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    DbCommand Cmd = Con.CreateCommand();
                    Cmd.CommandText = "insert into PICTURES (IMG) values (?)";

                    DbParameter Param = Cmd.CreateParameter();
                    Param.ParameterName = "IMG";                    

                    /*  after next assignement the DbParameter infers is DbType as DbType.Binary  
                        which permits a length of up to 8000 bytes. Specific data provider types
                        such as the SqlDbType.Image of the SqlClient allows for greater length. */
                    Param.Value = Bytes; 
                    Cmd.Parameters.Add(Param);

                    //MessageBox.Show(Param.DbType.ToString());
                    //MessageBox.Show(Param.Size.ToString());

                    Cmd.ExecuteNonQuery();                    
                }

                SelectTable();
            }           

        }



        /* updates a row with a picture to the database */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Grid.HasCurrentDataRow())
                return;

            OpenFileDialog Dlg = new OpenFileDialog();

            Dlg.Filter = "bitmaps (*.bmp)|*.bmp|Jpegs (*.jpg)|*.jpg";

            if (Dlg.ShowDialog() == DialogResult.OK)
            {

                /* File.ReadAllBytes() reads the file and returns its data as an array of bytes */
                byte[] Bytes = File.ReadAllBytes(Dlg.FileName);

                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    DbCommand Cmd = Con.CreateCommand();
                    Cmd.CommandText = "update PICTURES set IMG = ? where ID = ?";

                    DbParameter Param = Cmd.CreateParameter();
                    Param.ParameterName = "IMG";
                    Param.Value = Bytes;
                    Cmd.Parameters.Add(Param);

                    Param = Cmd.CreateParameter();
                    Param.ParameterName = "ID";
                    Param.Value = Grid.AsInteger("ID"); // using a custom extension method
                    Cmd.Parameters.Add(Param);

                    Cmd.ExecuteNonQuery();                    
                }

                SelectTable();
            }
        }

        /* deletes a row */
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Grid.HasCurrentDataRow())
                return;

            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();
                Cmd.CommandText = string.Format("delete from PICTURES where ID = {0}", Grid.AsInteger("ID")); // using a custom extension method

                Cmd.ExecuteNonQuery();                
            }

            SelectTable();
        }
 
    }
}
