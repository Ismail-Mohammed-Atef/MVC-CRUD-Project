using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
