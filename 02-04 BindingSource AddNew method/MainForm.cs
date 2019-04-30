using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/* BindingSource: using the AddingNew event and the AddNew() method */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        BindingSource bs = new BindingSource();

        void Initialize()
        {
            /* AddingNew event is triggered any time a new row is going to be added to the BindingSource */
            bs.AddingNew += new AddingNewEventHandler(BindingSource_AddingNew);

            /* AddNew() triggers the AddingNew event */
            bs.AddNew();

            Grid.DataSource = bs;

            edtName.DataBindings.Add("Text", bs, "Name");
            edtAge.DataBindings.Add("Text", bs, "Age");
        }

        void BindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new Person();
        }
    }


    public class Person
    {
        public Person()
            : this("John Doe", 32)
        {
        }
        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
