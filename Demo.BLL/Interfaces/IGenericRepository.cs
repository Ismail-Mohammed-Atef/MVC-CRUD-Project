using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Add(T model);
        void Update(T model);
        void Delete(T model);
    }
}
