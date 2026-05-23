using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repository
{
    public class GenericRepository<T> : IGeneric_Repository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        // i use Action<T> because Private set of some properties in the entity, so i can't assign value to them directly, but with Action<T> i can pass a lambda expression to update those properties.
        public void Update(int id, Action<T> updateAction)
        {
            var existing = _context.Set<T>().Find(id);

            if (existing is null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            updateAction(existing);
            _context.SaveChanges();
            Console.WriteLine("Updated Successfully.");
        }

        public void Delete(int id)
        {
            var existing = _context.Set<T>().Find(id);

            if (existing is null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            _context.Set<T>().Remove(existing);
            _context.SaveChanges();
            Console.WriteLine("Deleted Successfully.");
        }
        // i use params Expression<Func<T, object>>[] Includes to allow the caller to specify which related entities to include in the query result, this is useful for eager loading of related data.
        public T GetById(int id, params Expression<Func<T, object>>[] Includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in Includes)
                query = query.Include(include);
            var entity= query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
            if (entity == null)
                throw new KeyNotFoundException($"" +
                    $"Entity with id {id} not found.");
           
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
            {
                query=query.Include(include);
            }
            return query.AsNoTracking().ToList();
        }
    }
}