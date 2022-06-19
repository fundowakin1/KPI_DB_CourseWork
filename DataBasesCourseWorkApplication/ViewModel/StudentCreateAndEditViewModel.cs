using System.Collections.Generic;
using System.Net.NetworkInformation;
using DataBasesCourseWorkApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataBasesCourseWorkApplication.ViewModel
{
    public class StudentCreateAndEditViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public bool Benefit { get; set; }
        public string FacultyName { get; set; }
        public int FacultyId { get; set; }
        public int? RoomId { get; set; }
        public SelectList FacultiesName { get; set; }
    }
}
