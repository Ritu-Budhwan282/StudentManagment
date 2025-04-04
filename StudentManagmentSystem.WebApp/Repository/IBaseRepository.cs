using System.Linq.Expressions;

namespace StudentManagmentSystem.WebApp.Repository
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetList(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter);
        T GetById(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        T GetById(Expression<Func<T, bool>> filter);
        T GetById(int id);
        bool AddEntity(T model);
        bool UpdateEntity(T model);
        bool DeleteEntity(int id);
        bool SaveChanges();
        T GetAccount(Expression<Func<T, bool>> filter);

    }
}
