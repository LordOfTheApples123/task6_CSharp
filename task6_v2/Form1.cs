using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task6_v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                @"DLL FILES  (*.dll) | *.dll | All files (*.*) | *.*"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                Assembly assembly = Assembly.LoadFrom(path);
                Type myInterface = assembly.GetType("ClassLibrary1.IMobile");
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract)
                    {
                        comboBox1.Items.Clear();
                        comboBox1.Items.Add(type.FullName ?? throw new InvalidOperationException());
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string methodName = comboBox1.SelectedItem.ToString();
            string classname = comboBox2.SelectedItem.ToString();
            
            Type myClass = Type.GetType(classname);
            if (myClass != null)
            {
                MethodInfo method = myClass.GetMethod(methodName);
                ConstructorInfo[] constructorInfo = myClass.GetConstructors();

                ConstructorInfo c1 = constructorInfo[0];
                object myClass1 = null;
                if (classname.Equals("ModelB") || classname.Equals("ModelA"))
                {
                    myClass1 = c1.Invoke(new object[] { "something", 1, 20.4F, "something2", 1 });
                } else if (classname.Equals("Model"))
                {
                    myClass1 = c1.Invoke(new object[] { "something", 1, 20.4F, 21.4F, 1 });
                }
                else
                {
                    Console.WriteLine("!!method!!");
                }

                string var1 = textBox1.Text;
                string var2 = textBox2.Text;
                string var3 = textBox3.Text;

                List<object> args = new List<object>();

                if (!string.IsNullOrEmpty(var1))
                {
                 args.Add(var1);   
                }
                if (!string.IsNullOrEmpty(var2) && !string.IsNullOrEmpty(var1))
                {
                    args.Add(var2);   
                }
                if (!string.IsNullOrEmpty(var3) && !string.IsNullOrEmpty(var2) && !string.IsNullOrEmpty(var1))
                {
                    args.Add(var3);   
                }
                
                string result = method.Invoke(myClass1, args.ToArray()).ToString();
                label4.Text = result;

            }
            else
            {
                Console.WriteLine("fuck");
            }
        }


        //class choosing
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBox2.SelectedItem.ToString();
            Type selectedClass = Type.GetType(name);
            if (selectedClass != null)
            {
                MethodInfo[] methods = selectedClass.GetMethods();
                //methods combobox
                comboBox1.Items.Clear();
                foreach (MethodInfo method in methods)
                {
                    comboBox1.Items.Add(method.Name);
                }
            }
            else
            {
                Console.WriteLine("fuck");
            }
        }


        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }
}