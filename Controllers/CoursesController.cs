using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using System;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "admin")]
        public  ViewResult Index()
        {
            return View();
        }

        [HttpGet, ActionName("IndexGet")]
        public async Task<IActionResult> IndexGet()
        {
            var courses = await _courseServices.GetCoursesAsync();
            var courseModels = MappingFunctions.ToCourseModelList(courses);
            return PartialView("~/Views/Courses/PartialViews/IndexPartial.cshtml", courseModels);
        }

        public ViewResult Details(int? id)
        {
            return View(id);
        }


        // GET: Courses/Details/5
        [HttpGet, ActionName("DetailsGet")]
        public async Task<IActionResult> DetailsGet(int? id)
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

            return PartialView("~/Views/Courses/PartialViews/DetailsPartial.cshtml", coursemodel);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<JsonResult> Create([Bind("CourseID,Title,Credits")] CourseModel coursemodel)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var course = MappingFunctions.ToCourse(coursemodel);
                    await _courseServices.AddCourseAsync(course);
                    return Json(new { success = true });


                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }

            }
                

            return Json(new { success = false, error = "Model validation failed" });

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
        
        public async Task<JsonResult> EditPost(int? id)
        {
            
            var course = await _courseServices.GetCourseByIdAsync(id.Value);
            var coursemodel = MappingFunctions.ToCourseModel(course);

            if (await TryUpdateModelAsync(coursemodel,"",
                c => c.CourseID, c => c.Title, c => c.Credits
                ))
            {
                
                    var updatecourse = MappingFunctions.UpdateCourse(coursemodel,course);
                    await _courseServices.UpdateCourseAsync(updatecourse);
                    return Json(new { success = true, message = "Enrollment updated successfully" });

            }
            return Json(new { success = false, message = "Error updating course" });
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
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            try
            {
                var course = await _courseServices.GetCourseByIdAsync(id);
                await _courseServices.DeleteCourseAsync(course);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
