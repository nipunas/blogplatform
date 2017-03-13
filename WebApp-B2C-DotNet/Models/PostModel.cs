using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPlatform.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
        public string ImageUrl { get; set; }

        public Guid UserId { get; set; }

        public PostModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
    }
}