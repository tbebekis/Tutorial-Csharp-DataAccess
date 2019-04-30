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
using System.Configuration;





/*  ADO.NET 
    Data Providers
    Provider neutral data access and Provider Factories (DbProviderFactories, DbProviderFactory)
    Connection strings
    Connection string Builders (DbConnectionStringBuilder)
    Connection strings and configuration files
    
    The DbConnection class
    Connection pooling
    The DbCommand class
    Retrieving data: DbCommand.ExecuteScalar()
    Retrieving data: the DbDataReader class  
 
 */
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

  
 

        /*  The DbProviderFactories.GetFactoryClasses() method returns information in regard to
            registered providers. That method returns a DataTable object. */
        private void button1_Click(object sender, EventArgs e)
        {
            Grid.DataSource = DbProviderFactories.GetFactoryClasses();
        }


        /* DbConnectionStringBuilder */
        private void button2_Click(object sender, EventArgs e)
        {
            /*  An application can use the DbProviderFactories static class in order to get a proper
                DbProviderFactory object, based on the invariant name, for any registered data provider. */
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");


            /*  To help with building connection strings each provider provides a specific connection 
                string builder. Connection string builders inherit from the base DbConnectionStringBuilder.
                As always it is easier to work with a neutral connection string builder. */
            DbConnectionStringBuilder csBuilder = factory.CreateConnectionStringBuilder();

            csBuilder["Data Source"] = "localhost";
            csBuilder["Initial Catalog"] = "MyDB";
            csBuilder["Integrated Security"] = true;

            string cs = csBuilder.ConnectionString;

            MessageBox.Show(cs);
        }


        /*  The System.Configuration namespace (in System.Configuration.dll assembly) contains classes capable
            of handling that connection string information contained in the app.comfig file. */
        private void button3_Click(object sender, EventArgs e)
        {
            ConnectionStringSettings csSettings = ConfigurationManager.ConnectionStrings["MyDB"];
            string cs = csSettings.ConnectionString;

            MessageBox.Show(cs);
        }



        /*  DbConnection and DbCommand class.
             
            DbConnection.CreateCommand()
            DbCommand.ExecuteScalar()            */        
        private void button4_Click(object sender, EventArgs e)
        {
            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "select count(ID) from COUNTRY";

                /*  The DbCommand.ExecuteScalar() executes the CommandText and returns the first column of the 
                    first row in the result set of the returned query. */
                int count = (int)cmd.ExecuteScalar();

                MessageBox.Show(count.ToString());
            }
        }


        /* a helper class for demonstrating the DbDataReader class */
        public class Country
        {
            public Country(int ID, string Code, string Name)
            {
                this.ID = ID;
                this.Code = Code;
                this.Name = Name;
            }

            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }



        /*  DbCommand.ExecuteReader()
            DbDataReader class                */
        private void button5_Click(object sender, EventArgs e)
        {

            using (DbConnection con = factory.CreateConnection())
            {
                con.ConnectionString = cs;
                con.Open();

                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from COUNTRY";

                /*  The DbCommand.ExecuteReader() overloaded method executes the CommandText and returns a 
                    DbDataReader object. The DbDataReader class represents a forward-only group of rows from a 
                    datasource. */
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    List<Country> list = new List<Country>();

                    while (reader.Read())
                    {
                        list.Add(new Country((int)reader["ID"], (string)reader["Code"], (string)reader["Name"]));
                    }

                    Grid.DataSource = list;
                }
            }
        }


    }
}
