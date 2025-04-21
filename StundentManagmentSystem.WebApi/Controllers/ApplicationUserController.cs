using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentManagmentSystem.Model;
using StudentManagmentSystem.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using StudentManagmentSystem.Repository.Database;
using System.Security.Claims;
using System.Text;
using StudentManagmentSystem.WebApi.Controllers;
using StudentManagmentSystem.Service;



namespace StudentManagmentSystem.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        private readonly IApplicationUserService _userRepository;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public ApplicationUserController(IApplicationUserService userRepository, IConfiguration config, ITokenService tokenService, ILogger<BaseController> logger):base(logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;

        }


        #region SignIn

        [AllowAnonymous]
        [HttpPost("SignIn")]
        
        
        public async Task<IActionResult> SignIn([FromBody] SignInRequestModel login)
        {
            try
            {
                var user = await _userRepository.isSignIn(login.Username, login.Password);
                if (user == null)
                    return Unauthorized(new { message = "Invalid credentials" });

                var tokenString = _tokenService.GenerateToken(user);
                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }



        #endregion SignIn

        #region Get
        [HttpGet("GetUsers")]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<UserResponseModel>>>> GetUsers()
        {
            var user = () => _userRepository.GetUsers();
            return await ExecuteWithExceptionChecking(user);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<BaseResponseDto<ApplicationUserViewModel>>> GetUserById(int id)
        {
            var UserInfo =()=> _userRepository.GetApplicationUserById(id);
            return await ExecuteWithExceptionChecking(UserInfo);


        }
        #endregion Get


        [HttpGet("AddUserDetails")]
        public async Task<IActionResult> AddUserDetails()
        {
            var model = new ApplicationUserViewModel();
            model.Departments = await _userRepository.ApplicationDepartmentList();
            model.Roles = await _userRepository.ApplicationRoleList();
            return Ok(model);
        }

        [HttpGet("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails(int id)
        {
            var model = await _userRepository.GetApplicationUserById(id);
            model.Departments = await _userRepository.ApplicationDepartmentList();
            model.Roles = await _userRepository.ApplicationRoleList();

            return Ok(model);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<BaseResponseDto<bool>>> AddUser(ApplicationUserViewModel model)
        {

            var result = await _userRepository.AddUser(model);
            return Ok(result);
        }



        

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<BaseResponseDto<bool>>> UpdateUser(ApplicationUserViewModel model)
        {
            var result =await _userRepository.UpdateUser(model);
            return Ok(result);
        }


        
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<BaseResponseDto<bool>>> DeleteUser(int id)
        {
            var isdeleteduser =()=>  _userRepository.DeleteUser(id);
            return await ExecuteWithExceptionChecking(isdeleteduser);
            
        }
    }
}
