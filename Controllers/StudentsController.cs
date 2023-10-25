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
using DataAccessLayer.Repositary;




namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;
        private IStudentRepository studentRepository;


        public StudentsController(SchoolContext context, IMapper mapper, IStudentRepository studentRepository)
        {
            _context = context;
            _mapper = mapper;
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

            var studentmodels = _mapper.Map<List<StudentModel>>(students);

            return View(studentmodels);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await studentRepository.GetStudentByIdAsync(id.Value);


            var studentmodel = _mapper.Map<StudentModel>(student);

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
                var student = _mapper.Map<StudentModel, Student>(studentmodel);
                await studentRepository.InsertStudentAsync(student);
                await studentRepository.SaveAsync();
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

            var studentmodel = _mapper.Map<StudentModel>(student);

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
            var studentmodel = _mapper.Map<StudentModel>(student);

            if (await TryUpdateModelAsync(studentmodel,"",
                s=>s.FirstMidName,s=>s.LastName,s=>s.EnrollmentDate
                ))
            {
                try
                {
                    var updatestudent = _mapper.Map(studentmodel, student);
                    await _context.SaveChangesAsync();
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

            var student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentmodel = _mapper.Map<StudentModel>(student);
            return View(studentmodel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        



    }
}
