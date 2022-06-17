using System;
using System.Collections.Generic;

#nullable disable

namespace DataBasesCourseWorkApplication.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            DormitoryOfFaculties = new HashSet<DormitoryOfFaculty>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DormitoryOfFaculty> DormitoryOfFaculties { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
