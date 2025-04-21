using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagmentSystem.Model;
using StudentManagmentSystem.Repository.Database;

namespace StudentManagmentSystem.Service.Interfaces
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserViewModel>> GetApplicationUsers();
        Task<ApplicationUserViewModel> GetApplicationUserById(int id);
        Task<bool> AddUser(ApplicationUserViewModel model);
        Task<bool> UpdateUser(ApplicationUserViewModel model);

        Task<bool> DeleteUser(int id);
        Task<IEnumerable<UserResponseModel>> GetUsers();

        Task<IEnumerable<DropdownViewModel>> ApplicationRoleList();
        Task<IEnumerable<DropdownViewModel>> ApplicationDepartmentList();
        Task<ApplicationUserProfile> isSignIn(string username, string password);
    }
}
