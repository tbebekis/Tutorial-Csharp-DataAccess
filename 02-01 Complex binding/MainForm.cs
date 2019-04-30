using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*  Complex (or list-based) binding
    Change notification requirements for complex data binding
    IBindingList
    BindingList<T>
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }



        /*  Using datasources of different types. 
            It turns out that the BindingList<T> is the most flexible one. */

        //Person[] persons = { new Person(), new Person("Jane Doe", 30)   };
        //List<Person> persons = new List<Person>(){ new Person(), new Person("Jane Doe", 30)  };
        BindingList<Person> persons = new BindingList<Person>() { new Person(), new Person("Jane Doe", 30) };

        private void MainForm_Load(object sender, EventArgs e)
        {      
            edtName.DataBindings.Add("Text", persons, "Name");
            edtAge.DataBindings.Add("Text", persons, "Age");

            Grid.DataSource = persons;

            /* if the DisplayMember is left unassigned then the binding mechanism calls 
               the ToString() method on the datasource element. Else it displays the defined member. 
               Setting the ValueMember sets the DisplayMember too. */
            listBox.DataSource = persons; 
            listBox.ValueMember = "Name";
 
        }

        private void listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex >= 0)
                MessageBox.Show(listBox.SelectedValue.ToString());
        }
    }





    /*  A demo class which follows the Change Notification contract 
        required by Windows Forms. That is, it implements the
        INotifyPropertyChanged interface.
     
        When this contract is implemented then changes to property values
        made by code, are automatically propagated to any bound control.
     */
    // /*
    public class Person : INotifyPropertyChanged
    {
        private string name = "";
        private int age = 0;

        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public Person()
            : this("John Doe", 32)
        {
        }
        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Age);
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;

                    OnPropertyChanged("Name");
                }
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value != age)
                {
                    age = value;

                    OnPropertyChanged("Age");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

    }
    // */

    /* A demo class which does not follow the Change Notification contract   */
    /*
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
    // */

 }
