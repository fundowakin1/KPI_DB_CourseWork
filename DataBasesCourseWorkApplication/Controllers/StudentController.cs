using System.Linq;
using DataBasesCourseWorkApplication.Context;
using DataBasesCourseWorkApplication.Models;
using DataBasesCourseWorkApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBasesCourseWorkApplication.Controllers
{
    public class StudentController : Controller
    {
        private FacultyDbContext _context;
        public StudentController(FacultyDbContext context)
        {
            _context = context;
        }
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == id);
            if (student != null) _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("MainPage","Home");
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.SingleOrDefault(x => x.Id == id);
            var faculties = _context.Faculties.Select(x=>x.Name).ToList();
            var resultModel = new StudentCreateAndEditViewModel()
            {
                Benefit = student.Benefit,
                FacultiesName = new SelectList(faculties),
                FacultyId = student.FacultyId,
                FullName = student.FullName,
                Gender = student.Gender,
                Id = student.Id,
                PhoneNumber = student.PhoneNumber,
                RoomId = student.RoomId,
            };
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentCreateAndEditViewModel student)
        {
            var facultyId = _context.Faculties.FirstOrDefault(x => x.Name == student.FacultyName).Id;
            var dormitories = _context.Dormitories.Join(_context.DormitoryOfFaculties, dormitory => dormitory.Id,
                    faculty => faculty.DormitoryId, (dormitory, faculty) => new {dormitory, faculty})
                .FirstOrDefault(x => x.faculty.FacultyId == facultyId);
            int? id = null;
            if (dormitories != null)
            {
                var dormitoryId = dormitories.dormitory.Id;
                var rooms = _context.Rooms.Where(x => x.DormitoryId == dormitoryId && x.Gender == student.Gender).ToList();

                while (rooms.Any())
                {
                    var room = rooms.FirstOrDefault();
                    var studentsInRoom = _context.Students.Count(x => x.RoomId == room.Id);
                    if (studentsInRoom < room.Capacity)
                    {
                        id = room.Id;
                        break;
                    }
                    rooms.Remove(room);
                }
            }
            var studentToUpdate = new Student()
            {
                Benefit = student.Benefit,
                FacultyId = facultyId,
                FullName = student.FullName,
                Gender = student.Gender,
                Id = student.Id,
                PhoneNumber = student.PhoneNumber,
                RoomId = id
            };
            _context.Students.Update(studentToUpdate);
            _context.SaveChanges();
            return RedirectToAction("MainPage", "Home");
        }


        [HttpGet]
        public IActionResult Create()
        {
            var faculties = _context.Faculties.Select(x => x.Name).ToList();
            var resultModel = new StudentCreateAndEditViewModel()
            {
                FacultiesName = new SelectList(faculties)
            };
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult Create(StudentCreateAndEditViewModel student)
        {
            var facultyId = _context.Faculties.FirstOrDefault(x => x.Name == student.FacultyName).Id;
            var dormitories = _context.Dormitories.Join(_context.DormitoryOfFaculties, dormitory => dormitory.Id,
                    faculty => faculty.DormitoryId, (dormitory, faculty) => new { dormitory, faculty })
                .FirstOrDefault(x => x.faculty.FacultyId == facultyId);
            int? id = null;
            if (dormitories != null)
            {
                var dormitoryId = dormitories.dormitory.Id;
                var rooms = _context.Rooms.Where(x => x.DormitoryId == dormitoryId && x.Gender == student.Gender).ToList();

                while (rooms.Any())
                {
                    var room = rooms.FirstOrDefault();
                    var studentsInRoom = _context.Students.Count(x => x.RoomId == room.Id);
                    if (studentsInRoom < room.Capacity)
                    {
                        id = room.Id;
                        break;
                    }
                    rooms.Remove(room);
                }
            }
            var studentToUpdate = new Student()
            {
                Benefit = student.Benefit,
                FacultyId = facultyId,
                FullName = student.FullName,
                Gender = student.Gender,
                PhoneNumber = student.PhoneNumber,
                RoomId = id
            };
            _context.Students.Add(studentToUpdate);
            _context.SaveChanges();
            return RedirectToAction("MainPage", "Home");
        }
    }
}
