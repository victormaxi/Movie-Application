using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Dtos
{
    public class MovieDtoModel
    {
        public string? Title { get; set; }
        public string? ReleaseYear { get; set; }
        public string? MovieImage { get; set; }
        public string? Cast { get; set; }
        public string? Director { get; set; }
    }
}
