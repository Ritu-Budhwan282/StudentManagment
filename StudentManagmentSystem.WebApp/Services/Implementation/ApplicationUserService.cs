using Microsoft.EntityFrameworkCore;
using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;
using System.Data;

namespace StudentManagmentSystem.WebApp.Repository.Implementation
{

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IBaseRepository<ApplicationUserProfile> _repository;
        private readonly IBaseRepository<ApplicationRole> _repositoryRole;
        private readonly IBaseRepository<Department> _repositoryDepartment;
        private readonly IBaseRepository<UserAccount> _UserAccountService;
       
        public ApplicationUserService(IBaseRepository<UserAccount> UserAccountService, IBaseRepository<ApplicationUserProfile> repository, StudentManagmentContext context, IBaseRepository<ApplicationRole> repositoryRole, IBaseRepository<Department> repositoryDepartment)
        {
            _repository = repository;
            _UserAccountService = UserAccountService;
            _repositoryRole = repositoryRole;
            _repositoryDepartment = repositoryDepartment;
        }

        #region GET
        public IEnumerable<ApplicationUserViewModel> GetApplicationUsers()
        {
            var models = _repository.GetList(x=>x.Role,d=> d.Department);
            var userList = new List<ApplicationUserViewModel>();
            foreach(var item in models)
            {
                var user = new ApplicationUserViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EmailId = item.EmailId,
                    RoleName = item.Role.Name,
                    DepartmentName=item.Department.Name,
                    isDelete=(bool)item.IsDeleted
                };
                userList.Add(user);
            }
               
            return userList;
        }

        public ApplicationUserViewModel GetApplicationUserById(int id)
        {
            var model = _repository.GetById(x => x.Id == id, x => x.Role, x => x.Department);
            var userAccount = _UserAccountService.GetAccount(x => x.Id == id);
            var viewModel = new ApplicationUserViewModel();
           
            if (model != null)
            {
                viewModel.Id = model.Id;
                viewModel.FirstName = model.FirstName;
                viewModel.LastName = model.LastName;
                viewModel.EmailId = model.EmailId;
                viewModel.RoleName = model.Role.Name;
                viewModel.DepartmentName = model.Department.Name;
                viewModel.Address1 = model.Address1;
                viewModel.Address2 = model.Address2;
                viewModel.MobileNumber = model.MobileNumber;
                viewModel.DepartmentId = model.DepartmentId;
                viewModel.RoleId = model.RoleId;
                viewModel.isDelete = (bool)model.IsDeleted;
                viewModel.Username = userAccount.Username;
                viewModel.Password = userAccount.Password;
                
            }
            return viewModel;
        }
        #endregion GET

        #region Add/Update
        public bool AddUser(ApplicationUserViewModel model)
        {
           
            var entity = new ApplicationUserProfile();
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.EmailId = model.EmailId;
            entity.RoleId = model.RoleId;
            entity.DepartmentId = model.DepartmentId;
            entity.CreatedBy = 1;
            entity.CreatedOn = DateTime.Now;
            entity.Address1 = model.Address1;
            entity.Address2 = model.Address2;
            entity.MobileNumber = model.MobileNumber;
         
            _repository.AddEntity(entity);
            var user = new UserAccount();
            user.Id = entity.Id;
            user.Username = model.Username;
            user.Password = model.Password;
            return _UserAccountService.AddEntity(user);

        }

        public bool UpdateUser(ApplicationUserViewModel model)
        {
            var entity = _repository.GetById(x => x.Id == model.Id, r => r.Role, d => d.Department);
               
            if(entity ==null)
            {
                return false;
            }
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.EmailId = model.EmailId;
            entity.RoleId = model.RoleId;
            entity.DepartmentId = model.DepartmentId;
            entity.Address1 = model.Address1;
            entity.Address2 = model.Address2;
            entity.MobileNumber = model.MobileNumber;
            entity.UpdatedBy = 1;
            entity.UpdatedOn = DateTime.Now;
            _repository.UpdateEntity(entity);
            var user = new UserAccount();
            user.Id = entity.Id;
            user.Username = model.Username;
            user.Password = model.Password;
            return _UserAccountService.UpdateEntity(user);

        }
        #endregion Add/Update


        #region Delete
        public bool DeleteUser(int id)
        {

            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return false; 
            }


            return _repository.DeleteEntity(id);
        }
        #endregion Delete
       
        public IEnumerable<DropdownViewModel> ApplicationRoleList()
        {
            var RoleList = _repositoryRole.GetList();
            var Roles = new List<DropdownViewModel>();
            foreach(var item in RoleList)
            {
                var role = new DropdownViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                };
                Roles.Add(role);
            }
            return Roles;
        }

        public IEnumerable<DropdownViewModel> ApplicationDepartmentList()
        {
            var DepartmentList = _repositoryDepartment.GetList().Where(x => !(bool)x.IsDeleted);
            var Departments = new List<DropdownViewModel>();
            foreach (var item in DepartmentList)
            {
                var Department = new DropdownViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsDelete=(bool)item.IsDeleted
                   
                };
                Departments.Add(Department);
            }
            return Departments;
           
        }
        public ApplicationUserProfile isSignIn(string username, string password)
        {
            var userVal =_UserAccountService.GetAccount(x => x.Username == username);
            var user = _repository.GetById(x => x.Id == userVal.Id, x => x.Department,x => x.Role);
            if (user != null && userVal.Password == password && user.IsDeleted==false)
            {
                return user;
            }
            return null;
        }

        public IEnumerable<ApplicationUserViewModel> GetUsers()
        {
            var userList = _repository.GetList(x => x.Role, x => x.Department).Where(x=>!(bool)x.IsDeleted);
            var userAccount = _UserAccountService.GetList(x => !x.IsDeleted);
            var users = new List<ApplicationUserViewModel>();
            foreach(var item in userList)
            {
                var userA = userAccount.FirstOrDefault(x => x.Id == item.Id);
                var user = new ApplicationUserViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    EmailId = item.EmailId,
                    Address1 = item.Address1,
                    Address2 = item.Address2,
                    MobileNumber = item.MobileNumber,
                    RoleId = item.RoleId,
                    RoleName = item.Role.Name,
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.Department.Name,
                    isDelete = item.IsDeleted ?? false,
                    Username = userA.Username,
                    Password = userA.Password
                };
                users.Add(user);
            }
            return users;
        }



    }
}
