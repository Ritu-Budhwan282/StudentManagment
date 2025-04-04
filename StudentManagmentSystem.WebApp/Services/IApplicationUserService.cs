using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;

namespace StudentManagmentSystem.WebApp.Repository
{
    public interface IApplicationUserService 
    {
        IEnumerable<ApplicationUserViewModel> GetApplicationUsers();
        ApplicationUserViewModel GetApplicationUserById(int id);
        bool AddUser(ApplicationUserViewModel model);
        bool UpdateUser(ApplicationUserViewModel model);
        bool DeleteUser(int id);
        IEnumerable<ApplicationUserViewModel> GetUsers();

        IEnumerable<DropdownViewModel> ApplicationRoleList();
        IEnumerable<DropdownViewModel> ApplicationDepartmentList();
        ApplicationUserProfile isSignIn(string username, string password);
    }
}

