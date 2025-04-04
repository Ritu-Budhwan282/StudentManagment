using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;
using StudentManagmentSystem.WebApp.Repository;

namespace StudentManagmentSystem.WebApp.Controllers
{
    public class ApplicationUserController : Controller
    {

        private readonly IApplicationUserService _userRepository;
        public ApplicationUserController(IApplicationUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult GetUsers()
        {
            
            return Json(_userRepository.GetUsers());
        }
        public IActionResult Index()
        {
            
            var user = _userRepository.GetApplicationUsers();
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult SignInForm()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromForm] SignInRequestModel model )
        {
            var user = _userRepository.isSignIn(model.Username, model.Password);
            if(user != null)
            {
                HttpContext.Session.SetString("LoggedInUser", model.Username);
                HttpContext.Session.SetString("LoggedInUserDept", user.Department.Name);
                HttpContext.Session.SetString("LoggedInUserRole", user.Role.Name);
                HttpContext.Session.SetInt32("LoggedInUserId", user.Id);
               
                return Json(new { success = true, isSignIn = true });
            }    
            return Json(new { success = true, isSignIn = false});
        }

        public IActionResult UserLogOut()
        {
            HttpContext.Session.Remove("LoggedInUser");
            HttpContext.Session.Clear();
            return RedirectToAction("SignInForm");
        }
        

        public IActionResult Info(int id)
        {
            var UserInfo = _userRepository.GetApplicationUserById(id);
            return Json(UserInfo);
           

        }
       public IActionResult Profile(int id)
        {
            var UserInfo = _userRepository.GetApplicationUserById(id);
            return View("Info", UserInfo);
        }

        public IActionResult AddUpdate(int id)
        {
            var user = _userRepository.GetApplicationUserById(id) ?? new ApplicationUserViewModel();
            user.Roles = _userRepository.ApplicationRoleList();
            user.Departments = _userRepository.ApplicationDepartmentList();
            return Json(user);
        }

        [HttpPost]
        public IActionResult AddUpdateUser(ApplicationUserViewModel model)
        {


            if (model.Id == 0)
            {
                _userRepository.AddUser(model);
            }
            else
            {
                _userRepository.UpdateUser(model);
            }


            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (_userRepository.DeleteUser(id))
            {
                TempData["UserSuccessMessage"] = "User deleted successfully.";
                return Json(new { success = true, message = "User deleted successfully!" });
            }

            TempData["UserErrorMessage"] = "User not found.";
            return Json(new { success = false, message = "User not found." });

        }
    }
}
