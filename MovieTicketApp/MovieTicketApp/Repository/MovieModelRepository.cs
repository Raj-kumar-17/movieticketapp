using MongoDB.Driver;
using MovieTicketApp.Interface;
using MovieTicketApp.Models;
using System;
namespace MovieTicketApp.Repository
{
    public class MovieModelRepository : IMovieModelRepository
    {
        private readonly IMongoCollection<MovieModel> movie;

        public MovieModelRepository(IMovieDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            movie = database.GetCollection<MovieModel>(settings.MovieCollectionName);
        }

        public List<MovieModel> GetAllMovies()
        {
            List<MovieModel> movies;
                movies = movie.Find(std => true).ToList();
                return movies;



        }

        public MovieModel GetMovieById(string id)
        {
            return movie.Find<MovieModel>(std => std.movie_id == id).FirstOrDefault();

        }
    }
}

