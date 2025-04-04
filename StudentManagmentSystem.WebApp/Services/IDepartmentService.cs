using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;

namespace StudentManagmentSystem.WebApp.Repository
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentViewModel> GetAllDepartments();
        IEnumerable<DepartmentViewModel> GetDepartments();
        DepartmentViewModel GetDepartmentById(int id);
        bool AddDepartment(DepartmentViewModel department);
        bool UpdateDepartment(DepartmentViewModel department);
        bool DeleteDepartment(int id);
    }
}
