using System;
using MovieTicketApp.Models;

namespace MovieTicketApp.Interface
{
	public interface IAdminApiServices
	{
        public string AddMovie(MovieModel movieModel);
        public string UpdateMovie(MovieModel movieModel);
        public string DeleteMovie(string id);

    }
}

