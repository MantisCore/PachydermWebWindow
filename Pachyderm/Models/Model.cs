using System;
using System.Collections.Generic;
using System.Text;

namespace Pachyderm.Models
{
    public abstract class Model
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
