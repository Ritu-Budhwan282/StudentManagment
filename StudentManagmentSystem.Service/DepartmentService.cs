using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagmentSystem.Model;
using StudentManagmentSystem.Repository.Database;
using StudentManagmentSystem.Repository;
using StudentManagmentSystem.Service.Interfaces;


namespace StudentManagmentSystem.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IBaseRepository<Department> _repositoryDepartment;
        private readonly IBaseRepository<ApplicationUserProfile> _repository;
     

        public DepartmentService(IBaseRepository<ApplicationUserProfile> repository, IBaseRepository<Department> repositoryDepartment, StudentManagmentContext context)
        {
            _repositoryDepartment = repositoryDepartment;
            _repository = repository;
        }

        #region Get
        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
        {
            var departmentList = await _repositoryDepartment.GetList();
            return departmentList.Select(item => new DepartmentViewModel
            {
                Id = item.Id,
                Name = item.Name,
                isDelete = item.IsDeleted ?? false
            }).ToList();
        }


        public async Task<DepartmentViewModel> GetDepartmentById(int id)
        {
            var model = await _repositoryDepartment.GetById(x => x.Id == id);
            if (model == null)
                return null;

            return new DepartmentViewModel
            {
                Id = model.Id,
                Name = model.Name,
                isDelete = model.IsDeleted ?? false
            };
        }

        #endregion Get

        #region Add/Update
        public async Task<bool> AddDepartment(DepartmentViewModel department)
        {
            var entity = new Department
            {
                Name = department.Name,
                CreatedOn = DateTime.Now,
                CreatedBy = 1
            };

            return await _repositoryDepartment.AddEntity(entity);
        }

        public async Task<bool> UpdateDepartment(DepartmentViewModel department)
        {
            var entity = await _repositoryDepartment.GetById(x => x.Id == department.Id);
            if (entity == null)
                return false;

            entity.Name = department.Name;
            entity.UpdatedBy = 1;
            entity.UpdatedOn = DateTime.Now;

            return await _repositoryDepartment.UpdateEntity(entity);
        }

        #endregion Add/Update

        #region Delete
        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _repositoryDepartment.GetById(id);
            if (department == null)
                return false;

            var users = await _repository.GetList(x => x.DepartmentId == id);
            foreach (var user in users)
            {
                user.IsDeleted = true;
            }

            department.IsDeleted = true;
            return await _repository.SaveChanges();
        }


        public async Task<IEnumerable<DepartmentViewModel>> GetDepartments()
        {
            // Get the department list with filtering (no need for Include here unless you have navigation properties)
            var departmentList = await _repositoryDepartment.GetList(x => !(bool)x.IsDeleted);

            // Map the department list to DepartmentViewModel
            return departmentList.Select(item => new DepartmentViewModel
            {
                Id = item.Id,
                Name = item.Name,
                isDelete = item.IsDeleted ?? false
            }).ToList();
        }


        #endregion Delete


    }
}
