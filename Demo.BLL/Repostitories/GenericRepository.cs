using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostitories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            if (entity is not null)
            {
                 _context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            if (entity is not null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<T> Get(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _context.Employees.Include(e => e.Department).ToListAsync();
            }
            else
            {

            return await _context.Set<T>().ToListAsync();
            }
            
        }

        public void Update(T entity)
        {
            if (entity is not null)
            {
                _context.Set<T>().Update(entity);
            }
        }
    }
}
