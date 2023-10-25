using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PresentationLayer.helper;





namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {

        private IStudentRepository studentRepository;


        public StudentsController(IStudentRepository studentRepository)
        {
            
            
            this.studentRepository = studentRepository;
        }

        //READ

        public async Task<ViewResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var students = await studentRepository.GetStudentsAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName).ToList();
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate).ToList();
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate).ToList();
                    break;
                default:
                    students = students.OrderBy(s => s.LastName).ToList();
                    break;
            }

            var studentmodels = MappingFunctions.ToStudentModelList(students);

            return View(studentmodels);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentRepository.GetDetStudentByIdAsync(id.Value);


            var studentmodel = MappingFunctions.ToStudentModel(student);

            return View(studentmodel);
        }


        //CREATE
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> CreateAsync([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentModel studentmodel)
        {
            if (ModelState.IsValid)
            {
                var student = MappingFunctions.ToStudent(studentmodel);
                await studentRepository.InsertStudentAsync(student);
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

            var student = await studentRepository.GetStudentById(id.Value);

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
            if(id == null)
            {
                return NotFound();
            }
            var student = await studentRepository.GetStudentById(id.Value);
            var studentmodel = MappingFunctions.ToStudentModel(student);

            if (await TryUpdateModelAsync(studentmodel,"",
                s=>s.FirstMidName,s=>s.LastName,s=>s.EnrollmentDate
                ))
            {
                try
                {
                    var updatestudent = MappingFunctions.UpdateStudent(studentmodel, student);
                    await studentRepository.UpdateStudentAsync(updatestudent);
                    return RedirectToAction(nameof(Index));

                }

                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }

            return View(studentmodel);
        }



        //DELETE
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet,ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentRepository.GetStudentById(id.Value);
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
            await studentRepository.DeleteStudentAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }

        



    }
}
