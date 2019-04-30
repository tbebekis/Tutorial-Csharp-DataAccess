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
using System.Data.SqlClient;
using System.Data.OleDb;

/*  Executing DDL (Data Definition Language) statements
    Using stored procedures
      
    This sample uses an automatically attached MS SQL database.
    Consider the AttachDbFilename key in the connection string, below.
    The user has to create a database table and two stored procedures before any other action.
    Here are the three scripts. The scripts must be executed one by one. 

    create table TEST_TABLE
    ( 
      ID integer identity not null primary key,
      NAME varchar(20)
    )

     
    create procedure AddNumbers 
      @NumberA integer,
      @NumberB integer,
      @Result integer output
    as
      set nocount on
      
      set @Result = @NumberA + @NumberB;     
     
     
    create procedure SelectTestTable
      @ID integer
    as
      set nocount on

      select * from TEST_TABLE
      where ID >= @ID 

 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Pager.SelectedTab = tabEditor;
        }

        const string DbFileName = @"..\..\..\Lessons.MDF";
        readonly string cs = string.Format(@"Data Source=localhost; Initial Catalog=Lessons; AttachDbFilename=""{0}""; Integrated Security=SSPI", Path.GetFullPath(DbFileName));
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");



        /* drops the TEST_TABLE table and the AddNumbers and SelectTestTable stored procedures */
        private void MainForm_Load(object sender, EventArgs e)
        {//*
            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();
                Cmd.Connection = Con;

                Cmd.CommandText = "if OBJECT_ID('TEST_TABLE', 'U') is not null drop table TEST_TABLE ";
                Cmd.ExecuteNonQuery();

                Cmd.CommandText = "if OBJECT_ID('AddNumbers', 'P') is not null drop procedure AddNumbers ";
                Cmd.ExecuteNonQuery(); 

                Cmd.CommandText = "if OBJECT_ID('SelectTestTable', 'P') is not null drop procedure SelectTestTable ";
                Cmd.ExecuteNonQuery(); 
            }
         // */ 
        }


        /*  Executes not selectable SQL statements (CREATE, DROP, ALTER, INSERT, UPDATE, DELETE). 
            The statement is written by the user in the Editor text box. */
        private void btnExec_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    DbCommand Cmd = Con.CreateCommand();
                    Cmd.Connection = Con;

                    Cmd.CommandText = Editor.Text;
                    Cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /* Executes SELECT statements. The statement is written by the user in the Editor text box */
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    DbCommand Cmd = Con.CreateCommand();
                    Cmd.Connection = Con;

                    Cmd.CommandText = Editor.Text;

                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = Cmd;

                    DataTable Table = new DataTable();
                    adapter.Fill(Table);

                    Grid.DataSource = Table;
                    Pager.SelectedTab = tabGrid;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /* Demonstrates how to use selectable and non-selectable stored procedures */
        private void btnExecStoreProc_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection Con = factory.CreateConnection())
                {
                    Con.ConnectionString = cs;
                    Con.Open();

                    DbCommand Cmd = Con.CreateCommand();

                    /* by default CommandType is Text, that is any DDL or DML statement */
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Connection = Con;

                    DbParameter Param;


                    /* AddNumbers is a NON-selectable stored proc that adds two numbers passed as parameters. 
                       The sum is returned as an output parameter. 
                       DbParameter.Direction property controls the direction (in/out/in-out) of a parameter */
                    Cmd.Parameters.Clear();
                    Cmd.CommandText = "AddNumbers";

                    Param = factory.CreateParameter();
                    Param.ParameterName = "@NumberA";
                    Param.Direction = ParameterDirection.Input;
                    Param.Value = 2;
                    Cmd.Parameters.Add(Param);

                    Param = factory.CreateParameter();
                    Param.ParameterName = "@NumberB";
                    Param.Direction = ParameterDirection.Input;
                    Param.Value = 3;
                    Cmd.Parameters.Add(Param);

                    Param = factory.CreateParameter();
                    Param.ParameterName = "@Result";
                    Param.Direction = ParameterDirection.Output;
                    Param.Value = 0;
                    Cmd.Parameters.Add(Param);

                    /* non-selectable stored procs are executed by using the DbCommand.ExecuteScalar() */
                    Cmd.ExecuteScalar();

                    /* read and display the output parameter after stored proc execution */
                    string ProcResult = Cmd.Parameters["@Result"].Value.ToString();
                    MessageBox.Show(ProcResult);




                    /* SelectTestTable is a selectable stored proc that accepts a single input parameter */
                    Cmd.Parameters.Clear();
                    Cmd.CommandText = "SelectTestTable";                    

                    Param = factory.CreateParameter();
                    Param.ParameterName = "@ID";
                    Param.Direction = ParameterDirection.Input;
                    Param.Value = 1;
                    Cmd.Parameters.Add(Param);

                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = Cmd;

                    /* selectable stored procs are executed by using the DbDataAdapter.Fill() */
                    DataTable Table = new DataTable();
                    adapter.Fill(Table);

                    Grid.DataSource = Table;
                    Pager.SelectedTab = tabGrid;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
