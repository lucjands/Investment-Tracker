using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T>
    {
        bool TableExists();
        bool GenerateTable();
        bool Insert(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        List<T> GetAll();
        T GetByID(int id);
    }
}
