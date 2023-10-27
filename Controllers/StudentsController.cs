using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Threading.Tasks;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;

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

        public async Task<ViewResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var students = await studentServices.GetStudentsAsync(sortOrder, searchString);

            var studentmodels = MappingFunctions.ToStudentModelList(students);

            return View(studentmodels);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentServices.GetStudentDetailsAsync(id.Value);

            var studentmodel = MappingFunctions.ToStudentModel(student);

            return View(studentmodel);
        }


        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentModel studentmodel)
        {
            if (ModelState.IsValid)
            {
                var student = MappingFunctions.ToStudent(studentmodel);
                await studentServices.CreateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }

            return View(studentmodel);
        }



        //UPDATE
        public IActionResult Edit()
        {
            return View();
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await studentServices.GetStudentById(id.Value);
            var studentmodel = MappingFunctions.ToStudentModel(student);

            if (await TryUpdateModelAsync(studentmodel, "",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
              
                    var updatestudent = MappingFunctions.UpdateStudent(studentmodel, student);
                    await studentServices.UpdateStudentAsync(updatestudent);
                    return RedirectToAction(nameof(Index));
                  
            }
            return View(studentmodel);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            await studentServices.DeleteStudentAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

    }
}
