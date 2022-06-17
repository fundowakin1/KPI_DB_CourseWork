using System;
using System.Collections.Generic;


namespace DataBasesCourseWorkApplication.Models
{
    public partial class Room
    {
        public Room()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public bool Gender { get; set; }
        public int DormitoryId { get; set; }

        public virtual Dormitory Dormitory { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
