using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using AutoMapper;
using PresentationLayer.Models;
using PresentationLayer.helper;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {   
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public CoursesController(SchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var course = await _context.Course.ToListAsync();
            var coursemodels = MappingFunctions.ToCourseModelList(course);
            return View(coursemodels);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(c => c.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            var coursemodel = MappingFunctions.ToCourseModel(course);

            return View(coursemodel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] CourseModel coursemodel)
        {
            if (ModelState.IsValid)
            {
                var course = MappingFunctions.ToCourse(coursemodel);
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coursemodel);
        }

        // GET: Courses/Edit/5

        public IActionResult Edit()
        {
            return View();
        }


        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var coursemodel = MappingFunctions.ToCourseModel(course);
            return View(coursemodel);
        }

        
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FirstOrDefaultAsync(c => c.CourseID == id);
            var coursemodel = MappingFunctions.ToCourseModel(course);

            if (await TryUpdateModelAsync(coursemodel,"",
                c => c.CourseID, c => c.Title, c => c.Credits
                ))
            {
                try
                {
                    var updatecourse = MappingFunctions.UpdateCourse(coursemodel,course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
                
            }
            return View(coursemodel);
        }
        public IActionResult Delete()
        {
            return View();
        }


        // GET: Courses/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            var coursemodel = MappingFunctions.ToCourseModel(course);

            return View(coursemodel);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseID == id);
        }
    }
}
