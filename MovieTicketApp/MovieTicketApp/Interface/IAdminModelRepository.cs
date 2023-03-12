using System;
using MovieTicketApp.Models;

namespace MovieTicketApp.Interface
{
	public interface IAdminModelRepository
	{

        public Boolean FindMovie(MovieModel movieModel);
        public string UpdateMovie(MovieModel movieModel);
        public string AddMovie(MovieModel movieModel);
        public string DeleteMovie(string id);

    }
}

