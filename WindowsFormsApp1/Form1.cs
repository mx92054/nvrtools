gusing System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool[] bCameraRunning;
        private VideoCapture[] cap;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bCameraRunning = new bool[] { false, false, false, false };
            cap = new VideoCapture[4];
            for (int i = 0; i < 4; i++)
                cap[i] = new VideoCapture();

            cbPosition.SelectedIndex = 0;
        }
        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnAddCamera_Click(object sender, EventArgs e)
        {
            int no = cbPosition.SelectedIndex;
            if (bCameraRunning[no])
                cap[no].Release();
            
            try
            {
                cap[no].Open(0);
                bCameraRunning[no] = true;
            }
            catch( Exception ex)
            {
                label1.Text = ex.ToString();
            }
        }
        private void BtnRemoveCameara_Click(object sender, EventArgs e)
        {
            int no = cbPosition.SelectedIndex;
            if ( bCameraRunning[no])
            {
                cap[no].Release();
                bCameraRunning[no] = false;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Mat frame = new Mat();

            for(int i = 0; i < 4; i++)
            {
                if ( bCameraRunning[i])
                {
                    cap[i].Read(frame);
                    switch(i)
                    {
                        case 0:
                            pictureBox1.Image = BitmapConverter.ToBitmap(frame);
                            break;
                        case 1:
                            pictureBox2.Image = BitmapConverter.ToBitmap(frame);
                            break;
                        case 2:
                            pictureBox3.Image = BitmapConverter.ToBitmap(frame);
                            break;
                        case 3:
                            pictureBox4.Image = BitmapConverter.ToBitmap(frame);
                            break;
                    }
                }
            }
        }


    }
}
