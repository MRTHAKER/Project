﻿using System;
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
            
        }

        public DateTime getdate()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            return indianTime;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = conn();
            con.Open();
            String time = textBox1.Text+" "+ comboBox2.SelectedItem + ":" + comboBox1.SelectedItem + ":" + comboBox4.SelectedItem + " " + comboBox3.SelectedItem;
            DateTime ti = DateTime.Parse(time);
            ti = ti - new TimeSpan(0, 30, 0);
            //label11.Text = ti.ToString();
            //label10.Text = getdate().ToString();
            string q = "insert into rem values ('" + textBox2.Text + "','"+textBox3.Text+"','"+ti.ToString()+"')";
            SqlCommand cmd = new SqlCommand(q, con);
            int k = cmd.ExecuteNonQuery();
            con.Close();
            if (k > 0) { label10.Text = "done"; }
            else { label10.Text = "derp"; }
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
                        //MessageBox.Show("" + full + " " + too + " " + ms);
                    }
                }
                
            }
            catch(Exception e){}
        }

        public SqlConnection conn()
        {
            string s = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\Om\Documents\Visual Studio 2012\Project\Reminder\SequenceDiagram1.mdf'";
            SqlConnection con = new SqlConnection(s);
            return con;
        }
        private void date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
