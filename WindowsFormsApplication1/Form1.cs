using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plugins;
using NSE_Framework;
namespace TestPlugin
{
    public partial class Form1 : Form
    {
        IPluginHost Host;

        public Form1(IPluginHost Host)
        {
            InitializeComponent();
            this.Host = Host;
            this.editor1.Sprites.Add(new NSE_Framework.Data.Sprite(2, 4, NSE_Framework.Data.Sprite.SpriteType.Color16));
            this.editor1.CurrentIndex = 0;
            
                        byte[] paletteData = new byte[32];

                        for (int i = 0; i <= 15; i++)
                        {
                            byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));
                            paletteData[i * 2] = col[0];
                            paletteData[i * 2 + 1] = col[1];
                        }


                        editor1.CurrentSprite.Palette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color16, paletteData);
                        editor1.Sprites.Add(new NSE_Framework.Data.Sprite(2, 4, NSE_Framework.Data.Sprite.SpriteType.Color16));
                        editor1.Sprites.Add(new NSE_Framework.Data.Sprite(2, 4, NSE_Framework.Data.Sprite.SpriteType.Color16));

            selectColor1.LoadEditor(ref editor1);            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            ColorDialog di = new ColorDialog();
            if (di.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {

                NSE_Framework.Data.GBAcolor color = new NSE_Framework.Data.GBAcolor(di.Color.R, di.Color.G, di.Color.B);
                byte[] output = NSE_Framework.Data.Translator.PaletteToByte(color);

                button1.Text = output[0].ToString("X2") + output[1].ToString("X2");
                button1.BackColor = di.Color;
                this.Text = "Success!";
            }
            else
            {
                this.Text = "FAIL";
            }



        }

        private void Form1_Enter(object sender, EventArgs e)
        {
            Host.SetEditor(ref this.editor1);
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            TestPlugin.Program.opened = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectColor1.Index = 15;
        }
    }
}
