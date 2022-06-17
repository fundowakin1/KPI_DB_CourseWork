using System;
using System.Collections.Generic;

#nullable disable

namespace DataBasesCourseWorkApplication.Models
{
    public partial class Dormitory
    {
        public Dormitory()
        {
            DormitoryOfFaculties = new HashSet<DormitoryOfFaculty>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public string CommandantFullName { get; set; }
        public string CommandantPhoneNumber { get; set; }

        public virtual ICollection<DormitoryOfFaculty> DormitoryOfFaculties { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
