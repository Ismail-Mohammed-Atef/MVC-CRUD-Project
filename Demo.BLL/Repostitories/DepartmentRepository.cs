using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostitories
{
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
