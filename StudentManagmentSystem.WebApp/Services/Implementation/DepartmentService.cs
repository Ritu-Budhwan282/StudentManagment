using Microsoft.EntityFrameworkCore;
using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Models.ViewModel;

namespace StudentManagmentSystem.WebApp.Repository.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> _repositoryDepartment;
        private readonly IBaseRepository<ApplicationUserProfile> _repository;
        private readonly StudentManagmentContext _context;

        public DepartmentService(IBaseRepository<ApplicationUserProfile> repository, IBaseRepository<Department> repositoryDepartment, StudentManagmentContext context)
        {
            _repositoryDepartment = repositoryDepartment;
            _repository = repository;
        }

        #region Get
        public IEnumerable<DepartmentViewModel> GetAllDepartments()
        {

            var models = _repositoryDepartment.GetList();
            var DepartmentList = new List<DepartmentViewModel>();
            foreach(var item in models)
               {
                var department = new DepartmentViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    isDelete = (bool)item.IsDeleted
                };
                DepartmentList.Add(department);
               }         
                return DepartmentList;
        }

        public DepartmentViewModel GetDepartmentById(int id)
        {
            var model = _repositoryDepartment.GetById(x => x.Id == id);
            var viewModel = new DepartmentViewModel();
            if (model != null)
            {
                viewModel.Id = model.Id;
                viewModel.Name = model.Name;
                viewModel.isDelete = (bool)model.IsDeleted;   
            }
            return viewModel;
        }
        #endregion Get

        #region Add/Update
        public bool AddDepartment(DepartmentViewModel department)
        {
            var entity = new Department();
            
            entity.Name = department.Name;
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = 1;

            return _repositoryDepartment.AddEntity(entity);
        }
        public bool UpdateDepartment(DepartmentViewModel department)
        {
            var entity = _repositoryDepartment.GetById(x => x.Id == department.Id);
            entity.Name =department.Name;
            entity.UpdatedBy = 1;
            entity.UpdatedOn =DateTime.Now;
            

            

            return _repositoryDepartment.UpdateEntity(entity);
        }
        #endregion Add/Update

        #region Delete
        public bool DeleteDepartment(int id)
        {
            var department = _repositoryDepartment.GetById(id);
            var users = _repository.GetList(x => x.DepartmentId == id);
            foreach(var user in users)
            {
                user.IsDeleted = true;
            }
            department.IsDeleted = true;
            return _repository.SaveChanges();
            
        }

        public IEnumerable<DepartmentViewModel> GetDepartments()
        {
            var departmentList = _repositoryDepartment.GetList().Select(item => new DepartmentViewModel
            {
                Id = item.Id,
                Name = item.Name,
                isDelete = item.IsDeleted ?? false
            }).Where(x => !x.isDelete);
            return departmentList;
        }
        #endregion Delete


    }

}
