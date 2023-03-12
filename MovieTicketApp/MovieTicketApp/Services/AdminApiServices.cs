using System;
using System.Reflection;
using System.Xml.Linq;
using MongoDB.Driver;
using MovieTicketApp.Database;
using MovieTicketApp.Models;
using MovieTicketApp.Interface;
using MovieTicketApp.Repository;

namespace MovieTicketApp.Services
{
	public class AdminApiServices:IAdminApiServices
	{

        private readonly IAdminModelRepository _adminModelRepository;
        public AdminApiServices(IAdminModelRepository adminModelRepository) {
            _adminModelRepository = adminModelRepository;
        }

        public string AddMovie(MovieModel movieModel) {
          
                var res=_adminModelRepository.FindMovie(movieModel);
                if (res == false) {
                   var result= _adminModelRepository.AddMovie(movieModel);
                return result;

                 }

            return "duplicate entry";

            

        }

        public string UpdateMovie(MovieModel movieModel)
        {

            var res = _adminModelRepository.UpdateMovie(movieModel);

            return res;
                  

        }

        public string DeleteMovie(string id) {
      
           var res=_adminModelRepository.DeleteMovie(id);

            return res;

        }

	}
}

