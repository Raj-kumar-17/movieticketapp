using System;
using Moq;
using MovieTicketApp.Controllers;
using MovieTicketApp.Models;
using MovieTicketApp.Services;
using Microsoft.AspNetCore.Mvc;
using MovieTicketApp.Interface;
using MovieTicketApp.JWT_Token_Manager;

namespace MovieTicketApp.Test.Controllers
{
	public class UserActionControllerTest
	{
        Mock<IAdminApiServices> adminapiservices;
        Mock<IJwtAuthenticationManager> jwtauthenticationmanager;
        [SetUp]
        public void setup()
        {
           adminapiservices = new Mock<IAdminApiServices>();
           jwtauthenticationmanager = new Mock<IJwtAuthenticationManager>();
        }
        [Test]
        public void AddMovieSuccessTest() {
            MovieModel movieModel = new MovieModel();
            movieModel.Name = "inception";
            movieModel.Duration = "2h";
            movieModel.Genre = "Thriller";
            movieModel.Rating = "9.8";
            movieModel.ImageUrl = "test.jpg";
            movieModel.TicketCount = "200";
            movieModel.movie_id = "17";
            adminapiservices.Setup(val => val.AddMovie(movieModel)).Returns("Record Inserted Successfully");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object,adminapiservices.Object);
            var actual = useractioncontroller.AddMovie(movieModel);
            var res = actual.Result as OkObjectResult;
            Assert.That(res.Value, Is.EqualTo("Inserted Successfully"));
            Assert.That(res.StatusCode, Is.EqualTo(200));

        }

        [Test]
        public void AddMovieFailedTest()
        {
            MovieModel movieModel = new MovieModel();
            movieModel.Name = "inception";
            movieModel.Duration = "2h";
            movieModel.Genre = "Thriller";
            movieModel.Rating = "9.8";
            movieModel.ImageUrl = "test.jpg";
            movieModel.TicketCount = "200";
            movieModel.movie_id = "17";
            adminapiservices.Setup(val => val.AddMovie(movieModel)).Returns("Failed to Insert");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object, adminapiservices.Object);
            var actual = useractioncontroller.AddMovie(movieModel);
            var res = actual.Result as StatusCodeResult;
            Assert.That(res.StatusCode, Is.EqualTo(500));

        }

        [Test]
        public void UpdateMovieSuccessTest()
        {
            MovieModel movieModel = new MovieModel();
            movieModel.Name = "inception";
            movieModel.Duration = "2h";
            movieModel.Genre = "Thriller";
            movieModel.Rating = "9.8";
            movieModel.ImageUrl = "test.jpg";
            movieModel.TicketCount = "200";
            movieModel.movie_id = "17";
            adminapiservices.Setup(val => val.UpdateMovie(movieModel)).Returns("Movie Updated");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object, adminapiservices.Object);
            var actual = useractioncontroller.UpdateMovie(movieModel);
            var res = actual.Result as OkObjectResult;
            Assert.That(res.Value, Is.EqualTo("Updated Successfully"));
            Assert.That(res.StatusCode, Is.EqualTo(200));

        }

        [Test]
        public void UpdateMovieFailedTest()
        {
            MovieModel movieModel = new MovieModel();
            movieModel.Name = "inception";
            movieModel.Duration = "2h";
            movieModel.Genre = "Thriller";
            movieModel.Rating = "9.8";
            movieModel.ImageUrl = "test.jpg";
            movieModel.TicketCount = "200";
            movieModel.movie_id = "17";
            adminapiservices.Setup(val => val.UpdateMovie(movieModel)).Returns("Failed to Update");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object, adminapiservices.Object);
            var actual = useractioncontroller.UpdateMovie(movieModel);
            var res = actual.Result as StatusCodeResult;
            Assert.That(res.StatusCode, Is.EqualTo(500));
        }



        [Test]
        public void DeleteMovieSuccessTest()
        {
            string id = "12";
            adminapiservices.Setup(val => val.DeleteMovie(id)).Returns("Record Deleted");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object, adminapiservices.Object);
            var actual = useractioncontroller.DeleteMovie(id);
            var res = actual.Result as OkObjectResult;
            Assert.That(res.Value, Is.EqualTo("Deleted Successfully"));
            Assert.That(res.StatusCode, Is.EqualTo(200));

        }

        [Test]
        public void DeleteMovieFailedTest()
        {
            string id = "12";
            adminapiservices.Setup(val => val.DeleteMovie(id)).Returns("Failed to Delete");
            UserActionController useractioncontroller = new UserActionController(jwtauthenticationmanager.Object, adminapiservices.Object);
            var actual = useractioncontroller.DeleteMovie(id);
            var res = actual.Result as NotFoundObjectResult;
            Assert.That(res.StatusCode, Is.EqualTo(404));

        }

    }
}

