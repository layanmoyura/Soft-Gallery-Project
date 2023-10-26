using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataAccessLayer.Data;
using PresentationLayer.Models;
using PresentationLayer.helper;
using BusinessLayer.Interfaces;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        
        private readonly IEnrollmentService _enrollmentService;


        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            
            _enrollmentService = enrollmentService;
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
            ViewData["CourseID"] = new SelectList(await _enrollmentService.GetCourseIDsAsync());
            ViewData["StudentID"] = new SelectList(await _enrollmentService.GetStudentIDsAsync());
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] EnrollmentModel enrollmentmodel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = MappingFunctions.ToEnrollment(enrollmentmodel);
                await _enrollmentService.AddEnrollment(enrollment);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(await _enrollmentService.GetCourseIDsAsync());
            ViewData["StudentID"] = new SelectList(await _enrollmentService.GetStudentIDsAsync());
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

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            if (enrollment == null)
            {
                return NotFound();
            }

            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            ViewData["CourseID"] = new SelectList(await _enrollmentService.GetCourseIDsAsync());
            ViewData["StudentID"] = new SelectList(await _enrollmentService.GetStudentIDsAsync());
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

            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            var enrollmentmodel = MappingFunctions.ToEnrollmentModel(enrollment);

            if (await TryUpdateModelAsync<EnrollmentModel>(enrollmentmodel,"",
                e => e.StudentID, e => e.CourseID, s => s.Grade
                ))
            {
               
                var updateenrollment = MappingFunctions.UpdateEnrollment(enrollmentmodel, enrollment);
                await _enrollmentService.UpdateEnrollment(updateenrollment);
                return RedirectToAction(nameof(Index));        
            }
            ViewData["CourseID"] = new SelectList(await _enrollmentService.GetCourseIDsAsync());
            ViewData["StudentID"] = new SelectList(await _enrollmentService.GetStudentIDsAsync());
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentById(id.Value);
            await _enrollmentService.DeleteEnrollment(enrollment);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
