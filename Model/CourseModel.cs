using DataAccessLayer.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PresentationLayer.Models
{
    public class CourseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int CourseID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
