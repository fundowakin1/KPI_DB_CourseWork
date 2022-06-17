using System;
using System.Collections.Generic;

#nullable disable

namespace DataBasesCourseWorkApplication.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public bool Benefit { get; set; }
        public int FacultyId { get; set; }
        public int? RoomId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual Room Room { get; set; }
    }
}
