using DataBasesCourseWorkApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataBasesCourseWorkApplication.Context;
using DataBasesCourseWorkApplication.ViewModel;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataBasesCourseWorkApplication.Controllers
{
    public class HomeController : Controller
    {
        private FacultyDbContext _context;

        public HomeController(FacultyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students
                .Join(_context.Faculties, student => student.FacultyId,
                    faculty => faculty.Id,
                    (student, faculty) => new StudentFullInfoViewModel()
                    {
                        FullName = student.FullName,
                        Benefit = student.Benefit,
                        FacultyName = faculty.Name,
                        Gender = student.Gender,
                        PhoneNumber = student.PhoneNumber,
                        RoomId = student.RoomId
                    }).ToList();
            var studentsWithRoom = students
                .Where(x => x.RoomId != null)
                .Join(_context.Rooms, model => model.RoomId, room => room.Id, (model, room) => new StudentFullInfoViewModel()
                {
                    FullName = model.FullName,
                    Benefit = model.Benefit,
                    FacultyName = model.FacultyName,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    RoomId = model.RoomId,
                    RoomNumber = room.Number
                }).ToList();
            var studentsWithoutRoom =
                students.Where(x => x.RoomId == null).ToList();

            studentsWithRoom.AddRange(studentsWithoutRoom);
            
            return View(studentsWithRoom);
        }
    }
}
