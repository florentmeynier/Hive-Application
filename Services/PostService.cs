using System.Collections.Generic;
using MongoDB.Driver;
using HivePSTL.Models;

namespace HivePSTL.Services
{
    public class PostService
    {
        private readonly IMongoCollection<Post> _posts;
        public PostService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _posts = database.GetCollection<Post>("Posts");
        }

        //if Post
        public Post Create (Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public IList<Post> Read() => _posts.Find(post => true).ToList();

        public Post Find(string id) => _posts.Find(post => post.Id == id).SingleOrDefault();

        public void Update(Post post) => _posts.ReplaceOne(p => p.Id == post.Id, post);

        public void Delete(string id) => _posts.DeleteOne(post => post.Id == id);

        //Endif Post
    }
}