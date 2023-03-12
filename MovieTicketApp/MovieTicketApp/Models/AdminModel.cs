using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieTicketApp.Models
{
	public class AdminModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }

    }
}

