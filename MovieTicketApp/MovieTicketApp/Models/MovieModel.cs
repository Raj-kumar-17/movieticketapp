using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieTicketApp.Models
{
	public class MovieModel
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Duration { get; set; }

        public string Genre { get; set; }

        public string Rating { get; set; }

        public string ImageUrl { get; set; }

        public string TicketCount { get; set; }

        public string movie_id { get; set; }


        public MovieModel(string Name, string Duration, string Genre, string Rating, string ImageUrl, string TicketCount, string movie_id) {
            this.Name = Name;
            this.Duration = Duration;
            this.Genre = Genre;
            this.Rating = Rating;
            this.ImageUrl = ImageUrl;
            this.TicketCount = TicketCount;
            this.movie_id = movie_id;

        }

        public MovieModel()
        {
        }
    }
}

