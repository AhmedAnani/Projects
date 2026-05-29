using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces.IRepository;
using Project.src.Models;
using System.Linq.Expressions;

namespace Project.src.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, Action<T> updateAction)
        {
            if (updateAction == null)
                throw new ArgumentNullException(nameof(updateAction));

            var existing = _context.Set<T>().Find(id);

            if (existing is null)
                throw new KeyNotFoundException($"Entity with id {id} was not found.");

            updateAction(existing);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.Set<T>().Find(id);

            if (existing is null)
                throw new KeyNotFoundException($"Entity with id {id} was not found.");

            _context.Set<T>().Remove(existing);
            _context.SaveChanges();
        }

        public T? GetById(int id, params Expression<Func<T, object>>[] Includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in Includes)
                query = query.Include(include);

            var entity = query
               
                .FirstOrDefault(e => EF.Property<int>(e, "Id") == id);

            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} was not found.");

            return entity;
        }

        public bool CheckExists(int id)
        {
            return _context.Set<T>().Find(id) != null;
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] Includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in Includes)
                query = query.Include(include);

            return query.AsNoTracking().ToList();
        }
    }
}