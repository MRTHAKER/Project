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


namespace Reminder
{
    public partial class Form1 : Form
    {
        private int counter;
        

        public Form1()
        {
            InitializeComponent();
          
            
            /*Thread tt = new Thread(
    () => check()
);
            tt.Start();*/
            }

         
        private void button2_Click(object sender, EventArgs e)
        {

            check();
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
            string q = "insert into rem values ('" + textBox1.Text + "','" + comboBox1.SelectedItem + "','" + comboBox2.SelectedItem + "','" + comboBox3.SelectedItem + "','" + textBox2.Text + "','" + textBox3.Text + "')";
            SqlCommand cmd = new SqlCommand(q, con);
            int k = cmd.ExecuteNonQuery();
            con.Close();
            if (k > 0) { label10.Text = "done"; }
            else { label10.Text = "derp"; }
        }
        
        public void msgbox()
        {
            
            /*string m = "Send Message";
            string t = "Done";
            
            MessageBoxButtons b = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(m, t, b);
            if (result == DialogResult.Yes)
            {
                //this.Close();
                String to = textBox2.Text;
                String msg = textBox3.Text;
                String u = "https://www.fast2sms.com/dev/bulk?authorization=BLmencQXlgDH2ErNVfa7qd8R4KFv1pWuOY9MobhiysAtGJ3UCTpqBhYDojG1Qx8IlJ6OX2rLcMRHua0N&sender_id=FSTSMS&message=" + msg + "&language=english&route=p&numbers=" + to;
                
                ProcessStartInfo sInfo = new ProcessStartInfo(u);
                Process.Start(sInfo);
            }
            else
            {

            }*/
            /*DateTime dt = date.Value;
            DateTime dt2 = getdate();
            int result = DateTime.Compare(dt, dt2);
            MessageBox.Show(""+result);*/

            
        }




        public void check()
        {
            SqlConnection con = conn();
            con.Open();
            SqlCommand cm = new SqlCommand("Select * from rem", con);
            SqlDataReader dr = cm.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dr);
            int i = dataTable.Rows.Count;
            String[] id = new String[i];
            String[] date = new String[i];
            String[] hour = new String[i];
            String[] min = new String[i];
            String[] ap = new String[i];
            String[] to = new String[i];
            String[] msg = new String[i];
            int k = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                
                
                id[k] = row["Id"].ToString();
                date[k] = row["date"].ToString();
                hour[k] = row["hour"].ToString();
                min[k] = row["min"].ToString();
                ap[k] = row["ap"].ToString();
                to[k] = row["to"].ToString();
                msg[k] = row["msg"].ToString();
                k++;
            
                
            }

            MessageBox.Show("'" + id[0] + "''" + date[0] + "''" + hour[0] + "''" + min[0] + "''" + ap[0] + "''" + to[0] + "''" + msg[0] + "'");

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
