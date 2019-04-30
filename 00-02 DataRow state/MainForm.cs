using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tripous.Data;




/*  
    The state of a row
    ---------------------------------------------
    DataRow.RowState property (Added, Deleted, Modified, Unchanged)
    DataRow.SetAdded()  
    DataRow.SetModified()
    DataRow.Delete(), removes the row and marks the row as Deleted
    
    DataTable.AcceptChanges() method
 

 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            table = CreateTempTable();
            Grid.DataSource = table;
        }

        DataTable table;


        /* creates a demo table */
        DataTable CreateTempTable()
        {
            DataTable table = new DataTable("Temp");
            table.Columns.Add("ID", typeof(int));

            for (int i = 0; i < 5; i++)
                table.Rows.Add(i); // since there is only a single row, it's equal to table.Rows.Add(new object[] { i });

            return table;
        }



        private void btnAcceptChanges_Click(object sender, EventArgs e)
        {
            table.AcceptChanges();
        }

        /* displaying the value of the DataRow.RowState property, of type DataRowState. 
           The RowEnter event of the DataGridDiew */
        private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblStatus.Text = "";
 
            if (e.RowIndex < table.Rows.Count)
            {
                DataRow row = table.Rows[e.RowIndex];
                lblStatus.Text = row.RowState.ToString();
            }
        }

        /* The RowLeave event of the DataGridDiew  */
        private void Grid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            lblStatus.Text = "";
        }
          

        /*  Demostrates the DataTable.GetChanges() method  */
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable temp = CreateTempTable();

            /* display the table */
            temp.AcceptChanges();
            TableBox.Display(temp);


            
            /* display added rows */
            temp.Rows.Add(111);

            DataTable Added = temp.GetChanges(DataRowState.Added);
            if (Added != null)
            {
                Added.TableName = "Added";
                TableBox.Display(Added);
            }

            temp.AcceptChanges();


            /* display modified rows */
            temp.Rows[0]["ID"] = 123;
            temp.Rows[2]["ID"] = 456;


            DataTable Modified = temp.GetChanges(DataRowState.Modified);
            if (Modified != null)
            {
                Modified.TableName = "Modified";
                TableBox.Display(Modified);
            }

            temp.AcceptChanges();


            /* display deleted rows */
            temp.Rows[2].Delete();      // DataRow.Delete() marks the row for deletion
            temp.Rows[4].Delete();
            temp.Rows.RemoveAt(3);      // WARNING: DataTable.Rows.Remove() and RemoveAt() delete the row and call AcceptChanges()


            DataTable Deleted = temp.GetChanges(DataRowState.Deleted);
            if (Deleted != null)
            {
                Deleted.TableName = "Deleted";
                Deleted.RejectChanges();    // if RejectChanges() is omitted the Deleted table does not display any row
                TableBox.Display(Deleted);
            }
        }
    }
}
