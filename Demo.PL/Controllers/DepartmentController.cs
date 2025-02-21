using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repostitories;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //public IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // /Department/Index
        public async Task<IActionResult> IndexAsync()
        {


            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            _unitOfWork.Complete();
            //ViewData,ViewBag both refer to the same dictionary , 
            //they are used for adding extra information from the action to the view
            //view data is strongly typed but viewbag is dynamic
            ViewData["Message"] = "Hello View Data";

            ViewBag.Message = "Departments";

            var result = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( DepartmentViewModel model) 
        {
            
            var department = _mapper.Map<Department>(model);
            if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if(count > 0)
                {
                    TempData["Message"] = "Employee Added";
                }
                else
                {
                    TempData["Message"] = "Add Failed";
                }




                 return RedirectToAction(nameof(IndexAsync));
            }


            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> DetailsAsync([FromRoute]int? id,string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();

            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);

            if(department == null)
                return NotFound();

            var result = _mapper.Map<DepartmentViewModel>(department);

            return View(ViewName,result);
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int? id)
        {
            return await DetailsAsync(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var department = _mapper.Map<Department>(model);

            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(department);
                var count = _unitOfWork.Complete();
                if(count > 0)
                    return RedirectToAction(nameof(IndexAsync));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await DetailsAsync(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id,DepartmentViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var department = _mapper.Map<Department>(model);


            if (ModelState.IsValid) 
            {
            _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Complete();
            if(count > 0)
                return RedirectToAction(nameof(IndexAsync));
            }
            return View(model);
        }

    }
}
