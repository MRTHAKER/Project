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


namespace Reminder
{
    public partial class Form1 : Form
    {
        private int counter;
        

        public Form1()
        {
            InitializeComponent();
            date.CustomFormat = "dd-mmm-yyyy";
            time.CustomFormat = "hh:mm:ss tt";
            /*Thread tt = new Thread(
    () => check()
);
            tt.Start();*/

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            date.ResetText();
            time.ResetText();
            textBox2.Text = "";
            textBox3.Text = "";
            //label8.Text = "";
        }
        public DateTime getdate()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            return indianTime;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            msgbox();

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


        }

        

        private void date_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
