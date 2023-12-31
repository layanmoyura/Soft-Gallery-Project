﻿using System;
using System.ComponentModel.DataAnnotations;

public enum Grade
{
    A, B, C, D, F
}

namespace PresentationLayer.Models
{
    public class EnrollmentModel
    {
        public int EnrollmentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
        public Grade? Grade { get; set; }
        public string DisplayGrade => Grade?.ToString();

        public CourseModel Course { get; set; }
        public StudentModel Student { get; set; }


    }
}
