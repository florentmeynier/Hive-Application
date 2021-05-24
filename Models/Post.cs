using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HivePSTL.Models
{
    //if Post
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
    //endif Post
}
