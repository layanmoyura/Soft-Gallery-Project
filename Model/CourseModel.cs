using DataAccessLayer.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace PresentationLayer.Models
{
    public class CourseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }

        public int Credits { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
