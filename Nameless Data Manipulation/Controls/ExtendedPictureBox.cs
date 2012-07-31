using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace NSE_Framework.Controls
{
    public class ExtendedPictureBox : PictureBox
    {

        private InterpolationMode m_InterpolationMode;

        public ExtendedPictureBox()
        {
            this.InterpolationMode = InterpolationMode.NearestNeighbor;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            if(this.Image != null)
            {
                e.Graphics.InterpolationMode = m_InterpolationMode;

                RectangleF sourceRectangle = new RectangleF(-0.5f, -0.5f, (float)this.Image.Width, (float)this.Image.Height);

                // This rectangle defines where we want to draw on the control

                RectangleF destinationRectangle;

                int scale = (this.Size.Width / this.Image.Size.Width + this.Size.Height / this.Image.Size.Height) / 2;
                if (scale == 1 || (scale & 0x1) == 0)
                {
                    destinationRectangle = new RectangleF(0.0f, 0.0f, (float)this.Size.Width, (float)this.Size.Height);
                }
                else
                {
                    destinationRectangle = new RectangleF(-0.5f, -0.5f, (float)this.Size.Width, (float)this.Size.Height);
                }


                e.Graphics.DrawImage(this.Image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }

            
            //base.OnPaint(e); 
        } 

        [Description("The interpolation mode the PictureBox uses to draw its image"), Category("Behavior")]
        public InterpolationMode InterpolationMode 
        { 
            get { return this.m_InterpolationMode;	 } 
            set { 
                this.m_InterpolationMode = value;
                this.Invalidate(); 
            }
        }
    }
}
