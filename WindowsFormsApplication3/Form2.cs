using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(2);
            Form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = System.IO.File.ReadAllText(@"svg.txt");
            int svg = Convert.ToInt16(text);
            Form1 Form1 = new Form1(svg);
            Form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            Form1 Form1 = new Form1(7);
            Form1.Show();
            this.Hide();
        }
    }
}
