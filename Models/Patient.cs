using System;
using System.Collections.Generic;

namespace JKLHealthcare.Models
{
    public class Patient
    {
        public int PatientId { get; set; } 

        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public DateTime DateOfBirth { get; set; } 

        public string Email { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty; 
        public string Number { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 

        
        public string FullName => $"{FirstName} {LastName}";

       
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
