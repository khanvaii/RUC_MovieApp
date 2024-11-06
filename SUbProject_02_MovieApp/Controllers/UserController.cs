using DataAccessLayer.DataModels;
using DataAccessLayer.Repository.UserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SUbProject_02_MovieApp.DTOModels;
using SUbProject_02_MovieApp.DTOModels.UserDTOs;
using System.Security.Cryptography;
using System.Text;

namespace SUbProject_02_MovieApp.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(string Userid,string UserName, string Email,string Password)
        {
            var existingUser = await _userRepository.getUserbyUsername(Userid);
            if (existingUser != null)
            {
                return Conflict("Username is already taken.");
            }

            var passwordHash = HashPassword(Password);

            var user = new user_info
            {
                user_id = Userid,
                username = UserName,
                email = Email,
                user_psw = passwordHash,
                created_date = DateTime.UtcNow
            };
            await _userRepository.AddUser(user);
            return Ok(user);
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string Username,string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return BadRequest("Username and password are required.");
            }
            
            var user = await _userRepository.LoginUser(Username, Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { message = "Login successful" });
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, string username, string email,string password)
        {
            
            
            var existingUser = await _userRepository.getUserbyUsername(userId);
            if (existingUser == null)
            {
                return BadRequest("Invalid User id.");
            }

            
            existingUser.username = username ?? existingUser.username; 
            existingUser.email = email ?? existingUser.email; 

            
            if (!string.IsNullOrEmpty(password))
            {
                existingUser.user_psw = HashPassword(password); 
            }

            await _userRepository.UpdateUser(existingUser);

            return Ok("User updated successfully");
        }
        [HttpDelete("delete/{Userid}")]
        public async Task<IActionResult> DeleteUser(string Userid)
        {
            var user = await _userRepository.getUserbyUsername(Userid);
            if (user.user_id != Userid)
            {
                return BadRequest("Invalid User Id");
            }
            await _userRepository.DeleteUser(Userid);
            return Ok("User Deleted Successfully");
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
