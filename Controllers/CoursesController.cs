using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {   
        
        private readonly ICourseServices _courseServices;

        public CoursesController( ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var course = await _courseServices.GetCoursesAsync();
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

            var course = await _courseServices.GetCourseByIdAsync(id.Value);
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
                await _courseServices.AddCourseAsync(course);
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

            var course = await _courseServices.GetCourseByIdAsync(id.Value);
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

            var course = await _courseServices.GetCourseByIdAsync(id.Value);
            var coursemodel = MappingFunctions.ToCourseModel(course);

            if (await TryUpdateModelAsync(coursemodel,"",
                c => c.CourseID, c => c.Title, c => c.Credits
                ))
            {
                 
                    var updatecourse = MappingFunctions.UpdateCourse(coursemodel,course);
                    await _courseServices.UpdateCourseAsync(updatecourse);
                    return RedirectToAction(nameof(Index));
                
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

            var course = await _courseServices.GetCourseByIdAsync(id.Value);
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
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var course = await _courseServices.GetCourseByIdAsync(id.Value);
            await _courseServices.DeleteCourseAsync(course);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
