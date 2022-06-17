using System;
using System.Collections.Generic;

#nullable disable

namespace DataBasesCourseWorkApplication.Models
{
    public partial class DormitoryOfFaculty
    {
        public int DormitoryId { get; set; }
        public int FacultyId { get; set; }

        public virtual Dormitory Dormitory { get; set; }
        public virtual Faculty Faculty { get; set; }
    }
}
