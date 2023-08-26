using Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using ViewLayer;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Nodes;

namespace CarPool.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthService _authService;
        public AuthenticationController(IAuthService authService )
        {
            this._authService = authService;
        }
            
        [HttpPost("/signup")]
        public IActionResult SignUp(UserDTO user)
        {
            try
            {
                bool res = _authService.SignUp(user);
                if (!res)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/google/login")]
        public IActionResult GoogleLogin([FromBody] dynamic body)
        {
            Console.WriteLine(body);
            try
            {
                JsonNode data = JsonNode.Parse(body.ToString());
                string token = (string)data["token"];
                string  userToken = _authService.GoogleLogin(token).Result;
                if (userToken != null)
                {
                    return Ok(userToken);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginRequestDTO requestDetails)
        {
            try
            {
                string userToken = _authService.Login(requestDetails);
                if (userToken != null)
                {
                    return Ok(userToken);
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
