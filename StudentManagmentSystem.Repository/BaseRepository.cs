using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentManagmentSystem.Repository.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace StudentManagmentSystem.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly StudentManagmentContext _context;
        public BaseRepository(StudentManagmentContext context)
        {
            _context = context;
        }
        public async Task< IEnumerable<T>> GetList(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> Users = _context.Set<T>();
            foreach (var include in includes)
            {
                Users = Users.Include(include);
            }
            return await Users.ToListAsync();

        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> entityList = _context.Set<T>().Where(filter);
            return await entityList.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> entityList = _context.Set<T>().Where(filter);

            // Apply includes (navigation properties)
            foreach (var include in includes)
            {
                entityList = entityList.Include(include);
            }

            return await entityList.ToListAsync();
        }

        public async Task<T?> GetById(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var user = _context.Set<T>().Where(filter);

            foreach (var include in includes)
            {
                user = user.Include(include);
            }

            return await  user.FirstOrDefaultAsync();
        }
        public async Task<T?> GetById(Expression<Func<T, bool>> filter)
        {
            var user = _context.Set<T>().Where(filter);
            return await user.FirstOrDefaultAsync();
        }
        public async Task<T?> GetById(int id)
        {
            var user = _context.Set<T>();
            return  await user.FindAsync(id);
        }
        public async Task<bool> AddEntity(T model)
        {
            _context.Set<T>().Add(model);
            return await SaveChanges();
        }
        public async Task<bool> UpdateEntity(T model)
        {
            _context.Set<T>().Update(model);
            return await SaveChanges();
        }
        public async Task<bool> DeleteEntity(int id)
        {

            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                return false;

            var isDeletedProp = entity.GetType().GetProperty("IsDeleted");
            if (isDeletedProp != null)
            {
                isDeletedProp.SetValue(entity, true);

                var updatedOnProp = entity.GetType().GetProperty("UpdatedOn");
                updatedOnProp?.SetValue(entity, DateTime.Now);

                var updatedByProp = entity.GetType().GetProperty("UpdatedBy");
                updatedByProp?.SetValue(entity, 1); // or pass from service

                _context.Set<T>().Update(entity);
            }
            else
            {
                // fallback to hard delete if soft delete not supported
                _context.Set<T>().Remove(entity);
            }

            return _context.SaveChanges() > 0;
            }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T?> GetAccount(Expression<Func<T, bool>> filter)
        {
            var userAccount = _context.Set<T>().Where(filter);
            return await userAccount.FirstOrDefaultAsync();
        }
        

    }
}
