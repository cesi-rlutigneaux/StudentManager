using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Services;
using StudentManager.Models.Entities;
using StudentManager.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StudentManager.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return View(students);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                Phone = addStudentViewModel.Phone,
                Subscribed = addStudentViewModel.Subscribed,
            };

            await _studentService.CreateStudentAsync(student);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Subscribed = student.Subscribed
            };

            return View(studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Id = studentViewModel.Id,
                    Name = studentViewModel.Name,
                    Email = studentViewModel.Email,
                    Phone = studentViewModel.Phone,
                    Subscribed = studentViewModel.Subscribed
                };

                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }

            return View(studentViewModel);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Subscribed = student.Subscribed
            };

            return View(studentViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
