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
    public partial class Form1 : Form
    {
        game plateau;
        int mapi;
        int pointP1;
        int pointP2;
        public Form1(int map = 2, int nbBombe = 1, int porteBombe = 1, int point = 0, int point2 = 0)
        {
            mapi = map;

            InitializeComponent();
            label2.Text = "Niveau : " + Convert.ToString(map - 1);
            plateau = new game(11, 11, map);
            if (nbBombe >= 1 && porteBombe >= 1)
            {
                plateau.player[0].nbBombe = nbBombe;
                plateau.player[0].porteBombe = porteBombe;
                plateau.player[0].point = point;
            }
            pointP1 = point;
            pointP2 = point2;
            affGame();
        }
        public void affGame()
        {

            if (mapi == 7)
            {
                label4.Enabled = true;
                label3.Text = "score P1= " + Convert.ToString(pointP1);
                label4.Text = "score P2= " + Convert.ToString(pointP2);
            }
            else
            {
                label4.Enabled = false;
                label3.Text = "score = " + Convert.ToString(plateau.player[0].point);
            }
            int j = 0;
            int k = 0;
            for (int i = 0; i < 121; i++)
            {
                tableLayoutPanel1.BackColor = Color.Black;
                j = i / 11;
                if (k == 11) k = 0;
                if (plateau.getCase(j, k) == 0)
                {
                    tableLayoutPanel1.Controls[i].BackColor = Color.White;
                    tableLayoutPanel1.Controls[i].BackgroundImage = null;
                }
                int q = 0;
                while (q < plateau.plo.Count())
                {
                    if (plateau.plo[q].getEnemy(j, k))
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.chaine_chomper_icone_9045_128;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    q++;
                }
                int m = 0;
                while (m < plateau.boom.Count())
                {
                    if (plateau.boom[m].getBombe(j, k) == true)
                    {
                        if (plateau.boom[m].porter == 1) tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bombe;
                        if (plateau.boom[m].porter == 2) tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bombeBonus;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    m++;
                }
                int n = 0;
                while (n < plateau.defl.Count())
                {
                    if (plateau.defl[n].getDeflagration(j, k) == true)
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.explosion;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    n++;
                }
                int l = 0;
                while (l < plateau.player.Count())
                {
                    if (plateau.player[l].getPerso(j, k) == true)
                    {
                        if (l == 0) tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bomberman;
                        else if (l == 1) tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.Red;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    l++;
                }
                int o = 0;
                while (o < plateau.decor.Count())
                {
                    if (plateau.decor[o].getObstacle(j, k) == true && plateau.decor[o].type == 1)
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.brique;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    o++;
                }
                int p = 0;
                while (p < plateau.decor.Count())
                {
                    if (plateau.decor[p].getObstacle(j, k) == true && plateau.decor[p].type == 2)
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.gravier;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Center;
                    }
                    p++;
                }
                int r = 0;
                while (r < plateau.bobo.Count())
                {
                    if (plateau.bobo[r].getBonus(j, k) == 1)
                    {
                        tableLayoutPanel1.Controls[i].BackgroundImage = WindowsFormsApplication3.Properties.Resources.bombeBonus;
                        tableLayoutPanel1.Controls[i].BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    r++;
                }
                k++;
            }
        }
        class coordonner
        {
            public int x;
            public int y;
        }
        class game
        {
            int[,] tab;
            public coordonner max = new coordonner();
            public List<perso> player = new List<perso>();
            public List<obstacle> decor = new List<obstacle>();
            public List<bombe> boom = new List<bombe>();
            public List<deflagration> defl = new List<deflagration>();
            public List<enemy> plo = new List<enemy>();
            public List<bonus> bobo = new List<bonus>();

            public game(int x, int y, int map = 2)
            {
                max.x = x;
                max.y = y;
                tab = new int[x, y];
                loadMap(map);
            }
            private void loadMap(int map)
            {
                string text = System.IO.File.ReadAllText(@"C:/Users/Administrateur/Documents/Visual Studio 2015/Projects/bomberman/map" + Convert.ToString(map) + ".txt");
                coordonner pos = new coordonner();
                pos.x = 0;
                pos.y = 0;

                for (int i = 0; i < text.ToCharArray().Count() - 1; i++)
                {
                    if (text.ToCharArray()[i] == '1')
                    {
                        decor.Add(new obstacle(pos, 1));
                        pos.y++;
                    }
                    else if (text.ToCharArray()[i] == '2')
                    {
                        decor.Add(new obstacle(pos, 2));
                        pos.y++;
                    }
                    else if (text.ToCharArray()[i] == '3')
                    {
                        plo.Add(new enemy(pos.x, pos.y));
                        pos.y++;
                    }
                    else if (text.ToCharArray()[i] == '4')
                    {
                        player.Add(new perso(pos.x, pos.y));
                        pos.y++;
                    }
                    else if (text.ToCharArray()[i] == '5')
                    {
                        bobo.Add(new bonus(pos.x, pos.y, 1));
                        pos.y++;
                    }
                    else if (text.ToCharArray()[i] == '.')
                    {
                        pos.x++;
                        pos.y = 0;
                    }
                    else if (text.ToCharArray()[i] == '0')
                    {
                        pos.y++;
                    }

                }
            }
            public void poseBombe(perso player)
            {
                if (boom.Count() < player.nbBombe) boom.Add(new bombe(player.pos.x, player.pos.y, player.porteBombe));

            }
            public void defla(bombe bom)
            {
                int i = 0;
                defl.Add(new deflagration(bom.pos));
                int k = 0;
                while (k++ < bom.porter * 4 + 1) defl.Add(new deflagration(bom.pos));
                i = 1;
                k = 1;
                bool nord = true;
                bool sud = true;
                bool est = true;
                bool ouest = true;
                while (i + 4 < defl.Count())
                {
                    if (defl[i] != null && nord)
                    {
                        defl[i].pos.x += 1 * k;
                        if (colisionObstacle(defl[i].pos))
                        {
                            nord = false;
                            supObstacle(defl[i].pos);
                        }
                    }
                    i++;
                    if (defl[i] != null && sud)
                    {
                        defl[i].pos.x -= 1 * k;
                        if (colisionObstacle(defl[i].pos))
                        {
                            sud = false;
                            supObstacle(defl[i].pos);
                        }
                    }
                    i++;
                    if (defl[i] != null && est)
                    {
                        defl[i].pos.y += 1 * k;
                        if (colisionObstacle(defl[i].pos))
                        {
                            est = false;
                            supObstacle(defl[i].pos);
                        }
                    }
                    i++;
                    if (defl[i] != null && ouest)
                    {
                        defl[i].pos.y -= 1 * k;
                        if (colisionObstacle(defl[i].pos))
                        {
                            ouest = false;
                            supObstacle(defl[i].pos);
                        }
                    }
                    i++;
                    k++;
                }
            }
            public bool colisionObstacle(coordonner pos)
            {
                int i = 0;
                while (i < decor.Count())
                {
                    if (decor[i].pos.x == pos.x && decor[i].pos.y == pos.y) return true;
                    i++;
                }
                return false;
            }
            public void supObstacle(coordonner pos)
            {
                int i = 0;
                while (i < decor.Count())
                {
                    if (decor[i] != null && decor[i].type == 2 && decor[i].pos.x == pos.x && decor[i].pos.y == pos.y) decor.Remove(decor[i]);
                    i++;
                }
            }

            public int getCase(int x, int y)
            {
                return (tab[x, y]);
            }
        }
        class perso
        {
            public coordonner pos;
            public int nbBombe;
            public int porteBombe;
            public int point;

            public perso(int x, int y)
            {
                pos = new coordonner();
                pos.x = x;
                pos.y = y;
                nbBombe = 1;
                porteBombe = 1;
                point = 0;
            }

            public bool getPerso(int x, int y)
            {
                if (pos.x == x && pos.y == y)
                {
                    return true;
                }
                return false;
            }
            public void haut()
            {
                pos.x++;
            }
            public void bas()
            {
                pos.x--;
            }
            public void droit()
            {
                pos.y--;
            }
            public void gauche()
            {
                pos.y++;
            }
        }
        class enemy
        {
            public coordonner pos;
            int direction;

            public enemy(int x, int y, int move = 1)
            {
                direction = move;
                pos = new coordonner();
                pos.x = x;
                pos.y = y;
            }
            public bool getEnemy(int x, int y)
            {
                if (pos.x == x && pos.y == y)
                {
                    return true;
                }
                return false;
            }
            public void ia(game plateau)
            {
                Random rand = new Random();
                bool move_droite = true;
                bool move_gauche = true;
                bool move_bas = true;
                bool move_haut = true;
                for (int i = 0; i < plateau.decor.Count(); i++)
                {
                    if (plateau.decor[i] != null && plateau.decor[i].getObstacle(pos.x, pos.y - 1))
                    {
                        move_droite = false;
                    }
                }
                for (int i = 0; i < plateau.decor.Count(); i++)
                {
                    if (plateau.decor[i] != null && plateau.decor[i].getObstacle(pos.x, pos.y + 1))
                    {
                        move_gauche = false;
                    }
                }
                for (int i = 0; i < plateau.decor.Count(); i++)
                {
                    if (plateau.decor[i] != null && plateau.decor[i].getObstacle(pos.x - 1, pos.y))
                    {
                        move_bas = false;
                    }
                }
                for (int i = 0; i < plateau.decor.Count(); i++)
                {
                    if (plateau.decor[i] != null && plateau.decor[i].getObstacle(pos.x + 1, pos.y))
                    {
                        move_haut = false;
                    }
                }
                if (!((direction == 1 && move_droite) || (direction == 2 && move_gauche) || (direction == 3 && move_haut) || (direction == 4 && move_bas)))
                {
                    direction = rand.Next(1, 4);
                    while ((direction == 1 && !move_droite) || (direction == 2 && !move_gauche) || (direction == 3 && !move_haut) || (direction == 4 && !move_bas))
                    {
                        rand = new Random();
                        direction = rand.Next(1, 5);
                    }
                }
                if (move_droite && direction == 1) droit();
                if (move_gauche && direction == 2) gauche();
                if (move_bas && direction == 4) bas();
                if (move_haut && direction == 3) haut();
            }

            public void haut()
            {
                pos.x++;
            }
            public void bas()
            {
                pos.x--;
            }
            public void droit()
            {
                pos.y--;
            }
            public void gauche()
            {
                pos.y++;
            }
        }
        class bombe
        {
            public coordonner pos;
            public int time;
            public int porter;
            public bombe(int x, int y, int port = 2)
            {
                pos = new coordonner();
                pos.x = x;
                pos.y = y;
                time = 3;
                porter = port;
            }
            public bool getBombe(int x, int y)
            {
                if (x == pos.x && y == pos.y) return true;
                return false;
            }
        }
        class obstacle
        {
            public coordonner pos;
            public int type;

            public obstacle(coordonner depart, int t)
            {
                type = t;
                pos = new coordonner();
                pos.x = depart.x;
                pos.y = depart.y;
            }
            public bool getObstacle(int i, int j)
            {
                if (i == pos.x && j == pos.y) return true;
                return false;
            }

        }
        class deflagration
        {
            public coordonner pos;
            public int time;

            public deflagration(coordonner depar)
            {
                pos = new coordonner();
                pos.x = depar.x;
                pos.y = depar.y;
                time = 2;
            }
            public bool getDeflagration(int x, int y)
            {
                if ((x == pos.x && y == pos.y)) return true;
                return false;
            }
        }
        class bonus
        {
            public coordonner pos;
            public int type;

            public bonus(int x, int y, int ty)
            {
                pos = new coordonner();
                pos.x = x;
                pos.y = y;
                type = ty;
            }

            public int getBonus(int x, int y)
            {
                if (pos.x == x && pos.y == y) return type;
                else return 0;
            }

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool test = true;
            int i = 0;
            if (plateau.player.Count > 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    plateau.poseBombe(plateau.player[0]);
                }
                else if (e.KeyCode == Keys.Up && plateau.player[0].pos.x < plateau.max.x - 1)
                {

                    while (i < plateau.decor.Count())
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[0].pos.x + 1, plateau.player[0].pos.y)) test = false;
                        i++;
                    }
                    if (test) plateau.player[0].haut();
                }
                else if (e.KeyCode == Keys.Down && plateau.player[0].pos.x > 0)
                {

                    while (i < plateau.decor.Count())
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[0].pos.x - 1, plateau.player[0].pos.y)) test = false;
                        i++;
                    }
                    if (test) plateau.player[0].bas();
                }
                else if (e.KeyCode == Keys.Left && plateau.player[0].pos.y < plateau.max.y - 1)
                {

                    while (i < plateau.decor.Count())
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[0].pos.x, plateau.player[0].pos.y + 1)) test = false;
                        i++;
                    }
                    if (test) plateau.player[0].gauche();
                }
                else if (e.KeyCode == Keys.Right && plateau.player[0].pos.y > 0)
                {

                    while (i < plateau.decor.Count())
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[0].pos.x, plateau.player[0].pos.y - 1)) test = false;
                        i++;
                    }
                    if (test) plateau.player[0].droit();
                }
                int j = 0;
                while (j < plateau.bobo.Count())
                {
                    if (plateau.bobo[j].getBonus(plateau.player[0].pos.x, plateau.player[0].pos.y) == 1)
                    {
                        plateau.player[0].porteBombe = 2;
                        plateau.bobo.Remove(plateau.bobo[j]);
                    }
                    j++;
                }
            }
            i = 0;
            if (plateau.player.Count > 1)
            {
                if (e.KeyCode == Keys.Space)
                {
                    plateau.poseBombe(plateau.player[1]);
                }
                else if (e.KeyCode == Keys.Z && plateau.player[1].pos.x < plateau.max.x - 1)
                {

                    while (plateau.decor.Count() > i)
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[1].pos.x + 1, plateau.player[1].pos.y)) test = false;
                        i++;
                    }
                    if (test) plateau.player[1].haut();
                }
                else if (e.KeyCode == Keys.S && plateau.player[1].pos.x > 0)
                {

                    while (plateau.decor.Count() > i)
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[1].pos.x - 1, plateau.player[1].pos.y)) test = false;
                        i++;
                    }
                    if (test) plateau.player[1].bas();
                }
                else if (e.KeyCode == Keys.Q && plateau.player[1].pos.y < plateau.max.y - 1)
                {

                    while (plateau.decor.Count() > i)
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[1].pos.x, plateau.player[1].pos.y + 1)) test = false;
                        i++;
                    }
                    if (test) plateau.player[1].gauche();
                }
                else if (e.KeyCode == Keys.D && plateau.player[1].pos.y > 0)
                {

                    while (plateau.decor.Count() > i)
                    {
                        if (plateau.decor[i].getObstacle(plateau.player[1].pos.x, plateau.player[1].pos.y - 1)) test = false;
                        i++;
                    }
                    if (test) plateau.player[1].droit();
                }
            }
            affGame();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (plateau.plo.Count() == 0)
            {
                System.IO.File.WriteAllText(@"C:/Users/Administrateur/Documents/Visual Studio 2015/Projects/bomberman/svg.txt", Convert.ToString(mapi + 1));
                if (mapi + 1 == 6)
                {
                    Form5 Form5 = new Form5();
                    Form5.Show();
                    this.Hide();
                    timer1.Enabled = false;
                }
                else if (mapi + 1 < 8)
                {
                    Form1 Form1 = new Form1(mapi + 1, plateau.player[0].nbBombe, plateau.player[0].porteBombe, plateau.player[0].point);
                    Form1.Show();
                    this.Hide();
                    timer1.Enabled = false;
                }
            }
            if (plateau.player.Count() == 0 && mapi != 7)
            {
                Form3 Form3 = new Form3();
                Form3.Show();
                this.Hide();
                timer1.Enabled = false;
            }
            if (plateau.player.Count() == 1 && mapi == 7)
            {
                if (pointP1 == 100 && pointP2 == 100)
                {
                    timer1.Stop();
                    MessageBox.Show("egaliter");
                }
                else if (pointP1 == 100)
                {
                    timer1.Stop();
                    MessageBox.Show("P1 Win");
                }
                else if (pointP2 == 100)
                {
                    timer1.Stop();
                    MessageBox.Show("P2 Win");
                }
                else
                {
                    Form1 Form1 = new Form1(7, plateau.player[0].nbBombe, plateau.player[0].porteBombe, pointP1, pointP2);
                    Form1.Show();
                    this.Hide();
                    timer1.Enabled = false;
                }
            }
            int i = 0;
            while (plateau.boom.Count() > i)
            {
                plateau.boom[i].time--;
                if (plateau.boom[i] != null && plateau.boom[i].time < 0)
                {
                    plateau.defla(plateau.boom[i]);
                    plateau.boom.Remove(plateau.boom[i]);

                }
                i++;
            }
            i = 0;
            while (plateau.defl.Count() > i)
            {

                int j = 0;
                while (j < plateau.player.Count)
                {
                    if (plateau.defl[i].getDeflagration(plateau.player[j].pos.x, plateau.player[j].pos.y))
                    {
                        if (j == 0) pointP2 += 10;
                        else if (j == 1) pointP1 += 10;
                        plateau.player.Remove(plateau.player[j]);
                    }
                    j++;
                }
                j = 0;
                int m = 1;
                while (j < plateau.plo.Count())
                {
                    if (plateau.defl[i].getDeflagration(plateau.plo[j].pos.x, plateau.plo[j].pos.y))
                    {
                        plateau.plo.Remove(plateau.plo[j]);
                        plateau.player[0].point += 10 * m;
                        m++;
                    }
                    j++;
                }

                if (plateau.defl[i] != null && plateau.defl[i].time < 1)
                {
                    plateau.defl.Remove(plateau.defl[i]);
                }
                else plateau.defl[i].time--;
                i++;
            }
            for (int k = 0; k < plateau.plo.Count(); k++)
            {

                int l = 0;
                while (l < plateau.player.Count())
                {
                    if ((plateau.player[l].pos.x == plateau.plo[k].pos.x && plateau.player[l].pos.y == plateau.plo[k].pos.y))
                    {

                        plateau.player.Remove(plateau.player[l]);
                    }
                    l++;
                }
                plateau.plo[k].ia(plateau);
            }
            affGame();
        }



        private void label1_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(@"C:/Users/Administrateur/Documents/Visual Studio 2015/Projects/bomberman/svg.txt", Convert.ToString(mapi));
            Form2 Form2 = new Form2();
            Form2.Show();
            timer1.Enabled = false;
            this.Hide();

        }
    }
}
