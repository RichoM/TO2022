using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingRoutesSimulation
{
    public partial class Form1 : Form
    {
        const int WIDTH = 400;
        const int HEIGHT = 300;
        const int SCALE = 2;

        const int NMERCHANTS = 250;

        static Random rng = new Random();

        Dictionary<TerrainType, Color> terrainColors = new Dictionary<TerrainType, Color>()
        {
            { TerrainType.WaterDeep, Color.FromArgb(3, 103, 176) },
            { TerrainType.WaterShallow, Color.FromArgb(23, 123, 196) },
            { TerrainType.Sand, Color.FromArgb(227, 225, 84) },
            { TerrainType.Forest, Color.FromArgb(36, 173, 60) },
            { TerrainType.Mountain, Color.FromArgb(62, 60, 57) },
            { TerrainType.Snow, Color.FromArgb(254, 254, 254) },
        };

        TerrainType[,] map;
        Image background = null;
        MapGenerator mapgen = new MapGenerator(rng);

        Point capitalCity;
        Merchant[] merchants;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                   | ControlStyles.UserPaint
                   | ControlStyles.AllPaintingInWmPaint,
                   true);

            ClientSize = new Size(WIDTH * SCALE, HEIGHT * SCALE);

            InitMap();
            InitCapitalCity();
            InitMerchants();
            background = DrawBackground();
        }

        private void InitCapitalCity()
        {
            var walker = new GridWalker(WIDTH / 2, HEIGHT / 2);
            walker.SpiralUntil(() => map[walker.X.Mod(WIDTH), walker.Y.Mod(HEIGHT)] == TerrainType.Mountain);
            capitalCity = walker.Position.Mod(WIDTH, HEIGHT);
        }

        private void InitMerchants()
        {
            merchants = new Merchant[NMERCHANTS];

            // NOTE(Richo): Parallel.For(..) to hopefully speed up initialization of all merchants.
            Parallel.For(0, NMERCHANTS, i =>
            {
                int x, y;
                do
                {
                    x = rng.Next(0, WIDTH);
                    y = rng.Next(0, HEIGHT);
                }
                while (capitalCity.Dist(x, y) < 25);

                var walker = new GridWalker(x, y);
                walker.SpiralUntil(() => map[walker.X.Mod(WIDTH), walker.Y.Mod(HEIGHT)] == TerrainType.Forest);
                merchants[i] = new Merchant(walker.Position.Mod(WIDTH, HEIGHT), capitalCity);
            });
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.ScaleTransform(SCALE, SCALE);
            g.DrawImage(background, 0, 0, WIDTH, HEIGHT);

            // Draw merchants and their towns
            foreach (var merchant in merchants)
            {
                g.DrawRectangle(Pens.Red, merchant.Town.X - 2, merchant.Town.Y - 2, 4, 4);
                g.FillRectangle(Brushes.Orange, merchant.Position.X - 2, merchant.Position.Y - 2, 4, 4);
                g.FillRectangle(merchant.GoingToCapital ? Brushes.Yellow : Brushes.Cyan, merchant.Position.X - 1, merchant.Position.Y - 1, 2, 2);
            }

            // Draw capital
            if (Environment.TickCount / 500 % 2 == 0)
            {
                g.FillRectangle(Brushes.Yellow, capitalCity.X - 4, capitalCity.Y - 4, 8, 8);
                g.DrawLine(Pens.Red, capitalCity.X - 4, capitalCity.Y - 4, capitalCity.X + 4, capitalCity.Y + 4);
                g.DrawLine(Pens.Red, capitalCity.X + 4, capitalCity.Y - 4, capitalCity.X - 4, capitalCity.Y + 4);
                g.DrawRectangle(Pens.Red, capitalCity.X - 4, capitalCity.Y - 4, 8, 8);
            }
            else
            {
                g.FillRectangle(Brushes.Red, capitalCity.X - 4, capitalCity.Y - 4, 8, 8);
                g.DrawLine(Pens.Yellow, capitalCity.X - 4, capitalCity.Y - 4, capitalCity.X + 4, capitalCity.Y + 4);
                g.DrawLine(Pens.Yellow, capitalCity.X + 4, capitalCity.Y - 4, capitalCity.X - 4, capitalCity.Y + 4);
                g.DrawRectangle(Pens.Yellow, capitalCity.X - 4, capitalCity.Y - 4, 8, 8);
            }
        }

        private void InitMap()
        {
            map = mapgen.GenerateMap(WIDTH, HEIGHT);
        }

        private Bitmap DrawBackground()
        {
            /*
             * NOTE(Richo): To avoid redrawing the map every time, we simply create a bitmap image
             * with the map data and use this image to draw in the Paint event.
             * Also, instead of using Bitmap.SetPixel(..) I write to the bitmap directly using 
             * LockBits(..) and unsafe {...}
             */
            Bitmap bmp = new Bitmap(WIDTH, HEIGHT);

            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int w = bmp.Width;
                int h = bmp.Height;
                for (int y = 0; y < h; y++)
                {
                    int* row = (int*)ptr;
                    for (int x = 0; x < w; x++)
                    {
                        int* color = row++;
                        var terrain = map[x, y];
                        *color = terrainColors[terrain].ToArgb();
                    }
                    ptr += data.Stride;
                }
            }

            bmp.UnlockBits(data);
            return bmp;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            foreach (var merchant in merchants)
            {
                merchant.UpdateOn(map);
            }
            Refresh();
        }
    }
}
