using SimplexNoise;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    public enum TerrainType
    {
        WaterDeep,
        WaterShallow,
        Sand,
        Forest,
        Mountain,
        Snow
    }

    public class MapGenerator
    {
        Random rng;

        public MapGenerator(Random rng)
        {
            this.rng = rng;
        }

        public TerrainType[,] GenerateMap(int w, int h)
        {
            var scale = 0.0054f;
            var noise = GenerateNoise(4, w, h, 0.5f, scale);
            var map = new TerrainType[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var n = noise[x, y];
                    TerrainType terrain;
                    if (n > 0.745f)
                    {
                        terrain = TerrainType.Snow;
                    }
                    else if (n > 0.65f)
                    {
                        terrain = TerrainType.Mountain;
                    }
                    else if (n > 0.498f)
                    {
                        terrain = TerrainType.Forest;
                    }
                    else if (n > 0.466f)
                    {
                        terrain = TerrainType.Sand;
                    }
                    else if (n > 0.366f)
                    {
                        terrain = TerrainType.WaterShallow;
                    }
                    else
                    {
                        terrain = TerrainType.WaterDeep;
                    }
                    map[x, y] = terrain;
                }
            }
            return map;
        }

        // NOTE(Richo): Code adapted from https://cmaher.github.io/posts/working-with-simplex-noise/
        private float[,] GenerateNoise(int num_iterations, int w, int h, float persistence, float scale)
        {
            var maxAmp = 0.0f;
            var amp = 1.0f;
            var freq = scale;
            var result = new float[w, h];

            for (int i = 0; i < num_iterations; ++i)
            {
                Noise.Seed = rng.Next();
                var noise = Noise.Calc2D(w, h, freq);
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        result[x, y] += (noise[x, y] / 255) * amp;
                    }
                }

                maxAmp += amp;
                amp *= persistence;
                freq *= 2;
            }

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    result[x, y] /= maxAmp;
                }
            }

            return result;
        }
    }
}
