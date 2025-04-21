using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagmentSystem.Model;
using StudentManagmentSystem.Model.Exceptions;

namespace StudentManagmentSystem.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ILogger<BaseController> Logger { get; private set; }
        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
        }
        public async Task<ActionResult<BaseResponseDto<TT>>> ExecuteWithExceptionChecking<TT>(Func<Task<TT>> function)
        {
            try
            {
                var result = await function();
                return Ok(new BaseResponseDto<TT>
                {
                    data = result,
                    isSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new BaseResponseDto<TT>
                {
                    Message = ex.ExceptionMessage,
                    isSuccess = false
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new BaseResponseDto<TT>
                {
                    Message = ex.Message,
                    isSuccess = false
                });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Exception in controller");
                return StatusCode(500, new BaseResponseDto<TT>
                {
                    Message = ex.Message,
                    isSuccess = false
                });
            }
        }
    }
}
