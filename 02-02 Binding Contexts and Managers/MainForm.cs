using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



/* Windows Forms binding mechanism: binding contexts and binding managers */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        DataTable table = new DataTable("temp");

        void Initialize()
        {
            /* create and fill a table */
            table.Columns.Add("ID", typeof(System.Int32));
            table.Columns.Add("Name", typeof(System.String));

            for (int i = 1; i < 100; i++)
                table.Rows.Add(new object[] { i, "Name_" + i.ToString() });


            /* bind the two ListBox controls to the very same table */
            listBox1.DataSource = table;
            listBox1.DisplayMember = "Name";

            listBox2.DataSource = table;
            listBox2.DisplayMember = "Name";


            /* if the next line is commented, then both the above ListBox controls 
               will use the same CurrencyManager, the one provided by the Form's BindingContext.
               If the next line is left un-commented then the second ListBox will have a separate
               BindingContext and thus a separate CurrencyManager, although it continues to be bound
               to the same datasource */
            listBox2.BindingContext = new BindingContext(); 
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            BindingManagerBase Manager = listBox1.BindingContext[table];
            Manager.Position--;
        }
 

        private void btnNext_Click(object sender, EventArgs e)
        {
            BindingManagerBase Manager = listBox1.BindingContext[table];
            Manager.Position++;
        }        
    }
}
