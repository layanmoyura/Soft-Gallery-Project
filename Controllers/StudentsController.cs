using AutoMapper;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper; 

    
        public StudentsController(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //READ

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            var studentmodels = _mapper.Map<List<StudentModel>>(students);
            Console.WriteLine(studentmodels);
            return View(studentmodels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            student.Enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentID == student.ID)
                .ToListAsync();

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
        
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentModel studentmodel)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<StudentModel, Student>(studentmodel);
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentmodel);
        }



        //UPDATE
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet("Edit/{id}")]
       
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.ID == id);

            if(student == null)
            {
                return NotFound();
            }

            var studentmodel = _mapper.Map<StudentModel>(student);

            return View(studentmodel);
        }


        [HttpPost, ActionName("EditPost")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditPost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            var studentmodel = _mapper.Map<StudentModel>(student);

            if (await TryUpdateModelAsync<StudentModel>(studentmodel,"",
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

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentmodel = _mapper.Map<StudentModel>(student);
            return View(studentmodel);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("DeleteConfirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        



    }
}
