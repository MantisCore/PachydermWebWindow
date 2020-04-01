using System;
using System.Collections.Generic;
using System.Text;

namespace Pachyderm.Models
{
    public class MovieModel : Model
    {
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string CoverType { get; set; }
    }
}
