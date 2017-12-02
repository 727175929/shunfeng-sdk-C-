using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form3 : Form
    {
        public static string Form3_appid;
        public static string Form3_appkey; 
        public static string Form3_cardid; 

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form3_appid = textBox1.Text.ToString();
            Form3_appkey = textBox2.Text.ToString();
            Form3_cardid = textBox3.Text.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            this.Hide();
            fr1.Show();
        }
    }
}
