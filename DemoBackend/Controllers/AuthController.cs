using DemoBackend.Dtos;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private IAuthServiceInterface _authService;
        private IUserInterface _userRepository;

        public AuthController(IAuthServiceInterface authService,IUserInterface userRepository) {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] UserDto request)
        {
            if(request == null)
                return BadRequest(ModelState);

            if(_userRepository.UserExist(request.Email))
                return BadRequest("User Already Exist");

            if (!_authService.IsValid(request.Email))
                return BadRequest("Invalid Email");

            int lastUserId = _userRepository.LastUser() + 1;

            User user = new User();
            _authService.CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            user.UserId = lastUserId;
            user.Email= request.Email.Trim();
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            string token = _authService.CreateToken(user);

            var refreshToken = _authService.GenerateRefreshToken();
            user = _authService.SetRefreshToken(refreshToken,user);

            bool userAdded = _userRepository.AddUser(user);

            if (!userAdded)
                return BadRequest("Something went wrong");

            AuthResponseData authResponseData = new AuthResponseData();

            authResponseData.Email = user.Email;
            authResponseData.idToken = token;
            authResponseData.RefreshToken = refreshToken.Token;
            authResponseData.TokenExpires = user.TokenExpires.ToString();   

            return Ok(authResponseData);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UserDto request)
        {
            if (request == null)
                return BadRequest(ModelState);

            if (!_authService.IsValid(request.Email))
                return BadRequest("Invalid Email");

            if (!_userRepository.UserExist(request.Email))
                return BadRequest("User Doesn't Exist");

            User user = _userRepository.GetUser(request.Email);


            if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            string token = _authService.CreateToken(user);

            var refreshToken = _authService.GenerateRefreshToken();
            user = _authService.SetRefreshToken(refreshToken, user);

            AuthResponseData authResponseData = new AuthResponseData();

            authResponseData.Email = user.Email;
            authResponseData.idToken = token;
            authResponseData.RefreshToken = refreshToken.Token;
            authResponseData.TokenExpires = user.TokenExpires.ToString();

            return Ok(authResponseData);
        }
    }
}
