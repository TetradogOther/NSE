using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSE_Framework.Controls
{

    public partial class SelectColor : UserControl
    {

        public Editor Editor;
        int EditorIndex;
        public event EventHandler SelectedColorChanged;
        public event EventHandler SelectedEditorChanged;
        public event EventHandler Redrawn;
        public event EventHandler Refreshed;

        bool trans = true;
        public bool ShowBackground
        {
            get { return trans; }
            set
            {
                if (value == true)
                {
                    base.BackgroundImage = Properties.Resources.transparent;
                }
                else
                {
                    base.BackgroundImage = null;
                }
                trans = value;
            }

        }
        public bool AllowSwitchSwap = true;
        
        [BrowsableAttribute(false)]
        public byte Index
        {
            get {
                if (this.Editor != null)
                {
                    return Editor.SelectedColorIndex;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (this.Editor != null)
                {
                    if (value < Editor.CurrentSprite.Palette.Colors.Length)
                    {
                        if (value != Editor.SelectedColorIndex)
                        {
                            Editor.SelectedColorIndex = value;

                            if (SelectedColorChanged != null)
                            {
                                SelectedColorChanged(this,new EventArgs());
                            }

                            int a = this.Size.Width / 16;
                            int b = this.Size.Height / 16;

                            Point p = new Point((value % 16) * a, (value - (value % 16)) / 16 * b);
                            Colors.Refresh();
                            Graphics g = this.Colors.CreateGraphics();
                            g.DrawRectangle(new Pen(Color.Red, 1), p.X, p.Y, a - 1, b - 1);
                            if (a > 4 && b > 4)
                            {
                                g.DrawRectangle(new Pen(Color.Black, 1), p.X + 1, p.Y + 1, a - 3, b - 3);
                            }
                        }
                    }
                }
            }
        }
        private ushort nIndex = ushort.MaxValue;
        Bitmap bm;

        public SelectColor()
        {
            InitializeComponent();
        }
        public SelectColor(ref Editor Editor)
        {
            InitializeComponent();
            
            if (Editor.CurrentSprite.Palette != null)
            {
                LoadEditor(ref Editor);
            }

        }

        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                if (value != null)
                {
                    base.BackgroundImage = value;
                    
                }
                else
                {
                    if (trans == true)
                    {
                        base.BackgroundImage = Properties.Resources.transparent;
                    }
                    
                }
                
            }
        }
        public void LoadEditor(ref NSE_Framework.Controls.Editor Editor)
        {          
            if (Editor.CurrentSprite.Palette != null)
            {
                this.Editor = Editor;
                this.Editor.ParentSelectColor = this;
                Draw();

                if (SelectedEditorChanged != null)
                {
                    SelectedEditorChanged(this, new EventArgs());
                }

            }
        }

        public override void Refresh()
        {
            base.Refresh();
            Colors.Refresh();


            short a = (short)(this.Size.Width / 16);
            short b = (short)(this.Size.Height / 16);

            Point px = new Point((Index % 16) * a, (Index - (Index % 16)) / 16 * b);
            
            if (Colors.IsDisposed == true)
            {

            }
            Graphics gx = this.Colors.CreateGraphics();
            gx.DrawRectangle(new Pen(Color.Red, 1), px.X, px.Y, a - 1, b - 1);
            if (a > 4 && b > 4)
            {
                gx.DrawRectangle(new Pen(Color.Black, 1), px.X + 1, px.Y + 1, a - 3, b - 3);
            }

            if (Refreshed != null)
            {
                Refreshed(this, new EventArgs());
            }

        }

        public void Redraw()
        {
            Draw();
            if (Redrawn != null)
            {
                Redrawn(this, new EventArgs());
            }
        }

        private void Draw()
        {
            short a = (short)(this.Size.Width / 16);
            short b = (short)(this.Size.Height / 16);

            bm = new Bitmap(a*16, b*16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bm);

            short x = 0;
            short y = 0;

            foreach (NSE_Framework.Data.GBAcolor p in this.Editor.CurrentSprite.Palette.Colors)
            {
                g.FillRectangle(new SolidBrush(p.Color), x, y, a, b);
                if (x < a * 15)
                {
                    x += a;
                }
                else
                {
                    x = 0;

                    y += b;
                }
            }

            Colors.Image = bm;

            Point px = new Point((Index % 16) * a, (Index - (Index % 16)) / 16 * b);
            Colors.Refresh();
            if(Colors.IsDisposed == true)
            {
                throw new Exception("The Colors Component of this select color control has been disposed.\nPlease report to Link12552. -Thanks!");               
            }
            Graphics gx = this.Colors.CreateGraphics();
            gx.DrawRectangle(new Pen(Color.Red, 1), px.X, px.Y, a - 1, b - 1);
            if (a > 4 && b > 4)
            {
                gx.DrawRectangle(new Pen(Color.Black, 1), px.X + 1, px.Y + 1, a - 3, b - 3);
            }
        }

        private void Colors_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Editor.CurrentSprite.Palette != null)
            {

                Point p = this.Colors.PointToClient(Cursor.Position);

                int a = this.Size.Width / 16;
                int b = this.Size.Height / 16;
                p.X -= p.X % a;
                p.Y -= p.Y % b;
                if ((p.Y / b * 16 + p.X / a) < this.Editor.CurrentSprite.Palette.Colors.Length)
                {
                    //Colors.Refresh();
                    //Graphics g = this.Colors.CreateGraphics();
                    //g.DrawRectangle(new Pen(Color.Red, 1), p.X, p.Y, a-1, b-1);
                    //if (a > 4 && b > 4)
                    //{
                    //    g.DrawRectangle(new Pen(Color.Black, 1), p.X + 1, p.Y + 1, a - 3, b - 3);
                    //}

                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        nIndex = (byte)(p.Y / b * 16 + p.X / a);                      
                    }
                    else
                    {
                        Index = (byte)(p.Y / b * 16 + p.X / a);
                    }

                }
            }

        }


        private void switchColors_Click(object sender, EventArgs e)
        {
            byte[] oldColor = Editor.CurrentSprite.Palette.Colors[Index].Bytes;

            Editor.CurrentSprite.Palette.Colors[Index] = new Data.GBAcolor(Editor.CurrentSprite.Palette.Colors[nIndex].Bytes);
            Editor.CurrentSprite.Palette.Colors[nIndex] = new Data.GBAcolor(oldColor);

            if (Editor.CurrentSprite.Type == Data.Sprite.SpriteType.Color256)
            {
                #region 256color
                for (int i = 0; i < Editor.CurrentSprite.ImageData.Length; i++)
                {
                    if (Editor.CurrentSprite.ImageData[i] == Index)
                    {
                        Editor.CurrentSprite.ImageData[i] = (byte)nIndex;
                    }
                    else if (Editor.CurrentSprite.ImageData[i] == nIndex)
                    {
                        Editor.CurrentSprite.ImageData[i] = Index;
                    }
                }
                #endregion
            }
            else if (Editor.CurrentSprite.Type == Data.Sprite.SpriteType.Color16)
            {
                #region 16color
                for (int i = 0; i < Editor.CurrentSprite.ImageData.Length; i++)
                {
                    byte r = (byte)(Editor.CurrentSprite.ImageData[i] & 0x0F);
                    byte l = (byte)((Editor.CurrentSprite.ImageData[i] & 0x0F0) >> 4);

                    if (r == Index)
                    {
                        r = (byte)nIndex;
                    }
                    else if (r == nIndex)
                    {
                        r = Index;
                    }

                    if (l == Index)
                    {
                        l = (byte)nIndex;
                    }
                    else if (l == nIndex)
                    {
                        l = Index;
                    }

                    Editor.CurrentSprite.ImageData[i] = (byte)((l << 4) | r);

                }
                #endregion
            }
                
            Redraw();
            Editor.Redraw();     
        }
        private void swapColors_Click(object sender, EventArgs e)
        {
            byte[] oldColor = Editor.CurrentSprite.Palette.Colors[Index].Bytes;

            Editor.CurrentSprite.Palette.Colors[Index] = new Data.GBAcolor(Editor.CurrentSprite.Palette.Colors[nIndex].Bytes);
            Editor.CurrentSprite.Palette.Colors[nIndex] = new Data.GBAcolor(oldColor);

            Redraw();
            Editor.Redraw(); 
        }
    }
}
