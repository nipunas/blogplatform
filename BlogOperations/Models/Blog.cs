using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOperations.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
