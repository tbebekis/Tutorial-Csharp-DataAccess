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




/*  Windows Forms data binding  
    
    Simple binding
    The Control.DataBindings property
    ControlBindingsCollection class
    Binding class  
   
    Change notification requirements for simple data binding 
    The <PropertyName>Changed event pattern
    The INotifyPropertyChanged interface (.Net 2.0)
 
 */
namespace Lessons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();             
        }

        
        /* a simple class used as a datasource. There are two versions of this class 
           in this file, one that implements the property change notification contract
           and one that it doesn't. See implementations below */
        Person person = new Person();

        private void MainForm_Load(object sender, EventArgs e)
        {
            /* adding Binding items to Control.DataBindings collection property. 
               DataBindings and Binding are used in simple binding only */
            edtName.DataBindings.Add("Text", person, "Name");
            edtName2.DataBindings.Add("Text", person, "Name");
            edtAge.DataBindings.Add("Text", person, "Age");
        }

      
        /* the edtNameNotBound and edtAgeNotBound boxes are not data bound. 
           They are used here in order to check the property change notification behavior
           or the class used as a datasource */
        private void btnSaveNotBound_Click(object sender, EventArgs e)
        {
            person.Name = edtNameNotBound.Text;
            person.Age = int.Parse(edtAgeNotBound.Text);
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
