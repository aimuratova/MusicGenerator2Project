using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.BLL.Rng
{
    public sealed class SeededRandom
    {
        private readonly Random _random;
        public SeededRandom(int seed)
        {
            _random = new Random(seed);
        }
        public int NextInt(int maxValue)
        {
            return _random.Next(maxValue);
        }
        public double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}
