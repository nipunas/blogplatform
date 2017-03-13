using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogPlatform.Models;
using BlogOperations.Operations;
using BlogOperations.Models;
using System.Security.Claims;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;

namespace BlogPlatform.Controllers
{
    //[Authorize]
    public class BlogController : Controller
    {
        BlogOperator oper = new BlogOperator();

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BlogPosts()
        {
            Claim oidClaim = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");
            Guid guid = Guid.Parse(oidClaim.Value);

            List<Post> posts = oper.GetBlogPosts(guid);
            List<PostModel> models = new List<PostModel>();
            posts.ForEach(p => { models.Add(new PostModel() {
                    Id = p.PostId,
                    Title = p.Title,
                    Content = p.Content,
                    ImageUrl = "https://blogplatform2.blob.core.windows.net/mycontainer/" + p.ImageName
                });
            });

            ViewBag.Posts = models;

            return View();
        }

        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public void CreateBlog(BlogModel model)
        {
            Claim oidClaim = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");

            Blog blog = new Blog();
            blog.Name = model.Name;
            blog.UserId = Guid.Parse(oidClaim.Value);

            oper.CreateBlog(blog);
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public void CreatePost(PostModel post)
        {
            Claim oidClaim = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");

            Post dbPost = new Post();
            dbPost.Title = post.Title;
            dbPost.Content = post.Content;
            dbPost.UserId = Guid.Parse(oidClaim.Value);

            if (post.Files.Any(f => f != null))
            {
                Guid blobId = Guid.NewGuid();

                //Using the Azure Configuration Manager is optional. You can also use an API like the .NET Framework's ConfigurationManager class.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId.ToString() + Path.GetExtension(post.Files[0].FileName));
                blockBlob.Properties.ContentType = post.Files[0].ContentType;

                // Create or overwrite the "myblob" blob with contents from a local file.
                blockBlob.UploadFromStream(post.Files[0].InputStream);
                var a = blockBlob.Uri.AbsoluteUri;

                dbPost.ImageName = blobId.ToString() + Path.GetExtension(post.Files[0].FileName);
            }

            oper.CreatePost(dbPost);
            Response.Redirect("/");
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}