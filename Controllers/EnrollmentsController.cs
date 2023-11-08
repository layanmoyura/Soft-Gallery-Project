using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        
        private readonly IEnrollmentService _enrollmentService;


        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            
            _enrollmentService = enrollmentService;
        }

        private SelectList CreateSelectList<T>(IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            var selectList = new SelectList(items.Select(item => new SelectListItem
            {
                Value = valueSelector(item),
                Text = $"{valueSelector(item)}. {textSelector(item)}"
            }), "Value", "Text");

            return selectList;
        }

        // GET: Enrollments
        public async Task <ViewResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollments();
            var enrollmentmodels = MappingFunctions.ToEnrollmentModelList(enrollments);
            return View(enrollmentmodels);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            return View(enrollmentmodel);
        }

 

        // GET: Enrollments/Create
        public async Task<ViewResult> Create()
        {
            var courses = await _enrollmentService.GetCourses();
            var students = await _enrollmentService.GetStudents();
            var gradeValues = Enum.GetValues(typeof(Grade)).Cast<int>();

            ViewData["Course"] = CreateSelectList(courses, c => c.CourseID.ToString(), c => c.Title);
            ViewData["Student"] = CreateSelectList(students, s => s.ID.ToString(), s => s.LastName);
            ViewData["Grade"] = CreateSelectList(gradeValues, value => value.ToString(), value => Enum.GetName(typeof(Grade), value));

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade,EnrollmentDate")] EnrollmentModel enrollmentmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var enrollment = MappingFunctions.ToEnrollment(enrollmentmodel);
                    await _enrollmentService.AddEnrollment(enrollment);

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }

            var courses = await _enrollmentService.GetCourses();
            var students = await _enrollmentService.GetStudents();
            var gradeValues = Enum.GetValues(typeof(Grade)).Cast<int>();

            ViewData["Course"] = CreateSelectList(courses, c => c.CourseID.ToString(), c => c.Title);
            ViewData["Student"] = CreateSelectList(students, s => s.ID.ToString(), s => s.LastName);
            ViewData["Grade"] = CreateSelectList(gradeValues, value => value.ToString(), value => Enum.GetName(typeof(Grade), value));

            return Json(new { success = false, error = "Model validation failed" });
        }


        // GET: Enrollments/Edit/5
        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            var courses = await _enrollmentService.GetCourses();
            var students = await _enrollmentService.GetStudents();
            var gradeValues = Enum.GetValues(typeof(Grade)).Cast<int>();

            ViewData["Course"] = CreateSelectList(courses, c => c.CourseID.ToString(), c => c.Title);
            ViewData["Student"] = CreateSelectList(students, s => s.ID.ToString(), s => s.LastName);
            ViewData["Grade"] = CreateSelectList(gradeValues, value => value.ToString(), value => Enum.GetName(typeof(Grade), value));

            return View(enrollmentmodel);
        }

        // POST: Enrollments/Edit/5
        
        [HttpPost,ActionName("Edit")]
        public async Task<JsonResult> EditPost(int? id)
        {
            

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            if (await TryUpdateModelAsync(enrollmentmodel, "",
                e => e.StudentID, e => e.CourseID, s => s.Grade
                ))
            {

                var updateenrollment = MappingFunctions.UpdateEnrollment(enrollmentmodel, enrollment);
                await _enrollmentService.UpdateEnrollment(updateenrollment);
                return Json(new {  success = true, message = "Enrollment updated successfully" });        
            }

            return Json(new { success = false, message = "Error updating enrollment" });


        }

        // GET: Enrollments/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }
            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            return View(enrollmentmodel);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentById(id);
                await _enrollmentService.DeleteEnrollment(enrollment);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }



    }
}
