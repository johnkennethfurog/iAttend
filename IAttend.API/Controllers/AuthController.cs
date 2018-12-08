  using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using IAttend.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _authentication;
        private readonly IConfiguration _configuration;

        public AuthController(
            IMapper mapper,
            IAuthentication authentication,
            IConfiguration configuration)
        {

            _mapper = mapper;
            _authentication = authentication;
            _configuration = configuration;
        }

        [Authorize]
        [HttpPut("setPassword")]
        public async Task<IActionResult> SetPassword([FromBody] ResetPasswordDto passwordDto)
        {
            var success = await _authentication.SetPassword(User.GetInstructorNumber(), passwordDto.Password);

            if (success)
                return Ok(true);
            else
                return BadRequest(new ErrorDto("Something went wrong"));
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {

            return Ok(true);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCredentialDto loginCredentialDto)
        {
            var instructor = await _authentication.Login(loginCredentialDto.InstructorNumber, loginCredentialDto.Password);

            if (instructor == null)
                return BadRequest(new ErrorDto("Invalid username or password"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,instructor.InstructorNumber),
                    new Claim(ClaimTypes.Name,instructor.Name)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            var tokenString = tokenHandler.WriteToken(token);


            return Ok(new { tokenString});
        }

    }
}