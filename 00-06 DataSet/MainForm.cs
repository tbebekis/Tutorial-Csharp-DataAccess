using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;






/*  The DataSet class 
    Foreign key constraints: the ForeignKeyConstraint class
    Foreign key constraing rules
    DataRelation class: a master-detail relationship
 
 
    This example uses two DataTable objects, Orders and Lines.
    It also uses two DataGridView controls which display those DataTable objects
    in a master-detail relationship.
 
 */
namespace Lessons
{
    public enum Currency
    {
        Dollar,
        Euro
    }


    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PrepareData();
        }

        

        DataSet ds = new DataSet("CustomerOrders");

        DataTable tblOrders = new DataTable("Orders");
        DataTable tblLines = new DataTable("Lines");



        /* prepares two DataTable objects and the relations between them */
        void PrepareData()
        {
            DataColumn column;

            /* master: Orders table */
            column = tblOrders.Columns.Add("ID", typeof(int));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;

            tblOrders.Columns.Add("Customer", typeof(string));
            tblOrders.Columns.Add("OrderDate", typeof(DateTime));
            tblOrders.Columns.Add("Currency", typeof(Currency));

            tblOrders.Columns["Customer"].AllowDBNull = false;
            tblOrders.Columns["Currency"].AllowDBNull = false;
            tblOrders.Columns["OrderDate"].DefaultValue = DateTime.Today;


            /* detail: Lines table */
            column = tblLines.Columns.Add("ID", typeof(int));
            column.AutoIncrement = true;
            tblLines.Columns.Add("OrderID", typeof(int)); 
            tblLines.Columns.Add("Item", typeof(string));
            tblLines.Columns.Add("Price", typeof(double));
            tblLines.Columns.Add("VAT", typeof(double)); // Value Added Tax
            tblLines.Columns.Add("RetailPrice", typeof(double), "Price + (Price * VAT)");

            tblLines.Columns["Item"].AllowDBNull = false;
            tblLines.Columns["Price"].AllowDBNull = false;
            tblLines.Columns["VAT"].AllowDBNull = false;
            tblLines.Columns["VAT"].DefaultValue = 0.19;


            /* this next unique constraint is not strictly required here, since the foreign key constraint 
               that follows, places a unique constraint in the Orders.ID column */
            //UniqueConstraint uc = new UniqueConstraint("UC_ORDERS", tblOrders.Columns["ID"]);
            //tblOrders.Constraints.Add(uc);


            /* adding a foreign key to the tblLines */
            ForeignKeyConstraint fkcOrders = new ForeignKeyConstraint("FK_ORDERS", tblOrders.Columns["ID"], tblLines.Columns["OrderID"]);
            tblLines.Constraints.Add(fkcOrders);


            /* define the foreign key constraint rules */
            fkcOrders.DeleteRule = Rule.SetNull;
            fkcOrders.UpdateRule = Rule.Cascade;
            fkcOrders.AcceptRejectRule = AcceptRejectRule.Cascade;
            

            /* add tables to DataSet */
            ds.Tables.Add(tblOrders);
            ds.Tables.Add(tblLines);
            

            /* create a DataRelation between Orders and Lines */
            DataRelation relation = new DataRelation("OrderLines", tblOrders.Columns["ID"], tblLines.Columns["OrderID"]);
            ds.Relations.Add(relation);            

            /* another way of creating and adding a DataRelation in a single statement */
            //tblOrders.ChildRelations.Add("OrderLines", tblOrders.Columns["ID"], tblLines.Columns["OrderID"]);

            ds.EnforceConstraints = true;


            /* pass the ds DataSet as the DataSource to both DataGridView objects, instead of DataTables */
            gridMaster.DataSource = ds;
            gridDetail.DataSource = ds;


            /* set the DataMember properly, in order to have 
               automatic generation of the OrderID value for the detail table */
            gridMaster.DataMember = "Orders";
            gridDetail.DataMember = "Orders.OrderLines"; // OrderLines refers to the DataRelation here

            
        } 


        /* display tblOrders constraints */
        private void button1_Click(object sender, EventArgs e)
        {
            string S = "";

            foreach (Constraint constraint in tblOrders.Constraints)
                S += "(" + constraint.GetType().Name + ") " + constraint.ToString() + Environment.NewLine;

            MessageBox.Show(S);
 
        }

 
    }
}
