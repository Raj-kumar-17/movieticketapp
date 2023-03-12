using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketApp.Models;
using MovieTicketApp.Services;
using MovieTicketApp.Interface;

namespace MovieTicketApp.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieApiServices movieapiservices;

        public MoviesController(IMovieApiServices movieapiservices) {

            this.movieapiservices = movieapiservices;
        }

        [HttpGet]
        [Route("getallmovies")]
        public ActionResult<List<MovieModel>> GetAllMovies()
        {
            var res=movieapiservices.GetAllMovies();
            if(res == null) { return StatusCode(500); }

            return res;
        }

        [HttpGet]
        [Route("getmoviebyid/{id}")]
        public ActionResult<MovieModel> GetMovieById(string id)
        {
            var res = movieapiservices.GetMovieById(id);
            if (res == null) { return StatusCode(404); }

            return res;
        }

    }
}

