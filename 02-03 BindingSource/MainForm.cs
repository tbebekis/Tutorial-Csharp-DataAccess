using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



/* BindingSource: binding a BindingSource to a type */
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
            /* bind the bs BindingSource to the Person class */
            bs.DataSource = typeof(Person);

            Grid.DataSource = bs;

            edtName.DataBindings.Add("Text", bs, "Name");
            edtAge.DataBindings.Add("Text", bs, "Age");

            Navigator.BindingSource = bs;
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
