using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Data;
using SwordAndFather.Models;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")] //[atributes] this are not used through out code but used by the framework 
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;

        public UserController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }
        //static List<User> _users = new List<User>(); //if we didn't set (new List<User>()) _users will be null and we will get exception errors
        // you get one of all instance of a class, if we share this list we will share the same list of users across all classes. 

        [HttpPost("register")] // the rout will now be (api/users/register)
        //excuteNonqueary
            // ExcuteScalar; when ever we want to know the left top colum and row 
        public ActionResult<int> AddUser(CreateUserRequest createRequest)
        {
            if (!_validator.Validate(createRequest))

                return BadRequest(new { error = "users must have a username and password" });

            var newUser = _userRepository.AddUser(createRequest.Username, createRequest.Password);
            return Created($"api/users/{newUser.Id}", newUser);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }
    }
}
