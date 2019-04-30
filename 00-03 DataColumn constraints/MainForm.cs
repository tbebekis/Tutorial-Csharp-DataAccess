using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



/*      DataColumn constraints, DataRow editing methods and DataRow versions
        
        DataRow.BeginEdit()
        DataRow.EndEdit()
        DataRow.CancelEdit()    
 
        DataRow versions (DataRowVersion enum)
        DataRow.HasVersion() 
 
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PrepareData();
        }

        DataTable table = new DataTable("Material");


        /* prepares a table with a unique constaint */
        void PrepareData()
        {
            DataColumn column;

            column = table.Columns.Add("ID", typeof(int));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AllowDBNull = false;
            
            column = table.Columns.Add("Code", typeof(string));

            /* adding a unique constraint */
            UniqueConstraint constraint = new UniqueConstraint("UC_Code_Material", column);
            table.Constraints.Add(constraint);

            /* a unique constaint can be added in many ways              
                    column.Unique = true;
               or
                    table.Constraints.Add("UC_Code_Material", column, false);
             */

            column = table.Columns.Add("Name", typeof(string));

            column = table.Columns.Add("Price", typeof(double));
            column.DefaultValue = 0.0;


            DataRow row;

            row = table.NewRow();
            row["Code"] = "00";
            row["Name"] = "Hard disk";
            row["Price"] = 60.45;
            table.Rows.Add(row);

            Grid.DataSource = table;
        }
        
 
        /* displays constraints defined in table by name */
        private void btnConstraints_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Constraints in table: " + table.TableName + Environment.NewLine);
            sb.Append("----------------------" + Environment.NewLine);


            foreach (Constraint constraint in table.Constraints)
            {
                sb.AppendLine(string.Format("{0} ({1}) ", constraint.ConstraintName, constraint.GetType().Name));
            }

            MessageBox.Show(sb.ToString());
        }


        /*  Demonstrates the power of a unique constraint.
            The line 
                row["Code"] = "00";
           causes an exception because of a constaint violation */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row;

                row = table.NewRow();
                table.Rows.Add(row);

                row["Code"] = "00";
                row["Name"] = "Monitor";
                row["Price"] = 120.90;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /* The DataRow.BeginEdit() and EndEdit() methods.
           The exception caused by the line 
            row["Code"] = "00";
           is now defered because of the BeginEdit(), until the EndEdit() call */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row;

                row = table.NewRow();
                table.Rows.Add(row);                

                row.BeginEdit();

                row["Code"] = "00";
                row["Name"] = "Monitor";
                row["Price"] = 120.90;

                row.EndEdit();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*  Demonstrates row versions.
            see: DataRowVersion Enumeration in online help */           
        private void button3_Click(object sender, EventArgs e)
        {
            if (table.Rows.Count > 0)
            {                
                DataRow row = table.Rows[0];

                /* commit any penging row changes  */
                row.AcceptChanges();    

                row.BeginEdit();
                row["Code"] = "99";

                string S = "";

                /* display the original version of the column */
                S = row["Code", DataRowVersion.Original].ToString();
                MessageBox.Show("Original: " + S);

                /* display the proposed version of the column */
                S = row["Code", DataRowVersion.Proposed].ToString();
                MessageBox.Show("Proposed: " + S);

                /* cancel row changes */
                row.CancelEdit();                
            } 
        }
 

 
    }
}
