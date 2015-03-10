//Form1.cs 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        NotifyIcon snapshotIdle;
        Icon activeIcon;
        Icon idleIcon;


        public Form1()


        {
            
            InitializeComponent();
            
            
         

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            idleIcon = new Icon("idle1Icon.ico");
            activeIcon = new Icon("active1Icon.ico");
            snapshotIdle = new NotifyIcon();
            snapshotIdle.Icon = idleIcon;
            snapshotIdle.Visible = true;
            this.ShowInTaskbar = false;
            MenuItem function = new MenuItem("Take Snapshot");

            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(function);
            snapshotIdle.ContextMenu = ContextMenu;

            snapshotIdle.Click += snapshotIdle_Click;

         
        }

        void snapshotIdle_Click(object sender, EventArgs e)
        {
            
            timer1.Start();
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        public void screenshot()
        {

            //Telling the form the boundaries of the document
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Telling the form that the graphics is an image from bitmap
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            //Telling the graphic to copy the image
            // in this case it is the screenshot of the primary screen
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            //Telling the picture box to stretch the image so that it doesn't look to small.
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //Telling the form what to put inside the picture box
            //In this case it is the bitmap that we defined earlier.
            pictureBox1.Image = bitmap;
            var itembubble = new NotifyIcon(this.components);
            itembubble.Visible = true;
            itembubble.Icon = activeIcon;
            itembubble.ShowBalloonTip(3000, "StartShot", "Screenshot Taken", ToolTipIcon.None);
          
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            //bitmap.save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            Thread pic_thread = new Thread(screenshot);
            pic_thread.Start();
            timer1.Stop();

            #region savefile 
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }

                pictureBox1.Image.Save(sfd.FileName, format);
            }

            #endregion

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }



       




    }
}
