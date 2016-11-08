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
    public partial class Form4 : Form
    {
        int[] tab = new int[121];
        public Form4()
        {
            InitializeComponent();
            for (int i = 0; i < 121; i++)
            {
                tableLayoutPanel1.Controls[i].BackColor = Color.White;
                tableLayoutPanel1.Controls[i].BackgroundImage = null;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 121; i++)
            {
                if (sender.Equals(tableLayoutPanel1.Controls[i]))
                {

                    if (comboBox1.Text == "Brique")
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.brique;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Stretch;
                        tab[i] = 1;
                    }
                    else if (comboBox1.Text == "Gravier")
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.gravier;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Stretch;
                        tab[i] = 2;
                    }
                    else if (comboBox1.Text == "Blanc")
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = null;
                        tableLayoutPanel1.Controls[i].BackColor = Color.White;
                        tab[i] = 0;
                    }
                    else if (comboBox1.Text == "Enemy")
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.chaine_chomper_icone_9045_128;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                        tab[i] = 3;
                    }
                    else if (comboBox1.Text == "Point de depart")
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bomberman;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                        tab[i] = 4;
                    }
                    else if ("Bombe Bonus Porter" == comboBox1.Text)
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bombeBonus;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                        tab[i] = 5;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text +".txt";
            String map = "";
            int j = 0;
            int k = 0;
            int i = 0;
            for (i = 0; i < 131; i++)
            {
                if (tab[k] == 1) map = map.Insert(i, "1");
                else if (tab[k] == 2) map = map.Insert(i, "2");
                else if (tab[k] == 3) map = map.Insert(i, "3");
                else if (tab[k] == 4) map = map.Insert(i, "4");
                else if (tab[k] == 5) map = map.Insert(i, "5");
                else map = map.Insert(i, "0");
                if (j == 11)
                {
                    j = 1;
                    map = map.Insert(i, ".");
                    i++;
                }
                else j++;
                k++;
            }
            map = map.Insert(i, ".");
            System.IO.File.WriteAllText(@name, map);
        }
    }
}