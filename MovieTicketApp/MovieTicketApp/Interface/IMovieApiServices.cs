using System;
using MovieTicketApp.Models;

namespace MovieTicketApp.Interface
{
	public interface IMovieApiServices
	{

        public List<MovieModel> GetAllMovies();
        public MovieModel GetMovieById(string id);

    }
}

