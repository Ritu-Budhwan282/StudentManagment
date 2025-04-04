using Microsoft.AspNetCore.Mvc;
using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;
using StudentManagmentSystem.WebApp.Repository;

namespace StudentManagmentSystem.WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentRepository;
  
        public DepartmentController(IDepartmentService departmentRepository)
        {
            _departmentRepository = departmentRepository;
            
        }
        public IActionResult GetDepartments()
        {
           
            return Json(_departmentRepository.GetDepartments());
        }
        public IActionResult Index()
        {
            
            return View(_departmentRepository.GetAllDepartments());
        }
        public IActionResult Info(int id)
        {
            var departmentInfo = _departmentRepository.GetDepartmentById(id);
            return Json(departmentInfo);

        }
        public IActionResult AddUpdate(int id)
        {
            var model = new DepartmentViewModel();
            if (id > 0)
            {
                var user = _departmentRepository.GetDepartmentById(id);
                if (user != null)
                {                   
                    model.Name = user.Name;
                    model.Id = user.Id;
                }
            }
            return Json(model);
        }
        public IActionResult AddUpdateDepartment(DepartmentViewModel model)
        {
          
            if (model.Id == 0)
            {
                _departmentRepository.AddDepartment(model);
            }
            else
            {
                _departmentRepository.UpdateDepartment(model);
                 
            }
            
           
            return RedirectToAction("Index");


        }


        public IActionResult Delete(int id)
        {
            if (_departmentRepository.DeleteDepartment(id))
            {
                TempData["SuccessMessage"] = "Department deleted successfully.";
                return Json(new { success = true, message = "Department deleted successfully!" });
            }

            TempData["ErrorMessage"] = "department not found.";
            return Json(new { success = false, message = "department not found." });

             
        }
    }

}
