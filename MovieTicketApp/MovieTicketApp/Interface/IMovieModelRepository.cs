using MovieTicketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketApp.Interface
{
    public interface IMovieModelRepository
    {
        public List<MovieModel> GetAllMovies();
        public MovieModel GetMovieById(string id);
    }
}
