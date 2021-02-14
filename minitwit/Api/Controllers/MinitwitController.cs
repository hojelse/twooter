using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared;
using System.Collections.Generic;
using static Microsoft.AspNetCore.Http.StatusCodes;

using System;

namespace Controllers
{
    [ApiController]
    [Route("/")]
    public class MinitwitController : ControllerBase
    {
        private readonly IMessageRepository MessageRepo;
        private readonly IUserRepository UserRepo;

        public MinitwitController(IMessageRepository msgrepo, IUserRepository usrrepo)
        {
            this.MessageRepo = msgrepo;
            this.UserRepo = usrrepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoot()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> GetUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> CreateUserAsync(UserCreateDTO user)
        {
            if(user.username == "" || user.username is null) return BadRequest("You have to enter a username");
            if(!user.email.Contains('@')) return BadRequest("You have to enter a valid email address");
            if(user.password1 == "" || user.password1 is null) return BadRequest("You have to enter a password");
            if(user.password1 != user.password2) return BadRequest("The two passwords do not match");

            var exist = await UserRepo.ReadAsync(user.username);

            if(exist is not null) return BadRequest("The username is already taken");

            await UserRepo.CreateAsync(user);
            return Ok("You were succesfully registered and can login now");
        }
        
        [HttpGet("/register")]
        public async Task<IActionResult> GetRegisterPage()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{followed}/follow")]
        public async Task<IActionResult> FollowUserAsync([FromBody] string follower, string followed)
        {
            var res = await UserRepo.FollowAsync(followed, follower);

            if (res != 0) return BadRequest();
            return Ok();
        }

        [HttpDelete("{unfollowed}/unfollow")]
        public async Task<IActionResult> UnfollowUserAsync([FromBody] string unfollower, string unfollowed)
        {
            var res = await UserRepo.UnfollowAsync(unfollowed, unfollower);

            if (res == -3) return NotFound();
            if (res != 0) return BadRequest();
            return Ok();
        }

        [HttpGet("/timeline")]
        public async Task<IActionResult> GetTimeline(string username)
        {
            throw new NotImplementedException();
        }

        [HttpGet("/public")]
        public async Task<IActionResult> GetPublicTimeline()
        {
            throw new NotImplementedException();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> PostLogin()
        {
            throw new NotImplementedException();
        }

        [HttpGet("/login")]
        public async Task<IActionResult> GetLoginPage()
        {
            throw new NotImplementedException();
        }
    }
}