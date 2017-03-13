using BlogOperations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogOperations.Operations
{
    public class BlogOperator
    {
        BlogContext context = new BlogContext();

        public List<Post> GetBlogPosts(Guid userId)
        {
            return context.Posts.Where(p => p.UserId == userId).ToList();
        }

        public void CreatePost(Post post)
        {
            //Get the blog of the user
            Blog blog = context.Blogs.FirstOrDefault(b => b.UserId == post.UserId);

            Post newPost = context.Posts.Create();
            newPost.Title = post.Title;
            newPost.Content = post.Content;
            newPost.BlogId = blog != null ? blog.BlogId : 1;
            newPost.UserId = post.UserId;
            newPost.ImageName = post.ImageName;

            context.Posts.Add(newPost);

            context.SaveChanges();
            Console.WriteLine("New Post ID is " + newPost.PostId);
        }

        public bool UserHasBlog(string oid)
        {
            Guid guidId = Guid.Parse(oid);
            return context.Blogs.Any(b => b.UserId == guidId);
        }

        public void CreateBlog(Blog blog)
        {
            Blog newBlog = context.Blogs.Create();
            newBlog.Name = blog.Name;
            newBlog.UserId = blog.UserId;

            context.Blogs.Add(newBlog);

            context.SaveChanges();
            Console.WriteLine("newBlog ID is " + newBlog.BlogId);
        }
    }
}
