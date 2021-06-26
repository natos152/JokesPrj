﻿using JokesPrj.Models;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JokesPrj.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class UserController : ApiController
    {
        //Get One User from users table.
        [HttpPost]
        [Route("api/user")]
        public IHttpActionResult GetUserFromDB([FromBody] User user)
        {
            try
            {
                user = Globals.UserDAL.GetUserHash(user);
                if (user == null)
                    return Content(HttpStatusCode.NotFound, $"User {user.Username} or pass is incorrect");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/add/user")]
        public IHttpActionResult AddNewUser([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }
                Created(new Uri(Request.RequestUri.AbsoluteUri + user.Username), Globals.UserDAL.SaveNewUserToDB(user));
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
