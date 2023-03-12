using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketApp.Interface;
using MovieTicketApp.JWT_Token_Manager;
using MovieTicketApp.Models;
using MovieTicketApp.Services;

namespace MovieTicketApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserActionController : Controller
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly IAdminApiServices adminApiServices;

        public UserActionController(IJwtAuthenticationManager jwtAuthenticationManager, IAdminApiServices adminApiServices) {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.adminApiServices = adminApiServices;
        }




        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody] AdminModel adminModel)
        {
            var token = jwtAuthenticationManager.Authenticate(adminModel.UserName, adminModel.PassWord);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost("addmovie")]
        public ActionResult<string> AddMovie([FromBody] MovieModel movieModel)
        {
            var token = adminApiServices.AddMovie(movieModel);
            if (token == "Failed to Insert")
                return StatusCode(500); 
            if (token == "duplicate entry") 
                return StatusCode(403);

            return Ok("Inserted Successfully");

        }

        [HttpPut("updatemovie")]
        public ActionResult<string> UpdateMovie([FromBody] MovieModel movieModel)
        {
            var token = adminApiServices.UpdateMovie(movieModel);
            if (token == "Failed to Update")
                return StatusCode(500);

            return Ok("Updated Successfully");


        }
         
        [HttpDelete("deletemovie/{id}")]
        public ActionResult<string> DeleteMovie(string id)
        {
            var token = adminApiServices.DeleteMovie(id);
            if (token == "Failed to Delete")
                return NotFound("Delete failed");

            return Ok("Deleted Successfully");


        }
    }
}

