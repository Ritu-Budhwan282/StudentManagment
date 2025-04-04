using Microsoft.EntityFrameworkCore;
using StudentManagmentSystem.WebApp.Models;
using System.Linq.Expressions;

namespace StudentManagmentSystem.WebApp.Repository.Implementation
{
    public class BaseRepository<T>: IBaseRepository<T> where T:class
    {
        private readonly StudentManagmentContext _context;
        public BaseRepository(StudentManagmentContext context)
        {
            _context = context;
        }
       public virtual IEnumerable<T> GetList(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> Users = _context.Set<T>();
            foreach (var include in includes)
            {
                Users =Users.Include(include);
            }
            return Users.ToList();
           
        }

        public virtual IEnumerable<T> GetList( Expression<Func<T, bool>> filter)
        {
            IQueryable<T> userList = _context.Set<T>().Where(filter);
            return userList.ToList();
        }
        public T GetById(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> userList = _context.Set<T>().Where(filter);

            foreach (var include in includes)
            {
                userList = userList.Include(include);
            }

            return userList.FirstOrDefault();
        }
        public T GetById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> userList = _context.Set<T>().Where(filter);
            return userList.FirstOrDefault();
        }
        public T GetById(int id) 
        {
           return _context.Set<T>().Find(id);
        }
       public bool AddEntity(T model)
        {
            _context.Set<T>().Add(model);
            return _context.SaveChanges() > 0;
        }
       public bool UpdateEntity(T model)
        {
           _context.Set<T>().Update(model);
           return _context.SaveChanges() > 0;
        }
       public bool DeleteEntity(int id)
        {
            
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);

            return _context.SaveChanges()>0;
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
       
        public T GetAccount(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).FirstOrDefault();
        }

    }
}
