using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.BLL.Rng
{
    public static class SeedComposer
    {
        public static int Compose(int userSeed, int page)
        {
            unchecked
            {
                return (userSeed * 31) + page;
            }
        }
    }
}
