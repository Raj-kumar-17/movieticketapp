using System;
using Moq;
using MovieTicketApp.Controllers;
using MovieTicketApp.Models;
using MovieTicketApp.Services;
using Microsoft.AspNetCore.Mvc;
using MovieTicketApp.Interface;
namespace MovieTicketApp.Test.Controllers
{
	public class MoviesControllerTest
	{
		Mock<IMovieApiServices> movieapiservices;
		[SetUp]
		public void setup() {
			movieapiservices = new Mock<IMovieApiServices>();
		}
		[Test]
		public void GetAllMoviesTest() {

			MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };
			List<MovieModel> movies = new List<MovieModel> { movieModel};

			movieapiservices.Setup(val => val.GetAllMovies()).Returns(movies);

			MoviesController moviescontroller = new MoviesController(movieapiservices.Object);

			var actual=moviescontroller.GetAllMovies();
			//ActionResult<List<MovieModel>> result = actual as ActionResult<List<MovieModel>>;
			var res = actual.Result as OkObjectResult;

			Assert.That(res.Value, Is.EqualTo(movies));
			Assert.That(res.StatusCode, Is.EqualTo(200));
		}

        [Test]
        public void GetMovieByIdTest() {
            MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };
            movieapiservices.Setup(val => val.GetMovieById("12")).Returns(movieModel);
            MoviesController moviescontroller = new MoviesController(movieapiservices.Object);
            var actual = moviescontroller.GetMovieById("12");
            var res = actual.Result as OkObjectResult;
            Assert.That(res.Value, Is.EqualTo(movieModel));
            Assert.That(res.StatusCode, Is.EqualTo(200));

        }
	}
}

