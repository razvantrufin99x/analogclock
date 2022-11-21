using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace analogclock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Graphics g;
        public Font font0;
        public Font fontmin;
        public Pen pen0 = new Pen(Color.Silver);
        public Pen pen2 = new Pen(Color.Silver,3);
        public Pen penH = new Pen(Color.Red,4);
        public Pen penM = new Pen(Color.Blue,4);
        public Pen penS = new Pen(Color.Green,4);
        public Pen penmS = new Pen(Color.Brown,4);
        public Brush brush0 = new SolidBrush(Color.DimGray);
        public DateTime dt = DateTime.Now;
        public DateTime timpulinitial = DateTime.Now;
        public TimeSpan ts;
        public int ora;
        public int minutul;
        public int secunda;
        public int milisecunda;
        public static int posx = 20;
        public static int posy = 20;
        public int xwidth = 100;
        public int yheight = 100;
        public double rad = (180 / Math.PI);
        public int cx = posx + 50;
        public int cy = posy + 50;
        public int ismousedown = 0;


        public void updatetime()
        {
            dt = DateTime.Now;
            ora = dt.Hour;
            minutul = dt.Minute;
            secunda = dt.Second;
            milisecunda = dt.Millisecond;

        }

        public void writetime()
        {
            label1.Text = ora.ToString() + " : " + minutul.ToString() + " : " + secunda.ToString() + " : " + milisecunda.ToString();

        }

        public void calculatetimespan()
        {
            ts = dt - timpulinitial;
            label2.Text = ts.ToString();
        }

        public void drawTimer()
        {
            //g.DrawEllipse(pen0, posx, posy, xwidth, yheight);
            g.DrawEllipse(pen2, posx-8, posy-8, xwidth+16, yheight+16);
            g.DrawEllipse(pen2, posx + 8, posy + 8, xwidth - 16, yheight - 16);
            g.DrawEllipse(pen2, posx + 30, posy + 30, xwidth - 60, yheight - 60);
            g.DrawEllipse(pen2, posx + 45, posy + 45, xwidth - 90, yheight - 90);

        }

        public void drawOra()
        {
            ora = ora - 3;
            double sint = Math.Sin((ora * 30) / rad) * (xwidth - 75) + cx;
            double scos = Math.Cos((ora * 30) / rad) * (yheight - 75) + cy;
            g.DrawLine(penH, +cx, cy, Convert.ToInt16(scos), Convert.ToInt16(sint));
        }

        public void drawMinutul()
        {
            minutul = minutul - 15;
            double sint = Math.Sin((minutul * 6) / rad) * (xwidth - 50) + cx;
            double scos = Math.Cos((minutul * 6) / rad) * (yheight - 50) + cy;
            g.DrawLine(penM, +cx, cy, Convert.ToInt16(scos), Convert.ToInt16(sint));
        }

        public void drawSecunda()
        {
            secunda = secunda - 15;
            double sint = Math.Sin((secunda * 6) / rad) * (xwidth - 85) + cx;
            double scos = Math.Cos((secunda * 6) / rad) * (yheight - 85) + cy;
            g.DrawLine(penS, +cx, cy, Convert.ToInt16(scos), Convert.ToInt16(sint));
        }

        public void drawMilisecunda()
        {
            milisecunda = milisecunda - 75;
            double sint = Math.Sin((milisecunda * 1) / rad) * (xwidth - 90) + cx;
            double scos = Math.Cos((milisecunda * 1) / rad) * (yheight - 90) + cy;
            g.DrawLine(penmS, +cx, cy, Convert.ToInt16(scos), Convert.ToInt16(sint));
        }

        public void drawOraText(int x, int y, string h) {
            g.DrawString(h, font0, brush0, x-5, y-7);
        }

        public void drawMinutulText(int x, int y, string m)
        {
            g.DrawString(m, fontmin, brush0, x, y);
        }


        public void drawCadranOre()
        {
            int sora = 2;
            for (int i = 0; i < 360; i += 30)
            {
                double sint = Math.Sin((i) / rad) * (xwidth / 2) + cx;
                double scos = Math.Cos((i) / rad) * (yheight / 2) + cy;
                g.DrawLine(penH, Convert.ToInt16(scos) , Convert.ToInt16(sint) , Convert.ToInt16(scos), Convert.ToInt16(sint));
                if (sora < 12)
                {
                    sora++;
                }
                else { sora = 0; sora++; }
                drawOraText(Convert.ToInt16(scos), Convert.ToInt16(sint), sora.ToString());
            }
        }

        public void drawCadranMinute()
        {
            int smin = 15;
            for (int i = 0; i < 360; i += 6)
            {
                double sint = Math.Sin((i) / rad) * (xwidth / 2) + cx;
                double scos = Math.Cos((i) / rad) * (yheight / 2) + cy;
                g.DrawLine(penM, Convert.ToInt16(scos) , Convert.ToInt16(sint) , Convert.ToInt16(scos), Convert.ToInt16(sint));
                if (smin < 60)
                {
                    smin+=1;
                }
                else { smin = 0; smin+=1; }
                if(smin % 5 == 0){
               // drawMinutulText(Convert.ToInt16(scos), Convert.ToInt16(sint), smin.ToString());
                //Thread.Sleep(100);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //font0 = Font;
            font0 = new Font("Tahoma", 8);
            fontmin = new Font("Tahoma", 2);
            g = CreateGraphics();
           

        }

        public void clear()
        {
            g.Clear(BackColor);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            clear();

            drawTimer();
            drawCadranOre();
            drawCadranMinute();

            updatetime();


            writetime();
            calculatetimespan();


            drawOra();
            drawMinutul();
            drawSecunda();
            //drawMilisecunda();

            Thread.Sleep(100);



        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            drawTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            ismousedown = 0;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ismousedown == 1)
            {
                Left += e.X;
                Top += e.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ismousedown = 1;
        }
    }
}
