using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ClassLibrary1;
using System.Threading;
using Microsoft.VisualBasic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //start splashscreen
            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();       
            Thread.Sleep(4500); //set time 
            t.Abort();
            //end splashscreen                     
            InitializeComponent();
            choosepic.ShortcutKeys = Keys.Control | Keys.P;
            endprogram.ShortcutKeys = Keys.Control | Keys.Q;
            // status bar           
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_status);
            pictureBox1.MouseLeave += new EventHandler(colsepictureBox1_status);
            pictureBox_color_left.MouseMove += new MouseEventHandler(pictureBox_color_status);
            pictureBox_color_left.MouseLeave += new EventHandler(closepictureBox_color_status);
            pictureBox_bald_left.MouseMove += new MouseEventHandler(pictureBox_bald1_status);
            pictureBox_bald_left.MouseLeave += new EventHandler(closepictureBox_bald1_status);
            pictureBox_other_left.MouseMove += new MouseEventHandler(pictureBox_other_status);
            pictureBox_other_left.MouseLeave += new EventHandler(closepictureBox_other_status);
            pictureBox_color_right.MouseMove += new MouseEventHandler(pictureBox3_status);
            pictureBox_color_right.MouseLeave += new EventHandler(closepictureBox3_status);
            pictureBox_other_right.MouseMove += new MouseEventHandler(pictureBox_other2_status);
            pictureBox_other_right.MouseLeave += new EventHandler(closepictureBox_other2_status);
            pictureBox_Histogram.MouseMove += new MouseEventHandler(pictureBox_Histogram_status);
            pictureBox_Histogram.MouseLeave += new EventHandler(closepictureBox_Histogram_status);
            pictureBox_Histogrambef.MouseMove += new MouseEventHandler(pictureBox_Histogrambef_status);
            pictureBox_Histogrambef.MouseLeave += new EventHandler(closepictureBox_Histogrambef_status);
            pictureBox_Histogram_other.MouseMove += new MouseEventHandler(pictureBox_Histogram_other_status);
            pictureBox_Histogram_other.MouseLeave += new EventHandler(closepictureBox_Histogram_other_status);
            checkedListBox_subregion.ItemCheck += new ItemCheckEventHandler(checkedListBox_subregion_ItemCheck);
            chart_dynamic.MouseDown += new MouseEventHandler(dynamic_MouseDown);
          
        }
        void pictureBox1_status(object sender, MouseEventArgs e) 
        {            
            if (pictureBox1.Image != null)
            {
                Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                panel_status.Visible = true;
                labelstatus_x.Text = "X:" + e.X.ToString();
                labelstatus_y.Text = "Y:" + e.Y.ToString();
                Color pixelColor = image1.getBitmap.GetPixel(e.X, e.Y);
                Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                for (int x = 0; x < statusRed.Width; x++)
                {
                    for (int y = 0; y < statusRed.Height; y++)
                    {
                        statusRed.SetPixel(x, y, nowColorR);
                        statusGreen.SetPixel(x, y, nowColorG);
                        statusBlue.SetPixel(x, y, nowColorB);
                        statusColor.SetPixel(x, y, pixelColor);
                    }
                }
                pictureBox_statusR.Image = statusRed;
                pictureBox_statusG.Image = statusGreen;
                pictureBox_statusB.Image = statusBlue;
                pictureBox_statusColor.Image = statusColor;
            }
            else return;
        }
        void colsepictureBox1_status(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        void pictureBox_color_status(object sender, MouseEventArgs e)
        {          
            if (pictureBox_color_left.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_color_left.Width - pictureBox_color_left.Image.Width) / 2;
                offset_y = (pictureBox_color_left.Height - pictureBox_color_left.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_color_left.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_color_left.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_color_left.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }      
        void closepictureBox_color_status(object sender, EventArgs e)
        {
            if (pictureBox_color_left.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        void pictureBox_bald1_status(object sender, MouseEventArgs e)
        {
            if (pictureBox_bald_left.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_bald_left.Width - pictureBox_bald_left.Image.Width) / 2;
                offset_y = (pictureBox_bald_left.Height - pictureBox_bald_left.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_bald_left.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_bald_left.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_bald_left.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                    label_baldr.Visible = true; label_baldg.Visible = true; label_baldb.Visible = true;
                    label_baldr.Text = "R:" + pixelColor.R;
                    label_baldg.Text = "G:" + pixelColor.G;
                    label_baldb.Text = "B:" + pixelColor.B;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_bald1_status(object sender, EventArgs e)
        {
            if (pictureBox_bald_left.Image != null)
            {
                panel_status.Visible = false;
                label_baldr.Visible = false; label_baldg.Visible = false; label_baldb.Visible = false;
            }
            else return;
        }
        void pictureBox_other_status(object sender, MouseEventArgs e)
        {          
            if (pictureBox_other_left.Image != null)
            {
                int offset_x,offset_y;               
                offset_x = (pictureBox_other_left.Width - pictureBox_other_left.Image.Width) / 2;
                offset_y = (pictureBox_other_left.Height - pictureBox_other_left.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_other_left.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_other_left.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_other_left.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_other_status(object sender, EventArgs e)
        {
            if (pictureBox_other_left.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        void pictureBox3_status(object sender, MouseEventArgs e)
        {         
            if (pictureBox_color_right.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_color_right.Width - pictureBox_color_right.Image.Width) / 2;
                offset_y = (pictureBox_color_right.Height - pictureBox_color_right.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_color_right.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_color_right.Image.Height)
                {
                        Bitmap imagenow = new Bitmap(pictureBox_color_right.Image);
                        Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                        Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                        Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                        Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                        panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                        Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                        Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                        Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                        for (int x = 0; x < statusRed.Width; x++)
                        {
                            for (int y = 0; y < statusRed.Height; y++)
                            {
                                statusRed.SetPixel(x, y, nowColorR);
                                statusGreen.SetPixel(x, y, nowColorG);
                                statusBlue.SetPixel(x, y, nowColorB);
                                statusColor.SetPixel(x, y, pixelColor);
                            }
                        }
                        pictureBox_statusR.Image = statusRed;
                        pictureBox_statusG.Image = statusGreen;
                        pictureBox_statusB.Image = statusBlue;
                        pictureBox_statusColor.Image = statusColor;
                 }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox3_status(object sender, EventArgs e)
        {
            if (pictureBox_color_right.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        void pictureBox_other2_status(object sender, MouseEventArgs e)
        {          
            if (pictureBox_other_right.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_other_right.Width - pictureBox_other_right.Image.Width) / 2;
                offset_y = (pictureBox_other_right.Height - pictureBox_other_right.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_other_right.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_other_right.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_other_right.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);                   
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_other2_status(object sender, EventArgs e)
        {
            if (pictureBox_other_right.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        
        void pictureBox_Histogram_status(object sender, MouseEventArgs e)
        {
            if (pictureBox_Histogram.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_Histogram.Width - pictureBox_Histogram.Image.Width) / 2;
                offset_y = (pictureBox_Histogram.Height - pictureBox_Histogram.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_Histogram.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_Histogram.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_Histogram.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_Histogram_status(object sender, EventArgs e)
        {
            if (pictureBox_Histogram.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        
        void pictureBox_Histogrambef_status(object sender, MouseEventArgs e)
        {
            if (pictureBox_Histogrambef.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_Histogrambef.Width - pictureBox_Histogrambef.Image.Width) / 2;
                offset_y = (pictureBox_Histogrambef.Height - pictureBox_Histogrambef.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_Histogrambef.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_Histogrambef.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_Histogrambef.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_Histogrambef_status(object sender, EventArgs e)
        {
            if (pictureBox_Histogrambef.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        
        void pictureBox_Histogram_other_status(object sender, MouseEventArgs e)
        {
            if (pictureBox_Histogram_other.Image != null)
            {
                int offset_x, offset_y;
                offset_x = (pictureBox_Histogram_other.Width - pictureBox_Histogram_other.Image.Width) / 2;
                offset_y = (pictureBox_Histogram_other.Height - pictureBox_Histogram_other.Image.Height) / 2;
                if (offset_x <= e.X && e.X < offset_x + pictureBox_Histogram_other.Image.Width && offset_y <= e.Y && e.Y < offset_y + pictureBox_Histogram_other.Image.Height)
                {
                    Bitmap imagenow = new Bitmap(pictureBox_Histogram_other.Image);
                    Bitmap statusRed = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusGreen = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusBlue = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    Bitmap statusColor = new Bitmap(20, 20, PixelFormat.Format24bppRgb);
                    panel_status.Visible = true;
                    labelstatus_x.Text = "X:" + (e.X - offset_x).ToString();
                    labelstatus_y.Text = "Y:" + (e.Y - offset_y).ToString();
                    Color pixelColor = imagenow.GetPixel(e.X - offset_x, e.Y - offset_y);
                    Color nowColorR = Color.FromArgb(pixelColor.R, 0, 0);
                    Color nowColorG = Color.FromArgb(0, pixelColor.G, 0);
                    Color nowColorB = Color.FromArgb(0, 0, pixelColor.B);
                    for (int x = 0; x < statusRed.Width; x++)
                    {
                        for (int y = 0; y < statusRed.Height; y++)
                        {
                            statusRed.SetPixel(x, y, nowColorR);
                            statusGreen.SetPixel(x, y, nowColorG);
                            statusBlue.SetPixel(x, y, nowColorB);
                            statusColor.SetPixel(x, y, pixelColor);
                        }
                    }
                    pictureBox_statusR.Image = statusRed;
                    pictureBox_statusG.Image = statusGreen;
                    pictureBox_statusB.Image = statusBlue;
                    pictureBox_statusColor.Image = statusColor;
                }
                else panel_status.Visible = false;
            }
            else return;
        }
        void closepictureBox_Histogram_other_status(object sender, EventArgs e)
        {
            if (pictureBox_Histogram_other.Image != null)
            {
                panel_status.Visible = false;
            }
            else return;
        }
        
        // ststus bar end
        public void SplashStart()
        {
            Application.Run(new Form2());
        }

        public void initial_controltool() 
        {           
            panel_Histogram.Visible = false;

            label_depth.Visible = false;
            panel_scale.Visible = false;           
            trackBar_scale.Value = 0;
            label_other.Visible = false;
            label_other2.Visible = false;
            trackBar_light.Visible = false;
            label_light.Visible = false;
            label_dark.Visible = false;

            panel_rotate.Visible = false;
            trackBar_rotate.Value = 0;
            counterclockwise_updown.Value = 0;
            clockwise_updown.Value = 0;

            trackBar_transparent.Value = 0;
            trackBar_transparent.Visible = false;
            //magic bar
            magic_function = false;
            IsSelected_subregion = false;
            transparent = false;

            label_Otsu.Visible = false;

            checkedListBox_subregion.Visible = false;
            foreach (int i in checkedListBox_subregion.CheckedIndices)
                checkedListBox_subregion.SetItemCheckState(i, CheckState.Unchecked);
            pictureBox_other_left.Refresh();            
            //watermark
            panel_bitplane.Visible = false;
            button_Watermark.Enabled = false;
            pictureBox_watermark.Image = null;
            label_watermark.Visible = false;
            button_watermarkslice.Enabled = false;
            comboBox_watermark.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            //dynamic
            chart_dynamic.Series[0].Points.Clear();
            chart_dynamic.Series[0].Points.AddXY(0, 0);
            pictureBox_dynamic.Image = image_dynamictemp;
            for (int n = 0; n < 256; n++)
            {
                dynamic[n] = n;
                chart_dynamic.Series[0].Points.AddXY(n, dynamic[n]);
            }
            for (int n = 1; n < 255; n++)
            {
                endpoint[n] = false;
                chart_dynamic.Series[0].Points.AddXY(n, dynamic[n]);
            }
            endpoint[0] = true;
            endpoint[255] = true;
            //filter
            pictureBox_filter_left.Image = null;
            pictureBox_filter_right.Image = null;
            label41.Visible = false;
            label42.Visible = false;
        }      

        Class1.readPic image1, image2;
        Class1.readHeader readHeader1;
        Class1.readTailPalette palette;
        Bitmap image3 = null;
        Bitmap image_forscale = null;
        Bitmap image_dynamictemp = null;
        Bitmap image_watermarkslice = null;
        Bitmap image_watermarkorigin = null;
        Bitmap image_watermarkslicebit0 = null;
        Bitmap image_watermarkslicebit1 = null;
        Bitmap image_watermarkslicebit2 = null;
        Bitmap image_watermarkslicebit3 = null;
        Bitmap image_watermarkslicebit4 = null;
        Bitmap image_watermarkslicebit5 = null;
        Bitmap image_watermarkslicebit6 = null;
        Bitmap image_watermarkslicebit7 = null;
        Bitmap image_noise1 = null;


        Boolean IsSelected_subregion = false;
        Boolean transparent = false; 
        Boolean magic_function = false;

        //讀取R通道
        private void red_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if(image_forscale != null){ image3 = image_forscale; image_forscale = null; }              
                Bitmap image_red = new Bitmap(image3.Width,image3.Height, PixelFormat.Format24bppRgb);
                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                        image_red.SetPixel(x, y, newColor);
                    }
                }
                // Set the PictureBox to display the image. 
                initial_controltool();
                image3 = image_red;
                pictureBox_color_left.Image = image3;                                            
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;                                           
            }
            else return;

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        //縮放
        private void enlarge_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                //scale
                initial_controltool();
                panel_scale.Visible = true;
                trackBar_scale.Value = 0;
                if (image3 == null) {image3 = image1.getBitmap;}
                pictureBox_other_left.Image = image3;
                pictureBox_other_right.Image = null;              
            }
            else return;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }      

        //旋轉
        private void rotate_Click(object sender, EventArgs e)
        {           
            if (image3 == null) { image3 = new Bitmap(image1.getBitmap); }
            else if (image_forscale != null) { image3 = new Bitmap(image_forscale); image_forscale = null; }
            if (image3 != null)
            {
                initial_controltool();
                panel_rotate.Visible = true;
            }
            else return;
        }       
        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {         
            if (trackBar_rotate.Value < 0){
                counterclockwise_updown.Value = Math.Abs(trackBar_rotate.Value);
                clockwise_updown.Value = 0;
                clockwisechange = false;
            }
            else {                 
                clockwise_updown.Value = trackBar_rotate.Value;
                counterclockwise_updown.Value = 0;
                counterclockwisechange = false;
            }         
            double degrees = -(trackBar_rotate.Value);                     
            double angle = Math.PI * degrees / 180.0;
            double revangle = -angle;         
            //rotate
            Bitmap image_hole;
            image_hole = new Bitmap(
            Math.Abs(Convert.ToInt32(Math.Cos(angle) * image3.Width)) + Math.Abs(Convert.ToInt32(Math.Sin(angle) * image3.Height)) +2,
            Math.Abs(Convert.ToInt32(Math.Sin(angle) * image3.Width)) + Math.Abs(Convert.ToInt32(Math.Cos(angle) * image3.Height)) +2
            );
            Bitmap image_perfect = new Bitmap(image_hole.Width, image_hole.Height);                           
            double M11 = Math.Cos(angle);
            double M12 = Math.Sin(angle);
            double M21 = -Math.Sin(angle);
            double M22 = Math.Cos(angle);

            double invM11 = Math.Cos(revangle);
            double invM12 = Math.Sin(revangle);
            double invM21 = -Math.Sin(revangle);
            double invM22 = Math.Cos(revangle);
            //hole
            for (int x = 0; x < image3.Width; x++)
            {
              for (int y = 0; y < image3.Height; y++)
              {
               Color originpixelColor = image3.GetPixel(x, y);
               int x_vec, y_vec;
               int x_coordinate, y_coordinate;

               x_vec = x - (image3.Width / 2);
               y_vec = y - (image3.Height / 2);

               x_coordinate = Convert.ToInt32(x_vec * M11 + y_vec * M12);
               y_coordinate = Convert.ToInt32(x_vec * M21 + y_vec * M22);

               x_coordinate = x_coordinate + (image_hole.Width / 2);
               y_coordinate = y_coordinate + (image_hole.Height / 2);
               if (0 <= x_coordinate && x_coordinate < image_hole.Width && 0 <= y_coordinate && y_coordinate < image_hole.Height)
                {
                image_hole.SetPixel(x_coordinate, y_coordinate, originpixelColor);
                }
              }
            }

           //perfect
            for (int x = 0; x < image_perfect.Width; x++)
            {
                    for (int y = 0; y < image_perfect.Height; y++)
                    {
                        int x_vec, y_vec;
                        int x_coordinate, y_coordinate;

                        x_vec = x - (image_perfect.Width / 2);
                        y_vec = y - (image_perfect.Height / 2);

                        x_coordinate = Convert.ToInt32(x_vec * invM11 + y_vec * invM12);
                        y_coordinate = Convert.ToInt32(x_vec * invM21 + y_vec * invM22);

                        x_coordinate = x_coordinate + (image3.Width / 2);
                        y_coordinate = y_coordinate + (image3.Height / 2);

                        if (0 <= x_coordinate && x_coordinate < image3.Width && 0 <= y_coordinate && y_coordinate < image3.Height)
                        {
                            Color originpixelColor = image3.GetPixel(x_coordinate, y_coordinate);
                            image_perfect.SetPixel(x, y, originpixelColor);
                        }
                    }
            }
            //image3 = image_perfect;
            // Set the PictureBox to display the image.

            pictureBox_other_left.Image = image_perfect;
            pictureBox_other_right.Image = image_hole;          
            image_forscale = image_perfect;
            pictureBox_color_left.Image = image_forscale;
        }

        //Gray
        private void gray_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {               
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                Bitmap image_gray = new Bitmap(image1.getBitmap);
              
                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image_gray.GetPixel(x, y);
                        int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        int index_range = 256 / Convert.ToInt32(Math.Pow(2, Convert.ToInt32(numericUpDown_depth.Value)));
                        int color_range = 255/ (Convert.ToInt32(Math.Pow(2, Convert.ToInt32(numericUpDown_depth.Value)))-1);
                        gray = (gray / index_range) * color_range;
                        Color newColor = Color.FromArgb(gray, gray, gray);
                        image_gray.SetPixel(x, y, newColor);                        
                    }
                }

                // Set the PictureBox to display the image.
                initial_controltool();
                label_depth.Visible = true;
                image3 = image_gray;
                pictureBox_color_left.Image = image3;                                         
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;
            }
            else return;
        }

        //Black and White
        private void whiteblack_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                Bitmap image_whiteblack = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
               
                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        if (gray < 128)
                        {
                            Color newColor = Color.FromArgb(0, 0, 0);
                            image_whiteblack.SetPixel(x, y, newColor);
                        }
                        else
                        {
                            Color newColor = Color.FromArgb(255, 255, 255);
                            image_whiteblack.SetPixel(x, y, newColor);
                        }
                    }
                }
                // Set the PictureBox to display the image.
                initial_controltool();
                image3 = image_whiteblack;
                pictureBox_color_left.Image = image3;                          
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;
            }
            else return;
        }

        //透明度
        private void transparency_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,//該值確定是否可以選擇多個檔案
                    Title = "請選擇資料夾",
                    Filter = "所有檔案(*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string FileName = dialog.FileName;
                    // Retrieve the image.
                    image2 = new Class1.readPic(FileName);
                    // Set the PictureBox to display the image.
                    initial_controltool();
                    transparent = true;
                    trackBar_transparent.Visible = true;
                    trackBar_transparent.Value = 0;

                    pictureBox_color_left.Image = image2.getBitmap;
                    pictureBox_color_right.Image = image3;                                     
                    pictureBox_other_right.Image = null;               
                }                               
            }
        }

        //負片
        private void negative_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }                
                Bitmap image_negative = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
               
                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        Color newColor = Color.FromArgb(
                            255 - pixelColor.R,
                            255 - pixelColor.G,
                            255 - pixelColor.B);
                        image_negative.SetPixel(x, y, newColor);
                    }
                }

                // Set the PictureBox to display the image.
                initial_controltool();
                image3 = image_negative;
                pictureBox_color_left.Image = image3;              
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;                             
                pictureBox_other_right.Image = null;
            }
            else return;
        }

        public void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (image3 == null) { image3 = image1.getBitmap; }
            else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
            if (transparent == true) 
            {               
                Bitmap image_transparent = new Bitmap(
                Math.Max(image3.Width, image2.getBitmap.Width), Math.Max(image3.Height, image2.getBitmap.Height)
                );
                double transparency = Convert.ToInt32(trackBar_transparent.Value);
                transparency = transparency / 100;
                //different pic size,enlarge them to same size
                if (image3.Width != image2.getBitmap.Width && image3.Height != image2.getBitmap.Height)
                {
                    //enlarge to same size
                    Bitmap image1big = new Bitmap(image_transparent.Width, image_transparent.Height);
                    image1big.MakeTransparent(Color.White);
                    Bitmap image2big = new Bitmap(image_transparent.Width, image_transparent.Height);
                    image2big.MakeTransparent(Color.White);

                    //color themselves pixelcolor
                    int x, y;
                    for (x = 0; x < image3.Width; x++)
                    {
                        for (y = 0; y < image3.Height; y++)
                        {
                            Color image1bigpixelColor = image3.GetPixel(x, y);
                            image1big.SetPixel(x, y, image1bigpixelColor);
                        }
                    }
                    for (x = 0; x < image2.getBitmap.Width; x++)
                    {
                        for (y = 0; y < image2.getBitmap.Height; y++)
                        {
                            Color image2bigpixelColor = image2.getBitmap.GetPixel(x, y);
                            image2big.SetPixel(x, y, image2bigpixelColor);
                        }
                    }

                    //overlapping them
                    for (x = 0; x < image_transparent.Width; x++)
                    {
                        for (y = 0; y < image_transparent.Height; y++)
                        {
                            Color image1pixelColor = image1big.GetPixel(x, y);
                            Color image2pixelColor = image2big.GetPixel(x, y);

                            Color newColor = Color.FromArgb
                                (
                                Convert.ToInt32((1 - transparency) * image1pixelColor.R + transparency * image2pixelColor.R),
                                Convert.ToInt32((1 - transparency) * image1pixelColor.G + transparency * image2pixelColor.G),
                                Convert.ToInt32((1 - transparency) * image1pixelColor.B + transparency * image2pixelColor.B)
                                );
                            image_transparent.SetPixel(x, y, newColor);
                        }
                    }
                    // Set the PictureBox to display the image.
                    pictureBox_color_right.Image = image_transparent;                
                }

                //same size
                else
                {
                    //overlapping them
                    int x, y;
                    for (x = 0; x < image3.Width; x++)
                    {
                        for (y = 0; y < image3.Height; y++)
                        {
                            Color image1pixelColor = image3.GetPixel(x, y);
                            Color image2pixelColor = image2.getBitmap.GetPixel(x, y);

                            Color newColor = Color.FromArgb
                                (
                                Convert.ToInt32(transparency * image1pixelColor.R + (1 - transparency) * image2pixelColor.R),
                                Convert.ToInt32(transparency * image1pixelColor.G + (1 - transparency) * image2pixelColor.G),
                                Convert.ToInt32(transparency * image1pixelColor.B + (1 - transparency) * image2pixelColor.B)
                                );
                            image_transparent.SetPixel(x, y, newColor);
                        }
                    }
                    // Set the PictureBox to display the image.
                    pictureBox_color_right.Image = image_transparent;                   
                }
            }
        }
        
        

        public void trackBar2_Scroll(object sender, EventArgs e)
        {          
            int Scale;
            Bitmap image_register = image3;
            Bitmap image_interpolation = null;
            Bitmap image_duplication = null;          
            if (Convert.ToInt32(trackBar_scale.Value) <= -1)
            {
                Scale = Math.Abs(Convert.ToInt32(trackBar_scale.Value)) + 1;  //縮小
            }
            else { Scale = Convert.ToInt32(trackBar_scale.Value) + 1; } //放大

            //enlarge
            if (trackBar_scale.Value >= 1)
            {              
                image_interpolation = new Bitmap(image_register.Width * Scale - (Scale - 1), image_register.Height * Scale - (Scale - 1)); //create new pic  
                image_duplication = new Bitmap(image_register.Width * Scale - (Scale - 1), image_register.Height * Scale - (Scale - 1));
                LockBitmap lockimage_interpolation = new LockBitmap(image_interpolation);
                LockBitmap lockimage_duplication = new LockBitmap(image_duplication);
                LockBitmap lockimage_register = new LockBitmap(image_register);

                label_other.Text = "Interpolation";
                label_other2.Text = "Duplication";

                lockimage_interpolation.LockBits();
                lockimage_duplication.LockBits();
                lockimage_register.LockBits();
                
                //複製法
                for (int x = 0; x < image_register.Width; x++)
                {
                    for (int y = 0; y < image_register.Height; y++)
                    {
                        Color originpixelColor = lockimage_register.GetPixel(x, y);
                        for (int z = 0; z < Scale; z++)
                        {
                            for (int k = 0; k < Scale; k++)
                            {
                                if (x * Scale + z < image_duplication.Width && y * Scale + k < image_duplication.Height)
                                { lockimage_duplication.SetPixel(x * Scale + z, y * Scale + k, originpixelColor); }
                                else { lockimage_duplication.SetPixel(x * Scale, y * Scale, originpixelColor); }
                            }
                        }
                        
                    }
                }
                
                //copy some point to new pic(內插法)
                for (int x = 0; x < image_register.Width; x++)
                {
                    for (int y = 0; y < image_register.Height; y++)
                    {
                        Color originpixelColor = lockimage_register.GetPixel(x, y);
                        lockimage_interpolation.SetPixel(x * Scale, y * Scale, originpixelColor);
                    }
                }
                //add color
                for (int x = 0; x < image_interpolation.Width - Scale; x = x + Scale)

                {
                    for (int y = 0; y < image_interpolation.Height - Scale; y = y + Scale)
                    {
                        
                        int up_r = (lockimage_interpolation.GetPixel(x + Scale, y).R - (lockimage_interpolation.GetPixel(x, y).R)) / Scale;
                        int up_g = (lockimage_interpolation.GetPixel(x + Scale, y).G - (lockimage_interpolation.GetPixel(x, y).G)) / Scale;
                        int up_b = (lockimage_interpolation.GetPixel(x + Scale, y).B - (lockimage_interpolation.GetPixel(x, y).B)) / Scale;

                        int down_r = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).R - lockimage_interpolation.GetPixel(x, y + Scale).R) / Scale;
                        int down_g = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).G - lockimage_interpolation.GetPixel(x, y + Scale).G) / Scale;
                        int down_b = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).B - lockimage_interpolation.GetPixel(x, y + Scale).B) / Scale;

                        int left_r = (lockimage_interpolation.GetPixel(x, y + Scale).R - lockimage_interpolation.GetPixel(x, y).R) / Scale;
                        int left_g = (lockimage_interpolation.GetPixel(x, y + Scale).G - lockimage_interpolation.GetPixel(x, y).G) / Scale;
                        int left_b = (lockimage_interpolation.GetPixel(x, y + Scale).B - lockimage_interpolation.GetPixel(x, y).B) / Scale;

                        int right_r = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).R - lockimage_interpolation.GetPixel(x + Scale, y).R) / Scale;
                        int right_g = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).G - lockimage_interpolation.GetPixel(x + Scale, y).G) / Scale;
                        int right_b = (lockimage_interpolation.GetPixel(x + Scale, y + Scale).B - lockimage_interpolation.GetPixel(x + Scale, y).B) / Scale;
                        //draw edge
                        for (int z = 1; z < Scale; z++)
                        {
                            Color up_edge = Color.FromArgb(lockimage_interpolation.GetPixel(x, y).R + z * up_r, lockimage_interpolation.GetPixel(x, y).G + z * up_g, lockimage_interpolation.GetPixel(x, y).B + z * up_b);
                            lockimage_interpolation.SetPixel(x + z, y, up_edge);
                            Color down_edge = Color.FromArgb(lockimage_interpolation.GetPixel(x, y + Scale).R + z * down_r, lockimage_interpolation.GetPixel(x, y + Scale).G + z * down_g, lockimage_interpolation.GetPixel(x, y + Scale).B + z * down_b);
                            lockimage_interpolation.SetPixel(x + z, y + Scale, down_edge);
                            Color left_edge = Color.FromArgb(lockimage_interpolation.GetPixel(x, y).R + z * left_r, lockimage_interpolation.GetPixel(x, y).G + z * left_g, lockimage_interpolation.GetPixel(x, y).B + z * left_b);
                            lockimage_interpolation.SetPixel(x, y + z, left_edge);
                            Color right_edge = Color.FromArgb(lockimage_interpolation.GetPixel(x + Scale, y).R + z * right_r, lockimage_interpolation.GetPixel(x + Scale, y).G + z * right_g, lockimage_interpolation.GetPixel(x + Scale, y).B + z * right_b);
                            lockimage_interpolation.SetPixel(x + Scale, y + z, right_edge);
                        }
                    }
                }

                for (int x = 0; x < image_interpolation.Width - Scale; x += Scale)
                {
                    for (int y = 0; y < image_interpolation.Height - Scale; y += Scale)
                    {
                        for (int t = 1; t < Scale; t++)
                        {
                            int parallel_r = (lockimage_interpolation.GetPixel(x + Scale, y + t).R - lockimage_interpolation.GetPixel(x, y + t).R) / Scale;
                            int parallel_g = (lockimage_interpolation.GetPixel(x + Scale, y + t).G - lockimage_interpolation.GetPixel(x, y + t).G) / Scale;
                            int parallel_b = (lockimage_interpolation.GetPixel(x + Scale, y + t).B - lockimage_interpolation.GetPixel(x, y + t).B) / Scale;


                            for (int z = 1; z < Scale; z++)
                            {
                                 Color inside = Color.FromArgb(
                                (lockimage_interpolation.GetPixel(x, y + t).R + z * parallel_r),
                                (lockimage_interpolation.GetPixel(x, y + t).G + z * parallel_g),
                                (lockimage_interpolation.GetPixel(x, y + t).B + z * parallel_b)
                                );
                                lockimage_interpolation.SetPixel(x + z, y + t, inside);
                            }
                        }
                    }
                }
                lockimage_interpolation.UnlockBits();
                lockimage_duplication.UnlockBits();
                lockimage_register.UnlockBits();
            }
            //shrink
            else if (trackBar_scale.Value <= -1) 
            {
                label_other.Text = "Average";
                label_other2.Text = "Decimation";
                PixelFormat pixelFormat = PixelFormat.Format24bppRgb;
                image_interpolation = new Bitmap(image_register.Width / Scale, image_register.Height / Scale, pixelFormat); //create new pic 
                image_duplication = new Bitmap(image_register.Width / Scale, image_register.Height / Scale, pixelFormat);
               
                //刪去法
                for (int x = 0; x <= image_register.Width - Scale; x += Scale)
                {
                    for (int y = 0; y <= image_register.Height - Scale; y += Scale)
                    {
                        Color originpixelColor = image_register.GetPixel(x, y);                       
                        image_duplication.SetPixel(x / Scale , y / Scale , originpixelColor);                       
                    }
                }
                //平均法
                for (int x = 0; x <= image_register.Width -Scale; x = x + Scale)
                {
                    for (int y = 0; y <= image_register.Height - Scale; y = y + Scale)
                    {
                        int r = 0;
                        int g = 0;
                        int b = 0;
                        for (int z = 0; z < Scale; z++) 
                        {
                            for (int t = 0; t < Scale; t++) 
                            {
                                r = r + image_register.GetPixel(x + z, y + t).R;
                                g = g + image_register.GetPixel(x + z, y + t).G;
                                b = b + image_register.GetPixel(x + z, y + t).B;                              
                            }
                        }
                        r = Convert.ToInt32(r / Math.Pow(Scale, 2));
                        g = Convert.ToInt32(g / Math.Pow(Scale, 2));
                        b = Convert.ToInt32(b / Math.Pow(Scale, 2));
                        Color newpixelColor = Color.FromArgb(r,g,b);
                        image_interpolation.SetPixel(x / Scale, y / Scale, newpixelColor);
                    }
                }
            }
            //original pic
            else {image_interpolation = image_register; label_other.Text = "";label_other2.Text = "";
            }
            /* //check for debug
                 bool test = false;
                 for (int x = 0; x < image4.Width; x++)
                 {
                     for (int y = 0; y < image4.Height; y++)
                     {
                         Color pixelColor = image4.GetPixel(x, y);
                         if (pixelColor == null || pixelColor == Color.Black)
                         {
                             Console.WriteLine((x, y));
                             test = true;
                         }
                     }
                 }
                 if (test == true) { MessageBox.Show("There was an error"); }*/
            // Set the PictureBox to display the image.      
            image_forscale = image_interpolation;
            pictureBox_other_left.Image = image_interpolation;
            pictureBox_other_right.Image = image_duplication;
            pictureBox_color_left.Image = pictureBox_other_left.Image;
            pictureBox_color_right.Image = null;
            label_other.Visible = true;
            label_other2.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
      
        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        //choose pic
        private void choosepic_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,//該值確定是否可以選擇多個檔案
                    Title = "請選擇資料夾",
                    Filter = "所有檔案(*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string FileName = dialog.FileName;
                    // Retrieve the image.
                    image1 = new Class1.readPic(FileName);
                }

                readHeader1 = new Class1.readHeader(dialog.FileName);
                palette = new Class1.readTailPalette(dialog.FileName);

                // Set the PictureBox to display the image.
                pictureBox1.Image = image1.getBitmap;
                pictureBox_color_left.Image = image1.getBitmap;
                pictureBox_other_left.Image = image1.getBitmap;

                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;
                image3 = image1.getBitmap;
                image_noise1= image1.getBitmap;

                // Display the headerbyte in textbox
                string st = "";
                foreach (byte b in readHeader1.Headerbit)
                { st += b.ToString(); st += " "; }
                textBox1.Text = st;
                textBox1.Visible = true;
                st = "The fixed header field valued:";
                st += readHeader1.Headerbit[0].ToString(); st += " ";
                label9.Text = st;
                label9.Visible = true;

                st = "The version number:";
                st += readHeader1.Headerbit[1].ToString(); st += " ";
                label10.Text = st;
                label10.Visible = true;
                st = "The method used for encoding the image data:";
                st += readHeader1.Headerbit[2].ToString(); st += " ";
                label11.Text = st;
                label11.Visible = true;
                st = "The number of bits constituting one pixel in a plane:";
                st += readHeader1.Headerbit[3].ToString(); st += " ";
                label12.Text = st;
                label12.Visible = true;

                // clear picturebox                             
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                initial_controltool();
                // active buttom
                colorRed.Enabled = true;
                colorGreen.Enabled = true;
                colorBlue.Enabled = true;
                gray.Enabled = true;
                label_depth.Visible = true;
                whiteblack.Enabled = true;
                transparency.Enabled = true;
                negative.Enabled = true;
                rotate.Enabled = true;
                enlarge.Enabled = true;
                subregion.Enabled = true;
                clear_color.Enabled = true;
                button_HistogramFullcolor.Enabled = true;
                button_HistogramGray.Enabled = true;
                button_Otsu.Enabled = true;
                button_threshold.Enabled = true;
                numericUpDown_depth.Visible = true;
                numericUpDown_thres.Visible = true;
                button_Bitplanebinary.Enabled = true;
                button_Bitplanegray.Enabled = true;
                button_magic.Enabled = true;
                clear_color.Visible = true;
                panel_noise.Visible = true;
                panel_filter.Visible = true;
                button_component.Visible = true;
                button_light.Enabled = true;
                button_specification.Enabled = true;
                button_magicwand.Enabled = true;
                // palette
                Color[] color = palette.getPalette();
                Bitmap image_Palette = new Bitmap(320, 320, PixelFormat.Format24bppRgb);
                int i = 0;
                for (int x = 0; x < image_Palette.Width; x = x + 20)
                {
                    for (int y = 0; y < image_Palette.Height; y = y + 20)
                    {
                        for (int z = 0; z < 20; z++)
                        {
                            for (int t = 0; t < 20; t++)
                            {
                                Color pixelColor = color[i];
                                image_Palette.SetPixel(x + z, y + t, pixelColor);
                            }
                        }
                        i++;
                    }
                }
                picture_Palette.Image = image_Palette;
               
                //chart clear
                chart_Red.Series[0].Points.Clear();
                chart_Red.Series[0].Points.AddXY(0, 0);
                chart_Green.Series[0].Points.Clear();
                chart_Green.Series[0].Points.AddXY(0, 0);
                chart_Blue.Series[0].Points.Clear();
                chart_Blue.Series[0].Points.AddXY(0, 0);
               
                chart_RGBcolor.Series[0].Points.Clear();
                chart_RGBcolor.Series[0].Points.AddXY(0, 0);
                chart_RGBcolor.Series[0].Color = Color.Red;
                chart_RGBcolor.Series[1].Points.Clear();
                chart_RGBcolor.Series[1].Points.AddXY(0, 0);
                chart_RGBcolor.Series[1].Color = Color.Green;
                chart_RGBcolor.Series[2].Points.Clear();
                chart_RGBcolor.Series[2].Points.AddXY(0, 0);
                chart_RGBcolor.Series[2].Color = Color.Blue;

                chart_Gray.Series[0].Points.Clear();
                chart_Gray.Series[0].Points.AddXY(0, 0);
                //count
                int[] dataR = new int[256];
                int[] dataG = new int[256];
                int[] dataB = new int[256]; 
                int[] dataGray = new int[256];
                for (int x = 0; x < image1.getBitmap.Width; x++)
                {
                    for (int y = 0; y < image1.getBitmap.Height; y++)
                    {
                        Color pixelColor = image1.getBitmap.GetPixel(x, y);
                        dataR[pixelColor.R]++;
                        dataG[pixelColor.G]++;
                        dataB[pixelColor.B]++;
                        dataGray[Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B)]++;
                    }
                }
                //draw chart
                for (int t = 0; t < 256; t++)
                {
                    chart_Red.Series[0].Points.AddXY(t, dataR[t]);
                    chart_Green.Series[0].Points.AddXY(t, dataG[t]);
                    chart_Blue.Series[0].Points.AddXY(t, dataB[t]);
                    
                    chart_RGBcolor.Series[0].Points.AddXY(t, dataR[t]);
                    chart_RGBcolor.Series[1].Points.AddXY(t, dataG[t]);
                    chart_RGBcolor.Series[2].Points.AddXY(t, dataB[t]);

                    chart_Gray.Series[0].Points.AddXY(t, dataGray[t]);
                }
                chart_Red.Series[0].Color = Color.Red;
                chart_Green.Series[0].Color = Color.Green;
                chart_Blue.Series[0].Color = Color.Blue;               
                chart_Red.Visible = true;
                chart_Green.Visible = true;
                chart_Blue.Visible = true;
                chart_RGBcolor.Visible = true;
                checkedListBox1.Visible = true;

                chart_Gray.Visible = true;
                for (int n = 0; n < checkedListBox1.Items.Count; n++)
                {
                    checkedListBox1.SetItemChecked(n, true);                   
                }

                //dynamic     
                for (int n = 0; n < 256; n++)
                {
                    dynamic[n] = n;
                    chart_dynamic.Series[0].Points.AddXY(n, dynamic[n]);
                }
                for (int n = 1; n < 255; n++)
                {
                    endpoint[n] = false;
                    chart_dynamic.Series[0].Points.AddXY(n, dynamic[n]);
                }
                endpoint[0] = true;
                endpoint[255] = true;
                image_dynamictemp = new Bitmap(image1.getBitmap);
                    // Loop through the images pixels to reset color.                  
                    for (int x = 0; x < image_dynamictemp.Width; x++)
                    {
                        for (int y = 0; y < image_dynamictemp.Height; y++)
                        {
                            Color pixelColor = image_dynamictemp.GetPixel(x, y);
                            int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                            Color newColor = Color.FromArgb(gray, gray, gray);
                            image_dynamictemp.SetPixel(x, y, newColor);
                        }
                    }
                pictureBox_dynamic.Image = image_dynamictemp;
                panel_dynamic.Visible = true;
            }
            catch (ArgumentException)
            {
            }                   
        }
       
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        //reset program
        private void clear_color_Click(object sender, EventArgs e)
        {
            initial_controltool();
            pictureBox_color_right.Image = null;
            pictureBox_other_right.Image = null;
            image3 = image1.getBitmap;
            image_forscale = null;
            pictureBox_color_left.Image = image3;
            pictureBox_other_left.Image = image3;
            image_noise1 = image1.getBitmap;
        }

        private void 功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //讀取G通道
        private void colorGreen_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                Bitmap iamge_green = new Bitmap(image3);
                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        Color newColor = Color.FromArgb(0, pixelColor.G, 0);
                        iamge_green.SetPixel(x, y, newColor);
                    }
                }
                // Set the PictureBox to display the image.  
                initial_controltool();
                image3 = iamge_green;
                pictureBox_color_left.Image = image3;             
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;
            }
            else return;

        }

        private void endprogram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemChecked(0) == false) { chart_RGBcolor.Series[0].Color = Color.Transparent ; }
            else { chart_RGBcolor.Series[0].Color = Color.Red; }
            if (checkedListBox1.GetItemChecked(1) == false) { chart_RGBcolor.Series[1].Color = Color.Transparent; }
            else { chart_RGBcolor.Series[1].Color = Color.Green; }
            if (checkedListBox1.GetItemChecked(2) == false) { chart_RGBcolor.Series[2].Color = Color.Transparent; }
            else { chart_RGBcolor.Series[2].Color = Color.Blue; }
        }

        private void chart_color_Click(object sender, EventArgs e)
        {

        }

        Boolean counterclockwisechange = false;  //互斥控制
        Boolean clockwisechange = false;         //互斥控制
        private void counterclockwise_updown_ValueChanged(object sender, EventArgs e)
        {
            if (counterclockwisechange == false || clockwise_updown.Value == 0)  //互斥控制
            {
                trackBar_rotate.Value = Convert.ToInt32(-counterclockwise_updown.Value);
                trackBar1_Scroll_1(sender, e);
                counterclockwisechange = true;
            }          
        }

        private void clockwise_updown_ValueChanged(object sender, EventArgs e)
        {
            if (clockwisechange == false || counterclockwise_updown.Value == 0)   //互斥控制
            { 
            trackBar_rotate.Value = Convert.ToInt32(clockwise_updown.Value);
            trackBar1_Scroll_1(sender, e);
            clockwisechange = true;
            }        
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        //直方圖均衡化灰階
        private void button_HistogramGray_Click(object sender, EventArgs e)
        {
            //Histogram Equalization
            //Histogram_whiteblack
            Bitmap image_gray = new Bitmap(image1.getBitmap.Width, image1.getBitmap.Height, PixelFormat.Format24bppRgb);
            Bitmap image_Histogram = new Bitmap(image_gray);
            
            chart_Histogrambef.Series[0].Points.Clear();
            chart_Histogrambef.Series[0].Points.AddXY(0, 0);
            chart_Histogrambef.Series[1].Points.Clear();
            chart_Histogrambef.Series[1].Points.AddXY(0, 0);
            chart_Histogrambef.Series[2].Points.Clear();
            chart_Histogrambef.Series[2].Points.AddXY(0, 0);
            chart_Histogrambef.Series[3].Points.Clear();
            chart_Histogrambef.Series[3].Points.AddXY(0, 0);
            chart_Histogrambef.Series[4].Points.Clear();
            chart_Histogrambef.Series[4].Points.AddXY(0, 0);
            chart_Histogrambef.Series[5].Points.Clear();
            chart_Histogrambef.Series[5].Points.AddXY(0, 0);
            chart_Histogrambef.Series[6].Points.Clear();
            chart_Histogrambef.Series[6].Points.AddXY(0, 0);
            chart_Histogrambef.Series[7].Points.Clear();
            chart_Histogrambef.Series[7].Points.AddXY(0, 0);
            //
            chart_Histogram_other.Visible = false;
            pictureBox_Histogram_other.Visible = false;
            checkedListBox_Histogram_other.Visible = false;
            checkedListBox_Histogram.Visible = false;
            checkedListBox_Histogrambef.Visible = false;
            //gray
            for (int x = 0; x < image1.getBitmap.Width; x++)
            {
                for (int y = 0; y < image1.getBitmap.Height; y++)
                {
                    Color pixelColor = image1.getBitmap.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_gray.SetPixel(x, y, newColor);
                }
            }
            pictureBox_Histogrambef.Image = image_gray;
            double[] dataGray = new double[256];
            double[] countGray = new double[256];
            double totalpixel = image1.getBitmap.Width * image1.getBitmap.Height;
            for (int x = 0; x < image_gray.Width; x++)
            {
                for (int y = 0; y < image_gray.Height; y++)
                {
                    Color pixelColor = image_gray.GetPixel(x, y);
                    dataGray[pixelColor.G]++;
                }
            }           
            for (int t = 0; t < 256; t++)
            {
                chart_Histogrambef.Series[6].Points.AddXY(t, dataGray[t] / totalpixel);
            }
            panel_Histogram.Visible = true;
            countGray[0] = dataGray[0];         
            for (int t = 1; t < 256; t++)
            {
                //累積
                countGray[t] = countGray[t - 1] + dataGray[t];
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countGray[t] = countGray[t] / totalpixel;
                chart_Histogrambef.Series[7].Points.AddXY(t, countGray[t]);
            }
            for (int x = 0; x < image_gray.Width; x++)
            {
                for (int y = 0; y < image_gray.Height; y++)
                {
                    Color pixelColorGray = image_gray.GetPixel(x, y);
                    Color HistogramGray = Color.FromArgb((Convert.ToInt32(countGray[pixelColorGray.R] * 255)), (Convert.ToInt32(countGray[pixelColorGray.R] * 255)), (Convert.ToInt32(countGray[pixelColorGray.R] * 255)));
                    image_Histogram.SetPixel(x, y, HistogramGray);
                }
            }
            pictureBox_Histogram.Image = image_Histogram;          
            //Histogram_ chart
            double[] dataGrayHistogram = new double[256];
            double[] countGrayHistogram = new double[256];
            label17.Text = "灰階均衡化";
            label18.Text = "灰階";
            label19.Text = "";
            for (int x = 0; x < image_Histogram.Width; x++)
            {
                for (int y = 0; y < image_Histogram.Height; y++)
                {
                    Color pixelColor = image_Histogram.GetPixel(x, y);
                    dataGrayHistogram[pixelColor.G]++;
                }
            }
            chart_Histogram.Series[0].Points.Clear();  //DataR
            chart_Histogram.Series[0].Points.AddXY(0, 0);
            chart_Histogram.Series[1].Points.Clear();  //DataG
            chart_Histogram.Series[1].Points.AddXY(0, 0);
            chart_Histogram.Series[2].Points.Clear();  //DataB
            chart_Histogram.Series[2].Points.AddXY(0, 0);
            chart_Histogram.Series[6].Points.Clear();  //DataGray
            chart_Histogram.Series[6].Points.AddXY(0, 0);
            for (int t = 0; t < 256; t++)
            {
                chart_Histogram.Series[6].Points.AddXY(t, dataGrayHistogram[t] / totalpixel);
            }
            chart_Histogram.Visible = true;
            countGrayHistogram[0] = dataGrayHistogram[0];
            chart_Histogram.Series[3].Points.Clear();  //CountR
            chart_Histogram.Series[3].Points.AddXY(0, 0);
            chart_Histogram.Series[4].Points.Clear();  //CountG
            chart_Histogram.Series[4].Points.AddXY(0, 0);
            chart_Histogram.Series[5].Points.Clear();  //CountB
            chart_Histogram.Series[5].Points.AddXY(0, 0);
            chart_Histogram.Series[7].Points.Clear();  //CountGray
            chart_Histogram.Series[7].Points.AddXY(0, 0);
            for (int t = 1; t < 256; t++)
            {
                //累積
                countGrayHistogram[t] = countGrayHistogram[t - 1] + (dataGrayHistogram[t]);
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countGrayHistogram[t] = countGrayHistogram[t] / totalpixel;
                chart_Histogram.Series[7].Points.AddXY(t, countGrayHistogram[t]);
            }
        }

        //直方圖均衡化彩色
        private void button_HistogramFullcolor_Click(object sender, EventArgs e)
        {
            chart_Histogram_other.Visible = true;
            pictureBox_Histogram_other.Visible = true;
            checkedListBox_Histogram_other.Visible = true;
            checkedListBox_Histogram.Visible = true;
            checkedListBox_Histogrambef.Visible = true;
            for (int n = 0; n < checkedListBox_Histogram.Items.Count; n++)
            {
                checkedListBox_Histogram.SetItemChecked(n, true);
                checkedListBox_Histogrambef.SetItemChecked(n, true);
                checkedListBox_Histogram_other.SetItemChecked(n, true);
            }
            //Histogram Equalization
            //Histogram_Fullcolor          
            Bitmap image_Histogram = new Bitmap(image1.getBitmap);           
            pictureBox_Histogram_other.Image = image1.getBitmap;
            double[] dataR = new double[256];
            double[] dataG = new double[256];
            double[] dataB = new double[256];
            double[] countR = new double[256];
            double[] countG = new double[256];
            double[] countB = new double[256];
            double totalpixel = image1.getBitmap.Width * image1.getBitmap.Height;
            for (int x = 0; x < image1.getBitmap.Width; x++)
            {
                for (int y = 0; y < image1.getBitmap.Height; y++)
                {
                    Color pixelColor = image1.getBitmap.GetPixel(x, y);
                    dataR[pixelColor.R]++;
                    dataG[pixelColor.G]++;
                    dataB[pixelColor.B]++;
                }
            }
            chart_Histogram_other.Series[0].Points.Clear(); //DataR
            chart_Histogram_other.Series[0].Points.AddXY(0, 0);
            chart_Histogram_other.Series[0].Color = Color.Red;
            chart_Histogram_other.Series[1].Points.Clear(); //DataG
            chart_Histogram_other.Series[1].Points.AddXY(0, 0);
            chart_Histogram_other.Series[1].Color = Color.Green;
            chart_Histogram_other.Series[2].Points.Clear();  //DataB
            chart_Histogram_other.Series[2].Points.AddXY(0, 0);
            chart_Histogram_other.Series[2].Color = Color.Blue;
            for (int t = 0; t < 256; t++)
            {
                chart_Histogram_other.Series[0].Points.AddXY(t, dataR[t] / totalpixel);
                chart_Histogram_other.Series[1].Points.AddXY(t, dataG[t] / totalpixel);
                chart_Histogram_other.Series[2].Points.AddXY(t, dataB[t] / totalpixel);
            }
            panel_Histogram.Visible = true;
            //new pic
            countR[0] = dataR[0];
            countG[0] = dataG[0];
            countB[0] = dataB[0];
            chart_Histogram_other.Series[3].Points.Clear();  //CountR
            chart_Histogram_other.Series[3].Points.AddXY(0, 0);
            chart_Histogram_other.Series[3].Color = Color.DarkRed;
            chart_Histogram_other.Series[4].Points.Clear();  //CountG
            chart_Histogram_other.Series[4].Points.AddXY(0, 0);
            chart_Histogram_other.Series[4].Color = Color.DarkGreen;
            chart_Histogram_other.Series[5].Points.Clear();  //CountB
            chart_Histogram_other.Series[5].Points.AddXY(0, 0);
            chart_Histogram_other.Series[5].Color = Color.DarkBlue;
            for (int t = 1; t < 256; t++)
            {
                //累積
                countR[t] = countR[t - 1] + dataR[t];
                countG[t] = countG[t - 1] + dataG[t];
                countB[t] = countB[t - 1] + dataB[t];             
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countR[t] = countR[t] / totalpixel;
                countG[t] = countG[t] / totalpixel;
                countB[t] = countB[t] / totalpixel;
                chart_Histogram_other.Series[3].Points.AddXY(t, countR[t]);
                chart_Histogram_other.Series[4].Points.AddXY(t, countG[t]);
                chart_Histogram_other.Series[5].Points.AddXY(t, countB[t]);             
            }
            for (int x = 0; x < image1.getBitmap.Width; x++)
            {
                for (int y = 0; y < image1.getBitmap.Height; y++)
                {
                    Color pixelColor = image1.getBitmap.GetPixel(x, y);
                    Color HistogramColor = Color.FromArgb((Convert.ToInt32(countR[pixelColor.R] * 255)), (Convert.ToInt32(countG[pixelColor.G] * 255)), (Convert.ToInt32(countB[pixelColor.B] * 255)));
                    image_Histogram.SetPixel(x, y, HistogramColor);
                }
            }
            pictureBox_Histogram.Image = image_Histogram;
            //Histogram_ chart
            double[] dataRHistogram = new double[256];
            double[] countRHistogram = new double[256];
            double[] dataGHistogram = new double[256];
            double[] countGHistogram = new double[256];
            double[] dataBHistogram = new double[256];
            double[] countBHistogram = new double[256];
            label17.Text = "彩色均衡化";
            label18.Text = "彩色平均後均衡化";
            label19.Text = "彩圖";
            for (int x = 0; x < image_Histogram.Width; x++)
            {
                for (int y = 0; y < image_Histogram.Height; y++)
                {
                    Color pixelColor = image_Histogram.GetPixel(x, y);
                    dataRHistogram[pixelColor.R]++;
                    dataGHistogram[pixelColor.G]++;
                    dataBHistogram[pixelColor.B]++;
                }
            }
            chart_Histogram.Series[0].Points.Clear();  //DataR
            chart_Histogram.Series[0].Points.AddXY(0, 0);
            chart_Histogram.Series[0].Color = Color.Red;
            chart_Histogram.Series[1].Points.Clear();  //DataG
            chart_Histogram.Series[1].Points.AddXY(0, 0);
            chart_Histogram.Series[1].Color = Color.Green;
            chart_Histogram.Series[2].Points.Clear();  //DataB
            chart_Histogram.Series[2].Points.AddXY(0, 0);
            chart_Histogram.Series[2].Color = Color.Blue;
            chart_Histogram.Series[6].Points.Clear();  //DataGray
            chart_Histogram.Series[6].Points.AddXY(0, 0);
            for (int t = 0; t < 256; t++)
            {
                chart_Histogram.Series[0].Points.AddXY(t, dataRHistogram[t] / totalpixel);
                chart_Histogram.Series[1].Points.AddXY(t, dataGHistogram[t] / totalpixel);
                chart_Histogram.Series[2].Points.AddXY(t, dataBHistogram[t] / totalpixel);
            }            
            countRHistogram[0] = dataRHistogram[0];
            countGHistogram[0] = dataGHistogram[0];
            countBHistogram[0] = dataBHistogram[0];
            chart_Histogram.Series[3].Points.Clear();  //CountR
            chart_Histogram.Series[3].Points.AddXY(0, 0);
            chart_Histogram.Series[3].Color = Color.DarkRed;
            chart_Histogram.Series[4].Points.Clear();  //CountG
            chart_Histogram.Series[4].Points.AddXY(0, 0);
            chart_Histogram.Series[4].Color = Color.DarkGreen;
            chart_Histogram.Series[5].Points.Clear();  //CountB
            chart_Histogram.Series[5].Points.AddXY(0, 0);
            chart_Histogram.Series[5].Color = Color.DarkBlue;
            chart_Histogram.Series[7].Points.Clear();  //CountGray
            chart_Histogram.Series[7].Points.AddXY(0, 0);
            for (int t = 1; t < 256; t++)
            {
                //累積
                countRHistogram[t] = countRHistogram[t - 1] + (dataRHistogram[t]);
                countGHistogram[t] = countGHistogram[t - 1] + (dataGHistogram[t]);
                countBHistogram[t] = countBHistogram[t - 1] + (dataBHistogram[t]);
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countRHistogram[t] = countRHistogram[t] / totalpixel;
                chart_Histogram.Series[3].Points.AddXY(t, countRHistogram[t]);
                countGHistogram[t] = countGHistogram[t] / totalpixel;
                chart_Histogram.Series[4].Points.AddXY(t, countGHistogram[t]);
                countBHistogram[t] = countBHistogram[t] / totalpixel;
                chart_Histogram.Series[5].Points.AddXY(t, countBHistogram[t]);
            }            

            //Histogram_Fullcolor by mean   
            Bitmap image_Histogram_mean = new Bitmap(image1.getBitmap);
            double[] countnew = new double[256];
            for (int t = 0; t < 256; t++)
            {                            
                //累積機率
                countnew[t] = (countR[t] + countG[t] + countB[t])/3;               
            }
            for (int x = 0; x < image1.getBitmap.Width; x++)
            {
                for (int y = 0; y < image1.getBitmap.Height; y++)
                {
                    Color pixelColor = image1.getBitmap.GetPixel(x, y);
                    Color HistogramColor = Color.FromArgb((Convert.ToInt32(countnew[pixelColor.R] * 255)), (Convert.ToInt32(countnew[pixelColor.G] * 255)), (Convert.ToInt32(countnew[pixelColor.B] * 255)));
                    image_Histogram_mean.SetPixel(x, y, HistogramColor);
                }
            }
            pictureBox_Histogrambef.Image = image_Histogram_mean;
            //
            dataRHistogram = new double[256];
            countRHistogram = new double[256];
            dataGHistogram = new double[256];
            countGHistogram = new double[256];
            dataBHistogram = new double[256];
            countBHistogram = new double[256];           
            for (int x = 0; x < image_Histogram_mean.Width; x++)
            {
                for (int y = 0; y < image_Histogram_mean.Height; y++)
                {
                    Color pixelColor = image_Histogram_mean.GetPixel(x, y);
                    dataRHistogram[pixelColor.R]++;
                    dataGHistogram[pixelColor.G]++;
                    dataBHistogram[pixelColor.B]++;
                }
            }
            chart_Histogrambef.Series[0].Points.Clear();  //DataR
            chart_Histogrambef.Series[0].Points.AddXY(0, 0);
            chart_Histogrambef.Series[0].Color = Color.Red;
            chart_Histogrambef.Series[1].Points.Clear();  //DataG
            chart_Histogrambef.Series[1].Points.AddXY(0, 0);
            chart_Histogrambef.Series[1].Color = Color.Green;
            chart_Histogrambef.Series[2].Points.Clear();  //DataB
            chart_Histogrambef.Series[2].Points.AddXY(0, 0);
            chart_Histogrambef.Series[2].Color = Color.Blue;
            chart_Histogrambef.Series[6].Points.Clear();  //DataGray
            chart_Histogrambef.Series[6].Points.AddXY(0, 0);
            for (int t = 0; t < 256; t++)
            {
                chart_Histogrambef.Series[0].Points.AddXY(t, dataRHistogram[t] / totalpixel);
                chart_Histogrambef.Series[1].Points.AddXY(t, dataGHistogram[t] / totalpixel);
                chart_Histogrambef.Series[2].Points.AddXY(t, dataBHistogram[t] / totalpixel);
            }
            countRHistogram[0] = dataRHistogram[0];
            countGHistogram[0] = dataGHistogram[0];
            countBHistogram[0] = dataBHistogram[0];
            chart_Histogrambef.Series[3].Points.Clear();  //CountR
            chart_Histogrambef.Series[3].Points.AddXY(0, 0);
            chart_Histogrambef.Series[3].Color = Color.DarkRed;
            chart_Histogrambef.Series[4].Points.Clear();  //CountG
            chart_Histogrambef.Series[4].Points.AddXY(0, 0);
            chart_Histogrambef.Series[4].Color = Color.DarkGreen;
            chart_Histogrambef.Series[5].Points.Clear();  //CountB
            chart_Histogrambef.Series[5].Points.AddXY(0, 0);
            chart_Histogrambef.Series[5].Color = Color.DarkBlue;
            chart_Histogrambef.Series[7].Points.Clear();  //CountGray
            chart_Histogrambef.Series[7].Points.AddXY(0, 0);
            for (int t = 1; t < 256; t++)
            {
                //累積
                countRHistogram[t] = countRHistogram[t - 1] + (dataRHistogram[t]);
                countGHistogram[t] = countGHistogram[t - 1] + (dataGHistogram[t]);
                countBHistogram[t] = countBHistogram[t - 1] + (dataBHistogram[t]);
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countRHistogram[t] = countRHistogram[t] / totalpixel;
                chart_Histogrambef.Series[3].Points.AddXY(t, countRHistogram[t]);
                countGHistogram[t] = countGHistogram[t] / totalpixel;
                chart_Histogrambef.Series[4].Points.AddXY(t, countGHistogram[t]);
                countBHistogram[t] = countBHistogram[t] / totalpixel;
                chart_Histogrambef.Series[5].Points.AddXY(t, countBHistogram[t]);
            }           
            //
        }

        private void chart_Histogram_other_Click(object sender, EventArgs e)
        {                    
        }

        private void checkedListBox_Histogram_other_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox_Histogram_other.GetItemChecked(0) == false) {
                chart_Histogram_other.Series[0].Color = Color.Transparent;
                chart_Histogram_other.Series[3].Color = Color.Transparent;
            }
            else {
                chart_Histogram_other.Series[0].Color = Color.Red;
                chart_Histogram_other.Series[3].Color = Color.DarkRed;
            }
            if (checkedListBox_Histogram_other.GetItemChecked(1) == false) {
                chart_Histogram_other.Series[1].Color = Color.Transparent;
                chart_Histogram_other.Series[4].Color = Color.Transparent;
            }
            else {
                chart_Histogram_other.Series[1].Color = Color.Green;
                chart_Histogram_other.Series[4].Color = Color.DarkGreen;
            }
            if (checkedListBox_Histogram_other.GetItemChecked(2) == false) {
                chart_Histogram_other.Series[2].Color = Color.Transparent;
                chart_Histogram_other.Series[5].Color = Color.Transparent;
            }
            else {
                chart_Histogram_other.Series[2].Color = Color.Blue;
                chart_Histogram_other.Series[5].Color = Color.DarkBlue;
            }
        }

        private void checkedListBox_Histogram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox_Histogram.GetItemChecked(0) == false)
            {
                chart_Histogram.Series[0].Color = Color.Transparent;
                chart_Histogram.Series[3].Color = Color.Transparent;
            }
            else
            {
                chart_Histogram.Series[0].Color = Color.Red;
                chart_Histogram.Series[3].Color = Color.DarkRed;
            }
            if (checkedListBox_Histogram.GetItemChecked(1) == false)
            {
                chart_Histogram.Series[1].Color = Color.Transparent;
                chart_Histogram.Series[4].Color = Color.Transparent;
            }
            else
            {
                chart_Histogram.Series[1].Color = Color.Green;
                chart_Histogram.Series[4].Color = Color.DarkGreen;
            }
            if (checkedListBox_Histogram.GetItemChecked(2) == false)
            {
                chart_Histogram.Series[2].Color = Color.Transparent;
                chart_Histogram.Series[5].Color = Color.Transparent;
            }
            else
            {
                chart_Histogram.Series[2].Color = Color.Blue;
                chart_Histogram.Series[5].Color = Color.DarkBlue;
            }
        }

        private void checkedListBox_Histogrambef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox_Histogrambef.GetItemChecked(0) == false)
            {
                chart_Histogrambef.Series[0].Color = Color.Transparent;
                chart_Histogrambef.Series[3].Color = Color.Transparent;
            }
            else
            {
                chart_Histogrambef.Series[0].Color = Color.Red;
                chart_Histogrambef.Series[3].Color = Color.DarkRed;
            }
            if (checkedListBox_Histogrambef.GetItemChecked(1) == false)
            {
                chart_Histogrambef.Series[1].Color = Color.Transparent;
                chart_Histogrambef.Series[4].Color = Color.Transparent;
            }
            else
            {
                chart_Histogrambef.Series[1].Color = Color.Green;
                chart_Histogrambef.Series[4].Color = Color.DarkGreen;
            }
            if (checkedListBox_Histogrambef.GetItemChecked(2) == false)
            {
                chart_Histogrambef.Series[2].Color = Color.Transparent;
                chart_Histogrambef.Series[5].Color = Color.Transparent;
            }
            else
            {
                chart_Histogrambef.Series[2].Color = Color.Blue;
                chart_Histogrambef.Series[5].Color = Color.DarkBlue;
            }
        }

        //Otsu algo.
        private void button_Otsu_Click(object sender, EventArgs e)
        {         
            if (image3 == null) { image3 = image1.getBitmap; }
            else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
            Bitmap image_gray = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);

            // Loop through the images pixels to reset color.               
            for (int x = 0; x < image3.Width; x++)
            {
                for (int y = 0; y < image3.Height; y++)
                {
                    Color pixelColor = image3.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_gray.SetPixel(x, y, newColor);
                }
            }
            // Set the PictureBox to display the image.
            initial_controltool();
            // pictureBox_color.Image = image_gray;             
            pictureBox_color_right.Image = image_gray;
            pictureBox_other_right.Image = null;

            ////////////////////////////////////////////////////////
            // Otsu other algo.
            /*double[] dataGray = new double[256];
            double[] countGray = new double[256];
            double totalpixel = image_gray.Width * image_gray.Height;
            for (int x = 0; x < image_gray.Width; x++)
            {
                for (int y = 0; y < image_gray.Height; y++)
                {
                    Color pixelColor = image_gray.GetPixel(x, y);
                    dataGray[pixelColor.G]++;
                }
            }
            double inter_var;
            double min_var = totalpixel;
            int threshold = 0;
            for (int index_histo = 0; index_histo < 256; index_histo++)
            {
                double w_1 = 0;
                double w_2 = 0;
                double Var_1 = 0;
                double Var_2 = 0;
                double Expectedvalue_1 = 0;
                double Expectedvalue_2 = 0;

                for (int i = 0; i < index_histo; i++)
                {
                    w_1 = w_1 + dataGray[index_histo];
                    Expectedvalue_1 = Expectedvalue_1 + dataGray[i] * i;
                }
                for (int i = index_histo; i < 256; i++)
                {
                    w_2 = totalpixel - w_1;
                    Expectedvalue_2 = Expectedvalue_2 + dataGray[i] * i;
                }
                for (int i = 0; i < index_histo; i++)
                {
                    Var_1 = Var_1 + dataGray[i] * Math.Pow((i - Expectedvalue_1), 2);
                }
                for (int i = index_histo; i < 256; i++)
                {
                    Var_2 = Var_2 + dataGray[i] * Math.Pow((i - Expectedvalue_2), 2);
                }
                w_1 /= totalpixel;
                w_2 /= totalpixel;
                Var_1 /= totalpixel;
                Var_2 /= totalpixel;
                inter_var = w_1 * Var_1 + w_2 * Var_2;

                if (inter_var <= min_var)
                {
                    threshold = index_histo;
                    min_var = inter_var;
                }
            }
            Bitmap image_Otsu = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < image3.Width; x++)
            {
                for (int y = 0; y < image3.Height; y++)
                {
                    Color pixelColor = image3.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    if (gray <= threshold) { gray = 0; }
                    else { gray = 255; }
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_Otsu.SetPixel(x, y, newColor);
                }
            }*/
            ////////////////////////////////////////////////////////

            //最大化類間變異數
            int[] dataGray = new int[256];
           int pixel_total = image_gray.Width * image_gray.Height;
           for (int x = 0; x < image_gray.Width; x++)
           {
               for (int y = 0; y < image_gray.Height; y++)
               {
                   Color pixelColor = image_gray.GetPixel(x, y);
                   dataGray[pixelColor.G]++;
               }
           }
           int sum_right = 0;
           int sum_left = 0;
           float w_right = 0;
           float w_left;
           float u_left;
            double max_var = 0;
           double inter_var;
           int threshold = 0;
           
           for (int index_histo = 1; index_histo < 256; ++index_histo)
           {
               sum_left += dataGray[index_histo] * index_histo ;
           }

           for (int index_histo = 1; index_histo < 256; ++index_histo)
           {
               w_right = w_right + dataGray[index_histo];
               w_left = pixel_total - w_right;
               if (w_right == 0 || w_left == 0)
               {
                   continue;
               }
               sum_right = sum_right + dataGray[index_histo] * index_histo;
               u_left = (sum_left - sum_right) / w_left;
               inter_var = w_right * w_left *Math.Pow(((sum_right / w_right) - u_left),2);
               if (inter_var >= max_var)
               {
                   threshold = index_histo;
                   max_var = inter_var;
               }
           }
            Bitmap image_Otsu = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < image3.Width; x++)
            {
                for (int y = 0; y < image3.Height; y++)
                {
                    Color pixelColor = image3.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    if (gray <= threshold) { gray = 0; }
                    else { gray = 255; }
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_Otsu.SetPixel(x, y, newColor);
                }
            }
           //
           pictureBox_color_left.Image = image_Otsu;
           image3 = image_Otsu;
           pictureBox_other_left.Image = image3;
           label_Otsu.Visible = true;
           label_Otsu.Text = "Threshold = " + threshold.ToString();         
       }

        ////讀取B通道
        private void colorBlue_Click(object sender, EventArgs e)
       {
           if (pictureBox1.Image != null)
           {
               if (image3 == null) { image3 = image1.getBitmap; }
               else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
               Bitmap iamge_blue = new Bitmap(image3);
               // Loop through the images pixels to reset color.
               int x, y;
               for (x = 0; x < image3.Width; x++)
               {
                   for (y = 0; y < image3.Height; y++)
                   {
                       Color pixelColor = image3.GetPixel(x, y);
                       Color newColor = Color.FromArgb(0, 0, pixelColor.B);
                       iamge_blue.SetPixel(x, y, newColor);
                   }
               }
               // Set the PictureBox to display the image.
               initial_controltool();
               image3 = iamge_blue;
               pictureBox_color_left.Image = image3;             
               pictureBox_other_left.Image = image3;
               pictureBox_color_right.Image = null;
               pictureBox_other_right.Image = null;
           }
           else return;

       }

       private void chartR_Click(object sender, EventArgs e)
       {

       }

       int start_x = 0, start_y = 0, end_x = 0, end_y = 0;


       private void checkedListBox_subregion_ItemCheck(Object sender, ItemCheckEventArgs e)
       {
           if (e.NewValue == CheckState.Checked)
           {
               foreach (int i in checkedListBox_subregion.CheckedIndices)
                   checkedListBox_subregion.SetItemCheckState(i, CheckState.Unchecked);
           }           
       }

       private void button_magic_Click(object sender, EventArgs e)
       {
           if (image3 == null) { image3 = image1.getBitmap; }
           else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
           pictureBox_other_left.Image = image3;
           initial_controltool();         
           Rectangle rect = new Rectangle(0, 0, 0, 0);         
           Boolean magic_choose = false; 
           magic_function = true;

           Point StartPoint = new Point(0, 0);
           GraphicsPath graphPath = new GraphicsPath();

           int offset_x, offset_y;
           offset_x = (pictureBox_other_left.Width - pictureBox_other_left.Image.Width) / 2;
           offset_y = (pictureBox_other_left.Height - pictureBox_other_left.Image.Height) / 2;
           int[,] magic_record = new int[pictureBox_other_left.Image.Width, pictureBox_other_left.Image.Height]; //邊界矩陣         
           pictureBox_other_left.MouseDown += new MouseEventHandler(Imagemagic_MouseDown);
           pictureBox_other_left.MouseUp += new MouseEventHandler(Imagemagic_MouseUp);
           pictureBox_other_left.MouseMove += new MouseEventHandler(Imagemagic_MouseMove);
           pictureBox_other_left.Paint += new PaintEventHandler(Imagemagic_Paint);

           void Imagemagic_MouseDown(object senderDown, MouseEventArgs eDown)
           {
               if (pictureBox_other_left.Image != null && magic_function == true)
               {
                   if (magic_choose == false)
                   {
                       magic_choose = true;
                       graphPath.Reset();
                       pictureBox_other_left.Cursor = Cursors.Hand;                     
                       start_x = eDown.X - offset_x;
                       start_y = eDown.Y - offset_y;
                       StartPoint = eDown.Location;
                   }
                   else 
                   {
                       magic_choose = false;                      
                       Scanline_Fill(eDown.X - offset_x, eDown.Y - offset_y);
                       drwamagic();                     
                   }
               }
               else return;
           }
           void Imagemagic_MouseUp(object senderUp, MouseEventArgs eUp)
           {
               if (pictureBox_other_left.Image != null && magic_function == true)
               {
                   pictureBox_other_left.Cursor = Cursors.Default;
                   end_x = eUp.X - offset_x;
                   end_y = eUp.Y - offset_y;                    

               }
               else return;
           }
           void Imagemagic_MouseMove(object senderDraw, MouseEventArgs eDraw)
           {
               if (pictureBox_other_left.Image != null && magic_function == true)
               {
                   if (eDraw.Button != MouseButtons.Left)//判断是否按下左键
                       return;
                    //線條粗細:3*3
                    for (int i = -1;i < 2;i++) 
                    {
                        for (int j = -1; j < 2; j++) 
                        {
                            if (eDraw.Location.X - offset_x + i >= 0 && eDraw.Location.X - offset_x + i < pictureBox_other_left.Image.Width
                       && eDraw.Location.Y - offset_y + j >= 0 && eDraw.Location.Y - offset_y + j < pictureBox_other_left.Image.Height)
                                magic_record[eDraw.Location.X - offset_x + i, eDraw.Location.Y - offset_y + j] = 1;
                        }
                    }
                                     
                    Point tempEndPoint = eDraw.Location; //記錄點的位置                  
                    graphPath.AddLine(StartPoint, tempEndPoint);
                    StartPoint = tempEndPoint;                  
                    pictureBox_other_left.Invalidate();                                    
                }
            }

            void Imagemagic_Paint(object senderDraw, PaintEventArgs eDraw)
            {
                if (pictureBox_other_left.Image != null && magic_function == true)
                {
                    if (magic_choose == true)
                    {                                     
                     Pen redPen = new Pen(Color.Red, 3);                    
                     eDraw.Graphics.DrawPath(redPen, graphPath);                                                 
                   }
                    else return;
                    

                }

            }
            void drwamagic()
            {
                Bitmap image_magic = new Bitmap(pictureBox_other_left.Image.Width, pictureBox_other_left.Image.Height);             
                for (int x = 0; x < pictureBox_other_left.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox_other_left.Image.Height; y++)
                    {
                        if (magic_record[x, y] == 2)
                        {
                            Color pixelColor = image3.GetPixel(x, y);
                            image_magic.SetPixel(x, y, pixelColor);
                        }
                    }
                }
                pictureBox_other_right.Image = image_magic;
                //initial magic_record(邊界矩陣)
                for (int i = 0;i< pictureBox_other_left.Image.Width;i++) {
                    for (int j = 0; j < pictureBox_other_left.Image.Height; j++)
                    {                      
                        magic_record[i, j] = 0;
                    }   
                }                          
            }
            //Scanline_Fill
            void Scanline_Fill(int x, int y)
            {
                Bitmap image_magic = new Bitmap(pictureBox_other_left.Image.Width, pictureBox_other_left.Image.Height);
                Stack<Point> magicStack = new Stack<Point>();
                Point fillin = new Point(x, y);
                Point fillout;
                Point filloutU, filloutD;
                int x1;
                Boolean spanAbove, spanBelow;
                magicStack.Push(fillin);
                while (magicStack.Count != 0)
                {                   
                    fillout = magicStack.Pop();                   
                    x1 = fillout.X;
                    y = fillout.Y;
                    while (x1 >= 0 && magic_record[x1,y] == 0) x1--;
                    x1++;
                    spanAbove = false;
                    spanBelow = false;
                    while (x1 < image3.Width && magic_record[x1,y] == 0)
                    {
                        magic_record[x1,y] = 2;
                        if (y > 0) {
                            if (!spanAbove && magic_record[x1, y - 1] == 0)
                            {
                                filloutU = new Point(x1, y - 1);
                                magicStack.Push(filloutU);
                                spanAbove = true;
                            }
                            else if (spanAbove && y > 0 && magic_record[x1, y - 1] != 0)
                            {
                                spanAbove = true;
                            }
                        }
                        if (y < image3.Height - 1) {
                            if (!spanBelow && magic_record[x1, y + 1] == 0)
                            {
                                filloutD = new Point(x1, y + 1);
                                magicStack.Push(filloutD);
                                spanBelow = true;
                            }
                            else if (spanBelow && y < image3.Height - 1 && magic_record[x1, y + 1] != 0)
                            {
                                spanBelow = true;
                            }
                        }
                        x1++;
                    }                      
                }
                magicStack.Clear();                             
            }
        }

        //Bitplane_Binary-code
        private void button_Bitplanebinary_Click(object sender, EventArgs e)
        {
            //graylevel
            if (pictureBox1.Image != null)
            {               
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                //Bitmap image3 = new Bitmap(image1.getBitmap) ;
                Bitmap image_gray = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit0 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit1 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit2 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit3 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit4 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit5 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit6 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit7 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);

                // Loop through the images pixels to reset color.               
                int bitplane;                
                for (int x = 0; x < image3.Width; x++)
                {
                    for (int y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        Color newColor = Color.FromArgb(gray, gray, gray);
                        image_gray.SetPixel(x, y, newColor);
                        //
                        bitplane = gray%2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        Color bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit0.SetPixel(x, y, bitlevel);
                        //                      
                        bitplane = (gray >> 1) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit1.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 2) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit2.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 3) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit3.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 4) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit4.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 5) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit5.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 6) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit6.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 7) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit7.SetPixel(x, y, bitlevel);
                        //
                    }
                }            
                // Set the PictureBox to display the image.
                initial_controltool();
                panel_bitplane.Visible = true;
                button_Watermark.Enabled = true;               
                pictureBox_bit0.Image = image_bit0;
                pictureBox_bit1.Image = image_bit1;
                pictureBox_bit2.Image = image_bit2;
                pictureBox_bit3.Image = image_bit3;
                pictureBox_bit4.Image = image_bit4;
                pictureBox_bit5.Image = image_bit5;
                pictureBox_bit6.Image = image_bit6;
                pictureBox_bit7.Image = image_bit7;
                pictureBox_bitgray.Image = image_gray;
                
                garycode = false;
            }
            else return;
        }

        ////Bitplane_Gray-code
        private void button_Bitplanegray_Click(object sender, EventArgs e)
        {
            //graylevel
            if (pictureBox1.Image != null)
            {              
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                Bitmap image_gray = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit0 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit1 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit2 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit3 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit4 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit5 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit6 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit7 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);

                // Loop through the images pixels to reset color.               
                int bitplane;
                for (int x = 0; x < image3.Width; x++)
                {
                    for (int y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        int binary_graycolor = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        Color newColor = Color.FromArgb(binary_graycolor, binary_graycolor, binary_graycolor);
                        image_gray.SetPixel(x, y, newColor);
                        int gray_graycolor = binaryToGray(binary_graycolor); //binaryToGray
                        //                       
                        bitplane = gray_graycolor % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        Color bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit0.SetPixel(x, y, bitlevel);
                        //                      
                        bitplane = (gray_graycolor >> 1) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit1.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 2) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit2.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 3) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit3.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 4) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit4.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 5) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit5.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 6) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit6.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray_graycolor >> 7) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit7.SetPixel(x, y, bitlevel);
                        //
                    }
                }
                // Set the PictureBox to display the image.
                initial_controltool();
                panel_bitplane.Visible = true;
                button_Watermark.Enabled = true;
                pictureBox_bit0.Image = image_bit0;
                pictureBox_bit1.Image = image_bit1;
                pictureBox_bit2.Image = image_bit2;
                pictureBox_bit3.Image = image_bit3;
                pictureBox_bit4.Image = image_bit4;
                pictureBox_bit5.Image = image_bit5;
                pictureBox_bit6.Image = image_bit6;
                pictureBox_bit7.Image = image_bit7;
                pictureBox_bitgray.Image = image_gray;

                
                garycode = true;
            }
            else return;
            int binaryToGray(int num)
            {
                return (num >> 1) ^ num;
            }
        }
        Boolean garycode;

        //Add watermark at Bitplane
        private void button_Watermark_Click(object sender, EventArgs e)
        {
            ////選擇檔案
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,//該值確定是否可以選擇多個檔案
                    Title = "請選擇浮水印",
                    Filter = "所有檔案(*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)        
                {
                    string FileName = dialog.FileName;
                    // Retrieve the image.
                    image2 = new Class1.readPic(FileName);
                    image_watermarkorigin = new Bitmap(image2.getBitmap);
                    Bitmap image_gray = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit0 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit1 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit2 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit3 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit4 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit5 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit6 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Bitmap image_bit7 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                    Color watermark_Color = Color.FromArgb(0,0,0);
                   
                    // Loop through the images pixels to reset color.               
                    int bitplane;
                    int watermarkbit = 0;
                    for (int x = 0; x < image3.Width; x++)
                    {
                        for (int y = 0; y < image3.Height; y++)
                        {
                            Color pixelColor = image3.GetPixel(x, y);
                            int gray_color = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                            Color newColor = Color.FromArgb(gray_color, gray_color, gray_color);
                            image_gray.SetPixel(x, y, newColor);

                            if (garycode == true) { gray_color = binaryToGray(gray_color); }       // binaryToGray                                                                     
                            int origin_add_watermark;

                            // 浮水印轉灰階
                            if (x < image2.getBitmap.Width && y < image2.getBitmap.Height)
                            {
                                watermark_Color = image2.getBitmap.GetPixel(x, y);
                            
                            int watermark_gray = Convert.ToInt32(0.3 * watermark_Color.R + 0.3 * watermark_Color.G + 0.4 * watermark_Color.B);
                            //灰階分黑白
                            if (watermark_gray < 128) { watermarkbit = 0; }
                            else { watermarkbit = 255; }
                            ////
                            Color watermarkbit7 = Color.FromArgb(watermarkbit, watermarkbit, watermarkbit);
                            image_watermarkorigin.SetPixel(x, y, watermarkbit7);
                            watermarkbit = (watermark_gray >> 7) % 2; // 還原watermarkbit 1 or 0
                            }
                            //
                            bitplane = gray_color % 2;
                            origin_add_watermark = gray_color - bitplane + watermarkbit;
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            Color bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);
                            image_bit0.SetPixel(x, y, bitlevel);
                            //                      
                            bitplane = (gray_color >> 1) % 2;
                            origin_add_watermark = gray_color - (bitplane << 1) + (watermarkbit << 1);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);
                            image_bit1.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 2) % 2;
                            origin_add_watermark = gray_color - (bitplane << 2) + (watermarkbit << 2);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);
                            image_bit2.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 3) % 2;
                            origin_add_watermark = gray_color - (bitplane << 3) + (watermarkbit << 3);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);                          
                            image_bit3.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 4) % 2;
                            origin_add_watermark = gray_color - (bitplane << 4) + (watermarkbit << 4);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);
                            image_bit4.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 5) % 2;
                            origin_add_watermark = gray_color - (bitplane << 5) + (watermarkbit << 5);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);                            
                            image_bit5.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 6) % 2;
                            origin_add_watermark = gray_color - (bitplane << 6) + (watermarkbit << 6);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);
                            image_bit6.SetPixel(x, y, bitlevel);
                            //
                            bitplane = (gray_color >> 7) % 2;
                            origin_add_watermark = gray_color - (bitplane << 7) + (watermarkbit << 7);
                            if (garycode == true) { origin_add_watermark = grayToBinary(origin_add_watermark); }
                            bitlevel = Color.FromArgb(origin_add_watermark, origin_add_watermark, origin_add_watermark);                          
                            image_bit7.SetPixel(x, y, bitlevel);
                            //                          
                        }
                    }

                    calculateSNR(image_gray, image_bit0);
                    calculateSNR(image_gray, image_bit1);
                    calculateSNR(image_gray, image_bit2);
                    calculateSNR(image_gray, image_bit3);
                    calculateSNR(image_gray, image_bit4);
                    calculateSNR(image_gray, image_bit5);
                    calculateSNR(image_gray, image_bit6);
                    calculateSNR(image_gray, image_bit7);
                    
                    label29.Text = "SNR：" + calculateSNR(image_gray, image_bit0).ToString() + "(dB)";
                    label30.Text = "SNR：" + calculateSNR(image_gray, image_bit1).ToString() + "(dB)";
                    label31.Text = "SNR：" + calculateSNR(image_gray, image_bit2).ToString() + "(dB)";
                    label32.Text = "SNR：" + calculateSNR(image_gray, image_bit3).ToString() + "(dB)";
                    label33.Text = "SNR：" + calculateSNR(image_gray, image_bit4).ToString() + "(dB)";
                    label34.Text = "SNR：" + calculateSNR(image_gray, image_bit5).ToString() + "(dB)";
                    label35.Text = "SNR：" + calculateSNR(image_gray, image_bit6).ToString() + "(dB)";
                    label36.Text = "SNR：" + calculateSNR(image_gray, image_bit7).ToString() + "(dB)";

                                  
                    // Set the PictureBox to display the image.
                    initial_controltool();
                    panel_bitplane.Visible = true;
                    label_watermark.Visible = true;
                    button_watermarkslice.Enabled = true;
                    button_Watermark.Enabled = true;
                    pictureBox_bit0.Image = image_bit0;
                    pictureBox_bit1.Image = image_bit1;
                    pictureBox_bit2.Image = image_bit2;
                    pictureBox_bit3.Image = image_bit3;
                    pictureBox_bit4.Image = image_bit4;
                    pictureBox_bit5.Image = image_bit5;
                    pictureBox_bit6.Image = image_bit6;
                    pictureBox_bit7.Image = image_bit7;
                    image_watermarkslicebit0 = image_bit0;
                    image_watermarkslicebit1 = image_bit1;
                    image_watermarkslicebit2 = image_bit2;
                    image_watermarkslicebit3 = image_bit3;
                    image_watermarkslicebit4 = image_bit4;
                    image_watermarkslicebit5 = image_bit5;
                    image_watermarkslicebit6 = image_bit6;
                    image_watermarkslicebit7 = image_bit7;
                    pictureBox_bitgray.Image = image_gray;
                    pictureBox_watermark.Image = image_watermarkorigin;
                    label29.Visible = true;
                    label30.Visible = true;
                    label31.Visible = true;
                    label32.Visible = true;
                    label33.Visible = true;
                    label34.Visible = true;
                    label35.Visible = true;
                    label36.Visible = true;
                }
            }
            int binaryToGray(int num)
            {
                return (num >> 1) ^ num;
            }
            int grayToBinary(int num)
            {
                int temp = num ^ (num >> 8);
                temp ^= (temp >> 4);
                temp ^= (temp >> 2);
                temp ^= (temp >> 1);
                return temp;
            }          
        }

        //SNR
        double calculateSNR(Bitmap origin, Bitmap newimage)
        {
            double SNR;
            long square = 0;
            long  sigma = 0;

            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    Color pixelcolor = origin.GetPixel(x, y);
                    double original_signalR = pixelcolor.R;
                    double original_signalG = pixelcolor.G;
                    double original_signalB = pixelcolor.B;
                    Color pixelcolor_watermark = newimage.GetPixel(x, y);
                    double processed_signalR = pixelcolor_watermark.R;
                    double processed_signalG = pixelcolor_watermark.G;
                    double processed_signalB = pixelcolor_watermark.B;

                    square += Convert.ToInt64(Math.Pow(original_signalR, 2)+ Math.Pow(original_signalG, 2)+ Math.Pow(original_signalB, 2));
                    sigma += Convert.ToInt64(Math.Pow(original_signalR - processed_signalR, 2)+ Math.Pow(original_signalG - processed_signalG, 2)+ Math.Pow(original_signalB - processed_signalB, 2));                  
                }
            }         
            
            SNR = 10 * Math.Log(square / sigma, 10);
            return SNR;
        }

        //PSNR
        double calculatePSNR(Bitmap origin, Bitmap newimage)
        {
            double PSNR;
            long square;
            long sigma = 0;
            square = origin.Width * origin.Height * Convert.ToInt64(Math.Pow(255, 2) * 3);
            for (int x = 0; x < origin.Width; x++)
            {
                for (int y = 0; y < origin.Height; y++)
                {
                    Color pixelcolor = origin.GetPixel(x, y);
                    double original_signalR = pixelcolor.R;
                    double original_signalG = pixelcolor.G;
                    double original_signalB = pixelcolor.B;
                    Color pixelcolor_watermark = newimage.GetPixel(x, y);
                    double processed_signalR = pixelcolor_watermark.R;
                    double processed_signalG = pixelcolor_watermark.G;
                    double processed_signalB = pixelcolor_watermark.B;
                   
                    sigma += Convert.ToInt64(Math.Pow(original_signalR - processed_signalR, 2) + Math.Pow(original_signalG - processed_signalG, 2) + Math.Pow(original_signalB - processed_signalB, 2));
                }
            }

            PSNR = 10 * Math.Log(square / sigma, 10);
            return PSNR;
        }
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        //for dynamic change color function
        int[] dynamic = new int[256];
        Boolean[] endpoint = new Boolean[256];
    
        private void chart_dynamic_Click(object sender, EventArgs e)
        {                               
        }

        //Dynamic change color
        void dynamic_MouseDown(object senderDown, MouseEventArgs eDown)
        {                              
            ChartArea ca = chart_dynamic.ChartAreas[0];
            Axis ax = ca.AxisX;
            Axis ay = ca.AxisY;
            Bitmap image_dynamic = new Bitmap(image_dynamictemp);
            double r_l = 0;
            double r_r = 0;
            int x = Convert.ToInt32(ax.PixelPositionToValue(eDown.X));
            int y = Convert.ToInt32(ay.PixelPositionToValue(eDown.Y));
            if (x >= 0 && x <= 255 && y >= 0 && y <= 255)
            {
                chart_dynamic.Series[0].Points.Clear();
                chart_dynamic.Series[0].Points.AddXY(0, 0);
                endpoint[0] = true;
                endpoint[255] = true;
                int x_l = x - 1;
                int x_r = x + 1;
                dynamic[x] = y;  //點擊處x,y
                endpoint[x] = true;
                if (x_l >= 0 && x_r <= 255)  //點擊處在1~254之間
                {
                    while (endpoint[x_l] == false) { x_l--; }  //找左右端點               
                    while (endpoint[x_r] == false) { x_r++; }
                    r_l = (Convert.ToDouble(y) - Convert.ToDouble(dynamic[x_l])) / (Convert.ToDouble(x) - Convert.ToDouble(x_l)); //左斜率           
                    r_r = (Convert.ToDouble(dynamic[x_r]) - Convert.ToDouble(y)) / (Convert.ToDouble(x_r) - Convert.ToDouble(x)); //右斜率
                    for (int n = x_l; n <= x; n++)
                    {
                        dynamic[n] = Convert.ToInt32(r_l * (n - x_l) + dynamic[x_l]);
                    }
                    for (int n = x; n <= x_r; n++)
                    {
                        dynamic[n] = Convert.ToInt32(r_r * (n - x) + dynamic[x]);
                    }                 
                }
                else if (x == 0) //點擊處在0(最左)
                {                   
                    while (endpoint[x_r] == false) { x_r++; }
                    r_r = (Convert.ToDouble(dynamic[x_r]) - Convert.ToDouble(y)) / (Convert.ToDouble(x_r) - Convert.ToDouble(x));
                    for (int n = x; n <= x_r; n++)
                    {
                        dynamic[n] = Convert.ToInt32(r_r * (n - x) + dynamic[x]);
                    }
                }
                else if (x == 255) //點擊處在255(最右)
                {                 
                    while (endpoint[x_l] == false) { x_l--; }
                    r_l = (Convert.ToDouble(y) - Convert.ToDouble(dynamic[x_l])) / (Convert.ToDouble(x) - Convert.ToDouble(x_l));
                    for (int n = x_l; n <= x; n++)
                    {
                        dynamic[n] = Convert.ToInt32(r_l * (n - x_l) + dynamic[x_l]);
                    }
                }
                else return;
                for (int n = 0; n < 256; n++)
                {
                    chart_dynamic.Series[0].Points.AddXY(n, dynamic[n]);
                }
                for (int m = 0; m < image_dynamic.Width; m++) 
                {
                    for (int n = 0; n < image_dynamic.Height; n++) 
                    {
                        Color pixelColor = image_dynamic.GetPixel(m, n);
                        int dynamic_color = Convert.ToInt32(pixelColor.R);
                        dynamic_color = dynamic[dynamic_color];
                        Color newColor = Color.FromArgb(dynamic_color, dynamic_color, dynamic_color);
                        image_dynamic.SetPixel(m, n, newColor);
                    }
                }
                pictureBox_dynamic.Image = image_dynamic;
            }
            else return;
        }

        private void button_watermarkslice_Click(object sender, EventArgs e)
        {
            comboBox_watermark.Visible = true;
            comboBox_watermark.SelectedIndex = 0;        
        }

        //Add watermark at Bitplane and represent watermark
        private void comboBox_watermark_SelectedIndexChanged(object sender, EventArgs e)
        {                      
                Bitmap image_gray = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit0 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit1 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit2 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit3 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit4 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit5 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit6 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);
                Bitmap image_bit7 = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);

                int selectedIndex = comboBox_watermark.SelectedIndex;

                int bitplane;

                switch (selectedIndex)
                {
                    case 0:
                        image_watermarkslice = image_watermarkslicebit0;
                        break;
                    case 1:
                        image_watermarkslice = image_watermarkslicebit1;
                        break;
                    case 2:
                        image_watermarkslice = image_watermarkslicebit2;
                        break;
                    case 3:
                        image_watermarkslice = image_watermarkslicebit3;
                        break;
                    case 4:
                        image_watermarkslice = image_watermarkslicebit4;
                        break;
                    case 5:
                        image_watermarkslice = image_watermarkslicebit5;
                        break;
                    case 6:
                        image_watermarkslice = image_watermarkslicebit6;
                        break;
                    case 7:
                        image_watermarkslice = image_watermarkslicebit7;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                for (int x = 0; x < image_watermarkslice.Width; x++)
                {
                    for (int y = 0; y < image_watermarkslice.Height; y++)
                    {
                        Color pixelColor = image_watermarkslice.GetPixel(x, y);
                        int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        /*Color newColor = Color.FromArgb(gray, gray, gray);*/
                        image_gray.SetPixel(x, y, pixelColor);
                        if (garycode == true) { gray = binaryToGray(gray); }
                        //
                        bitplane = gray % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        Color bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit0.SetPixel(x, y, bitlevel);
                        //                      
                        bitplane = (gray >> 1) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit1.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 2) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit2.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 3) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit3.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 4) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit4.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 5) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit5.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 6) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit6.SetPixel(x, y, bitlevel);
                        //
                        bitplane = (gray >> 7) % 2;
                        if (bitplane == 0) { bitplane = 0; }
                        else { bitplane = 255; }
                        bitlevel = Color.FromArgb(bitplane, bitplane, bitplane);
                        image_bit7.SetPixel(x, y, bitlevel);
                        //
                    }
                }
                // Set the PictureBox to display the image.
                initial_controltool();
                panel_bitplane.Visible = true;
                button_Watermark.Enabled = true;
                button_watermarkslice.Enabled = true;
                pictureBox_bit0.Image = image_bit0;
                pictureBox_bit1.Image = image_bit1;
                pictureBox_bit2.Image = image_bit2;
                pictureBox_bit3.Image = image_bit3;
                pictureBox_bit4.Image = image_bit4;
                pictureBox_bit5.Image = image_bit5;
                pictureBox_bit6.Image = image_bit6;
                pictureBox_bit7.Image = image_bit7;
                pictureBox_bitgray.Image = image_gray;
                pictureBox_watermark.Image = image_watermarkorigin;
                label_watermark.Visible = true;
                comboBox_watermark.Visible = true;
            
            int binaryToGray(int num)
            {
                return (num >> 1) ^ num;
            }
        }

        //直方圖均衡specification(以別圖分布來調整原圖分布)
        private void button_specification_Click(object sender, EventArgs e)
        {         
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,//該值確定是否可以選擇多個檔案
                Title = "請選擇資料夾",
                Filter = "所有檔案(*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = dialog.FileName;
                // Retrieve the image.
                image2 = new Class1.readPic(FileName);
            }
            Bitmap image_origin = new Bitmap(image1.getBitmap);
            Bitmap image_addpic = new Bitmap(image2.getBitmap);
            Bitmap image_specification = new Bitmap(image_origin);
            chart_Histogrambef.Series[0].Points.Clear();
            chart_Histogrambef.Series[0].Points.AddXY(0, 0);
            chart_Histogrambef.Series[1].Points.Clear();
            chart_Histogrambef.Series[1].Points.AddXY(0, 0);
            chart_Histogrambef.Series[2].Points.Clear();
            chart_Histogrambef.Series[2].Points.AddXY(0, 0);
            chart_Histogrambef.Series[3].Points.Clear();
            chart_Histogrambef.Series[3].Points.AddXY(0, 0);
            chart_Histogrambef.Series[4].Points.Clear();
            chart_Histogrambef.Series[4].Points.AddXY(0, 0);
            chart_Histogrambef.Series[5].Points.Clear();
            chart_Histogrambef.Series[5].Points.AddXY(0, 0);
            chart_Histogrambef.Series[6].Points.Clear();
            chart_Histogrambef.Series[6].Points.AddXY(0, 0);
            chart_Histogrambef.Series[7].Points.Clear();
            chart_Histogrambef.Series[7].Points.AddXY(0, 0);

            chart_Histogram_other.Series[0].Points.Clear();
            chart_Histogram_other.Series[0].Points.AddXY(0, 0);
            chart_Histogram_other.Series[1].Points.Clear();
            chart_Histogram_other.Series[1].Points.AddXY(0, 0);
            chart_Histogram_other.Series[2].Points.Clear();
            chart_Histogram_other.Series[2].Points.AddXY(0, 0);
            chart_Histogram_other.Series[3].Points.Clear();
            chart_Histogram_other.Series[3].Points.AddXY(0, 0);
            chart_Histogram_other.Series[4].Points.Clear();
            chart_Histogram_other.Series[4].Points.AddXY(0, 0);
            chart_Histogram_other.Series[5].Points.Clear();
            chart_Histogram_other.Series[5].Points.AddXY(0, 0);
            chart_Histogram_other.Series[6].Points.Clear();
            chart_Histogram_other.Series[6].Points.AddXY(0, 0);
            chart_Histogram_other.Series[7].Points.Clear();
            chart_Histogram_other.Series[7].Points.AddXY(0, 0);
            //
            chart_Histogram_other.Visible = true;
            pictureBox_Histogram_other.Visible = true;
            checkedListBox_Histogram_other.Visible = false;
            checkedListBox_Histogram.Visible = false;
            checkedListBox_Histogrambef.Visible = false;
            //gray
            for (int x = 0; x < image_origin.Width; x++)
            {
                for (int y = 0; y < image_origin.Height; y++)
                {
                    Color pixelColor = image_origin.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_origin.SetPixel(x, y, newColor);
                }
            }
            for (int x = 0; x < image_addpic.Width; x++)
            {
                for (int y = 0; y < image_addpic.Height; y++)
                {
                    //pic2
                    Color pixelColor = image_addpic.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    image_addpic.SetPixel(x, y, newColor);
                }
            }
            pictureBox_Histogrambef.Image = image_origin;
            pictureBox_Histogram_other.Image = image_addpic;
            //histogram
            double[] dataGray = new double[256];
            double[] countGray = new double[256];
            double[] dataGrayadd = new double[256];
            double[] countGrayadd = new double[256];
            double totalpixel = image_origin.Width * image_origin.Height;
            for (int x = 0; x < image_origin.Width; x++)
            {
                for (int y = 0; y < image_origin.Height; y++)
                {
                    Color pixelColor = image_origin.GetPixel(x, y);
                    dataGray[pixelColor.G]++;                  
                }
            }
            for (int x = 0; x < image_addpic.Width; x++)
            {
                for (int y = 0; y < image_addpic.Height; y++)
                {                   
                    Color pixelColor = image_addpic.GetPixel(x, y);
                    dataGrayadd[pixelColor.G]++;
                }
            }

            for (int t = 0; t < 256; t++)
            {
                chart_Histogrambef.Series[6].Points.AddXY(t, dataGray[t] / totalpixel);
                chart_Histogram_other.Series[6].Points.AddXY(t, dataGrayadd[t] / totalpixel);
            }
            panel_Histogram.Visible = true;
            countGray[0] = dataGray[0];
            countGrayadd[0] = dataGrayadd[0];
            for (int t = 1; t < 256; t++)
            {
                //累積
                countGray[t] = countGray[t - 1] + dataGray[t];

                countGrayadd[t] = countGrayadd[t - 1] + dataGrayadd[t];
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countGray[t] = countGray[t] / totalpixel;
                chart_Histogrambef.Series[7].Points.AddXY(t, countGray[t]);

                countGrayadd[t] = countGrayadd[t] / totalpixel;
                chart_Histogram_other.Series[7].Points.AddXY(t, countGrayadd[t]);
            }
            for (int x = 0; x < image_origin.Width; x++)
            {
                for (int y = 0; y < image_origin.Height; y++)
                {
                    Color pixelColororigin = image_origin.GetPixel(x, y);
                   
                    int temp = Convert.ToInt32(countGray[pixelColororigin.R] * 255);
                    int mapping_color = 0;
                    int minvalue = Math.Abs(temp - Convert.ToInt32(countGrayadd[0]));
                    for (int t = 0; t < 256; t++) 
                    {                      
                        if (Math.Abs(temp - Convert.ToInt32(countGrayadd[t] * 255)) < minvalue) 
                        {
                            minvalue = Math.Abs(temp - Convert.ToInt32(countGrayadd[t] * 255));
                            mapping_color = t;
                        }
                    }                   
                    Color color_spec = Color.FromArgb(mapping_color, mapping_color, mapping_color);
                    image_specification.SetPixel(x, y, color_spec);
                }
            }
            pictureBox_Histogram.Image = image_specification;
            //Histogram_ chart
            double[] dataGrayspec = new double[256];
            double[] countGrayspec = new double[256];
            label17.Text = "Specification";
            label18.Text = "原圖灰階";
            label19.Text = "Reference Pic";
            for (int x = 0; x < image_specification.Width; x++)
            {
                for (int y = 0; y < image_specification.Height; y++)
                {
                    Color pixelColor = image_specification.GetPixel(x, y);
                    dataGrayspec[pixelColor.G]++;
                }
            }
            chart_Histogram.Series[0].Points.Clear();  //DataR
            chart_Histogram.Series[0].Points.AddXY(0, 0);
            chart_Histogram.Series[1].Points.Clear();  //DataG
            chart_Histogram.Series[1].Points.AddXY(0, 0);
            chart_Histogram.Series[2].Points.Clear();  //DataB
            chart_Histogram.Series[2].Points.AddXY(0, 0);
            chart_Histogram.Series[6].Points.Clear();  //DataGray
            chart_Histogram.Series[6].Points.AddXY(0, 0);
            for (int t = 0; t < 256; t++)
            {
                chart_Histogram.Series[6].Points.AddXY(t, dataGrayspec[t] / totalpixel);
            }
            //chart_Histogram.Visible = true;         
            chart_Histogram.Series[3].Points.Clear();  //CountR
            chart_Histogram.Series[3].Points.AddXY(0, 0);
            chart_Histogram.Series[4].Points.Clear();  //CountG
            chart_Histogram.Series[4].Points.AddXY(0, 0);
            chart_Histogram.Series[5].Points.Clear();  //CountB
            chart_Histogram.Series[5].Points.AddXY(0, 0);
            chart_Histogram.Series[7].Points.Clear();  //CountGray
            chart_Histogram.Series[7].Points.AddXY(0, 0);
            countGrayspec[0] = dataGrayspec[0];
            for (int t = 1; t < 256; t++)
            {
                //累積
                countGrayspec[t] = countGrayspec[t - 1] + dataGrayspec[t];
            }
            for (int t = 0; t < 256; t++)
            {
                //累積機率
                countGrayspec[t] = countGrayspec[t] / totalpixel;
                chart_Histogram.Series[7].Points.AddXY(t, countGrayspec[t]);
            }
        }
    
        void Add_Noise(int type)
        {
            //Pepper_Noise type=0,  Gaussian_Noise type=1
            if (image3 == null) { image3 = new Bitmap(image1.getBitmap); }
            else if (image_forscale != null) { image3 = new Bitmap(image_forscale); image_forscale = null; }           
            image_noise1 = new Bitmap(image1.getBitmap);
            var rand = new Random(Guid.NewGuid().GetHashCode());
            //Pepper_Noise
            if (type == 0)
            {
                double probability = Convert.ToDouble(numericUpDown_noiseprobability.Value) / 200;
                for (int i=0; i< image_noise1.Width;i++) 
                {
                    for (int j = 0; j < image_noise1.Height; j++)
                    {                                 
                        if (rand.NextDouble() < probability)
                        {
                            Color newColor = Color.FromArgb(0, 0, 0);
                            image_noise1.SetPixel(i, j, newColor);
                        }
                        else if (rand.NextDouble() > 1-probability) 
                        {
                            Color newColor = Color.FromArgb(255, 255, 255);
                            image_noise1.SetPixel(i, j, newColor);
                        }
                            
                    }
                }             
            }
            // Gaussian_Noise
            else if(type == 1) 
            {
                double mean;//期望值
                double std;//標準差
                double u, v;//均勻分布數值
                double X;//常態分佈數值
                mean = Convert.ToDouble(numericUpDown_mean.Value);
                std = Convert.ToDouble(numericUpDown_std.Value);
                for (int i = 0; i < image_noise1.Width; i++)
                {
                    for (int j = 0; j < image_noise1.Height; j++)
                    {                       
                        u = rand.NextDouble();                       
                        v = rand.NextDouble();                      
                        X = Math.Sqrt(-2.0 * Math.Log(u)) * Math.Cos(2.0 * Math.PI * v) * std + mean;      //Box-Muller方法               
                        int newColorR = Convert.ToInt32(image_noise1.GetPixel(i, j).R + X);
                        int newColorG = Convert.ToInt32(image_noise1.GetPixel(i, j).G + X);
                        int newColorB = Convert.ToInt32(image_noise1.GetPixel(i, j).B + X);                      
                        if (newColorR > 255) newColorR = 255;
                        else if(newColorR < 0) newColorR = 0;
                        if (newColorG > 255) newColorG = 255;
                        else if (newColorG < 0) newColorG = 0;
                        if (newColorB > 255) newColorB = 255;
                        else if (newColorB < 0) newColorB = 0;
                        Color newColor = Color.FromArgb(newColorR, newColorG, newColorB);
                        image_noise1.SetPixel(i, j, newColor);
                    }
                }
            }
            pictureBox_filter_left.Image = image_noise1;
        }

        //Generate Noise
        private void comboBox_noise_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox_noise.SelectedIndex;
            Add_Noise(selectedIndex);
            if (selectedIndex==0) 
            {
                numericUpDown_noiseprobability.Visible = true;
                numericUpDown_mean.Visible = false;
                numericUpDown_std.Visible = false;
                label_noiseprob.Visible = true;               
                label_gaussmean.Visible = false;
                label_gaussstd.Visible = false;
            }
            else
            {
                numericUpDown_noiseprobability.Visible = false;
                numericUpDown_mean.Visible = true;
                numericUpDown_std.Visible = true;
                label_noiseprob.Visible = false;
                label_gaussmean.Visible = true;
                label_gaussstd.Visible = true;
            }
            panel_filter.Visible = true;
        }
        private void numericUpDown_noiseprobability_ValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox_noise.SelectedIndex;
            Add_Noise(selectedIndex);
        }

        private void numericUpDown_mean_ValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox_noise.SelectedIndex;
            Add_Noise(selectedIndex);
        }

        private void numericUpDown_std_ValueChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox_noise.SelectedIndex;
            Add_Noise(selectedIndex);
        }


        ////Filter
        private void comboBox_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox_filter.SelectedIndex;          
            switch (selectedIndex)
            {
                case 0:
                    numericUpDown_threshold.Value = 200;
                    Outlier(Convert.ToInt32(numericUpDown_threshold.Value));
                    label_outlierthreshold.Visible = true;
                    numericUpDown_threshold.Visible = true;
                    label_outlierthreshold.Text = "Threshold";
                    label_masksize.Visible = false;
                    comboBox_mask.Visible = false;
                    label41.Text = "雜訊";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 1:
                    comboBox_mask.SelectedIndex = 0;
                    lowpass(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "雜訊";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 2:
                    comboBox_mask.SelectedIndex = 0;
                    Median_Square(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "雜訊";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 3:
                    comboBox_mask.SelectedIndex = 0;
                    Median_Cross(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "雜訊";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 4:
                    comboBox_mask.SelectedIndex = 0;
                    highpass(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "原圖";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 5:
                    comboBox_mask.SelectedIndex = 0;
                    crispening_Square(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "原圖";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 6:
                    comboBox_mask.SelectedIndex = 0;
                    crispening_Cross(comboBox_mask.SelectedIndex);
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "原圖";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 7:
                    comboBox_mask.SelectedIndex = 0;
                    numericUpDown_threshold.Value = 100;
                    high_boost(comboBox_mask.SelectedIndex, (Convert.ToInt32(numericUpDown_threshold.Value)));
                    label_outlierthreshold.Visible = true;                 
                    numericUpDown_threshold.Visible = true;
                    label_outlierthreshold.Text = "Factor(%)";
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "原圖";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                case 8:
                    comboBox_mask.SelectedIndex = 0;
                    robert();
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_outlierthreshold.Text = "Factor(%)";
                    label_masksize.Visible = false;
                    comboBox_mask.Visible = false;
                    label41.Visible = false;
                    label42.Visible = false;
                    labelfilter_snr.Visible = false;
                    break;
                case 9:
                    comboBox_mask.SelectedIndex = 0;
                    Prewitt();
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_outlierthreshold.Text = "Factor(%)";
                    label_masksize.Visible = false;
                    comboBox_mask.Visible = false;
                    label41.Text = "水平邊緣";
                    label42.Text = "垂直邊緣";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = false;
                    break;
                case 10:
                    comboBox_mask.SelectedIndex = 0;
                    sobel();
                    label_outlierthreshold.Visible = false;
                    numericUpDown_threshold.Visible = false;
                    label_outlierthreshold.Text = "Factor(%)";
                    label_masksize.Visible = false;
                    comboBox_mask.Visible = false;
                    label41.Text = "水平邊緣";
                    label42.Text = "垂直邊緣";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = false;
                    break;
                case 11:
                    comboBox_mask.SelectedIndex = 0;
                    numericUpDown_threshold.Value = 80;
                    Gaussian(comboBox_mask.SelectedIndex, Convert.ToInt32(numericUpDown_threshold.Value));
                    label_outlierthreshold.Visible = true;
                    numericUpDown_threshold.Visible = true;
                    label_outlierthreshold.Text = "SD(%)";
                    label_masksize.Visible = true;
                    comboBox_mask.Visible = true;
                    label41.Text = "原圖";
                    label42.Text = "處理後";
                    label41.Visible = true;
                    label42.Visible = true;
                    labelfilter_snr.Visible = true;
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
        void Outlier(int threshold) 
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            for (int i = 0; i < image_filter.Width; i++)
            {
                for (int j = 0; j < image_filter.Height; j++)
                {
                    Color pixelColor = image_noise1.GetPixel(i, j);
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -1; t < 2; t++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            if (t != 0 || k != 0)
                            {
                                if (i + t < 0) { m = i + t + image_filter.Width; }
                                else {m = (i + t)% image_filter.Width; }
                                if (j + k < 0) { n = j + k + image_filter.Height; }
                                else { n = (j + k) % image_filter.Height; }
                                Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                r = r+ (neiborhoodpixelColor.R / 8);
                                g = g+ (neiborhoodpixelColor.G / 8);
                                b = b+ (neiborhoodpixelColor.B / 8);
                            }
                        }
                    }                   
                    if (Math.Abs(pixelColor.R - r) < threshold && Math.Abs(pixelColor.G - g) < threshold && Math.Abs(pixelColor.B - b) < threshold) 
                    { r = pixelColor.R; g = pixelColor.G; b = pixelColor.B; } //比閾值小，用原本的顏色                  
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }                    
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void lowpass(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            mask = 2*mask + 3;             
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    Color pixelColor = image_noise1.GetPixel(i, j);
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -(mask/2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {                                                  
                                if (i + t < 0) { m = i + t + image_noise1.Width; }
                                else { m = (i + t) % image_noise1.Width; }
                                if (j + k < 0) { n = j + k + image_noise1.Height; }
                                else { n = (j + k) % image_noise1.Height; }
                                Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);                           
                                r = r + (neiborhoodpixelColor.R / (mask * mask));
                                g = g + (neiborhoodpixelColor.G / (mask * mask));
                                b = b + (neiborhoodpixelColor.B / (mask * mask));                        
                           // Console.WriteLine(neiborhoodpixelColor.R / (mask * mask));
                        }
                    }                
                    Color newColor = Color.FromArgb(r, g, b);                   
                    image_filter.SetPixel(i, j, newColor);                  
                }
            }
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void Median_Square(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            mask = 2 * mask + 3;
            int[] RArray = new int[mask * mask];
            int[] GArray = new int[mask * mask];
            int[] BArray = new int[mask * mask];
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {                                    
                    int m, n,r,g,b;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            m = t + (mask / 2);
                            n = k + (mask / 2);
                            RArray[m * mask + n] = neiborhoodpixelColor.R;
                            GArray[m * mask + n] = neiborhoodpixelColor.G;
                            BArray[m * mask + n] = neiborhoodpixelColor.B;
                        }
                    }
                   r = Median_sort(RArray);
                   g = Median_sort(GArray);
                   b = Median_sort(BArray);
                   Color newColor = Color.FromArgb(r, g, b);
                   image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void Median_Cross(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            mask = 2 * mask + 3;
            int[] RArray = new int[mask * 2 -1];
            int[] GArray = new int[mask * 2 -1];
            int[] BArray = new int[mask * 2 -1];
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {                   
                    int m, n, r, g, b;
                    int index = 0;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        if (i + t < 0) { m = i + t + image_noise1.Width; }
                        else { m = (i + t) % image_noise1.Width; }
                        if (j + t < 0) { n = j + t + image_noise1.Height; }
                        else { n = (j + t) % image_noise1.Height; }
                        Color neiborhoodpixelColor = image_noise1.GetPixel(m, j);
                        RArray[index] = neiborhoodpixelColor.R;
                        GArray[index] = neiborhoodpixelColor.G;
                        BArray[index] = neiborhoodpixelColor.B;
                        index++;
                        if (t != 0)//避免重複中心點
                        {
                            neiborhoodpixelColor = image_noise1.GetPixel(i, n);
                            RArray[index] = neiborhoodpixelColor.R;
                            GArray[index] = neiborhoodpixelColor.G;
                            BArray[index] = neiborhoodpixelColor.B;
                            index++;
                        }
                    }
                    r = Median_sort(RArray);
                    g = Median_sort(GArray);
                    b = Median_sort(BArray);
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        int Median_sort(int[] array)
        {
            int count = array.Length;

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (array[j + 1] < array[j])
                    {
                        int temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array[count/2];
        }
        void highpass(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            mask = 2 * mask + 3;
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    Color pixelColor = image_noise1.GetPixel(i, j);
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            if (t ==0 && k == 0)
                            {
                                r = r + ((mask * mask) -1) * neiborhoodpixelColor.R;
                                g = g + ((mask * mask) - 1) * neiborhoodpixelColor.G;
                                b = b + ((mask * mask) - 1) * neiborhoodpixelColor.B;
                            }
                            else
                            {
                                r = r + -neiborhoodpixelColor.R;
                                g = g + -neiborhoodpixelColor.G;
                                b = b + -neiborhoodpixelColor.B;
                            }                          
                        }
                    }
                    r = r / (mask * mask);
                    g = g / (mask * mask);
                    b = b / (mask * mask);
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
            Console.WriteLine(calculateSNR(image_noise1, image_filter));
        }
        void crispening_Square(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);         
            mask = 2 * mask + 3;
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {                   
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            if (t == 0 && k == 0)
                            {
                                r = r + (mask * mask) * neiborhoodpixelColor.R;
                                g = g + (mask * mask) * neiborhoodpixelColor.G;
                                b = b + (mask * mask) * neiborhoodpixelColor.B;
                            }
                            else
                            {
                                r = r + -neiborhoodpixelColor.R;
                                g = g + -neiborhoodpixelColor.G;
                                b = b + -neiborhoodpixelColor.B;
                            }
                        }
                    }
                    r = r / (mask * mask);
                    g = g / (mask * mask);
                    b = b / (mask * mask);
                    Color pixelColor = image_noise1.GetPixel(i, j);
                    r = r + pixelColor.R;
                    g = g + pixelColor.G;
                    b = b + pixelColor.B;
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_right.Image = image_filter;
            pictureBox_filter_left.Image = image_noise1;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void crispening_Cross(int mask)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
           
            mask = 2 * mask + 3;           
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        if (i + t < 0) { m = i + t + image_noise1.Width; }
                        else { m = (i + t) % image_noise1.Width; }
                        if (j + t < 0) { n = j + t + image_noise1.Height; }
                        else { n = (j + t) % image_noise1.Height; }
                        Color neiborhoodpixelColor;
                        if (t != 0)
                        {
                            neiborhoodpixelColor = image_noise1.GetPixel(m, j);
                            r = r + (-neiborhoodpixelColor.R);
                            g = g + (-neiborhoodpixelColor.G);
                            b = b + (-neiborhoodpixelColor.B);
                            neiborhoodpixelColor = image_noise1.GetPixel(i, n);
                            r = r + (-neiborhoodpixelColor.R);
                            g = g + (-neiborhoodpixelColor.G);
                            b = b + (-neiborhoodpixelColor.B);
                        }
                        else 
                        {
                            neiborhoodpixelColor = image_noise1.GetPixel(i, j);
                            r = r + (mask * 2 - 1) * neiborhoodpixelColor.R;
                            g = g + (mask * 2 - 1) * neiborhoodpixelColor.G;
                            b = b + (mask * 2 - 1) * neiborhoodpixelColor.B;
                        }                      
                    }
                    r = r / (mask * 2-1);
                    g = g / (mask * 2-1);
                    b = b / (mask * 2-1);

                    Color pixelColor = image_noise1.GetPixel(i, j);
                    r = r + pixelColor.R;
                    g = g + pixelColor.G;
                    b = b + pixelColor.B;
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;                
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_right.Image = image_filter;
            pictureBox_filter_left.Image = image_noise1;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void high_boost(int mask, double a)
        {
            a =a/100;
            Bitmap image_filter = new Bitmap(image_noise1);            
            mask = 2 * mask + 3;
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            if (t == 0 && k == 0)
                            {
                                r = Convert.ToInt32(r + (mask * mask * a - 1) * neiborhoodpixelColor.R);
                                g = Convert.ToInt32(g + (mask * mask * a - 1) * neiborhoodpixelColor.G);
                                b = Convert.ToInt32(b + (mask * mask * a - 1) * neiborhoodpixelColor.B);
                            }
                            else
                            {
                                r = r + -neiborhoodpixelColor.R;
                                g = g + -neiborhoodpixelColor.G;
                                b = b + -neiborhoodpixelColor.B;
                            }
                        }
                    }
                    r = r / (mask * mask);
                    g = g / (mask * mask);
                    b = b / (mask * mask);
                    if (r < 0) r = 0;
                    else if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    else if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    else if (b > 255) b = 255;
                    
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_right.Image = image_filter;
            pictureBox_filter_left.Image = image_noise1;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        void robert()
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            for (int i = 0; i < image_filter.Width; i++)
            {
                for (int j = 0; j < image_filter.Height; j++)
                {
                    Color pixelColor = image_noise1.GetPixel(i, j);
                    int xr = 0;
                    int xg = 0;
                    int xb = 0;
                    int yr = 0;
                    int yg = 0;
                    int yb = 0;
                    int m, n;
                    Color neiborhoodpixelColor = image_noise1.GetPixel(i, j);
                    xr += -neiborhoodpixelColor.R;
                    xg += -neiborhoodpixelColor.G;
                    xb += -neiborhoodpixelColor.B;
                    neiborhoodpixelColor = image_noise1.GetPixel((i+1)% image_filter.Width, (j + 1) % image_filter.Width);
                    xr += neiborhoodpixelColor.R;
                    xg += neiborhoodpixelColor.G;
                    xb += neiborhoodpixelColor.B;
                    neiborhoodpixelColor = image_noise1.GetPixel((i + 1) % image_filter.Width, (j) % image_filter.Width);
                    yr += -neiborhoodpixelColor.R;
                    yg += -neiborhoodpixelColor.G;
                    yb += -neiborhoodpixelColor.B;
                    neiborhoodpixelColor = image_noise1.GetPixel((i) % image_filter.Width, (j+1) % image_filter.Width);
                    yr += neiborhoodpixelColor.R;
                    yg += neiborhoodpixelColor.G;
                    yb += neiborhoodpixelColor.B;
                    int r = Math.Abs(xr) + Math.Abs(yr);
                    int g = Math.Abs(xg) + Math.Abs(yg);
                    int b = Math.Abs(xb) + Math.Abs(yb);
                    if (r<0) { r = 0; }
                    else if (r > 255) { r = 255; }
                    if (g < 0) { g = 0; }
                    else if (g > 255) { g = 255; }
                    if (b < 0) { b = 0; }
                    else if (b > 255) { b = 255; }
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
        }
        void Prewitt()
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            Bitmap image_filter2 = new Bitmap(image_noise1);
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    int m, n;
                    int xr = 0;
                    int xg = 0;
                    int xb = 0;
                    int yr = 0;
                    int yg = 0;
                    int yb = 0;
                    for (int t = -1; t < 2; t++)
                    {
                        for (int k =-1; k < 2; k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor;
                            if (k == -1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                xr += -neiborhoodpixelColor.R;
                                xg += -neiborhoodpixelColor.G;
                                xb += -neiborhoodpixelColor.B;
                            }
                            else if (k == 1) 
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                xr += neiborhoodpixelColor.R;
                                xg += neiborhoodpixelColor.G;
                                xb += neiborhoodpixelColor.B;
                            }

                            if (t == -1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                yr += -neiborhoodpixelColor.R;
                                yg += -neiborhoodpixelColor.G;
                                yb += -neiborhoodpixelColor.B;
                            }
                            else if (t == 1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                yr += neiborhoodpixelColor.R;
                                yg += neiborhoodpixelColor.G;
                                yb += neiborhoodpixelColor.B;
                            }
                        }
                    }
                    if (xr < 0) { xr = 0; }
                    else if (xr > 255) { xr = 255; }
                    if (xg < 0) { xg = 0; }
                    else if (xg > 255) { xg = 255; }
                    if (xb < 0) { xb = 0; }
                    else if (xb > 255) {xb = 255; }
                    if (yr < 0) { yr = 0; }
                    else if (yr > 255) { yr = 255; }
                    if (yg < 0) { yg = 0; }
                    else if (yg > 255) { yg = 255; }
                    if (yb < 0) { yb = 0; }
                    else if (yb > 255) { yb = 255; }
                    Color newColor = Color.FromArgb(xr, xg, xb);
                    image_filter.SetPixel(i, j, newColor);
                    newColor = Color.FromArgb(yr, yg, yb);
                    image_filter2.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_filter;
            pictureBox_filter_right.Image = image_filter2;
            
        }
        void sobel()
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            Bitmap image_filter2 = new Bitmap(image_noise1);
            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    int m, n;
                    int xr = 0;
                    int xg = 0;
                    int xb = 0;
                    int yr = 0;
                    int yg = 0;
                    int yb = 0;
                    for (int t = -1; t < 2; t++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor;
                            if (k == -1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                xr += -neiborhoodpixelColor.R;
                                xg += -neiborhoodpixelColor.G;
                                xb += -neiborhoodpixelColor.B;
                                if (t==0) //多扣一次-->(-2)
                                {                                   
                                    xr += -neiborhoodpixelColor.R;
                                    xg += -neiborhoodpixelColor.G;
                                    xb += -neiborhoodpixelColor.B;
                                }
                            }
                            else if (k == 1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                xr += neiborhoodpixelColor.R;
                                xg += neiborhoodpixelColor.G;
                                xb += neiborhoodpixelColor.B;
                                if (t == 0) //多加一次-->(+2)
                                {                                 
                                    xr += neiborhoodpixelColor.R;
                                    xg += neiborhoodpixelColor.G;
                                    xb += neiborhoodpixelColor.B;
                                }
                            }

                            if (t == -1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                yr += -neiborhoodpixelColor.R;
                                yg += -neiborhoodpixelColor.G;
                                yb += -neiborhoodpixelColor.B;
                                if (k == 0) //多扣一次-->(-2)
                                {                                 
                                    yr += -neiborhoodpixelColor.R;
                                    yg += -neiborhoodpixelColor.G;
                                    yb += -neiborhoodpixelColor.B;
                                }
                            }
                            else if (t == 1)
                            {
                                neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                                yr += neiborhoodpixelColor.R;
                                yg += neiborhoodpixelColor.G;
                                yb += neiborhoodpixelColor.B;
                                if (k == 0) //多加一次-->(+2)
                                {                              
                                    yr += neiborhoodpixelColor.R;
                                    yg += neiborhoodpixelColor.G;
                                    yb += neiborhoodpixelColor.B;
                                }
                            }
                        }
                    }
                    if (xr < 0) { xr = 0; }
                    else if (xr > 255) { xr = 255; }
                    if (xg < 0) { xg = 0; }
                    else if (xg > 255) { xg = 255; }
                    if (xb < 0) { xb = 0; }
                    else if (xb > 255) { xb = 255; }
                    if (yr < 0) { yr = 0; }
                    else if (yr > 255) { yr = 255; }
                    if (yg < 0) { yg = 0; }
                    else if (yg > 255) { yg = 255; }
                    if (yb < 0) { yb = 0; }
                    else if (yb > 255) { yb = 255; }
                    Color newColor = Color.FromArgb(xr, xg, xb);
                    image_filter.SetPixel(i, j, newColor);
                    newColor = Color.FromArgb(yr, yg, yb);
                    image_filter2.SetPixel(i, j, newColor);
                }
            }
            pictureBox_filter_left.Image = image_filter;
            pictureBox_filter_right.Image = image_filter2;

        }
        void Gaussian(int mask,double a)
        {
            Bitmap image_filter = new Bitmap(image_noise1);
            int l = mask + 1;
            mask = 2 * mask + 3;
            double sigma = a/100;
            double sum_coffient = 0;
            //小數型Mask
            for (int t = -(mask / 2); t <= (mask / 2); t++)
            {
                for (int k = -(mask / 2); k <= (mask / 2); k++)
                {                 
                    double coffient;                  
                    coffient = (1 / (2 * Math.PI * sigma * sigma)) * Math.Pow(Math.E, -((t ) * (t ) + (k ) * (k )) / 2 * sigma * sigma);                                     
                    sum_coffient = sum_coffient + coffient;                              
                }
            }

            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {                   
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;
                    
                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            double coffient;                                                                            
                            coffient = (1 / (2 * Math.PI * sigma * sigma)) * Math.Pow(Math.E, -((t) * (t) + (k) * (k)) / 2 * sigma * sigma);
                            coffient =coffient / sum_coffient;                           
                            r = Convert.ToInt32(r + neiborhoodpixelColor.R * coffient);
                            g = Convert.ToInt32(g + neiborhoodpixelColor.G * coffient);
                            b = Convert.ToInt32(b + neiborhoodpixelColor.B * coffient);
                        }
                    }                  
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);                   
                }
            }
            ///////////////////////////////////////////////////////
            //以下是整數型Mask
            /*Bitmap image_filter = new Bitmap(image_noise1);           
            mask = 2 * mask + 3;
            double sigma = a / 100;
            int sum_coffient = 0;
            double normal =(1 / (2 * Math.PI * sigma * sigma)) * Math.Pow(Math.E, -(((mask / 2)) * ((mask / 2)) + ((mask / 2)) * ((mask / 2))) / 2 * sigma * sigma); ;
            for (int t = -(mask / 2); t <= (mask / 2); t++)
            {
                for (int k = -(mask / 2); k <= (mask / 2); k++)
                {
                    double coffient;
                    coffient = (1 / (2 * Math.PI * sigma * sigma)) * Math.Pow(Math.E, -((t) * (t) + (k) * (k)) / 2 * sigma * sigma);
                    coffient = Convert.ToInt32(coffient / normal);                    
                    sum_coffient = Convert.ToInt32(sum_coffient + coffient);
                    Console.WriteLine(sum_coffient);
                }
            }

            for (int i = 0; i < image_noise1.Width; i++)
            {
                for (int j = 0; j < image_noise1.Height; j++)
                {
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int m, n;

                    for (int t = -(mask / 2); t <= (mask / 2); t++)
                    {
                        for (int k = -(mask / 2); k <= (mask / 2); k++)
                        {
                            if (i + t < 0) { m = i + t + image_noise1.Width; }
                            else { m = (i + t) % image_noise1.Width; }
                            if (j + k < 0) { n = j + k + image_noise1.Height; }
                            else { n = (j + k) % image_noise1.Height; }
                            Color neiborhoodpixelColor = image_noise1.GetPixel(m, n);
                            double coffient;
                            coffient = (1 / (2 * Math.PI * sigma * sigma)) * Math.Pow(Math.E, -((t) * (t) + (k) * (k)) / 2 * sigma * sigma);
                            coffient = Convert.ToInt32(coffient / normal);
                            coffient /= sum_coffient;
                            r = Convert.ToInt32(r + neiborhoodpixelColor.R * coffient);
                            g = Convert.ToInt32(g + neiborhoodpixelColor.G * coffient);
                            b = Convert.ToInt32(b + neiborhoodpixelColor.B * coffient);
                        }
                    }
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    Color newColor = Color.FromArgb(r, g, b);
                    image_filter.SetPixel(i, j, newColor);
                }
            }*/
            ////////////////////////////////////////////////////////////////////////////////
            pictureBox_filter_left.Image = image_noise1;
            pictureBox_filter_right.Image = image_filter;
            labelfilter_snr.Text = "SNR：" + calculateSNR(image_noise1, image_filter).ToString() + "(dB)";
        }
        private void comboBox_mask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_filter.SelectedIndex == 1) { lowpass(comboBox_mask.SelectedIndex);}
            else if (comboBox_filter.SelectedIndex == 2) { Median_Square(comboBox_mask.SelectedIndex); }
            else if (comboBox_filter.SelectedIndex == 3) { Median_Cross(comboBox_mask.SelectedIndex); }
            else if (comboBox_filter.SelectedIndex == 4) { highpass(comboBox_mask.SelectedIndex); }
            else if (comboBox_filter.SelectedIndex == 5) { crispening_Square(comboBox_mask.SelectedIndex); }
            else if (comboBox_filter.SelectedIndex == 6) { crispening_Cross(comboBox_mask.SelectedIndex); }
            else if (comboBox_filter.SelectedIndex == 7) { high_boost(comboBox_mask.SelectedIndex, (Convert.ToInt32(numericUpDown_threshold.Value))); }
            else if (comboBox_filter.SelectedIndex == 11) { Gaussian(comboBox_mask.SelectedIndex,Convert.ToInt32(numericUpDown_threshold.Value)); }
        }

        private void numericUpDown_threshold_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox_filter.SelectedIndex == 0) { Outlier(Convert.ToInt32(numericUpDown_threshold.Value)); }
            else if (comboBox_filter.SelectedIndex == 7) { high_boost(comboBox_mask.SelectedIndex, (Convert.ToInt32(numericUpDown_threshold.Value))); }
            else if (comboBox_filter.SelectedIndex == 11) { Gaussian(comboBox_mask.SelectedIndex, Convert.ToInt32(numericUpDown_threshold.Value)); }
        }


        //二值化上色(先轉黑白，判斷分割獨立區塊並上色)
        private void button_component_Click(object sender, EventArgs e)
        {
            Bitmap image_whiteblack = new Bitmap(image1.getBitmap);         
            // Loop through the images pixels to reset color.
            int x, y;
            for (x = 0; x < image_whiteblack.Width; x++)
            {
                for (y = 0; y < image_whiteblack.Height; y++)
                {
                    Color pixelColor = image_whiteblack.GetPixel(x, y);
                    int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                    if (gray < 128)
                    {
                        Color newColor = Color.FromArgb(0, 0, 0);
                        image_whiteblack.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        Color newColor = Color.FromArgb(255, 255, 255);
                        image_whiteblack.SetPixel(x, y, newColor);
                    }
                }
            }
            pictureBox_component_left.Image = image_whiteblack;
            Bitmap image_component = new Bitmap(image_whiteblack);
            int[,] Array = new int[image_whiteblack.Width, image_whiteblack.Height];
            int[] colorArray = new int[image_whiteblack.Width* image_whiteblack.Height/2];
            int index=0;          
            for (y = 0; y < image_whiteblack.Height; y++)
            {
                for (x = 0; x < image_whiteblack.Width; x++)
                {
                    int t = 0;
                    bool u, l;
                    u = false;
                    l = false;
                    Color pixelcolor = image_whiteblack.GetPixel(x, y);
                    if (pixelcolor.R==255) 
                    {
                        Array[x, y] = index;
                        colorArray[index] = index;
                        if (x - 1 >= 0) 
                        { 
                            Color leftcolor = image_whiteblack.GetPixel(x-1, y);
                            if (leftcolor.R == 255) { Array[x,y]= Array[x-1, y]; t = 1;l = true; }
                        }
                        if (y - 1 >= 0) 
                        { 
                            Color upcolor = image_whiteblack.GetPixel(x, y - 1);
                            if (upcolor.R == 255) { Array[x, y] = Array[x , y - 1]; t = 1; u = true; }
                        }
                        if (l == true && u == true) 
                        {
                            if (Array[x, y - 1] != Array[x - 1, y]) 
                            {
                                colorArray[Math.Max(Array[x - 1, y], Array[x, y - 1])] =Math.Min(Array[x - 1, y], Array[x, y - 1]);
                                while (colorArray[colorArray[Math.Max(Array[x - 1, y], Array[x, y - 1])]] != colorArray[Math.Max(Array[x - 1, y], Array[x, y - 1])])
                                {
                                    colorArray[Math.Max(Array[x - 1, y], Array[x, y - 1])] = colorArray[colorArray[Math.Max(Array[x - 1, y], Array[x, y - 1])]];
                                }
                                Array[x,y] = Math.Min(Array[x - 1, y], Array[x, y - 1]);                               
                            }
                        }
                        if (t != 1) { index++; }                                           
                    }
                }
            }
            for (y = image_whiteblack.Height - 1; y > -1; y--)
            {
                for (x = image_whiteblack.Width - 1;x > -1; x--)
                {                  
                    /*bool d, r;
                    d = false;
                    r = false;*/
                    Color pixelcolor = image_whiteblack.GetPixel(x, y);
                    if (pixelcolor.R == 255)
                    {
                        Array[x, y] = colorArray[Array[x, y]];                        
                    }
                }
            }
            for (x = 0; x < image_whiteblack.Width; x++)
            {
                for (y = 0; y < image_whiteblack.Height; y++)
                {
                    Color pixelcolor = image_whiteblack.GetPixel(x, y);                  
                    if (pixelcolor.R == 255)
                    {
                        switch (colorArray[Array[x, y]]%10)
                        {
                            case 0:
                                image_component.SetPixel(x, y, Color.Red);
                                break;
                            case 1:
                                image_component.SetPixel(x, y, Color.LightBlue);
                                break;
                            case 2:
                                image_component.SetPixel(x, y, Color.LightGreen);
                                break;
                            case 3:
                                image_component.SetPixel(x, y, Color.Yellow);                               
                                break;
                            case 4:
                                image_component.SetPixel(x, y, Color.Aqua);                               
                                break;
                            case 5:
                                image_component.SetPixel(x, y, Color.Purple);
                                break;
                            case 6:
                                image_component.SetPixel(x, y, Color.Pink);
                                break;
                            case 7:
                                image_component.SetPixel(x, y, Color.Aquamarine);
                                break;
                            case 8:
                                image_component.SetPixel(x, y, Color.BlueViolet);
                                break;
                            case 9:
                                image_component.SetPixel(x, y, Color.Orange);
                                break;                                               
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }                     
                    }
                }
            }     
            pictureBox_component_right.Image = image_component;
            label39.Visible = true;
            label40.Visible = true;
        }

        List<Image> imlist_origin = new List<Image>();        
        int Count = 0;
        System.Windows.Forms.Timer t;       
        string upperpath;
        string filename;
        List<Image> imlist_new = new List<Image>();
        List<Image> imlist_vector = new List<Image>();
        List<string> list_framename = new List<string>();
        Thread t_encode;
       
        //choose pic for playing movie
        private void button_choose_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Multiselect = true;             
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                imlist_origin.Clear();
                imlist_new.Clear();
                imlist_vector.Clear();
                imlist_vector.Add(null);
                int count=0;
                MemoryStream MS = null;
                if (comboBox_matching.SelectedIndex == 0)
                {
                    foreach (string item in OpenFile.FileNames)
                    //把所有選擇的圖片放到列表中 
                    {
                        MS = new MemoryStream(File.ReadAllBytes(item));
                        imlist_origin.Add(Image.FromStream(MS));
                        MS.Close();

                        upperpath = Path.GetDirectoryName(item); //上層路徑
                        filename = Path.GetFileNameWithoutExtension(item);
                        //filename = filename.Substring(0, filename.Length - 1); //去掉最後數字 1.0.0.X
                        list_framename.Add(filename);  //把檔案名加入LIST
                                                       //string path = upperpath+ @"\encode\" + Path.GetFileNameWithoutExtension(item)+ " - 複製.tiff";  

                        //要demo時還原

                        string path = upperpath + @"\encode\Exhaustive-MAD\" + Path.GetFileNameWithoutExtension(item) + ".Exhaustive-MAD.tiff";
                        MS = new MemoryStream(File.ReadAllBytes(path));
                        imlist_new.Add(Image.FromStream(MS));
                        MS.Close();
                        if (count != 0)
                        {
                            path = upperpath + @"\encode\Exhaustive-MAD\" + Path.GetFileNameWithoutExtension(item) + ".Exhaustive-MAD_vector.tiff";
                            MS = new MemoryStream(File.ReadAllBytes(path));
                            imlist_vector.Add(Image.FromStream(MS));
                            MS.Close();
                        }
                        count++;
                    }
                }
                else if(comboBox_matching.SelectedIndex == 1)
                {
                    foreach (string item in OpenFile.FileNames)
                    //把所有選擇的圖片放到列表中 
                    {
                        MS = new MemoryStream(File.ReadAllBytes(item));
                        imlist_origin.Add(Image.FromStream(MS));
                        MS.Close();

                        upperpath = Path.GetDirectoryName(item); //上層路徑
                        filename = Path.GetFileNameWithoutExtension(item);
                        //filename = filename.Substring(0, filename.Length - 1); //去掉最後數字 1.0.0.X
                        list_framename.Add(filename);  //把檔案名加入LIST
                                                       //string path = upperpath+ @"\encode\" + Path.GetFileNameWithoutExtension(item)+ " - 複製.tiff";  

                        //要demo時還原
                        string path = upperpath + @"\encode\TDL-MAD\" + Path.GetFileNameWithoutExtension(item) + ".TDL-MAD.tiff";
                        MS = new MemoryStream(File.ReadAllBytes(path));
                        imlist_new.Add(Image.FromStream(MS));
                        MS.Close();
                        if (count != 0)
                        {
                            path = upperpath + @"\encode\TDL-MAD\" + Path.GetFileNameWithoutExtension(item) + ".TDL-MAD_vector.tiff";
                            MS = new MemoryStream(File.ReadAllBytes(path));
                            imlist_vector.Add(Image.FromStream(MS));
                            MS.Close();
                        }
                        count++;
                    }
                }
                
                double[] psnr_data = new double[imlist_origin.Count];
                //要demo時還原
                
                imlist_vector[0] = null;
                pictureBox_movie_right.Image = imlist_new[0];
                pictureBox_movie3.Image = imlist_vector[0];
                

                chart_psnr.Series[0].Points.Clear();
                chart_psnr.Series[0].Points.AddXY(-1, 0);
                chart_psnr.ChartAreas[0].AxisX.Maximum = imlist_origin.Count;
                for (int h=1;h< imlist_new.Count; h++) 
                {
                    Bitmap movie_current =new Bitmap(imlist_origin[h]);
                    Bitmap movie_new = new Bitmap(imlist_new[h]);
                    psnr_data[h - 1] = calculatePSNR(movie_current, movie_new);
                    chart_psnr.Series[0].Points.AddXY(h + 1, psnr_data[h - 1]);  //imlist_origin[h]是第h+1張圖,psnr_data[h-1]是imlist_origin[h]-imlist_origin[h-1]的PSNR(第h+1張圖-第h張圖)
                    chart_psnr.Series[0].Color = Color.Black;
                }

                
                //int totalpic = imlist.Count;              
                pictureBox_movie3.Visible = true;
                pictureBox_movie_left.Image = imlist_origin[0];//起始畫面
                pictureBox_movie3.Size = pictureBox_movie_left.Size;

                label44.Text = "影片資訊:" + 1 + "/" + imlist_origin.Count;
                label44.Visible = true;
                Count = 0;
                t.Enabled = false;
                panel_playerbar.Location = new Point(pictureBox_movie_left.Location.X, pictureBox_movie_left.Location.Y + pictureBox_movie_left.Height + 10);
                panel_playerbar.Width = pictureBox_movie_left.Width;
                button_choose.Location = new Point(panel_playerbar.Width / 6 -15 , button_choose.Location.Y);
                button_play.Location = new Point(button_choose.Location.X + panel_playerbar.Width / 6, button_choose.Location.Y);
                button_pause.Location = new Point(button_play.Location.X + panel_playerbar.Width / 6, button_choose.Location.Y);
                button_playone.Location = new Point(button_pause.Location.X + panel_playerbar.Width / 6, button_choose.Location.Y);
                button_backone.Location = new Point(button_playone.Location.X + panel_playerbar.Width / 6, button_playone.Location.Y);
                panel_playerbar.AutoSize = true;
                chart_psnr.Visible = true;
                button_play.Enabled = true;
                button_pause.Enabled = true;
                button_playone.Enabled = true;
                button_backone.Enabled = true;
                button_movieencode.Enabled = true;
                checkBox_encodedemo.Enabled = true;

                ////////////////////////////////////////////////////////
                /*string path_frame;
                string path_txt;
                path_frame = upperpath + @"\encode\" + filename + (0 + 1) + ".decode.tiff";
                path_txt = upperpath + @"\encode\" + filename + (0 + 1) + ".motion_vector.txt";*/

                /*int[] lines = { 40, 50, 81, 50 };
                string a = null;
                for (int i = 0; i < lines.Length; i++)
                {
                    a += Convert.ToString(lines[i]) + " ";
                }
                a = a.Substring(0, a.Length - 1); //刪除最後一個空格
                File.WriteAllText(path_txt, a);*/

                /*Bitmap movie_origin = new Bitmap(imlist[0]);
                movie_origin.Save(path_frame);
                Console.WriteLine(path_frame);*/

                //以下是output
                /*Bitmap movie_origin = new Bitmap(imlist[0]);
                movie_origin.Save(@"C:\Users\user\Desktop\image process\ImagePCX\sequences\test\6.1.01.test.tiff");*/

                //以下是read by line txt
                /*FileStream fs = new FileStream(@"C:\Users\user\Desktop\image process\ImagePCX\sequences\test\number.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                string s1;
                s1 = sr.ReadLine();
                string[] s2 = new string[4];
                s2 = s1.Split(' ');
                while (s1 != null)
                {
                    for (int i = 0; i < s2.Length; i++)
                    {
                        int a = Convert.ToInt32(s2[i]);
                        Console.WriteLine(a);
                    }
                    s1 = sr.ReadLine();
                }*/

                //以下是write txt
                /*int[] lines = { 40,50,81,50 };
                string a=null;
                for (int i = 0; i < lines.Length; i++)
                {
                  a += Convert.ToString(lines[i])+" ";                   
                }
                a = a.Substring(0, a.Length - 1); //刪除最後一個空格
                File.WriteAllText(@"C:\Users\user\Desktop\image process\ImagePCX\sequences\test\number.txt", a); //全同一行*/
                //File.WriteAllLines是一行一行寫
                ///////////////////////////////////////////////////////////////
            }
        }

        //Encode picture
        private void button_movieencode_Click(object sender, EventArgs e)
        {
            pictureBox_movielarge.Image = null;
            pictureBox_movie2large.Image = null;
            pictureBox_movielarge.Location = new Point((pictureBox_movie_left.Location.X + imlist_origin[0].Width + pictureBox_movie_right.Location.X) / 2, pictureBox_movie_left.Location.Y);
            pictureBox_movie2large.Location = new Point((pictureBox_movie_left.Location.X + imlist_origin[0].Width + pictureBox_movie_right.Location.X) / 2, pictureBox_movie_left.Location.Y + Convert.ToInt32(numericUpDown_enlargesize.Value) + 100);
            button_encodepause.Enabled = true;
            button_encodestop.Enabled = true;
            button_movieencode.Enabled = false;
            button_encodepause.Invoke(new MethodInvoker(delegate ()
            {     ////解決跨執行緒作業無效
                button_encodepause.Text = "暫停";
            }));
            int selectedIndex = comboBox_matching.SelectedIndex;
            if (selectedIndex == 0)
            {
                t_encode = new Thread(new ThreadStart(Thread_Exhaustive));
                t_encode.IsBackground = true;
                t_encode.Start();
            }
            else if (selectedIndex == 1)
            {
                t_encode = new Thread(new ThreadStart(Thread_TDL));
                t_encode.IsBackground = true;
                t_encode.Start();
            }
        }

        //Thread of Exhaustive
        public void Thread_Exhaustive()
        {          
            string path_frame;
            string path_txt;
            string path_vector;
            double[] psnr_data = new double[imlist_origin.Count];

            Bitmap movie_origin;
            Bitmap movie_current;
            Bitmap movie_temp = new Bitmap(imlist_origin[0]);
            Bitmap movie_originlarge = new Bitmap(8, 8);
            Bitmap movie_currentlarge= new Bitmap(8, 8);

            StringBuilder sb = new StringBuilder();

            Graphics g_origin = pictureBox_movie_left.CreateGraphics();
            Graphics g_current = pictureBox_movie_right.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 3);          

            chart_psnr.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                chart_psnr.Series[0].Points.Clear();
                chart_psnr.Series[0].Points.AddXY(-1, 0);               
                chart_psnr.ChartAreas[0].AxisX.Maximum = imlist_origin.Count;
                chart_psnr.Visible = true;
            }));

            pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                pictureBox_movielarge.Width = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movielarge.Height = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movielarge.Image = movie_originlarge;
            }));
            pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                pictureBox_movie2large.Width = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movie2large.Height = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movie2large.Image = movie_currentlarge;
            }));


            for (int h = 1; h < imlist_origin.Count; h++)
            {
                label43.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    label43.Visible = true;
                    label43.Text = "影片資訊:" + (h+1) + "/" + imlist_origin.Count;
                }));
                label44.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    label44.Text = "影片資訊:" + h + "/" + imlist_origin.Count;
                }));              
                
                movie_current = new Bitmap(imlist_origin[h]);
                movie_origin = new Bitmap(imlist_origin[h - 1]);
                Bitmap movie_new = new Bitmap(imlist_origin[0].Width, imlist_origin[0].Height);
                Bitmap movie_vector = new Bitmap(imlist_origin[0].Width, imlist_origin[0].Height);
                pictureBox_movie_left.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie_left.Image = imlist_origin[h - 1];
                }));
                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie_right.Image = imlist_origin[h];
                }));
                pictureBox_movie3.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie3.Image = movie_vector;
                    pictureBox_movie3.Visible = true;
                }));
                
                Graphics g_vector = Graphics.FromImage(pictureBox_movie3.Image);

                LockBitmap lockmovie_origin = new LockBitmap(movie_origin);
                LockBitmap lockmovie_current = new LockBitmap(movie_current);
                LockBitmap lockmovie_new = new LockBitmap(movie_new);
                LockBitmap lockmovie_temp = new LockBitmap(movie_temp);
                LockBitmap lockmovie_originlarge = new LockBitmap(movie_originlarge);
                LockBitmap lockmovie_currentlarge = new LockBitmap(movie_currentlarge);

                lockmovie_origin.LockBits();
                lockmovie_current.LockBits();
                lockmovie_temp.LockBits();
                lockmovie_new.LockBits();
                lockmovie_originlarge.LockBits();
                lockmovie_currentlarge.LockBits();
               
                // write-out path of pics and txt
                path_frame = upperpath + @"\encode\" + list_framename[h] + ".Exhaustive-MAD.tiff";
                path_txt = upperpath + @"\encode\" + list_framename[h] + ".Exhaustive-MAD_motion_vector.txt";
                path_vector = upperpath + @"\encode\" + list_framename[h] + ".Exhaustive-MAD_vector.tiff";
                
                int[] origin_r = new int[64];
                int[] origin_g = new int[64];
                int[] origin_b = new int[64];
                int[] current_r = new int[64];
                int[] current_g = new int[64];
                int[] current_b = new int[64];
                using (StreamWriter file = new StreamWriter(path_txt, true))  //output vector
                {
                    for (int n = 0; n < movie_current.Height; n += 8)
                    {                                      
                        for (int m = 0; m < movie_current.Width; m += 8)
                        {                           
                            int min = 0;
                            int delta_x = 0;
                            int delta_y = 0;
                            //取第t+1圖原區data,順便比對第t圖對應區                   
                            for (int lh = 0; lh < 8; lh++)
                            {
                                for (int lw = 0; lw < 8; lw++)
                                {
                                    Color currentpixel = lockmovie_current.GetPixel(m + lw, n + lh);
                                    current_r[lw * 8 + lh] = currentpixel.R; current_g[lw * 8 + lh] = currentpixel.G; current_b[lw * 8 + lh] = currentpixel.B;
                                    Color originpixel = lockmovie_origin.GetPixel(m + lw, n + lh);
                                    min += Math.Abs(currentpixel.R - originpixel.R) + Math.Abs(currentpixel.G - originpixel.G) + Math.Abs(currentpixel.B - originpixel.B);  //計算current的對應區之誤差
                                    lockmovie_currentlarge.SetPixel(lw, lh, currentpixel);
                                    lockmovie_originlarge.SetPixel(lw, lh, originpixel);
                                }
                            }
                            if (checkBox_encodedemo.Checked == true)
                            { //畫格子    
                                pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                {   //解決跨執行緒作業無效                                   
                                    pictureBox_movie_left.Refresh();
                                    g_origin.DrawRectangle(redPen, m, n, 8, 8);                                   
                                }));
                                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效                                  
                                    pictureBox_movie_right.Refresh();
                                    g_current.DrawRectangle(redPen, m, n, 8, 8);                                    
                                }));
                                pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_originlarge.UnlockBits();
                                    pictureBox_movielarge.Visible = true;
                                    pictureBox_movielarge.Refresh();
                                    lockmovie_originlarge.LockBits();
                                }));
                                pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_currentlarge.UnlockBits();
                                    pictureBox_movie2large.Visible = true;
                                    pictureBox_movie2large.Refresh();
                                    lockmovie_currentlarge.LockBits();
                                }));
                                //
                            }
                            else 
                            {
                                //畫格子    
                                pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                {   //解決跨執行緒作業無效                                   
                                    pictureBox_movie_left.Refresh();                                  
                                }));
                                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效                                  
                                    pictureBox_movie_right.Refresh();                                  
                                }));
                                pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_originlarge.UnlockBits();
                                    pictureBox_movielarge.Visible = false;
                                    pictureBox_movielarge.Refresh();
                                    lockmovie_originlarge.LockBits();
                                }));
                                pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_currentlarge.UnlockBits();
                                    pictureBox_movie2large.Visible = false;
                                    pictureBox_movie2large.Refresh();
                                    lockmovie_currentlarge.LockBits();
                                }));
                                //
                            }

                            if (min != 0)  //current在origin的對應區沒有完全相同
                            {
                                for (int j = 0; j <= movie_origin.Height - 8; j++)
                                {
                                    for (int i = 0; i <= movie_origin.Width - 8; i++)
                                    {                                      
                                        int total_r = 0;
                                        int total_g = 0;
                                        int total_b = 0;

                                        for (int lh = 0; lh < 8; lh++)
                                        {
                                            for (int lw = 0; lw < 8; lw++)
                                            {
                                                Color originpixel = lockmovie_origin.GetPixel(i + lw, j + lh);
                                                origin_r[lw * 8 + lh] = originpixel.R; origin_g[lw * 8 + lh] = originpixel.G; origin_b[lw * 8 + lh] = originpixel.B;

                                                origin_r[lw * 8 + lh] = Math.Abs(current_r[lw * 8 + lh] - origin_r[lw * 8 + lh]);
                                                origin_g[lw * 8 + lh] = Math.Abs(current_g[lw * 8 + lh] - origin_g[lw * 8 + lh]);
                                                origin_b[lw * 8 + lh] = Math.Abs(current_b[lw * 8 + lh] - origin_b[lw * 8 + lh]);

                                                lockmovie_originlarge.SetPixel(lw, lh, originpixel);
                                            }
                                        }
                                        if (checkBox_encodedemo.Checked == true)
                                        {
                                            //畫格子                                  
                                            pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                            {   //解決跨執行緒作業無效                                              
                                                pictureBox_movie_left.Refresh();
                                                g_origin.DrawRectangle(redPen, i, j, 8, 8);                                              
                                            }));
                                            pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                lockmovie_originlarge.UnlockBits();
                                                pictureBox_movielarge.Visible = true;                                              
                                                pictureBox_movielarge.Refresh();
                                                lockmovie_originlarge.LockBits();
                                            }));
                                            pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                lockmovie_currentlarge.UnlockBits();
                                                pictureBox_movie2large.Visible = true;
                                                pictureBox_movie2large.Refresh();
                                                lockmovie_currentlarge.LockBits();
                                            }));
                                            //                                           
                                        }
                                        else 
                                        {
                                            //畫格子                                  
                                            pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                            {   //解決跨執行緒作業無效                                              
                                                pictureBox_movie_left.Refresh();                                                
                                            }));
                                            pictureBox_movie_right.Invoke(new MethodInvoker(delegate ()
                                            {   //解決跨執行緒作業無效                                              
                                                pictureBox_movie_right.Refresh();
                                            }));
                                            pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                lockmovie_originlarge.UnlockBits();
                                                pictureBox_movielarge.Visible = false;
                                                pictureBox_movielarge.Refresh();
                                                lockmovie_originlarge.LockBits();
                                            }));
                                            pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                lockmovie_currentlarge.UnlockBits();
                                                pictureBox_movie2large.Visible = false;
                                                pictureBox_movie2large.Refresh();
                                                lockmovie_currentlarge.LockBits();
                                            }));
                                            //          
                                        }

                                        foreach (int item in origin_r) { total_r += item; }
                                        foreach (int item in origin_g) { total_g += item; }
                                        foreach (int item in origin_b) { total_b += item; }
                                        if ((total_r + total_g + total_b) < min) { min = (total_r + total_g + total_b); delta_x = i - m; delta_y = j - n; }
                                    }
                                    if (checkBox_encodedemo.Checked == true)
                                    {
                                        //解決縮小視窗導致的格子消失
                                        pictureBox_movie_right.Invoke(new MethodInvoker(delegate ()
                                        {                                           
                                            pictureBox_movie_right.Refresh();
                                            g_current.DrawRectangle(redPen, m, n, 8, 8);                                            
                                        }));
                                    }

                                }
                            }
                            else { delta_x = 0; delta_y = 0; }     
                            
                            //write delta_x, delta_y                                                  
                            sb.Append(Convert.ToString(delta_x));
                            sb.Append(" ");
                            sb.Append(Convert.ToString(delta_y));
                            sb.Append(" ");
                            //draw vector
                            pictureBox_movie3.Invoke(new MethodInvoker(delegate ()
                            {
                                using (Pen vectorPen = new Pen(Color.Blue, 1))
                                {
                                    vectorPen.StartCap = LineCap.ArrowAnchor;
                                    vectorPen.EndCap = LineCap.RoundAnchor;
                                    g_vector.DrawLine(vectorPen, m + 4 , n + 4 , m + 4 + delta_x, n + 4 + delta_y);
                                    pictureBox_movie3.Image = movie_vector;
                                }
                            }));
                            
                            //draw decode frame block                            
                            for (int lh = 0; lh < 8; lh++)
                            {
                                for (int lw = 0; lw < 8; lw++)
                                {                                                                      
                                    Color originpixel = lockmovie_temp.GetPixel(m + delta_x + lw, n + delta_y + lh);
                                    lockmovie_new.SetPixel(m + lw, n + lh, originpixel);                                 
                                }
                            }                           
                        }
                        //找完一列後，輸出vector
                        //write delta_x, delta_y 
                        //a = a.Substring(0, a.Length - 1); //刪除最後一個空格
                        sb.Length = sb.Length - 1;//刪除最後一個空格                  
                        file.WriteLine(sb);
                        //file.Flush();
                        sb.Clear();                                         
                    }
                    lockmovie_origin.UnlockBits();
                    lockmovie_current.UnlockBits();
                    lockmovie_temp.UnlockBits();
                    lockmovie_new.UnlockBits();
                    lockmovie_originlarge.UnlockBits();
                    lockmovie_currentlarge.UnlockBits();
                    
                    //calculate psnr                   
                    chart_psnr.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                               
                        psnr_data[h-1] = calculatePSNR(movie_current, movie_new);
                        chart_psnr.Series[0].Points.AddXY(h+1, psnr_data[h-1]);  //imlist_origin[h]是第h+1張圖,psnr_data[h-1]是imlist_origin[h]-imlist_origin[h-1]的PSNR(第h+1張圖-第h張圖)
                        chart_psnr.Series[0].Color = Color.Black;
                    }));
                    //save frame
                    movie_new.Save(path_frame);
                    movie_temp = new Bitmap(movie_new);
                    //output vector
                    pictureBox_movie3.Image.Save(path_vector);                  
                }
            }
        }

        //Thread of TDL
        public void Thread_TDL()
        {
            string path_frame;
            string path_txt;
            string path_vector;
            double[] psnr_data = new double[imlist_origin.Count];

            Bitmap movie_origin;
            Bitmap movie_current;
            Bitmap movie_temp = new Bitmap(imlist_origin[0]);
            Bitmap movie_originlarge = new Bitmap(8, 8);
            Bitmap movie_currentlarge = new Bitmap(8, 8);

            StringBuilder sb = new StringBuilder();

            Graphics g_origin = pictureBox_movie_left.CreateGraphics();
            Graphics g_current = pictureBox_movie_right.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 3);

            chart_psnr.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                chart_psnr.Series[0].Points.Clear();
                chart_psnr.Series[0].Points.AddXY(-1, 0);
                chart_psnr.ChartAreas[0].AxisX.Maximum = imlist_origin.Count;
                chart_psnr.Visible = true;
            }));

            pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                pictureBox_movielarge.Width = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movielarge.Height = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movielarge.Image = movie_originlarge;
            }));
            pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                pictureBox_movie2large.Width = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movie2large.Height = Convert.ToInt32(numericUpDown_enlargesize.Value);
                pictureBox_movie2large.Image = movie_currentlarge;
            }));


            for (int h = 1; h < imlist_origin.Count; h++)
            {
                label43.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    label43.Visible = true;
                    label43.Text = "影片資訊:" + (h + 1) + "/" + imlist_origin.Count;
                }));
                label44.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    label44.Text = "影片資訊:" + h + "/" + imlist_origin.Count;
                }));

                movie_current = new Bitmap(imlist_origin[h]);
                movie_origin = new Bitmap(imlist_origin[h - 1]);
                Bitmap movie_new = new Bitmap(imlist_origin[0].Width, imlist_origin[0].Height);
                Bitmap movie_vector = new Bitmap(imlist_origin[0].Width, imlist_origin[0].Height);
                pictureBox_movie_left.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie_left.Image = imlist_origin[h - 1];
                }));
                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie_right.Image = imlist_origin[h];
                }));
                pictureBox_movie3.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                    pictureBox_movie3.Image = movie_vector;
                    pictureBox_movie3.Visible = true;
                }));
                
                Graphics g_vector = Graphics.FromImage(pictureBox_movie3.Image);

                LockBitmap lockmovie_origin = new LockBitmap(movie_origin);
                LockBitmap lockmovie_current = new LockBitmap(movie_current);
                LockBitmap lockmovie_new = new LockBitmap(movie_new);
                LockBitmap lockmovie_temp = new LockBitmap(movie_temp);
                LockBitmap lockmovie_originlarge = new LockBitmap(movie_originlarge);
                LockBitmap lockmovie_currentlarge = new LockBitmap(movie_currentlarge);

                lockmovie_origin.LockBits();
                lockmovie_current.LockBits();
                lockmovie_temp.LockBits();
                lockmovie_new.LockBits();
                lockmovie_originlarge.LockBits();
                lockmovie_currentlarge.LockBits();

                // write-out path of pics and txt
                path_frame = upperpath + @"\encode\" + list_framename[h] + ".TDL-MAD.tiff";
                path_txt = upperpath + @"\encode\" + list_framename[h] + ".TDL-MAD_motion_vector.txt";
                path_vector = upperpath + @"\encode\" + list_framename[h] + ".TDL-MAD_vector.tiff";

                int[] origin_r = new int[64];
                int[] origin_g = new int[64];
                int[] origin_b = new int[64];
                int[] current_r = new int[64];
                int[] current_g = new int[64];
                int[] current_b = new int[64];
                using (StreamWriter file = new StreamWriter(path_txt, true))  //output vector
                {
                    for (int n = 0; n < movie_current.Height; n += 8)
                    {
                        for (int m = 0; m < movie_current.Width; m += 8)
                        {
                            int min = 0;
                            int delta_x = 0;
                            int delta_y = 0;

                            //取第t+1圖(current)原區data,順便比對第t圖(origin)對應區                   
                            for (int lh = 0; lh < 8; lh++)
                            {
                                for (int lw = 0; lw < 8; lw++)
                                {
                                    Color currentpixel = lockmovie_current.GetPixel(m + lw, n + lh);
                                    current_r[lw * 8 + lh] = currentpixel.R; current_g[lw * 8 + lh] = currentpixel.G; current_b[lw * 8 + lh] = currentpixel.B;
                                    Color originpixel = lockmovie_origin.GetPixel(m + lw, n + lh);
                                    min += Math.Abs(currentpixel.R - originpixel.R) + Math.Abs(currentpixel.G - originpixel.G) + Math.Abs(currentpixel.B - originpixel.B);  //計算current的對應區之誤差
                                    lockmovie_currentlarge.SetPixel(lw, lh, currentpixel);
                                    lockmovie_originlarge.SetPixel(lw, lh, originpixel);
                                }
                            }
                            if (checkBox_encodedemo.Checked == true)
                            { //畫格子    
                                pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                {   //解決跨執行緒作業無效                                   
                                    pictureBox_movie_left.Refresh();
                                    g_origin.DrawRectangle(redPen, m, n, 8, 8);
                                }));
                                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                  
                                    pictureBox_movie_right.Refresh();
                                    g_current.DrawRectangle(redPen, m, n, 8, 8);
                                }));
                                pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_originlarge.UnlockBits();
                                    pictureBox_movielarge.Visible = true;
                                    pictureBox_movielarge.Refresh();
                                    lockmovie_originlarge.LockBits();
                                }));
                                pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_currentlarge.UnlockBits();
                                    pictureBox_movie2large.Visible = true;
                                    pictureBox_movie2large.Refresh();
                                    lockmovie_currentlarge.LockBits();
                                }));
                                //
                            }
                            else
                            { //畫格子    
                                pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                {   //解決跨執行緒作業無效                                   
                                    pictureBox_movie_left.Refresh();                                   
                                }));
                                pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效                                  
                                    pictureBox_movie_right.Refresh();                                    
                                }));
                                pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_originlarge.UnlockBits();
                                    pictureBox_movielarge.Visible = false;
                                    pictureBox_movielarge.Refresh();
                                    lockmovie_originlarge.LockBits();
                                }));
                                pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                    lockmovie_currentlarge.UnlockBits();
                                    pictureBox_movie2large.Visible = false;
                                    pictureBox_movie2large.Refresh();
                                    lockmovie_currentlarge.LockBits();
                                }));
                                //
                            }

                            if (min != 0)  //current在origin的對應區沒有完全相同
                            {                               
                                int start_x = movie_origin.Width / 2;
                                int start_y = movie_origin.Height / 2;
                                int new_x = movie_origin.Width / 2;
                                int new_y = movie_origin.Height / 2;
                                int s = movie_origin.Width / 4;  //jump distance
                                while (s != 1)
                                {                                  
                                    min = 255*64*3;                                  
                                    for (int i = -s; i <= s; i += s) 
                                    {
                                        for (int j = -s; j <= s; j += s)
                                        {
                                            if (start_x + i  >= 0 && start_x + i + 8 <= movie_origin.Width && start_y + j >=0 && start_y + j + 8<= movie_origin.Height)
                                            {  
                                                if (Math.Abs(i) != s || Math.Abs(j) != s)  //只找起點與起點+jump distance的上下左右點(共5點)
                                                {
                                                    int total_r = 0;
                                                    int total_g = 0;
                                                    int total_b = 0;
                                                    
                                                    for (int lh = 0; lh < 8; lh++)
                                                    {
                                                        for (int lw = 0; lw < 8; lw++)
                                                        {
                                                            Color originpixel = lockmovie_origin.GetPixel(start_x + i + lw, start_y + j + lh);
                                                            origin_r[lw * 8 + lh] = originpixel.R; origin_g[lw * 8 + lh] = originpixel.G; origin_b[lw * 8 + lh] = originpixel.B;

                                                            origin_r[lw * 8 + lh] = Math.Abs(current_r[lw * 8 + lh] - origin_r[lw * 8 + lh]);
                                                            origin_g[lw * 8 + lh] = Math.Abs(current_g[lw * 8 + lh] - origin_g[lw * 8 + lh]);
                                                            origin_b[lw * 8 + lh] = Math.Abs(current_b[lw * 8 + lh] - origin_b[lw * 8 + lh]);
                                                            lockmovie_originlarge.SetPixel(lw, lh, originpixel);
                                                        }
                                                    }
                                                    if (checkBox_encodedemo.Checked == true)
                                                    {
                                                        //畫格子                                  
                                                        pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                                        {   //解決跨執行緒作業無效                                              
                                                            pictureBox_movie_left.Refresh();
                                                            g_origin.DrawRectangle(redPen, start_x + i, start_y + j, 8, 8);
                                                        }));
                                                        pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                  
                                                            pictureBox_movie_right.Refresh();
                                                            g_current.DrawRectangle(redPen, m, n, 8, 8);
                                                        }));
                                                        pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                            lockmovie_originlarge.UnlockBits();
                                                            pictureBox_movielarge.Visible = true;
                                                            pictureBox_movielarge.Refresh();
                                                            lockmovie_originlarge.LockBits();
                                                        }));
                                                        pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                            lockmovie_currentlarge.UnlockBits();
                                                            pictureBox_movie2large.Visible = true;
                                                            pictureBox_movie2large.Refresh();
                                                            lockmovie_currentlarge.LockBits();
                                                        }));
                                                        //                                           
                                                    }
                                                    else
                                                    {
                                                        //畫格子                                  
                                                        pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                                        {   //解決跨執行緒作業無效                                              
                                                            pictureBox_movie_left.Refresh();                                                           
                                                        }));
                                                        pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                  
                                                            pictureBox_movie_right.Refresh();                                                           
                                                        }));
                                                        pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                            lockmovie_originlarge.UnlockBits();
                                                            pictureBox_movielarge.Visible = false;
                                                            pictureBox_movielarge.Refresh();
                                                            lockmovie_originlarge.LockBits();
                                                        }));
                                                        pictureBox_movie2large.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                            lockmovie_currentlarge.UnlockBits();
                                                            pictureBox_movie2large.Visible = false;
                                                            pictureBox_movie2large.Refresh();
                                                            lockmovie_currentlarge.LockBits();
                                                        }));
                                                        //                  
                                                    }
                                                    foreach (int item in origin_r) { total_r += item; }
                                                    foreach (int item in origin_g) { total_g += item; }
                                                    foreach (int item in origin_b) { total_b += item; }                                                  
                                                    if ((total_r + total_g + total_b) < min) { min = (total_r + total_g + total_b); new_x = start_x + i; new_y = start_y + j; }
                                                }                                            
                                            }                                          
                                        }
                                    }
                                    if (start_x == new_x && start_y == new_y) { s = s / 2;}
                                    start_x = new_x;  start_y = new_y;                                   
                                }
                                min = 255 * 64;
                                for (int i = -s; i <= s; i += s) 
                                {
                                        for (int j = -s; j <= s; j += s)
                                        {
                                            if (start_x + i >= 0 && start_x + i + 8 <= movie_origin.Width && start_y + j>=0 && start_y + j + 8<= movie_origin.Height)
                                            {                                                 
                                                    int total_r = 0;
                                                    int total_g = 0;
                                                    int total_b = 0;
                                                    for (int lh = 0; lh < 8; lh++)
                                                    {
                                                        for (int lw = 0; lw < 8; lw++)
                                                        {
                                                            Color originpixel = lockmovie_origin.GetPixel(start_x + i + lw, start_y + j + lh);
                                                            origin_r[lw * 8 + lh] = originpixel.R; origin_g[lw * 8 + lh] = originpixel.G; origin_b[lw * 8 + lh] = originpixel.B;

                                                            origin_r[lw * 8 + lh] = Math.Abs(current_r[lw * 8 + lh] - origin_r[lw * 8 + lh]);
                                                            origin_g[lw * 8 + lh] = Math.Abs(current_g[lw * 8 + lh] - origin_g[lw * 8 + lh]);
                                                            origin_b[lw * 8 + lh] = Math.Abs(current_b[lw * 8 + lh] - origin_b[lw * 8 + lh]);
                                                            lockmovie_originlarge.SetPixel(lw, lh, originpixel);
                                                        }
                                                    }
                                                    if (checkBox_encodedemo.Checked == true)
                                                    {
                                                        //畫格子                                  
                                                        pictureBox_movie_left.Invoke(new MethodInvoker(delegate ()
                                                        {   //解決跨執行緒作業無效                                              
                                                            pictureBox_movie_left.Refresh();
                                                            g_origin.DrawRectangle(redPen, start_x + i, start_y + j, 8, 8);
                                                        }));
                                                        pictureBox_movielarge.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效
                                                            lockmovie_originlarge.UnlockBits();
                                                            pictureBox_movielarge.Refresh();
                                                            lockmovie_originlarge.LockBits();
                                                        }));
                                                        //                                           
                                                    }
                                                    foreach (int item in origin_r) { total_r += item; }
                                                    foreach (int item in origin_g) { total_g += item; }
                                                    foreach (int item in origin_b) { total_b += item; }
                                                    if ((total_r + total_g + total_b) < min) { min = (total_r + total_g + total_b); delta_x = start_x + i - m; delta_y = start_y + j - n; }                                                                                            
                                            }                                          
                                        }
                                }

                            }
                            else { delta_x = 0; delta_y = 0; }
                            //write delta_x, delta_y                        
                            //a += Convert.ToString(delta_x) + " " + Convert.ToString(delta_y) + " ";
                            sb.Append(Convert.ToString(delta_x));
                            sb.Append(" ");
                            sb.Append(Convert.ToString(delta_y));
                            sb.Append(" ");
                            //draw vector
                            pictureBox_movie3.Invoke(new MethodInvoker(delegate ()
                            {
                                using (Pen vectorPen = new Pen(Color.Blue, 1))
                                {
                                    vectorPen.StartCap = LineCap.ArrowAnchor;
                                    vectorPen.EndCap = LineCap.RoundAnchor;
                                    g_vector.DrawLine(vectorPen, m + 4, n + 4, m + 4 + delta_x, n + 4 + delta_y);
                                    pictureBox_movie3.Image = movie_vector;
                                }
                            }));

                            //draw decode frame block                            
                            for (int lh = 0; lh < 8; lh++)
                            {
                                for (int lw = 0; lw < 8; lw++)
                                {
                                    Color originpixel = lockmovie_temp.GetPixel(m + delta_x + lw, n + delta_y + lh);
                                    lockmovie_new.SetPixel(m + lw, n + lh, originpixel);
                                }
                            }
                        }
                        //找完一列後，輸出vector
                        //write delta_x, delta_y 
                        //a = a.Substring(0, a.Length - 1); //刪除最後一個空格
                        sb.Length = sb.Length - 1;//刪除最後一個空格                  
                        file.WriteLine(sb);
                        //file.Flush();
                        sb.Clear();
                    }
                    lockmovie_origin.UnlockBits();
                    lockmovie_current.UnlockBits();
                    lockmovie_temp.UnlockBits();
                    lockmovie_new.UnlockBits();
                    lockmovie_originlarge.UnlockBits();
                    lockmovie_currentlarge.UnlockBits();
                    
                    //calculate psnr                   
                    chart_psnr.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                               
                        psnr_data[h - 1] = calculatePSNR(movie_current, movie_new);
                        chart_psnr.Series[0].Points.AddXY(h + 1, psnr_data[h - 1]);  //imlist_origin[h]是第h+1張圖,psnr_data[h-1]是imlist_origin[h]-imlist_origin[h-1]的PSNR(第h+1張圖-第h張圖)
                        chart_psnr.Series[0].Color = Color.Black;
                    }));
                    //save frame
                    movie_new.Save(path_frame);
                    movie_temp = new Bitmap(movie_new);
                    //output vector
                    pictureBox_movie3.Image.Save(path_vector);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap movie_origin;
            movie_origin = new Bitmap(imlist_origin[0]);
            int totalpic = 0;
            //所有圖片播放完後count計數歸零循環播放 
            if (Count < imlist_origin.Count)
            {                         
                pictureBox_movie_left.Image = imlist_origin[Count];
                pictureBox_movie_right.Image = imlist_new[Count];
                pictureBox_movie3.Image = imlist_vector[Count];
                //到列表中按序列取出圖片 
                Count++;  
                totalpic = imlist_origin.Count;
                label44.Text = "影片資訊:" + Count + "/" + totalpic;              
            }
            else
            {
                Count = 0;
            }
        }     
        private void Form1_Load(object sender, EventArgs e)
        {
            int fps = 30;
            t = new System.Windows.Forms.Timer() { Interval = 1000 / fps, Enabled = false };//啟動一個Timer 
            t.Tick += new EventHandler(timer1_Tick);
            comboBox_matching.SelectedIndex = 0;
        }      

        //start to play movie
        private void button_play_Click_1(object sender, EventArgs e)
        {
            t.Enabled = true;
        }

        //pause playing movie
        private void button_pause_Click_1(object sender, EventArgs e)
        {
            t.Enabled = false;
        }


        //next pic of movie pics
        private void button_playone_Click_1(object sender, EventArgs e)
        {
            int totalpic = 0;
            t.Enabled = false;
            if (Count < imlist_origin.Count-1)
            {
                Count++;
                pictureBox_movie_left.Image = imlist_origin[Count];
                pictureBox_movie_right.Image = imlist_new[Count];
                pictureBox_movie3.Image = imlist_vector[Count];
                //到列表中按序列取出圖片 

                totalpic = imlist_origin.Count;
                label44.Text = "影片資訊:" + (Count+1) + "/" + totalpic;
            }
            else
            {
                Count = 0;
                pictureBox_movie_left.Image = imlist_origin[Count];
                pictureBox_movie_right.Image = imlist_new[Count];
                pictureBox_movie3.Image = imlist_vector[Count];
                totalpic = imlist_origin.Count;
                label44.Text = "影片資訊:" + (Count+1) + "/" + totalpic;
            }
        }


        //pause encode
        private void button_encodestop_Click(object sender, EventArgs e)
        {         
            if (t_encode.ThreadState == (ThreadState.Background | ThreadState.Suspended))
            {
                t_encode.Resume();
                button_encodepause.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效
                    button_encodepause.Text = "暫停";
                }));                            
            }
            else 
            {
                t_encode.Suspend();
                button_encodepause.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效
                    button_encodepause.Text = "繼續";
                }));
            }
            //t_encode.Suspend();
        }

        //interrupt encode
        private void button_encodestop_Click_1(object sender, EventArgs e)
        {
            button_movieencode.Enabled = true;
            button_encodepause.Enabled = false;
            button_encodestop.Enabled = false;
            if (t_encode.ThreadState == (ThreadState.Background | ThreadState.Suspended)) {t_encode.Resume(); t_encode.Abort();}  //例外排除
            else {t_encode.Abort(); }
            pictureBox_movie_left.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效
                pictureBox_movie_left.Refresh();
            }));
            pictureBox_movie_right.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效
                pictureBox_movie_right.Refresh();
            }));
            pictureBox_movie3.Invoke(new MethodInvoker(delegate () {     ////解決跨執行緒作業無效               
                pictureBox_movie3.Visible = false;
            }));
            chart_psnr.Invoke(new MethodInvoker(delegate () {     //解決跨執行緒作業無效                                                              
                chart_psnr.Visible = false;               
            }));
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void button_threshold_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                Bitmap image_threshold = new Bitmap(image3.Width, image3.Height, PixelFormat.Format24bppRgb);

                // Loop through the images pixels to reset color.
                int x, y;
                for (x = 0; x < image3.Width; x++)
                {
                    for (y = 0; y < image3.Height; y++)
                    {
                        Color pixelColor = image1.getBitmap.GetPixel(x, y);
                        int gray = Convert.ToInt32(0.3 * pixelColor.R + 0.3 * pixelColor.G + 0.4 * pixelColor.B);
                        if (gray < Convert.ToInt32(numericUpDown_thres.Value))
                        {                          
                            Color newColor = Color.FromArgb(0, 0, 0);
                            image_threshold.SetPixel(x, y, newColor);
                        }
                        else
                        {
                            Color newColor = Color.FromArgb(255, 255, 255);
                            image_threshold.SetPixel(x, y, newColor);
                        }
                    }
                }
                // Set the PictureBox to display the image.
                initial_controltool();
                image3 = image_threshold;
                pictureBox_color_left.Image = image3;
                pictureBox_other_left.Image = image3;
                pictureBox_color_right.Image = null;
                pictureBox_other_right.Image = null;
            }
            else return;
        }

        private void label_other2_Click(object sender, EventArgs e)
        {

        }

        private void panel_scale_Paint(object sender, PaintEventArgs e)
        {

        }

        //Demo of encode
        private void checkBox_encodedemo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_encodedemo.Checked == true) { label_squaresize.Visible = true; numericUpDown_enlargesize.Visible = true; }
            else { label_squaresize.Visible = false; numericUpDown_enlargesize.Visible = false; }
        }

        private void numericUpDown_enlargesize_ValueChanged(object sender, EventArgs e)
        {

        }

        //previous pic of movie pics
        private void button_backone_Click(object sender, EventArgs e)
        {
            int totalpic = 0;
            t.Enabled = false;
            if (Count > 0)
            {
                Count--;
                pictureBox_movie_left.Image = imlist_origin[Count];
                pictureBox_movie_right.Image = imlist_new[Count];
                pictureBox_movie3.Image = imlist_vector[Count];
                //到列表中按序列取出圖片 

                totalpic = imlist_origin.Count;
                label44.Text = "影片資訊:" + (Count + 1) + "/" + totalpic;
            }
            else
            {
                Count = imlist_origin.Count-1;
                pictureBox_movie_left.Image = imlist_origin[Count];
                pictureBox_movie_right.Image = imlist_new[Count];
                pictureBox_movie3.Image = imlist_vector[Count];
                totalpic = imlist_origin.Count;
                label44.Text = "影片資訊:" + (Count + 1) + "/" + totalpic;
            }
        }
        
        //魔術棒
        private void button_magicwand_Click(object sender, EventArgs e)
        {
            if (image3 == null) { image3 = image1.getBitmap; }
            else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
            Bitmap temp = new Bitmap(image3);
            
            initial_controltool();          
            pictureBox_other_left.Image = temp;
            Boolean magic_choose = false;
            Point originPoint = new Point(0, 0);
            Point newPoint = new Point(0, 0);
            int origin_x = 0;
            int origin_y = 0;
            int new_x = 0;
            int new_y = 0;
            int offset_x, offset_y;
            offset_x = (pictureBox_other_left.Width - pictureBox_other_left.Image.Width) / 2;
            offset_y = (pictureBox_other_left.Height - pictureBox_other_left.Image.Height) / 2;

            Graphics g_origin = pictureBox_other_left.CreateGraphics();
            Graphics g_origin_lower = pictureBox_other_left.CreateGraphics();

            Pen redPen = new Pen(Color.Red, 3);
            pictureBox_other_left.MouseDown += new MouseEventHandler(Imagemagicwand_MouseDown);           
            pictureBox_other_left.MouseMove += new MouseEventHandler(Imagemagicwand_MouseMove);
           
            void Imagemagicwand_MouseDown(object senderDown, MouseEventArgs eDown)
            {
                
                if (pictureBox_other_left.Image != null)
                {
                    if (magic_choose == false)
                    {
                        
                        magic_choose = true;                      
                        pictureBox_other_left.Cursor = Cursors.Hand;
                        origin_x = eDown.X - offset_x;
                        origin_y = eDown.Y - offset_y;
                        originPoint = eDown.Location;
                         g_origin.DrawLine(redPen, originPoint.X - 5, originPoint.Y, originPoint.X + 5, originPoint.Y); //畫水平線
                         g_origin.DrawLine(redPen, originPoint.X, originPoint.Y - 5, originPoint.X, originPoint.Y + 5); //畫垂直線                      
                    }
                    else
                    {                  
                        pictureBox_other_left.Cursor = Cursors.Hand;
                        new_x = eDown.X - offset_x;
                        new_y = eDown.Y - offset_y;
                        newPoint = eDown.Location;
                    }
                }
                else return;
            }
            void Imagemagicwand_MouseMove(object senderDraw, MouseEventArgs eDraw)
            {               
                if (pictureBox_other_left.Image != null && magic_choose == true)
                {
                    if (eDraw.Button != MouseButtons.Left)//判断是否按下左键
                        return;
                    //線條粗細:11*11
                    for (int i = -5; i < 6; i++)
                    {
                        for (int j = -5; j < 6; j++)
                        {
                            if (origin_x + (eDraw.Location.X - offset_x - new_x) + i >= 0 && origin_x + (eDraw.Location.X - offset_x - new_x) + i < pictureBox_other_left.Image.Width
                       && origin_y + (eDraw.Location.Y - offset_y - new_y) + j >= 0 && origin_y + (eDraw.Location.Y - offset_y - new_y) + j < pictureBox_other_left.Image.Height)
                            {
                                Color origin_pixel = image3.GetPixel(origin_x + (eDraw.Location.X - offset_x - new_x) + i, origin_y + (eDraw.Location.Y - offset_y - new_y) + j);
                                if (eDraw.Location.X - offset_x + i >= 0 && eDraw.Location.X - offset_x + i < pictureBox_other_left.Image.Width
                                 && eDraw.Location.Y - offset_y + j >= 0 && eDraw.Location.Y - offset_y + j < pictureBox_other_left.Image.Height) 
                                {
                                    temp.SetPixel(eDraw.Location.X - offset_x + i, eDraw.Location.Y - offset_y + j, origin_pixel);
                                }
                                
                            }                                
                        }
                    }                   
                    pictureBox_other_left.Refresh();
                    g_origin_lower.DrawLine(redPen, originPoint.X - 5, originPoint.Y, originPoint.X + 5, originPoint.Y); //畫水平線
                    g_origin_lower.DrawLine(redPen, originPoint.X, originPoint.Y - 5, originPoint.X, originPoint.Y + 5); //畫垂直線                   
                }
            }
        }

        private void comboBox_matching_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //choose pic for bald
        private void button_baldchoose_Click(object sender, EventArgs e)
        {
            Bitmap image_bald = null;
            Bitmap image_bald2 = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//該值確定是否可以選擇多個檔案
            dialog.Title = "請選擇資料夾";
            dialog.Filter = "所有檔案(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                image_bald = new Bitmap(file);
                pictureBox_bald_left.Image = image_bald;

                float total = image_bald.Width*image_bald.Height;
                int skin = 0;
                image_bald2 = new Bitmap(image_bald);               

                LockBitmap lockimage_bald = new LockBitmap(image_bald);              
                LockBitmap lockimage_bald2 = new LockBitmap(image_bald2);
                lockimage_bald.LockBits();
                lockimage_bald2.LockBits();
                for (int i = 0; i < image_bald.Width; i++)
                {
                    for (int j =0; j < image_bald.Height; j++)
                    {
                        Color pixelColor = lockimage_bald.GetPixel(i, j);                                            
                        if (Math.Abs(pixelColor.R - pixelColor.G) < 25 && Math.Abs(pixelColor.R - pixelColor.B) < 25 || Math.Abs(pixelColor.R - pixelColor.G) >40 )
                        {                            
                            lockimage_bald2.SetPixel(i, j, Color.White);
                        }                                                
                    }
                }
                
                for (int i =0; i < image_bald2.Width; i++)
                {
                    for (int j =0; j < image_bald2.Height; j++)
                    {
                        Color pixelColor = lockimage_bald2.GetPixel(i, j);                       
                        if (pixelColor.R - pixelColor.G > 10 && pixelColor.R - pixelColor.B > 35 && pixelColor.R > 130)
                        {
                            lockimage_bald2.SetPixel(i, j, pixelColor);
                            skin++;
                        }
                        else
                        {
                            lockimage_bald2.SetPixel(i, j, Color.White);
                        }                      
                    }
                }
                total = (skin / total)*100;
                label_baldpresent.Text = total+"%";
                label_baldpresent.Visible = true;
                lockimage_bald.UnlockBits();
                lockimage_bald2.UnlockBits();
            }
            
            pictureBox_bald_right.Image = image_bald2;
        }

        //start elastic ball
        private void button_ballstart_Click(object sender, EventArgs e)
        {
            Bitmap ball_back;
            ball_back = new Bitmap(pictureBox_ball.Width, pictureBox_ball.Height);
            // Graphics ball = pictureBox_ball.CreateGraphics();
            //ball.SmoothingMode = SmoothingMode.AntiAlias;
            //Brush bush = new SolidBrush(Color.Green);
            //ball.FillEllipse(bush, pictureBox_ball.Location.X, pictureBox_ball.Location.Y, 40, 40);
            for (int i = 0; i < pictureBox_ball.Width; i++)
            {
                for (int j = 0; j < pictureBox_ball.Height; j++)
                {
                    if (Math.Pow(i - (pictureBox_ball.Width / 2), 2) + Math.Pow(j - (pictureBox_ball.Height / 2), 2) <= Math.Pow((pictureBox_ball.Width / 2), 2))
                    {
                        ball_back.SetPixel(i, j, Color.Blue);
                    }
                }
            }
            
            pictureBox_ball.Image = ball_back;
            horizontal = 2;
            vertical = 2;
            timer_ball.Enabled = true;
        }      

        int horizontal, vertical;
        //明亮度
        private void button_light_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (image3 == null) { image3 = image1.getBitmap; }
                else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
                // Set the PictureBox to display the image.
                initial_controltool();

                pictureBox_color_left.Image = image3;
                trackBar_light.Visible = true;
                trackBar_light.Value = 50;
                label_light.Visible = true;
                label_dark.Visible = true;
            }
        }


        //light bar control
        private void trackBar1_Scroll_2(object sender, EventArgs e)
        {
            Bitmap bitmap_light = new Bitmap(image3);
            int range = trackBar_light.Value;
            int r = 0, g = 0, b = 0;
            for (int i = 0;i <  bitmap_light.Width; i++) 
            {
                for (int j = 0; j < bitmap_light.Height; j++) 
                {
                    Color pixel = bitmap_light.GetPixel(i, j);
                    /*double upper_r = (255 - pixel.R)/50;
                    double upper_g = (255 - pixel.G)/50;
                    double upper_b = (255 - pixel.B)/50;
                    double lower_r = pixel.R/50;
                    double lower_g = pixel.G/50;
                    double lower_b = pixel.B/50;*/
                    
                    
                        /*r = Convert.ToInt32(pixel.R + (range - 50) * upper_r);
                        g = Convert.ToInt32(pixel.G + (range - 50) * upper_g);
                        b = Convert.ToInt32(pixel.B + (range - 50) * upper_b);*/
                        r = Convert.ToInt32(pixel.R + (range - 50)*5.1);
                        g = Convert.ToInt32(pixel.G + (range - 50)*5.1);
                        b = Convert.ToInt32(pixel.B + (range - 50)*5.1);

                    
                    
                        /* r = Convert.ToInt32(pixel.R + (range - 50) * lower_r);
                         g = Convert.ToInt32(pixel.G + (range - 50) * lower_g);
                         b = Convert.ToInt32(pixel.B + (range - 50) * lower_b);*/                       
                    
                    if (r > 255) { r = 255; }
                    if (r < 0) {r = 0; }
                    if (g > 255) { g = 255; }
                    if (g < 0) { g = 0; }
                    if (b > 255) { b = 255; }
                    if (b < 0) { b = 0; }
                    Color newpixel = Color.FromArgb(r, g, b);
                    bitmap_light.SetPixel(i, j, newpixel);
                }
            }
            pictureBox_color_left.Image = bitmap_light;
        }

        //pause elastic ball
        private void button_ballpause_Click(object sender, EventArgs e)
        {
            if (timer_ball.Enabled == true)
            {
                timer_ball.Enabled = false;
            }          
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void panel_data_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer_ball_Tick(object sender, EventArgs e)
        {         
            pictureBox_ball.Left += vertical;
            pictureBox_ball.Top += horizontal;
            if (pictureBox_ball.Location.X + pictureBox_ball.Width + vertical/2 > tabPage10.Width || pictureBox_ball.Location.X+ vertical/2 < 0) { vertical = -vertical; }
            if (pictureBox_ball.Location.Y + pictureBox_ball.Height + horizontal/2 > tabPage10.Height|| pictureBox_ball.Location.Y + horizontal/2 < 0) { horizontal = -horizontal; }                  
        }

        private void checkedListBox_subregion_SelectedIndexChanged(object sender, EventArgs e)
        {                              
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            Rectangle circle = new Rectangle(0, 0, 0, 0);
            //circle
            Point heartPoint = new Point(0, 0);
            int r = 0; // 半徑
           
            int offset_x, offset_y;
            offset_x = (pictureBox_other_left.Width - pictureBox_other_left.Image.Width) / 2;
            offset_y = (pictureBox_other_left.Height - pictureBox_other_left.Image.Height) / 2;

            
                    
            if (checkedListBox_subregion.GetItemChecked(0) == true && checkedListBox_subregion.GetItemChecked(1) == false)
            {               
                    pictureBox_other_left.MouseDown += new MouseEventHandler(Image_MouseDown);
                    pictureBox_other_left.MouseUp += new MouseEventHandler(Image_MouseUp);
                    pictureBox_other_left.MouseMove += new MouseEventHandler(Imagerect_MouseMove);
                    pictureBox_other_left.Paint += new PaintEventHandler(Imagerect_Paint);
                
            }               

            
                         
                else if (checkedListBox_subregion.GetItemChecked(1) == true && checkedListBox_subregion.GetItemChecked(0) == false)
                {                 
                pictureBox_other_left.MouseDown += new MouseEventHandler(Imagecircle_MouseDown);
                pictureBox_other_left.MouseUp += new MouseEventHandler(Imagecircle_MouseUp);
                pictureBox_other_left.MouseMove += new MouseEventHandler(Imagecircle_MouseMove);
                pictureBox_other_left.Paint += new PaintEventHandler(Imagecircle_Paint);
                
                }
            

            //rect
            void Image_MouseDown(object senderDown, MouseEventArgs eDown)
            {             
                if (checkedListBox_subregion.GetItemChecked(0) == true && checkedListBox_subregion.GetItemChecked(1) == false)
                {
                    if (pictureBox_other_left.Image != null)
                    {
                        pictureBox_other_left.Cursor = Cursors.Hand;
                        IsSelected_subregion = true;
                        start_x = eDown.X - offset_x;
                        start_y = eDown.Y - offset_y;                      
                    }
                    else return;
                }
            }
            void Image_MouseUp(object senderUp, MouseEventArgs eUp)
            {
                if (checkedListBox_subregion.GetItemChecked(0) == true && checkedListBox_subregion.GetItemChecked(1) == false)
                {
                    if (pictureBox_other_left.Image != null && IsSelected_subregion == true)
                    {
                        IsSelected_subregion = false;
                        pictureBox_other_left.Cursor = Cursors.Default;
                        end_x = eUp.X - offset_x;
                        end_y = eUp.Y - offset_y;                       
                    }
                    else return;
                    draw_sub(start_x, start_y, end_x, end_y);
                    
                }
            }           
            void Imagerect_MouseMove(object senderDraw, MouseEventArgs eDraw)
            {
                if (checkedListBox_subregion.GetItemChecked(0) == true && checkedListBox_subregion.GetItemChecked(1) == false)
                {
                    if (eDraw.Button != MouseButtons.Left)//判断是否按下左键
                        return;
                    Point tempEndPoint = eDraw.Location; //紀錄框的位置和大小
                    rect = new Rectangle(Math.Min(start_x + offset_x, tempEndPoint.X), Math.Min(start_y + offset_y, tempEndPoint.Y), Math.Abs(start_x + offset_x - tempEndPoint.X), Math.Abs(start_y + offset_y - tempEndPoint.Y));
                    pictureBox_other_left.Invalidate();
                }
            }

            void Imagerect_Paint(object senderDraw, PaintEventArgs eDraw)
            {
                if (checkedListBox_subregion.GetItemChecked(0) == true && checkedListBox_subregion.GetItemChecked(1) == false)
                {
                    if (rect != null && rect.Width > 0 && rect.Height > 0)
                    {
                        Pen redPen = new Pen(Color.Red, 3);
                        eDraw.Graphics.DrawRectangle(redPen, rect);                      
                        //eDraw.Graphics.DrawEllipse(redPen, rect);//橢圓
                    }
                }

            }
            //circle    
            void Imagecircle_MouseDown(object senderDown, MouseEventArgs eDown)
            {              
                if (checkedListBox_subregion.GetItemChecked(1) == true && checkedListBox_subregion.GetItemChecked(0) == false)
                {
                    if (pictureBox_other_left.Image != null)
                    {
                        pictureBox_other_left.Cursor = Cursors.Hand;
                        IsSelected_subregion = true;
                        start_x = eDown.X - offset_x;
                        start_y = eDown.Y - offset_y;                      
                    }
                    else return;
                }
            }
            void Imagecircle_MouseUp(object senderUp, MouseEventArgs eUp)
            {
                if (checkedListBox_subregion.GetItemChecked(1) == true && checkedListBox_subregion.GetItemChecked(0) == false)
                {
                    if (pictureBox_other_left.Image != null && IsSelected_subregion == true)
                    {
                        IsSelected_subregion = false;
                        pictureBox_other_left.Cursor = Cursors.Default;
                        end_x = eUp.X - offset_x;
                        end_y = eUp.Y - offset_y;
                    }
                    else return;
                    draw_subcircle(start_x, start_y, end_x, end_y, heartPoint, r);
                    
                }
            }
            void Imagecircle_MouseMove(object senderDraw, MouseEventArgs eDraw)
            {
                if (checkedListBox_subregion.GetItemChecked(1) == true && checkedListBox_subregion.GetItemChecked(0) == false)
                {
                    if (eDraw.Button != MouseButtons.Left)//判断是否按下左键
                        return;
                    Point tempEndPoint = eDraw.Location; //紀錄框的位置和大小                   
                    r = Convert.ToInt32((Math.Pow(Math.Pow(Math.Abs(start_x + offset_x - tempEndPoint.X), 2) + Math.Pow(Math.Abs(start_y + offset_y - tempEndPoint.Y), 2), 0.5)) / 2); //半徑
                    heartPoint = new Point((start_x + offset_x + tempEndPoint.X) / 2, (start_y + offset_y + tempEndPoint.Y) / 2);
                    circle = new Rectangle(Convert.ToInt32(heartPoint.X - r), Convert.ToInt32(heartPoint.Y - r), Convert.ToInt32(2 * r), Convert.ToInt32(2 * r));
                    pictureBox_other_left.Invalidate();
                }
            }

            void Imagecircle_Paint(object senderDraw, PaintEventArgs eDraw)
            {
                if (checkedListBox_subregion.GetItemChecked(1) == true && checkedListBox_subregion.GetItemChecked(0) == false)
                {
                    if (circle != null && circle.Width > 0 && circle.Height > 0)
                    {
                        Pen redPen = new Pen(Color.Red, 3);
                        eDraw.Graphics.DrawEllipse(redPen, circle);
                    }
                }

            }          
        }

        //選取區塊
        private void subregion_Click(object sender, EventArgs e)
        {
            if (image3 == null) { image3 = image1.getBitmap; }
            else if (image_forscale != null) { image3 = image_forscale; image_forscale = null; }
            pictureBox_other_left.Image = image3;
            initial_controltool();
            checkedListBox_subregion.Visible = true;                          
        }       

        //draw rect
        private void draw_sub(int start_x, int start_y,int end_x,int end_y)
        {
            if (Math.Abs(end_x - start_x) == 0 || Math.Abs(end_y - start_y) == 0) { return; }
            /*Bitmap image_subregion = new Bitmap(Math.Abs(end_x - start_x), Math.Abs(end_y - start_y));*/
            Bitmap image_subregion = new Bitmap(pictureBox_other_left.Image.Width, pictureBox_other_left.Image.Height);
            int x, y;
            for (x = Math.Min(start_x, end_x); x < Math.Max(start_x, end_x); x++)
            {
                for (y = Math.Min(start_y, end_y); y < Math.Max(start_y, end_y); y++)
                {
                    if (x >= 0 && y >= 0 && x < image3.Width && y < image3.Height)
                    {
                        Color pixelColor = image3.GetPixel(x, y);
                        image_subregion.SetPixel(x, y, pixelColor);
                    }

                }
            }
            //show pic
            pictureBox_other_right.Image = image_subregion;
        }

        //draw circle
        private void draw_subcircle(int start_x, int start_y, int end_x, int end_y,Point heart,int r)
        {
            int offset_x, offset_y;
            offset_x = (pictureBox_other_left.Width - pictureBox_other_left.Image.Width) / 2;
            offset_y = (pictureBox_other_left.Height - pictureBox_other_left.Image.Height) / 2;
            if (Math.Abs(end_x - start_x) == 0 || Math.Abs(end_y - start_y) == 0) { return; }         
            Bitmap image_subregion = new Bitmap(pictureBox_other_left.Image.Width, pictureBox_other_left.Image.Height);
            int x, y;
            for (x = heart.X - r; x <= heart.X + r; x++)
            {
                for (y = heart.Y - r; y <= heart.Y + r; y++)
                {
                    if (x >= 0 && y >= 0 && Math.Pow(x  - heart.X,2) + Math.Pow(y - heart.Y, 2) <= Math.Pow(r,2))
                    {
                        if (x - offset_x >= 0 && y - offset_y >= 0 && x - offset_x < image3.Width && y - offset_y < image3.Height)
                        {
                            Color pixelColor = image3.GetPixel(x - offset_x, y - offset_y);
                            image_subregion.SetPixel(x - offset_x, y - offset_y, pixelColor);
                        }
                    }
                }
            }
            //show pic
            pictureBox_other_right.Image = image_subregion;            
        }
    }
}
