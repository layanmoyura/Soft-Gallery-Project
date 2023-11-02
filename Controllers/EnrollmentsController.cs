using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using AutoMapper;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;


        public EnrollmentsController(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments =await _context.Enrollments.Include(e => e.Course).Include(e => e.Student).ToListAsync();
            var enrollmentmodels = _mapper.Map<List<EnrollmentModel>>(enrollments);
            return View(enrollmentmodels);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = _mapper.Map<EnrollmentModel>(enrollment);

            return View(enrollmentmodel);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID");
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] EnrollmentModel enrollmentmodel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = _mapper.Map<EnrollmentModel, Enrollment>(enrollmentmodel);
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID", enrollmentmodel.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", enrollmentmodel.StudentID);
            return View(enrollmentmodel);
        }

        // GET: Enrollments/Edit/5
        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = _mapper.Map<EnrollmentModel>(enrollment);

            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseID", enrollmentmodel.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "ID", enrollmentmodel.StudentID);
            return View(enrollmentmodel);
        }

        // POST: Enrollments/Edit/5
        
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentID == id);
            var enrollmentmodel = _mapper.Map<EnrollmentModel>(enrollment);

            if (await TryUpdateModelAsync<EnrollmentModel>(enrollmentmodel,"",
                e => e.StudentID, e => e.CourseID, s => s.Grade
                ))
            {
                try
                {
                    var updateenrollment = _mapper.Map(enrollmentmodel, enrollment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch 
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollmentmodel.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "LastName", enrollmentmodel.StudentID);

            return View(enrollmentmodel);
        }

        // GET: Enrollments/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            var enrollmentmodel = _mapper.Map<EnrollmentModel>(enrollment);

            return View(enrollmentmodel);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}
