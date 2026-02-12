using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.BLL.Models
{
    public class MusicItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int DurationSeconds { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Likes { get; set; }
        public string ImageSrc { get; set; }
    }
}
