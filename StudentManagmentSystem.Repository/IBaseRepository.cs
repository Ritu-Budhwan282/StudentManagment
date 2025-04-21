using StudentManagmentSystem.Repository.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Repository
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetList(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        
        Task <T?> GetById(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T?> GetById(Expression<Func<T, bool>> filter);
        Task<T?> GetById(int id);
        Task<bool> AddEntity(T model);
        Task<bool> UpdateEntity(T model);
        Task<bool> DeleteEntity(int id);
        Task<bool> SaveChanges();
        Task<T?> GetAccount(Expression<Func<T, bool>> filter);
        
    }
}
