using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MTS.DataAccess.Repository
{
    public class Repository<T, TContext> : IRepository<T, TContext> where T : class
        where TContext : DbContext
    {
        protected readonly IMapper _mapper;
        protected readonly TContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(TContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            entities = context.Set<T>();
        }
        /// <summary>
        /// This common repository method - Begins Transaction
        /// </summary>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        /// <summary>
        /// This common repository method - Retrieves all records
        /// </summary>
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
        /// <summary>
        /// This common repository method - Retrieves all records asynchronously
        /// </summary>
        public virtual async Task<ICollection<T>> GetAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }
        /// <summary>
        /// This common repository method - Retrieves a record by id 
        /// </summary>
        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        /// <summary>
        /// This common repository method - Retrieves a record by id asynchronously
        /// </summary>
        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// This common repository method - Inserts a record
        /// </summary>
        public virtual T Add(T t)
        {

            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }
        /// <summary>
        /// This common repository method - Inserts a record asynchronously
        /// </summary>
        public virtual async Task<T> AddAsync(T t)
        {
            try
            {
                _context.Set<T>().Add(t);
                await _context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex) { return t; }

        }
        /// <summary>
        /// This common repository method - Inserts a list of records asynchronously
        /// </summary>
        public virtual async Task<List<T>> AddAsync(List<T> t)
        {
            _context.Set<T>().AddRange(t);
            await _context.SaveChangesAsync();
            return t;

        }
        /// <summary>
        /// This common repository method - Updates a List of records asynchronously
        /// </summary>
        public virtual async Task<List<T>> UpdateAsync(List<T> t)
        {
            _context.Set<T>().UpdateRange(t);
            await _context.SaveChangesAsync();
            return t;

        }
        /// <summary>
        /// This common repository method - Finds a record with the given match
        /// </summary>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }
        /// <summary>
        /// This common repository method - Finds a record with the given match asynchronously
        /// </summary>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }
        /// <summary>
        /// This common repository method - Find list of records with the given match
        /// </summary>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }
        /// <summary>
        /// This common repository method - Find list of records with the given match asychronously
        /// </summary>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }
        /// <summary>
        /// This common repository method - Removes a record
        /// </summary>
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        /// <summary>
        /// This common repository method - Removes a record asynchronously
        /// </summary>
        public virtual async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This common repository method - Removes a list of records asynchronously
        /// </summary>
        public virtual async Task<int> DeleteAsync(List<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This common repository method - Updates a record with the given key
        /// </summary>
        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }
        /// <summary>
        /// This common repository method - Updates a list of records with the given key
        /// </summary>
        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            try
            {
                if (t == null)
                    return null;
                T exist = await _context.Set<T>().FindAsync(key);
                if (exist != null)
                {
                    _context.Entry(exist).CurrentValues.SetValues(t);
                    await _context.SaveChangesAsync();
                }
                return exist;
            }
            catch (Exception ex)
            { return t; }
        }
        /// <summary>
        /// This common repository method - Updates a record asynchronously
        /// </summary>
        public virtual async Task<T> UpdateAsync(T t)
        {
            if (t == null)
                return null;
            _context.Set<T>().Update(t);
            await _context.SaveChangesAsync();
            return t;
        }
        /// <summary>
        /// This common repository method - Retrieves the count
        /// </summary>
        public int Count()
        {
            return _context.Set<T>().Count();
        }
        /// <summary>
        /// This common repository method - Retrieves the count asynchronously
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        /// <summary>
        /// This common repository method - Saves the changes
        /// </summary>
        public virtual void Save()
        {

            _context.SaveChanges();
        }
        /// <summary>
        /// This common repository method - Save the changes asynchronously
        /// </summary>
        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// This common repository method - Finds a record
        /// </summary>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }
        /// <summary>
        /// This common repository method - Finds a record asynchronously
        /// </summary>
        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        /// <summary>
        /// This common repository method - Retrieves all including the given properties
        /// </summary>
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        /// <summary>
        /// This common repository method - Dispose the context according to the the flag set 
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
        /// <summary>
        /// This common repository method - Disposes the context
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
