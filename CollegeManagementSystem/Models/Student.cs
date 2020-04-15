using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    public class Student
    {
        [Display(Name = "Student Id")]
        public string StudentId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddr { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        public string ErrorMsg { get; set; }
    }
}