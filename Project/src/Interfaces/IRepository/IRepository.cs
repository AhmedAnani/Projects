using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        public void Add(T entity);

        public void Update(int id, Action<T> updateAction);
        public void Delete(int id);

        public T? GetById(int id, params Expression<Func<T, object>>[] Includes);
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] Includes);

        public bool CheckExists(int id);
    }
}
