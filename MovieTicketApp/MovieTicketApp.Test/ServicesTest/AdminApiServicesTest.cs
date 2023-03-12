using System;
using System.Reflection;
using System.Xml.Linq;
using MongoDB.Driver;
using MovieTicketApp.Database;
using MovieTicketApp.Models;
using MovieTicketApp.Interface;
using MovieTicketApp.Repository;
using Moq;
using MovieTicketApp.Services;
using Microsoft.AspNetCore.Mvc;
using MovieTicketApp.Controllers;

namespace MovieTicketApp.Test.Services
{
	public class AdminApiServicesTest {
        Mock<IAdminModelRepository> adminmodelrepository;

        [SetUp]
        public void setup()
        {
            adminmodelrepository = new Mock<IAdminModelRepository>();
        }

        [Test]
        public void AddMovieSucessTest()
        {

            MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };

            adminmodelrepository.Setup(val => val.FindMovie(movieModel)).Returns(false);
            adminmodelrepository.Setup(val => val.AddMovie(movieModel)).Returns("Record Inserted Successfully");

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.AddMovie(movieModel);

            Assert.That(actual, Is.EqualTo("Record Inserted Successfully"));

        }

        [Test]
        public void AddMovieDuplicateEntryTest()
        {

            MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };

            adminmodelrepository.Setup(val => val.FindMovie(movieModel)).Returns(true);

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.AddMovie(movieModel);

            Assert.That(actual, Is.EqualTo("duplicate entry"));

        }
        [Test]
        public void UpdateMovieSucessTest()
        {

            MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };

            adminmodelrepository.Setup(val => val.UpdateMovie(movieModel)).Returns("Movie Updated");

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.UpdateMovie(movieModel);

            Assert.That(actual, Is.EqualTo("Movie Updated"));

        }
        [Test]
        public void UpdateMovieFailedTest()
        {

            MovieModel movieModel = new MovieModel() { Name = "", Duration = "", Genre = "", Rating = "", ImageUrl = "", TicketCount = "", movie_id = "" };

            adminmodelrepository.Setup(val => val.UpdateMovie(movieModel)).Returns("Failed to Update");

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.UpdateMovie(movieModel);

            Assert.That(actual, Is.EqualTo("Failed to Update"));

        }
        [Test]
        public void DeleteMovieSuccessTest()
        {

            adminmodelrepository.Setup(val => val.DeleteMovie("11")).Returns("Record Deleted");

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.DeleteMovie("11");

            Assert.That(actual, Is.EqualTo("Record Deleted"));

        }
        [Test]
        public void DeleteMovieFailedTest()
        {

            adminmodelrepository.Setup(val => val.DeleteMovie("11")).Returns("Failed to Delete");

            AdminApiServices adminapiservices = new AdminApiServices(adminmodelrepository.Object);

            var actual = adminapiservices.DeleteMovie("11");

            Assert.That(actual, Is.EqualTo("Failed to Delete"));

        }

    }
}

