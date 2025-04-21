using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagmentSystem.Model;


namespace StudentManagmentSystem.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartments();
        Task<IEnumerable<DepartmentViewModel>> GetDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int id);
        Task<bool> AddDepartment(DepartmentViewModel department);
        Task<bool> UpdateDepartment(DepartmentViewModel department);
        Task<bool> DeleteDepartment(int id);
    }
}
