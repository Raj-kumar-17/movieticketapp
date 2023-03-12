using System;
using MongoDB.Driver;
using MovieTicketApp.Database;
using MovieTicketApp.Models;
using MovieTicketApp.Interface;

namespace MovieTicketApp.Services
{
	public class MovieApiServices:IMovieApiServices
	{
        private readonly IMovieModelRepository _movieModelRepository;

        public MovieApiServices(IMovieModelRepository movieModelRepository)
        {
            _movieModelRepository = movieModelRepository;
        }

        public List<MovieModel> GetAllMovies() {
           var res= _movieModelRepository.GetAllMovies();
            if(res==null) { return null; }
            return res;
            
        }

        public MovieModel GetMovieById(string id)
        {
            var res=_movieModelRepository.GetMovieById(id);
            if (res == null)
            {
                return null;
            }

            return res;
        }
        
    }
}

