using System;
using MongoDB.Driver;
using MovieTicketApp.Models;
using System.Xml.Linq;
using MongoDB.Driver;
using MovieTicketApp.Interface;

namespace MovieTicketApp.Repository
{
	public class AdminModelRepository:IAdminModelRepository
	{

        private readonly IMongoCollection<MovieModel> movie;

        public AdminModelRepository(IMovieDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            movie = database.GetCollection<MovieModel>(settings.MovieCollectionName);
        }

        public Boolean FindMovie(MovieModel movieModel) {
            return movie.Find(x => x.Name == movieModel.Name).Any();

        }

        public string UpdateMovie(MovieModel movieModel) {
            var filters = Builders<MovieModel>.Filter.Eq(x => x.Id, movieModel.Id);
            try
            {
                movie.ReplaceOne(filters, movieModel);
                return "Movie Updated";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed to Insert";
            }
        }

        public string DeleteMovie(string id)
        {
            try
            {
                movie.DeleteOne(item => item.movie_id == id);
                return "Record Deleted";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "Failed to Delete";

        }

        public string AddMovie(MovieModel movieModel) {
            try
            { movie.InsertOne(movieModel);
                return "Record Inserted Successfully";
            }
            catch(Exception ex)
            { Console.WriteLine(ex.Message);
                return "Failed to Insert";
            }
            
        }
    }
}

