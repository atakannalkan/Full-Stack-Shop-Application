using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.data.Abstract
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}