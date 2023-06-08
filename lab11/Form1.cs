using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace lab11
{
    public partial class Form1 : Form
    {
        bool flag1 = false;
        bool flag2 = false;
        DriveInfo[] MainItems = DriveInfo.GetDrives();
        public Form1()
        {
            InitializeComponent();

            foreach (DriveInfo d in MainItems)
            {
                listBox1.Items.Add(d.Name);
            }

            foreach (DriveInfo d in MainItems)
            {
                listBox2.Items.Add(d.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
            flag1 = true;
            flag2 = false;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = listBox2.SelectedItem.ToString();
            flag1 = false;
            flag2 = true;
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string curItem = listBox1.SelectedItem.ToString();
                string[] dirs = Directory.GetDirectories(curItem);
                string[] files = Directory.GetFiles(curItem);
                listBox1.Items.Clear();
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(dir);
                }
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
                
                flag1 = true;
                flag2 = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("can't be Opened");
            }

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string curItem = listBox2.SelectedItem.ToString();
                string[] dirs = Directory.GetDirectories(curItem);
                string[] files = Directory.GetFiles(curItem);
                listBox2.Items.Clear();
                foreach (string dir in dirs)
                {
                    listBox2.Items.Add(dir);
                }
                foreach (string file in files)
                {
                    listBox2.Items.Add(file);
                }
                textBox2.Text = curItem;
                flag1 = false;
                flag2 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("can't be Opened");
            }

        }


        //move ==>
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string file = Path.GetFileName(textBox1.Text);
                string parent = Directory.GetParent(textBox1.Text).ToString();
                if (textBox1.Text.Contains("."))
                {
                    File.Move(textBox1.Text, textBox2.Text + "\\" + file);
                }
                else
                {
                    Directory.Move(textBox1.Text, textBox2.Text + "\\" + file);
                }

                //remove item from 1
                listBox1.Items.Clear();
                foreach (string entry in Directory.GetDirectories(parent))
                {
                    listBox1.Items.Add(entry);
                }
                foreach (string entry in Directory.GetFiles(parent))
                {
                    listBox1.Items.Add(entry);
                }
                //catch item in 2
                listBox2.Items.Clear();
                foreach (string entry in Directory.GetDirectories(textBox2.Text))
                {
                    listBox2.Items.Add(entry);
                }
                foreach (string entry in Directory.GetFiles(textBox2.Text))
                {
                    listBox2.Items.Add(entry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("choose suitable distenation");
            }


        }

        //back
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                button5.Enabled = false;
            }

            if (flag1 == true)
            {
                listBox1.Items.Clear();
                if ((textBox1.Text).Length < 4)
                {
                    listBox1.Items.AddRange(MainItems);
                    textBox1.Text = "";
                }
                else
                {
                    string parent = Directory.GetParent(textBox1.Text).ToString();

                    foreach (string entry in Directory.GetDirectories(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                    textBox1.Text = parent;

                    flag1 = true;
                    flag2 = false;


                }
            }
            if (flag2 == true)
            {
                listBox2.Items.Clear();
                if (textBox2.Text.Length < 4)
                {
                    listBox2.Items.AddRange(MainItems);
                    textBox2.Text = "";
                }
                else
                {
                    string parent2 = Directory.GetParent(textBox2.Text).ToString();
                    foreach (string entry in Directory.GetDirectories(parent2))
                    {
                        listBox2.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent2))
                    {
                        listBox2.Items.Add(entry);
                    }
                    textBox2.Text = parent2;

                    flag1 = false;
                    flag2 = true;
                }

            }
        }
        //move <==
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string file = Path.GetFileName(textBox2.Text);
                string parent = Directory.GetParent(textBox2.Text).ToString();

                if (textBox2.Text.Contains("."))
                {
                    File.Move(textBox2.Text, textBox1.Text + "\\" + file);
                }
                else
                {
                    Directory.Move(textBox2.Text, textBox1.Text + "\\" + file);
                }

                //remove item from list2
                listBox2.Items.Clear();
                foreach (string entry in Directory.GetDirectories(parent))
                {
                    listBox2.Items.Add(entry);
                }
                foreach (string entry in Directory.GetFiles(parent))
                {
                    listBox2.Items.Add(entry);
                }
                //catch item in list2
                listBox1.Items.Clear();
                foreach (string entry in Directory.GetDirectories(textBox1.Text))
                {
                    listBox1.Items.Add(entry);
                }
                foreach (string entry in Directory.GetFiles(textBox1.Text))
                {
                    listBox1.Items.Add(entry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("choose suitable distenation");
            }

        }
        //copy
        private void button3_Click(object sender, EventArgs e)
        {
                if (flag2 == true)
                {
                    if (textBox1.Text.Contains(".")) 
                    { 
                    string file = Path.GetFileName(textBox1.Text);
                    File.Copy(textBox1.Text, textBox2.Text + "\\" + file, true);
                    }
                    else
                    {
                        string source = textBox1.Text;
                        string currFolder = Path.GetFileName(textBox1.Text);
                        string destination = textBox2.Text + "\\" + currFolder;
                        string[] filesOfCurrentFoler=Directory.GetFiles(source);
                        Directory.CreateDirectory(destination);
                        foreach(string file in filesOfCurrentFoler)
                        {
                            File.Copy(source+"\\"+Path.GetFileName(file), destination + "\\" + Path.GetFileName(file), true );
                        }

                    }
                    //catch item in listbox2
                    listBox2.Items.Clear();
                    foreach (string entry in Directory.GetDirectories(textBox2.Text))
                    {
                        listBox2.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(textBox2.Text))
                    {
                        listBox2.Items.Add(entry);
                    }
                }
                if (flag1 == true)
                {
                if (textBox2.Text.Contains("."))
                {
                    string file = Path.GetFileName(textBox2.Text);
                    File.Copy(textBox2.Text, textBox1.Text + "\\" + file, true);
                }
                else
                {
                    string source = textBox2.Text;
                    string currFolder = Path.GetFileName(textBox2.Text);
                    string destination = textBox1.Text + "\\" + currFolder;
                    string[] filesOfCurrentFoler = Directory.GetFiles(source);
                    Directory.CreateDirectory(destination);
                    foreach (string file in filesOfCurrentFoler)
                    {
                        File.Copy(source + "\\" + Path.GetFileName(file), destination + "\\" + Path.GetFileName(file), true);
                    }

                }
                //catch item in listbox1
                listBox1.Items.Clear();
                foreach (string entry in Directory.GetDirectories(textBox1.Text))
                {
                    listBox1.Items.Add(entry);
                }
                foreach (string entry in Directory.GetFiles(textBox1.Text))
                {
                    listBox1.Items.Add(entry);
                }
            }


        }
        //delete
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (flag1 == true)
                {

                    string parent = Directory.GetParent(textBox1.Text).ToString();

                    if (textBox1.Text.Contains("."))
                    {
                        File.Delete(textBox1.Text);
                    }
                    else
                    {
                        Directory.Delete(textBox1.Text, true);
                    }

                    listBox1.Items.Clear();

                    foreach (string entry in Directory.GetDirectories(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                }
                if (flag2 == true)
                {

                    string parent = Directory.GetParent(textBox2.Text).ToString();
                    if (textBox2.Text.Contains("."))
                    {
                        File.Delete(textBox2.Text);
                    }
                    else
                    {
                        Directory.Delete(textBox2.Text, true);
                    }
                    listBox2.Items.Clear();
                    foreach (string entry in Directory.GetDirectories(parent))
                    {
                        listBox2.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent))
                    {
                        listBox2.Items.Add(entry);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("can't delete it");
            }
        }
        //..1
        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear ();
            textBox1.Text = "";
            foreach (DriveInfo d in MainItems)
            {
                listBox1.Items.Add(d.Name);
            }
        }

        //.1
        private void button6_Click(object sender, EventArgs e)
        {

            if (flag1 == true)
            {
                listBox1.Items.Clear();
                if ((textBox1.Text).Length < 4)
                {
                    listBox1.Items.AddRange(MainItems);
                    textBox1.Text = "";
                }
                else
                {
                    string parent = Directory.GetParent(textBox1.Text).ToString();

                    foreach (string entry in Directory.GetDirectories(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent))
                    {
                        listBox1.Items.Add(entry);
                    }
                    textBox1.Text = parent;

                    flag1 = true;
                    flag2 = false;


                }
            }
        }
        //.2
        private void button8_Click(object sender, EventArgs e)
        {
            if (flag2 == true)
            {
                listBox2.Items.Clear();
                if (textBox2.Text.Length < 4)
                {
                    listBox2.Items.AddRange(MainItems);
                    textBox2.Text = "";
                }
                else
                {
                    string parent2 = Directory.GetParent(textBox2.Text).ToString();
                    foreach (string entry in Directory.GetDirectories(parent2))
                    {
                        listBox2.Items.Add(entry);
                    }
                    foreach (string entry in Directory.GetFiles(parent2))
                    {
                        listBox2.Items.Add(entry);
                    }
                    textBox2.Text = parent2;

                    flag1 = false;
                    flag2 = true;
                }

            }
        }
        //..2
        private void button9_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            textBox2.Text = "";
            foreach (DriveInfo d in MainItems)
            {
                listBox2.Items.Add(d.Name);
            }
        }
    }
}
