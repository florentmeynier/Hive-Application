using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HivePSTL.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;set; }

        public string UserId { get; set; }
        //if(UserName)
        public string UserName { get; set; }
        //Endif(UserName)
        public string Content { get; set; }
        //if(Created)
        public DateTime Created { get; set; }
        //Endif(Created)
        //if(LastUpdated)
        public DateTime LastUpdated { get; set; }
        //Endif(LastUpdated)
    }
}
