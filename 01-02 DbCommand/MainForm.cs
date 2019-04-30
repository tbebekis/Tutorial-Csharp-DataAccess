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



/*  Executing DbCommand(s) which modify data 
    Transactions and the DbTransaction class  */
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


        const string SELECT_CITIES = "select CITY.ID, COUNTRY.NAME as COUNTRY, CITY.NAME from CITY left join COUNTRY on CITY.COUNTRY_ID = COUNTRY.ID";

        /* arranges the user interface */
        void EnableCommands()
        {
            btnUpdate.Enabled = (Grid.DataSource is DataTable) && ((Grid.DataSource as DataTable).Rows.Count > 0);
            btnDelete.Enabled = btnUpdate.Enabled;
        }


        /* executes SQL and returns a DataTable. The SQL should be a SELECT statement */
        DataTable Select(string SQL)
        {
            DataTable Result = new DataTable();

            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = SQL;

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(Result);
                }
            }

            return Result;
        }
 

        /*  executes SQL. The SQL should be an INSERT, UPDATE or DELETE statement. 
            Actually SQL would be a DDL statement too, such as CREATE TABLE etc. 
            The statement is executed inside a transaction */
        void Exec(string SQL)
        {
            Exec(new string[] { SQL } );
        }

        /* executes multiple INSERT, UPDATE or DELETE statements inside a transaction */
        void Exec(string[] SQL)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                using (DbTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        DbCommand cmd = con.CreateCommand();
                        cmd.Transaction = trans;

                        foreach (string s in SQL)
                        {
                            cmd.CommandText = s;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }          
            }
        }

        /* executes an INSERT, UPDATE or DELETE statement without an explicit transaction */
        void Exec2(string SQL)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = SQL;
                cmd.ExecuteNonQuery();
            }
        } 

        /* selects the CITY table */
        void SelectCities()
        {
            Grid.DataSource = Select(SELECT_CITIES);
            EnableCommands();
 
        }


        /* returns the value of the ID column of the current row in the Grid */
        int GetCurrentCityID()
        {
            int Result = -1;

            DataRowView row = Grid.CurrentRow.DataBoundItem as DataRowView;

            if (row != null)
                Result = (int)row["ID"];

            return Result;
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            SelectCities();

            /* selects the COUNTRY table and displays the result to a combo box */
            DataTable table = Select("select ID, NAME from COUNTRY");
            cboCountry.DataSource = table;
            cboCountry.DisplayMember = "NAME";      // the value of this field is displayed by the combo box
            cboCountry.ValueMember = "ID";          // the value of this field is returned by the SelectedValue property of combo box

            Grid.ReadOnly = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtCity.Text))
                throw new ArgumentNullException("City");

            /* create an INSERT statement, format the arguments and Exec() it */
            string SQL =  "insert into CITY (COUNTRY_ID, NAME) values ({0}, '{1}')";
            SQL = string.Format(SQL, (int)cboCountry.SelectedValue, edtCity.Text);

            Exec(SQL);

            SelectCities();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtCity.Text))
                throw new ArgumentNullException("City");

            /* get the value of the ID colulmn of the current row in the Grid */
            int ID = GetCurrentCityID();

            if (ID > 0)
            {
                /* create an UPDATE statement, format the arguments and Exec() it */
                string SQL = "update CITY set NAME = '{0}' where ID = {1}";
                SQL = string.Format(SQL, edtCity.Text, ID);

                Exec(SQL);

                SelectCities();
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /* get the value of the ID colulmn of the current row in the Grid */
            int ID = GetCurrentCityID();

            if (ID > 0)
            {
                /* create an DELETE statement, format the arguments and Exec() it */
                string SQL = "delete from CITY where ID = {0}";
                SQL = string.Format(SQL, ID);

                Exec(SQL);

                SelectCities();
            } 
        }
    }
}
