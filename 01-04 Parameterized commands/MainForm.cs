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
using Tripous.Data;




/*  Parameterized commands: DbParameterCollection and DbParameter class 
    A helper library
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

        /* the cs connection string contains a Tripous DataProvider Alias */
        string cs = string.Format(@"Alias=OleDb; Provider=Microsoft.Jet.OLEDB.4.0; Data Source=""{0}"" ", Path.GetFullPath(MsAccessFileName));
        string Alias = "";

        /* the select sql statement  */
        string SelectSql =      "select                                                        " + Environment.NewLine +
                                "   TRADE.ID                 as ID                             " + Environment.NewLine +
                                "  ,TRADER.NAME              as CUSTOMER                       " + Environment.NewLine +
                                "  ,TRADE.TRADE_DATE         as TRADE_DATE                     " + Environment.NewLine +
                                "  ,TRADE.TOTAL_VALUE        as TOTAL                          " + Environment.NewLine +
                                "from                                                          " + Environment.NewLine +
                                "  TRADE                                                       " + Environment.NewLine +
                                "    left join TRADER on TRADER.ID = TRADE.TRADER_ID           " + Environment.NewLine
                                ;



        /* a ADO.NET factory */
        DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

        /* a Tripous DataProvider */
        DataProvider provider;
 

        private void MainForm_Load(object sender, EventArgs e)
        {
            /* extract the Alias and delete it from cs */
            DataProviders.ExtractAlias(cs, ref Alias, ref cs);

            /* get the Tripous DataProvider */
            provider = DataProviders.Find(Alias);
        }



        /* this version uses an ADO.NET DbProviderFactory and follows the standard procedure 
            in order to execute a parameterized query */
        private void button1_Click(object sender, EventArgs e)
        {           
            using (DbConnection Con = factory.CreateConnection())
            {
                Con.ConnectionString = cs;
                Con.Open();

                DbCommand Cmd = Con.CreateCommand();


                // 1. Where clause preparation.
                // ===============================================================

                /* TradeDate */
                string Where = "where TRADE.TRADE_DATE >= ? " + Environment.NewLine;          

                DbParameter Param = factory.CreateParameter();
                Param.ParameterName = "TradeDate";
                Param.Value = edtDate.Value;
                Cmd.Parameters.Add(Param);

                /* Customer */
                if (!string.IsNullOrEmpty(edtCustomer.Text))
                {
                    Where += " and TRADER.NAME like ? " + Environment.NewLine;

                    string Customer = edtCustomer.Text.Trim();
                    if (!Customer.EndsWith("%"))
                        Customer += "%";

                    Param = factory.CreateParameter();
                    Param.ParameterName = "Customer";
                    Param.Value = Customer;
                    Cmd.Parameters.Add(Param);
                }

                /* Total */
                if (!string.IsNullOrEmpty(edtTotal.Text))
                {
                    double Total = 0;
                    if (double.TryParse(edtTotal.Text, out Total))
                        Where += " and TRADE.TOTAL_VALUE >= ? " + Environment.NewLine;

                    Param = factory.CreateParameter();
                    Param.ParameterName = "Total";
                    Param.Value = Total;
                    Cmd.Parameters.Add(Param);
                }

                Cmd.CommandText = SelectSql + Where;

                // 2. Command execution
                // ===============================================================
                DataTable table = new DataTable();

                using (DbDataAdapter adapter = factory.CreateDataAdapter())
                {
                    adapter.SelectCommand = Cmd;
                    adapter.Fill(table);
                }

                Grid.DataSource = table;
            }
        }



        /* this version uses the custom Tripous DataProvider classes */
        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Where clause preparation.
            // ===============================================================

            /* prepare a Params Dictionary to be passed to DbCommand.Parameters */
            Dictionary<string, object> Params = new Dictionary<string, object>();


            /* TradeDate 
               Tripous DataProviders use the GlobalPrefix which defaults to : for all ADO.NET Data Providers. */
            string Where = "where TRADE.TRADE_DATE >= :TradeDate " + Environment.NewLine ;
            Params.Add("TradeDate",  edtDate.Value.ToString("yyyy-MM-dd"));


            /* Customer */
            if (!string.IsNullOrEmpty(edtCustomer.Text))
            {
                Where += " and TRADER.NAME like :Customer " + Environment.NewLine ;

                string Customer = edtCustomer.Text.Trim();
                if (!Customer.EndsWith("%"))
                    Customer += "%";

                Params.Add("Customer", Customer);
            }


            /* Total */
            if (!string.IsNullOrEmpty(edtTotal.Text))
            {
                double Total = 0;
                if (double.TryParse(edtTotal.Text, out Total))
                {
                    Where += " and TRADE.TOTAL_VALUE >= :Total " + Environment.NewLine;
                    Params.Add("Total", Total);                   
                }
            }


            /* displays the SELECT sql statement */
            //MessageBox.Show(SelectSql + Where);


            // 2. Command execution
            // ===============================================================
            DataTable table = new DataTable(); 

            using (DbConnection Con = provider.CreateConnection(cs))
            {
                Con.ConnectionString = cs;
                Con.Open();

                /* creates the DbCommand, parses SQL, creates DbParameters, and assigns values */
                DbCommand Cmd = provider.CreateCommand(Con, SelectSql + Where, Params);

                using (DbDataAdapter adapter = provider.CreateAdapter())
                {
                    adapter.SelectCommand = Cmd;
                    adapter.Fill(table);
                }

            }

            Grid.DataSource = table;

            /* the whole Command execution could be written in a single line as */
            //Grid.DataSource = provider.Select(cs, SelectSql + Where, Params);
        }


    }
}
