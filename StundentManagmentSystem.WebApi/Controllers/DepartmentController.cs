using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagmentSystem.Service.Interfaces;      
using StudentManagmentSystem.Model;
using StudentManagmentSystem.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using StudentManagmentSystem.WebApi.Controllers.Attributes;

namespace StudentManagmentSystem.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        
        private readonly IDepartmentService _departmentRepository;
        

        public DepartmentController(IDepartmentService departmentRepository, ILogger<BaseController> logger):base(logger)
        {
            _departmentRepository = departmentRepository;

        }
        
        
        [HttpGet("GetDepartments")]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<DepartmentViewModel>>>> GetDepartments()
        {
            var departments = () => _departmentRepository.GetDepartments();

            return await ExecuteWithExceptionChecking(departments);
        }
        
        [HttpGet("DepartmentInfo")]
        public async Task<ActionResult<BaseResponseDto<DepartmentViewModel>>> Info(int id)
        {
            var departmentInfo =()=> _departmentRepository.GetDepartmentById(id);
            return await ExecuteWithExceptionChecking(departmentInfo);

        }

        [RoleAdmin]
        [HttpGet("AddDepartmentDetails")]

        public async Task<ActionResult<BaseResponseDto<DepartmentViewModel>>> AddNewDepartment()
        {

            return await    ExecuteWithExceptionChecking(async () =>  new DepartmentViewModel());
        }
        [RoleAdmin]
        [HttpGet("UpdateDepartmentDetailsById")]
        public async Task<ActionResult<BaseResponseDto<DepartmentViewModel>>> UpdateNewDepartment(int id)
        {

            var department = new DepartmentViewModel();
            if (id != 0)
            {
                department = await _departmentRepository.GetDepartmentById(id);

            }
            return await ExecuteWithExceptionChecking(async () =>  department);
        }


        [RoleAdmin]
        [HttpPost("AddDepartment")]
        public async Task<ActionResult<BaseResponseDto<bool>>> AddDepartment(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(new {success= await _departmentRepository.AddDepartment(model) });
        }



        [RoleAdmin]
        [HttpPost("UpdatedepartmentById")]
        public async Task<ActionResult<BaseResponseDto<bool>>> UpdateDepartment(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(new { success = await _departmentRepository.UpdateDepartment(model) });
        }




        [RoleAdmin]
        [HttpDelete("DeleteDepartment")]
        public async Task<IActionResult> Delete(int id)
        {

            var isdeletedDepartment = await _departmentRepository.DeleteDepartment(id);
            return Ok(new { isdeletedDepartment });

        }
    }
}
