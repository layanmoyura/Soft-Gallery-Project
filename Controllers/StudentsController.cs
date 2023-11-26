using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Threading.Tasks;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentServices studentServices;

        public StudentsController(IStudentServices studentServices)
        {
            this.studentServices = studentServices;            
        }


        //READ
        public ViewResult Index()
        {
            return View();
        }


        [Authorize]
        [HttpGet, ActionName("IndexGet")]
        public async Task<IActionResult> IndexGet(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var students = await studentServices.GetStudentsAsync(sortOrder, searchString);

            var studentModels = MappingFunctions.ToStudentModelList(students);

            return PartialView("~/Views/Students/PartialViews/IndexPartial.cshtml", studentModels);
        }


        public ViewResult Details(int? id)
        {
            return View(id);
        }


        // GET: Courses/Details/5
        [Authorize]
        [HttpGet, ActionName("DetailsGet")]
        public async Task<IActionResult> DetailsGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentServices.GetStudentDetailsAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            var studentModel = MappingFunctions.ToStudentModel(student);

            return PartialView("~/Views/Students/PartialViews/DetailsPartial.cshtml", studentModel);
        }


        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> Create([Bind("ID,LastName,FirstMidName,JoinedDate")] StudentModel studentmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var student = MappingFunctions.ToStudent(studentmodel);
                    await studentServices.CreateStudentAsync(student);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }
            return Json(new { success = false, error = "Model validation failed" });
        }



        //UPDATE
        public IActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpGet, ActionName("Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = await studentServices.GetStudentById(id.Value);

            if(student == null)
            {
                return NotFound();
            }

            var studentmodel = MappingFunctions.ToStudentModel(student);

            return View(studentmodel);
        }


        [HttpPost, ActionName("Edit")]
     
        public async Task<JsonResult> EditPost(int? id)
        {
           
            var student = await studentServices.GetStudentById(id.Value);
            var studentmodel = MappingFunctions.ToStudentModel(student);

            if (await TryUpdateModelAsync(studentmodel, "",
                s => s.FirstMidName, s => s.LastName, s => s.JoinedDate))
            {
              
                    var updatestudent = MappingFunctions.UpdateStudent(studentmodel, student);
                    await studentServices.UpdateStudentAsync(updatestudent);
                    return Json(new { success = true, message = "Student updated successfully" });

            }
            return Json(new { success = false, message = "Error updating student" });
        }


        //DELETE
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentServices.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            var studentmodel = MappingFunctions.ToStudentModel(student);
            return View(studentmodel);
        }

        // POST: Students/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirm(int? id)
        {
            try
            {
                await studentServices.DeleteStudentAsync(id.Value);
                return Json(new { success = true });
            }
            
            catch(Exception ex){
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
