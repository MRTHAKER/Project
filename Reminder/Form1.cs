using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;


namespace Reminder
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            doProcess();
        }

         
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }

        public DateTime getdate()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            return indianTime;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Number Can't be Empty");
                textBox2.Focus();
            }
            /*else if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Number can't be in Characters");
                textBox2.Focus();
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }*/

            else if (String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Message Can't be Empty");
                textBox3.Focus();
            }
            else if (comboBox1.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Select Minutes");
                comboBox1.Focus();
            }


            else if (comboBox2.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Select Hour");
                comboBox2.Focus();
            }
            else if (comboBox3.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Select AM/PM");
                comboBox3.Focus();
            }

            else if (comboBox4.SelectedIndex.Equals(-1))
            {
                MessageBox.Show("Select Seconds");
                comboBox4.Focus();
            }
            else
            {
                SqlConnection con = conn();
                con.Open();
            String s = dateTimePicker3.Value.Date.ToString("d");
                label10.Text = s;
                String time =   ""+s+" " + comboBox2.SelectedItem + ":" + comboBox1.SelectedItem + ":" + comboBox4.SelectedItem + " " + comboBox3.SelectedItem;
                DateTime ti = DateTime.Parse(time);
                String act = ti.ToString();
                ti = ti - new TimeSpan(0, 30, 0);
                string q = "insert into rem values ('" + textBox2.Text + "','" + textBox3.Text + "','" + ti.ToString() + "','" + act + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                int k = cmd.ExecuteNonQuery();
                con.Close();
                if (k > 0) { label14.Text = "done"; }
                else { label14.Text = "derp"; }
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
            }

           
        }

        public async void doProcess()
        {
            while (true)
            {
                await Task.Delay(1000);
                check();
            }
        }
        
        public void check()
        {
            try
            {
                SqlConnection con = conn();
                con.Open();
                SqlCommand cm = new SqlCommand("Select * from rem", con);
                SqlDataReader dr = cm.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dr);
                int i = dataTable.Rows.Count;
                String[] time = new String[i];
                String[] to = new String[i];
                String[] msg = new String[i];
                int k = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    time[k]=row["time"].ToString();
                    to[k] = row["to"].ToString();
                    msg[k] = row["msg"].ToString();
                    k++;
                }
                for (int ii = 0; ii <= i;ii++)
                {
                    String full = time[ii];
                    String too = to[ii];
                    String ms = msg[ii];
                    label11.Text = getdate().ToString();
                    label10.Text = full;
                    if (full.Equals(getdate().ToString()))
                    {
                        String u = "https://www.fast2sms.com/dev/bulk?authorization=BLmencQXlgDH2ErNVfa7qd8R4KFv1pWuOY9MobhiysAtGJ3UCTpqBhYDojG1Qx8IlJ6OX2rLcMRHua0N&sender_id=FSTSMS&message=" + ms + "&language=english&route=p&numbers=" + too;
                        ProcessStartInfo sInfo = new ProcessStartInfo(u);
                        Process.Start(sInfo);
                        
                    }
                }
                
            }
            catch(Exception e){}
        }

        public SqlConnection conn()
        {
            string s = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\Arpit\Downloads\Project-master\Project-master\Reminder\SequenceDiagram1.mdf'";
            SqlConnection con = new SqlConnection(s);
            return con;
        }

        /*private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }
        }*/

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        
    }
}
