using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.BLL.Models
{
    public class MusicParams
    {
        public int Seed { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string Locale { get; set; } = "en";
        public double LikesMultiplier { get; set; } = 1.0;
    }
}
