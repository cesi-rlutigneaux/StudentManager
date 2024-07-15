using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;
using StudentManager.Models.Entities;
using StudentManager.Models.ViewModels;

namespace StudentManager.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext) 
        {
            this.dbContext=dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                Email = addStudentViewModel.Email,
                Phone = addStudentViewModel.Phone,
                Subscribed = addStudentViewModel.Subscribed,
            };

            await dbContext.Students.AddAsync(student);

            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student editStudentViewModel)
        {
            var student = await dbContext.Students.FindAsync(editStudentViewModel.Id);

            if(student is not null)
            {
                student.Name = editStudentViewModel.Name;
                student.Email = editStudentViewModel.Email;
                student.Phone = editStudentViewModel.Phone;
                student.Subscribed = editStudentViewModel.Subscribed;

                await dbContext.SaveChangesAsync(); dbContext.Students.AddAsync(student);
            };

            return RedirectToAction("List", "Students");
        }
    }
}
