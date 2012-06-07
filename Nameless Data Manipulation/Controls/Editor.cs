using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NSE_Framework.Controls
{
    public partial class Editor : UserControl
    {
        public event EventHandler CurrentIndexChanged;
        public event EventHandler Redrawn;
        public event EventHandler UndoneAction;
        public event EventHandler RedoneAction;
        public event EventHandler ModeChanged;
        public event EventHandler ZoomChanged;
        public event EventHandler Modified;

        public SelectColor ParentSelectColor;



        bool ces = true;
        [DescriptionAttribute("Whether or not NSE will compress sprites while exporting. Setting to false will increase speeds.")]
        public bool CompressExportedSprites
        {
            get { return ces; }
            set { ces = value; }
        }

        public Editor()
        {
            sprites = new List<Data.Sprite>();
            UndoBuffer = new List<byte[]>();
            RedoBuffer = new List<byte[]>();

            InitializeComponent();

        }

        public Editor(NSE_Framework.Data.Sprite Sprite)
        {
            InitializeComponent();
            index = 0;
            sprites = new List<Data.Sprite>() { Sprite };
            if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (Sprite.CompressedSprite != -1)
                {
                    this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " Compressed @ " + Sprite.CompressedSprite.ToString() + " bytes (4bpp)";
                }
                else
                {
                    this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " @ " + (Sprite.Width * Sprite.Height * 32) + " bytes (4bpp)";
                }

            }
            else if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {

                if (Sprite.CompressedSprite != -1)
                {
                    this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " Compressed @ " + Sprite.CompressedSprite.ToString() + " bytes (8bpp)";
                }
                else
                {
                    this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " @ " + (Sprite.Width * Sprite.Height * 64) + " bytes (8bpp)";
                }
            }
            this.Mode = EditMode.Pencil;

            EditBox.Size = new Size(Sprite.Width * zoom * 8, Sprite.Height * zoom * 8);

            AlignBox();

            bitmap = new Bitmap(Sprite.Width * 8, Sprite.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Draw.uDrawData(ref bitmap, Sprite.ImageData, Sprite.Palette, new Size(Sprite.Width, Sprite.Height));
            
            this.EditBox.Image = bitmap;
            this.EditBox.BackColor = Sprite.Palette.Colors[0].Color;
            DrawRulers();
            UndoBuffer = new List<byte[]>();
            RedoBuffer = new List<byte[]>();


        }

        [DescriptionAttribute("Whether or not to display the rulers.")]
        public bool RulersVisible
        {
            set
            {
                RulerX.Visible = value;
                RulerY.Visible = value;
            }

            get { return RulerX.Visible; }
        }

        [BrowsableAttribute(false)]
        [NonSerialized]
        List<NSE_Framework.Data.Sprite> sprites = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public List<NSE_Framework.Data.Sprite> Sprites
        {
            get { return sprites; }

            set
            {
                if (value != null)
                {
                    if (value.Count == 0)
                    {
                        index = -1;
                        sprites = value;
                    }
                    else if (index >= value.Count)
                    {
                        CurrentIndex = value.Count - 1;
                        sprites = value;
                    }
                    else if (index == -1 && value.Count > 0)
                    {
                        CurrentIndex = 0;
                        sprites = value;

                    }
                }
                else
                {
                    sprites = value;
                    index = -1;
                }
            }

        }
        int index = -1;

        [BrowsableAttribute(false)]
        public int CurrentIndex
        {
            get
            {
                if (sprites != null)
                {
                    if (sprites.Count == 0)
                    {
                        index = -1;
                    }
                }
                else
                {
                    index = -1;
                }
                return index; 
            }
            set
            {
                if (value >= -1 && value < sprites.Count)
                {
                    index = value;

                    if (index != -1)
                    {
                        if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                        {
                            if (CurrentSprite.CompressedSprite != -1)
                            {
                                this.Text = "Sprite: 0x" + CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + CurrentSprite.CompressedSprite.ToString() + " bytes (4bpp)";
                            }
                            else
                            {
                                this.Text = "Sprite: 0x" + CurrentSprite.ImageOffset.ToString("X") + " @ " + (CurrentSprite.Width * CurrentSprite.Height * 32) + " bytes (4bpp)";
                            }

                        }
                        else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                        {

                            if (CurrentSprite.CompressedSprite != -1)
                            {
                                this.Text = "Sprite: 0x" + CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + CurrentSprite.CompressedSprite.ToString() + " bytes (8bpp)";
                            }
                            else
                            {
                                this.Text = "Sprite: 0x" + CurrentSprite.ImageOffset.ToString("X") + " @ " + (CurrentSprite.Width * CurrentSprite.Height * 64) + " bytes (8bpp)";
                            }
                        }
                        

                        EditBox.Size = new Size(CurrentSprite.Width * zoom * 8, CurrentSprite.Height * zoom * 8);

                        AlignBox();

                        bitmap = new Bitmap(CurrentSprite.Width * 8, CurrentSprite.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        Draw.uDrawData(ref bitmap, CurrentSprite.ImageData, CurrentSprite.Palette, new Size(CurrentSprite.Width, CurrentSprite.Height));
                        this.EditBox.Image = bitmap;
                        this.EditBox.BackColor = CurrentSprite.Palette.Colors[0].Color;
                        DrawRulers();

                        if (CurrentIndexChanged != null)
                        {
                            CurrentIndexChanged(this, new EventArgs());
                        }

                        if (this.ParentSelectColor != null && this.ParentSelectColor.IsDisposed == false)
                        {
                            this.ParentSelectColor.Redraw();
                        }

                        UndoBuffer.Clear();
                        RedoBuffer.Clear();
                    }

                }
            }

        }

        [BrowsableAttribute(false)]
        public NSE_Framework.Data.Sprite CurrentSprite
        {
            get {
                if (index != -1)
                {
                    return sprites[index];
                }
                else
                {
                    return null;
                }
                
            }
            set
            {
                if (index != -1)
                {
                    sprites[index] = value;
                }
                else if (value != null) 
                {
                    if (sprites != null)
                    {
                        if (sprites.Count == 0)
                        {
                            sprites.Add(value);
                            CurrentIndex = 0;
                        }
                        else
                        {
                            throw new Exception("The CurrentIndex was not set");
                        }
                    }
                    else
                    {
                        sprites = new List<Data.Sprite>();
                        sprites.Add(value);
                        CurrentIndex = 0;
                    }
                }

            }
        }

        public void Redraw()
        {
            Draw.uDrawData(ref bitmap, CurrentSprite.ImageData, CurrentSprite.Palette, new Size(CurrentSprite.Width, CurrentSprite.Height));
            this.EditBox.Image = bitmap;
            this.EditBox.Invalidate();

            if (CurrentSprite.Palette.Colors[0].Color != this.EditBox.BackColor)
            {
                this.EditBox.BackColor = CurrentSprite.Palette.Colors[0].Color;
            }

            this.EditBox.Invalidate();

            if (Redrawn != null)
            {
                Redrawn(this, new EventArgs());
            }
        }

        byte selectedColor = 0;
        [BrowsableAttribute(false)]
        public byte SelectedColorIndex
        {
            get
            {
                return selectedColor;
            }
            set
            {
                if (CurrentSprite != null)
                {
                    if (CurrentSprite.Type == Data.Sprite.SpriteType.Color16 && value <16)
                    {
                        selectedColor = value;

                        Graphics gx = Graphics.FromImage(RulerX.BackgroundImage);

                        gx.DrawRectangle(Pens.Gray, 1, 1, 12, 12);
                        gx.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[value].Color), 3, 3, 9, 9);
                        RulerX.Refresh();

                        if (this.ParentSelectColor != null && this.ParentSelectColor.IsDisposed == false)
                        {
                            ParentSelectColor.Refresh();
                        }

                    }
                    else if (CurrentSprite.Type == Data.Sprite.SpriteType.Color256)
                    {
                        selectedColor = value;

                        Graphics gx = Graphics.FromImage(RulerX.BackgroundImage);

                        gx.DrawRectangle(Pens.Gray, 1, 1, 12, 12);
                        gx.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[value].Color), 3, 3, 9, 9);
                        RulerX.Refresh();

                        if (this.ParentSelectColor != null && this.ParentSelectColor.IsDisposed == false)
                        {
                            ParentSelectColor.Refresh();
                        }
                    }
                }

                
            }
        }

        public enum EditMode
        {
            None,
            Pencil,
            Brush,
            Bucket,
            Eyedropper,
            Selection
        }

        int brushstroke = 0;
        [BrowsableAttribute(false)]
        public int BrushStroke
        {
            get { return brushstroke; }

            set
            {
                if (value <= 7)
                {
                    brushstroke = value;

                    if (mode == EditMode.Brush)
                    {
                        //Bitmap cbm = new Bitmap(zoom * (2 * brushstroke + 3), zoom * (2 * brushstroke + 3));
                        //Graphics g = Graphics.FromImage(cbm);
                        //Pen p = new Pen(Color.Gray);
                        //p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        //g.DrawRectangle(p, 0, 0, zoom * (2 * brushstroke + 3) - 1, zoom * (2 * brushstroke + 3) - 1);
                        //g.DrawRectangle(Pens.DarkGray, 1, 1, zoom * (2 * brushstroke + 3) - 3, zoom * (2 * brushstroke + 3) - 3);
                        //g.DrawLine(Pens.Gray, zoom * (2 * brushstroke + 3) / 2 - 2, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 + 2, zoom * (2 * brushstroke + 3) / 2);
                        //g.DrawLine(Pens.Gray, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 + 2, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 - 2);
                        //EditBox.Cursor = CreateCursor(cbm, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2);

                        EditBox.Cursor = CursorCreator.iMakeBrush(brushstroke, zoom);
                    }
                }
            }
        }




        EditMode mode;
        public EditMode Mode
        {
            get { return mode; }
            set 
            { 
                mode = value;

                if (mode == EditMode.Eyedropper)
                {
                    EditBox.Cursor = CursorCreator.MakeImage(NSE_Framework.Properties.Resources.eyedropper, 0, 16);
                }
                else if (mode == EditMode.Bucket)
                {
                    EditBox.Cursor = CursorCreator.MakeImage(NSE_Framework.Properties.Resources.bucket, 0, 13);
                }
                else if (mode == EditMode.Brush)
                {

                    EditBox.Cursor = CursorCreator.iMakeBrush(brushstroke, zoom);
                }
                else if (mode == EditMode.Pencil && zoom > 3)
                {
                    EditBox.Cursor = CursorCreator.iMakePencil(zoom);
                }
                else if (mode == EditMode.Selection)
                {
                    EditBox.Cursor = Cursors.Cross;

                    Rectangle rc = new Rectangle();

                    // Convert the points to screen coordinates.
                    Point p1 = new Point(5, 5); //PointToScreen(new Point(5, 5));
                    Point p2 = new Point(100, 100); //PointToScreen(new Point(55, 55));
                    // Normalize the rectangle.
                    if (p1.X < p2.X)
                    {
                        rc.X = p1.X;
                        rc.Width = p2.X - p1.X;
                    }
                    else
                    {
                        rc.X = p2.X;
                        rc.Width = p1.X - p2.X;
                    }
                    if (p1.Y < p2.Y)
                    {
                        rc.Y = p1.Y;
                        rc.Height = p2.Y - p1.Y;
                    }
                    else
                    {
                        rc.Y = p2.Y;
                        rc.Height = p1.Y - p2.Y;
                    }
                    // Draw the reversible frame.
                    ControlPaint.DrawReversibleFrame(rc,
                                    Color.Black, FrameStyle.Dashed);
                }
                else
                {
                    EditBox.Cursor = Cursors.Arrow;
                }

                if (ModeChanged != null)
                {
                    ModeChanged(this, new EventArgs());
                }
            }
        }
        public EditMode PreviousMode;

        Bitmap bitmap;

        int zoom = 1;
        [BrowsableAttribute(false)]
        public int Zoom
        {
            get { return zoom; }
            set
            {

                if (value > 0 && value <= 10)
                {
                    if (CurrentSprite != null)
                    {
                        //bitmap = new Bitmap(CurrentSprite.Width * 8, CurrentSprite.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        //Draw.DrawData(ref bitmap, CurrentSprite.ImageData, CurrentSprite.Palette, new Size(CurrentSprite.Width, CurrentSprite.Height), new Point(0, 0), 1);
                        //this.EditBox.Image = bitmap;
                        EditBox.Size = new Size(CurrentSprite.Width * value * 8, CurrentSprite.Height * value * 8);
                        zoom = value;

                        AlignBox();


                        DrawRulers();
                        this.Invalidate();


                        if (mode == EditMode.Brush)
                        {
                            //Bitmap cbm = new Bitmap(zoom * (2 * brushstroke + 3), zoom * (2 * brushstroke + 3));
                            //Graphics g = Graphics.FromImage(cbm);
                            //g.DrawRectangle(Pens.Gray, 0, 0, zoom * (2 * brushstroke + 3) - 1, zoom * (2 * brushstroke + 3) - 1);
                            //g.DrawLine(Pens.Gray, zoom * (2 * brushstroke + 3) / 2 - 2, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 + 2, zoom * (2 * brushstroke + 3) / 2);
                            //g.DrawLine(Pens.Gray, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 + 2, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2 - 2);
                            //EditBox.Cursor = CreateCursor(cbm, zoom * (2 * brushstroke + 3) / 2, zoom * (2 * brushstroke + 3) / 2);
                            EditBox.Cursor = CursorCreator.iMakeBrush(brushstroke, zoom);
                        }
                        else if (mode == EditMode.Pencil)
                        {
                            if (zoom > 3)
                            {
                                //Bitmap cbm = new Bitmap(zoom, zoom);
                                //Graphics g = Graphics.FromImage(cbm);
                                //g.DrawRectangle(Pens.Gray, 0, 0, zoom - 1, zoom - 1);
                                //if (zoom > 4)
                                //{
                                //    g.DrawLine(Pens.Gray, zoom / 2 - 2, zoom / 2, zoom / 2 + 2, zoom / 2);
                                //    g.DrawLine(Pens.Gray, zoom / 2, zoom / 2 + 2, zoom / 2, zoom / 2 - 2);
                                //}
                                //EditBox.Cursor = CreateCursor(cbm, zoom / 2, zoom / 2);

                                EditBox.Cursor = CursorCreator.iMakePencil(zoom);
                            }
                            else
                            {
                                EditBox.Cursor = Cursors.Arrow;
                            }
                        }
                        if (ZoomChanged != null)
                        {
                            ZoomChanged(this, new EventArgs());
                        }
                    }
                }
            }
        }

        #region Ruler

        void DrawRulers()
        {
            #region xRuler
            bitmapx = new Bitmap(this.Size.Width, 15);
            Graphics g = Graphics.FromImage(bitmapx);
            int offset = EditBox.Location.X % (16 * zoom);
            Pen pen = Pens.DarkGray;

            for (int i = offset + 15; i < this.Width; i += 16 * zoom)
            {
                if (i > EditBox.Location.X && i < (EditBox.Location.X + EditBox.Width))
                {
                    pen = Pens.Black;
                }
                else
                {
                    pen = Pens.DarkGray;
                }

                if (i == (EditBox.Location.X + EditBox.Width + 15))
                {
                    g.DrawLine(Pens.Black, new Point(i, 8), new Point(i, 15));
                }
                else
                {
                    g.DrawLine(pen, new Point(i, 8), new Point(i, 15));
                }

                g.DrawLine(pen, new Point(i + 8 * zoom, 10), new Point(i + 8 * zoom, 15));


                if (zoom >= 3)
                {
                    g.DrawLine(pen, new Point(i + 4 * zoom, 12), new Point(i + 4 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 12 * zoom, 12), new Point(i + 12 * zoom, 15));
                }

                if (zoom >= 5)
                {
                    g.DrawLine(pen, new Point(i + 2 * zoom, 12), new Point(i + 2 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 6 * zoom, 12), new Point(i + 6 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 10 * zoom, 12), new Point(i + 10 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 14 * zoom, 12), new Point(i + 14 * zoom, 15));
                }

                if (zoom >= 7)
                {
                    g.DrawLine(pen, new Point(i + 1 * zoom, 12), new Point(i + 1 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 3 * zoom, 12), new Point(i + 3 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 5 * zoom, 12), new Point(i + 5 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 7 * zoom, 12), new Point(i + 7 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 9 * zoom, 12), new Point(i + 9 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 11 * zoom, 12), new Point(i + 11 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 13 * zoom, 12), new Point(i + 13 * zoom, 15));
                    g.DrawLine(pen, new Point(i + 15 * zoom, 12), new Point(i + 15 * zoom, 15));
                }


            }

            g.DrawLine(Pens.Black, new Point(15, 14), new Point(this.Size.Width, 14));

            if (index != -1)
            {
                g.DrawRectangle(Pens.Gray, 1, 1, 12, 12);
                g.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[selectedColor].Color), 3, 3, 9, 9);
            }

            RulerX.BackgroundImage = bitmapx;

            #endregion

            if (this.Size.Height != 0)
            {
                #region yRuler
                bitmapy = new Bitmap(15, this.Size.Height);
                g = Graphics.FromImage(bitmapy);
                offset = EditBox.Location.Y % (16 * zoom);
                pen = Pens.DarkGray;

                for (int i = offset; i < this.Height; i += 16 * zoom)
                {
                    if (i >= EditBox.Location.Y && i < (EditBox.Location.Y + EditBox.Height))
                    {
                        pen = Pens.Black;
                    }
                    else
                    {
                        pen = Pens.DarkGray;
                    }

                    if (i == (EditBox.Location.Y + EditBox.Height))
                    {
                        g.DrawLine(Pens.Black, new Point(8, i), new Point(15, i));
                    }
                    else
                    {
                        g.DrawLine(pen, new Point(8, i), new Point(15, i));
                    }

                    g.DrawLine(pen, new Point(10, i + 8 * zoom), new Point(15, i + 8 * zoom));


                    if (zoom >= 3)
                    {
                        g.DrawLine(pen, new Point(12, i + 4 * zoom), new Point(15, i + 4 * zoom));
                        g.DrawLine(pen, new Point(12, i + 12 * zoom), new Point(15, i + 12 * zoom));
                    }

                    if (zoom >= 5)
                    {
                        g.DrawLine(pen, new Point(12, i + 2 * zoom), new Point(15, i + 2 * zoom));
                        g.DrawLine(pen, new Point(12, i + 6 * zoom), new Point(15, i + 6 * zoom));
                        g.DrawLine(pen, new Point(12, i + 10 * zoom), new Point(15, i + 10 * zoom));
                        g.DrawLine(pen, new Point(12, i + 14 * zoom), new Point(15, i + 14 * zoom));
                    }

                    if (zoom >= 7)
                    {
                        g.DrawLine(pen, new Point(12, i + 1 * zoom), new Point(15, i + 1 * zoom));
                        g.DrawLine(pen, new Point(12, i + 3 * zoom), new Point(15, i + 3 * zoom));
                        g.DrawLine(pen, new Point(12, i + 5 * zoom), new Point(15, i + 5 * zoom));
                        g.DrawLine(pen, new Point(12, i + 7 * zoom), new Point(15, i + 7 * zoom));
                        g.DrawLine(pen, new Point(12, i + 9 * zoom), new Point(15, i + 9 * zoom));
                        g.DrawLine(pen, new Point(12, i + 11 * zoom), new Point(15, i + 11 * zoom));
                        g.DrawLine(pen, new Point(12, i + 13 * zoom), new Point(15, i + 13 * zoom));
                        g.DrawLine(pen, new Point(12, i + 15 * zoom), new Point(15, i + 15 * zoom));
                    }


                }

                g.DrawLine(Pens.Black, new Point(14, 0), new Point(14, this.Size.Height));
                RulerY.BackgroundImage = bitmapy;

                #endregion
            }

            hovering = false;

        }
        Bitmap bitmapx;
        Bitmap bitmapy;
        bool hovering = false;

        #endregion

        public List<byte[]> UndoBuffer;
        public List<byte[]> RedoBuffer;

        public void Undo()
        {
            if (UndoBuffer.Count > 0)
            {
                byte[] temp = new byte[CurrentSprite.ImageData.Length];
                CurrentSprite.ImageData.CopyTo(temp, 0);

                RedoBuffer.Add(temp);
                //CurrentSprite.ImageData = UndoBuffer[UndoBuffer.Count - 1];
                UndoBuffer[UndoBuffer.Count - 1].CopyTo(CurrentSprite.ImageData, 0);

                UndoBuffer.RemoveAt(UndoBuffer.Count - 1);

                if (UndoneAction != null)
                {
                    UndoneAction(this, new EventArgs());
                }

                lock (this)
                {
                    Redraw();
                }
            }
        }

        public void Redo()
        {
            if (RedoBuffer.Count > 0)
            {
                byte[] temp = new byte[CurrentSprite.ImageData.Length];
                CurrentSprite.ImageData.CopyTo(temp, 0);

                UndoBuffer.Add(temp);
                RedoBuffer[RedoBuffer.Count - 1].CopyTo(CurrentSprite.ImageData, 0);

                RedoBuffer.RemoveAt(RedoBuffer.Count - 1);

                if (RedoneAction != null)
                {
                    RedoneAction(this, new EventArgs());
                }

                lock (this)
                {
                    Redraw();
                }
            }
        }

        bool backcolor = true;
        public bool SpriteBackColor
        {
            get { return backcolor; }
            set
            {
                if (value == false)
                {
                    EditBox.BackgroundImage = NSE_Framework.Properties.Resources.transparent;
                }
                else if (value == true)
                {
                    EditBox.BackgroundImage = null;
                }
                backcolor = value;
            }

        }

        public void RefreshTitle()
        {
            
            if (this.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (this.CurrentSprite.CompressedSprite != -1)
                {
                    this.Text = "Sprite: 0x" + this.CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + this.CurrentSprite.CompressedSprite.ToString() + " bytes (4bpp)";
                }
                else
                {
                    this.Text = "Sprite: 0x" + this.CurrentSprite.ImageOffset.ToString("X") + " @ " + (this.CurrentSprite.Width * this.CurrentSprite.Height * 32) + " bytes (4bpp)";
                }

            }
            else if (this.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {

                if (this.CurrentSprite.CompressedSprite != -1)
                {
                    this.Text = "Sprite: 0x" + this.CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + this.CurrentSprite.CompressedSprite.ToString() + " bytes (8bpp)";
                }
                else
                {
                    this.Text = "Sprite: 0x" + this.CurrentSprite.ImageOffset.ToString("X") + " @ " + (this.CurrentSprite.Width * this.CurrentSprite.Height * 64) + " bytes (8bpp)";
                }
            }
        }
       
        private void Editor_Resize(object sender, EventArgs e)
        {

                if (this.Size.IsEmpty == false)
                {
                    AlignBox();
                    DrawRulers();
                    this.Refresh();
                }
            
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {

            RulerX.Refresh();
            Graphics g = RulerX.CreateGraphics();
            g.DrawLine(Pens.Black, this.EditorPanel.PointToClient(Cursor.Position).X + 15, 0, this.EditorPanel.PointToClient(Cursor.Position).X + 15, 15);

            RulerY.Refresh();
            g = RulerY.CreateGraphics();
            g.DrawLine(Pens.Black, 0, this.EditorPanel.PointToClient(Cursor.Position).Y, 15, this.EditorPanel.PointToClient(Cursor.Position).Y);
        }

        private void Editor_MouseHover(object sender, EventArgs e)
        {
            if (hovering == false)
            {
                RulerX.Refresh();
                Graphics g = RulerX.CreateGraphics();
                g.DrawLine(Pens.Black, this.EditorPanel.PointToClient(Cursor.Position).X + 15, 0, this.EditorPanel.PointToClient(Cursor.Position).X + 15, 15);

                RulerY.Refresh();
                g = RulerY.CreateGraphics();
                g.DrawLine(Pens.Black, 0, this.EditorPanel.PointToClient(Cursor.Position).Y, 15, this.EditorPanel.PointToClient(Cursor.Position).Y);
                hovering = true;
            }
        }

        private void Editor_LocationChanged(object sender, EventArgs e)
        {
            DrawRulers();
            this.Refresh();
        }

        private void panel_Scroll(object sender, ScrollEventArgs e)
        {
            DrawRulers();
        }        
        
        ////////////////////////////////
        private void MainMouseDown(object sender, MouseEventArgs e)
        {
            if (this.Mode != EditMode.Eyedropper && this.Mode != EditMode.None)
            {
                byte[] temp = new byte[CurrentSprite.ImageData.Length];
                CurrentSprite.ImageData.CopyTo(temp, 0);

                UndoBuffer.Add(temp);
                RedoBuffer.Clear();

                if (UndoBuffer.Count > 50)
                {
                    UndoBuffer.RemoveAt(0);
                }
            }

            MainFunction(sender, e);
        }

        private void MainFunction(object sender, MouseEventArgs e)
        {
            Point mouse = this.EditorPanel.PointToClient(Cursor.Position);
            RulerX.Refresh();
            Graphics g = RulerX.CreateGraphics();
            g.DrawLine(Pens.Black, this.EditorPanel.PointToClient(Cursor.Position).X + 15, 0, mouse.X + 15, 15);

            RulerY.Refresh();
            g = RulerY.CreateGraphics();
            g.DrawLine(Pens.Black, 0, this.EditorPanel.PointToClient(Cursor.Position).Y, 15, mouse.Y);

            
            mouse = this.EditBox.PointToClient(Cursor.Position);
            NormalizePoint(ref mouse);
            

            if (CurrentSprite != null)
            {
                g = Graphics.FromImage(EditBox.Image);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                #region Tools

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    //If we're using the pencil
                    if (this.Mode == EditMode.Pencil)
                    {
                        #region Pencil
                        if (mouse.X >= 0 && mouse.X < CurrentSprite.Width * 8 &&
                           mouse.Y >= 0 && mouse.Y < CurrentSprite.Height * 8)
                        {
                            int pos = Position2Index(new Size(this.CurrentSprite.Width * 8, this.CurrentSprite.Height * 8), this.EditBox.PointToClient(Cursor.Position), CurrentSprite.Type);

                            if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                            {
                                #region SetData16
                                if (mouse.X % 2 == 0)
                                {
                                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4) + selectedColor)[0];
                                    CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0xf0) + selectedColor);
                                }
                                else
                                {
                                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(
                                    //    CurrentSprite.ImageData[pos] -
                                    //    BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4))[0]
                                    //    + (selectedColor * 0x10))[0];
                                    CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0x0f) + (selectedColor << 4));
                                }
                                #endregion
                            }
                            else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                            {
                                #region SetData256
                                CurrentSprite.ImageData[pos] = selectedColor;
                                #endregion
                            }

                            if (selectedColor != 0)
                            {

                                g.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[selectedColor].Color), mouse.X, mouse.Y, 1, 1);
                            }
                            else if (selectedColor == 0)
                            {

                                g.FillRectangle(new SolidBrush(Color.Transparent), mouse.X, mouse.Y, 1, 1);

                            }


                        }
                        #endregion
                        
                        if (Modified != null)
                        {
                            Modified(this, new EventArgs());
                        }
                    }
                    else if (this.Mode == EditMode.Brush)
                    {
                        #region Brush
                        if (mouse.X >= 0 && mouse.X < CurrentSprite.Width * 8 &&
                           mouse.Y >= 0 && mouse.Y < CurrentSprite.Height * 8)
                        {
                            Rectangle r = new Rectangle(mouse.X - (((brushstroke + 1) * 2 + 1) / 2),
                                    mouse.Y - (((brushstroke + 1) * 2 + 1) / 2),
                                    (brushstroke + 1) * 2 + 1,
                                    (brushstroke + 1) * 2 + 1);

                            for (int y = r.Y; y < r.Bottom; y++)
                            {
                                for (int x = r.X; x < r.Right; x++)
                                {
                                    int pos = Position2Index(new Size(this.CurrentSprite.Width * 8, this.CurrentSprite.Height * 8), new Point(x, y), CurrentSprite.Type, false);
                                    if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                                    {
                                        #region SetData16
                                        if (x % 2 == 0)
                                        {
                                            //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4) + selectedColor)[0];
                                            CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0xf0) + selectedColor);

                                        }
                                        else
                                        {
                                            //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(
                                            //    CurrentSprite.ImageData[pos] -
                                            //    BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4))[0]
                                            //    + (selectedColor * 0x10))[0];
                                            CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0x0f) + (selectedColor << 4));
                                        }
                                        #endregion
                                    }
                                    else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                                    {
                                        #region SetData256
                                        CurrentSprite.ImageData[pos] = selectedColor;
                                        #endregion
                                    }
                                }
                            }


                            if (selectedColor != 0)
                            {

                                g.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[selectedColor].Color),
                                    r.X,
                                    r.Y,
                                    r.Width,
                                    r.Height);
                            }
                            else if (selectedColor == 0)
                            {
                                g.FillRectangle(new SolidBrush(Color.Transparent),
                                    r.X,
                                    r.Y,
                                    r.Width,
                                    r.Height);
                            }
                        }
                        #endregion

                        if (Modified != null)
                        {
                            Modified(this, new EventArgs());
                        }
                    }
                    else if (this.Mode == EditMode.Bucket)
                    {
                        #region Bucket
                        if (mouse.X >= 0 && mouse.X < CurrentSprite.Width * 8 &&
                           mouse.Y >= 0 && mouse.Y < CurrentSprite.Height * 8)
                        {
                            g.Dispose();

                            int pos = Position2Index(new Size(this.CurrentSprite.Width * 8, this.CurrentSprite.Height * 8), this.EditBox.PointToClient(Cursor.Position), CurrentSprite.Type);

                            byte ColorIndex;

                            #region GetColorIndex
                            if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                            {
                                #region Color16
                                ColorIndex = GetHalfByteFromIndex(pos, mouse.X);
                                #endregion
                            }
                            else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                            {
                                #region Color256
                                ColorIndex = CurrentSprite.ImageData[pos];
                                #endregion
                            }
                            else
                            {
                                throw new Exception("Non-supported sprite type.");
                            }
                            #endregion

                            if (selectedColor != ColorIndex)
                            {
                                #region SetInitialPixel
                                if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                                {
                                    SetHalfByteFromIndex(pos, mouse.X, selectedColor);
                                }
                                else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                                {
                                    CurrentSprite.ImageData[pos] = selectedColor;
                                }
                                #endregion
                                List<Point> Points = new List<Point>();
                                Points.Add(mouse);

                                while (Points.Count != 0)
                                {
                                    if (Points[0].X + 1 <= CurrentSprite.Width * 8)
                                    {
                                        if (GetByte(Points[0].X + 1, Points[0].Y) == ColorIndex)
                                        {
                                            #region Code
                                            Points.Add(new Point(Points[0].X + 1, Points[0].Y));
                                            SetByte(Points[0].X + 1, Points[0].Y, selectedColor);


                                            #endregion
                                        }
                                    }

                                    if (Points[0].X - 1 >= 0)
                                    {
                                        if (GetByte(Points[0].X - 1, Points[0].Y) == ColorIndex)
                                        {
                                            #region Code
                                            //                                 SpriteBatchFillRectangle(
                                            //new Rectangle(
                                            // (Points[0].X - 1) * zoom,
                                            // (Points[0].Y) * zoom,
                                            //  zoom, zoom), sprite.Palette.Colors[selectedColor].Color);

                                            Points.Add(new Point(Points[0].X - 1, Points[0].Y));
                                            SetByte(Points[0].X - 1, Points[0].Y, selectedColor);

                                            #endregion
                                        }
                                    }

                                    if (Points[0].Y + 1 <= CurrentSprite.Height * 8)
                                    {
                                        if (GetByte(Points[0].X, Points[0].Y + 1) == ColorIndex)
                                        {
                                            #region Code
                                            //SpriteBatchFillRectangle(
                                            //new Rectangle(
                                            //(Points[0].X) * zoom,
                                            //(Points[0].Y + 1) * zoom,
                                            //zoom, zoom), sprite.Palette.Colors[selectedColor].Color); 
                                            Points.Add(new Point(Points[0].X, Points[0].Y + 1));
                                            SetByte(Points[0].X, Points[0].Y + 1, selectedColor);

                                            #endregion
                                        }
                                    }

                                    if (Points[0].Y - 1 >= 0)
                                    {
                                        if (GetByte(Points[0].X, Points[0].Y - 1) == ColorIndex)
                                        {
                                            #region Code
                                            //SpriteBatchFillRectangle(
                                            //new Rectangle(
                                            //(Points[0].X) * zoom,
                                            //(Points[0].Y - 1) * zoom,
                                            //zoom, zoom), sprite.Palette.Colors[selectedColor].Color);

                                            Points.Add(new Point(Points[0].X, Points[0].Y - 1));
                                            SetByte(Points[0].X, Points[0].Y - 1, selectedColor);

                                            #endregion
                                        }
                                    }

                                    Points.RemoveAt(0);
                                }

                                bitmap = new Bitmap(CurrentSprite.Width * 8, CurrentSprite.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                                Draw.uDrawData(ref bitmap, CurrentSprite.ImageData, CurrentSprite.Palette, new Size(CurrentSprite.Width, CurrentSprite.Height));
                                this.EditBox.Image = bitmap;
                                this.EditBox.BackColor = CurrentSprite.Palette.Colors[0].Color;

                                if (selectedColor == 0)
                                {
                                    this.EditBox.Invalidate();
                                }

                            }

                        }
                        #endregion

                        if (Modified != null)
                        {
                            Modified(this, new EventArgs());
                        }
                    }
                    else if (this.Mode == EditMode.Eyedropper)
                    {
                        #region Eyedropper

                        if (mouse.X >= 0 && mouse.X < CurrentSprite.Width * 8 &&
                           mouse.Y >= 0 && mouse.Y < CurrentSprite.Height * 8)
                        {
                            selectedColor = GetByte(mouse.X, mouse.Y);
                            

                            Graphics gx = Graphics.FromImage(RulerX.BackgroundImage);

                            gx.DrawRectangle(Pens.Gray, 1, 1, 12, 12);
                            gx.FillRectangle(new SolidBrush(CurrentSprite.Palette.Colors[selectedColor].Color), 3, 3, 9, 9);
                            RulerX.Refresh();

                            if (this.ParentSelectColor != null && this.ParentSelectColor.IsDisposed == false)
                            {
                                ParentSelectColor.Refresh();


                            }
                        }

                        #endregion
                    }



                    EditBox.Invalidate();
                    //EditBox.Image = bitmap;

                #endregion

                    
                }

                
            }
            
        }

        void SetByte(int x, int y, byte val)
        {
            int pos = Position2Index(new Size(this.CurrentSprite.Width * 8, this.CurrentSprite.Height * 8), new Point(x, y), CurrentSprite.Type, false);

            if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (x % 2 == 0)
                {
                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4) + val)[0];
                    CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0xf0) + val);
                }
                else
                {
                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(
                    //    CurrentSprite.ImageData[pos] -
                    //    BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4))[0]
                    //    + (val * 0x10))[0];
                    CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0x0f) + (val << 4));
                }
            }
            else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                CurrentSprite.ImageData[pos] = val;
            }
            else
            {
                throw new Exception("Non-supported sprite type.");
            }
        }
        void SetHalfByteFromIndex(int pos, int x, byte val)
        {


            if (x % 2 == 0)
            {
                //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4) + val)[0];

                CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0xf0) + val);

            }
            else
            {
                //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(
                //    CurrentSprite.ImageData[pos] -
                //    BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4))[0]
                //    + (val * 0x10))[0];

                CurrentSprite.ImageData[pos] = (byte)((CurrentSprite.ImageData[pos] & 0x0f) + (val << 4));
            }
        }
        byte GetByte(int x, int y)
        {

            int pos = Position2Index(new Size(this.CurrentSprite.Width * 8, this.CurrentSprite.Height * 8), new Point(x, y), CurrentSprite.Type, false);
            if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (x % 2 == 0)
                {

                    return (byte)(CurrentSprite.ImageData[pos] & 0x0F);
                    //return BitConverter.GetBytes(CurrentSprite.ImageData[pos] -
                        //BitConverter.GetBytes((CurrentSprite.ImageData[pos] >> 4) << 4)[0])[0];
                }
                else
                {
                    return (byte)(CurrentSprite.ImageData[pos] >> 4);
                }
            }
            else if (CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                return CurrentSprite.ImageData[pos];
            }
            else
            {
                throw new Exception("Non-supported sprite type.");
            }


        }
        byte GetHalfByteFromIndex(int pos, int x)
        {

            if (x % 2 == 0)
            {
                //return BitConverter.GetBytes(CurrentSprite.ImageData[pos] -
                //    BitConverter.GetBytes((CurrentSprite.ImageData[pos] >> 4) << 4)[0])[0];
                return (byte)(CurrentSprite.ImageData[pos] & 0x0F);
            }
            else
            {
                //return BitConverter.GetBytes(CurrentSprite.ImageData[pos] >> 4)[0];
                return (byte)(CurrentSprite.ImageData[pos] >> 4);
            }

        }
        void NormalizePoint(ref Point Position, bool ClampValues = true)
        {
            Position.X = (Position.X - (Position.X % Zoom)) / zoom;
            Position.Y = (Position.Y - (Position.Y % Zoom)) / zoom;
            if (ClampValues)
            {
                Position.X = Clamp(Position.X, 0, CurrentSprite.Width * 8);
                Position.Y = Clamp(Position.Y, 0, CurrentSprite.Height * 8);
            }
        }
        int Position2Index(Size Size, Point Position, NSE_Framework.Data.Sprite.SpriteType Type, bool Normalize = true)
        {
            if (Normalize == true)
                NormalizePoint(ref Position);

            Position.X = Clamp(Position.X, 0, CurrentSprite.Width * 8 - 1);
            Position.Y = Clamp(Position.Y, 0, CurrentSprite.Height * 8 - 1);

            if (Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {

                int r = (Position.Y - (Position.Y % 8)) / 2 * Size.Width;
                r += (Position.X - (Position.X % 8)) * 4;
                r += (Position.Y % 8) * 4;
                r += (Position.X % 8) / 2;

                return Clamp(r, 0, Size.Width * Size.Height / 2 - 1); ;
            }
            else if (Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                int r = (Position.Y - (Position.Y % 8)) * Size.Width;
                r += (Position.X - (Position.X % 8)) * 8;
                r += (Position.Y % 8) * 8;
                r += (Position.X % 8);

                return Clamp(r, 0, Size.Width * Size.Height - 1);
            }
            else
            {
                return -1;
            }

        }

        public void AlignBox()
        {
            if (EditBox.Size.Width >= EditorPanel.Width || EditBox.Height >= EditorPanel.Height)
            {
                EditorPanel.AutoScrollPosition = Point.Empty;
                EditBox.Location = Point.Empty;
            }
            else
            {
                EditBox.Location = new Point(Math.Max(0, EditorPanel.Size.Width / 2 - EditBox.Size.Width / 2),
            Math.Max(0, EditorPanel.Size.Height / 2 - EditBox.Size.Height / 2));
            }
        }

        public static int Clamp(int val, int min, int max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            AlignBox();
            DrawRulers();
        }

    }
}
