using System;
using System.Collections.Generic;

namespace JKLHealthcare.Models
{
    public class Patient
    {
        public int PatientId { get; set; } // Primary key for Patient

        public string FirstName { get; set; } = string.Empty; // Non-nullable with default value
        public string LastName { get; set; } = string.Empty;  // Non-nullable with default value
        public DateTime DateOfBirth { get; set; } // Property for Date of Birth

        public string Email { get; set; } = string.Empty; // Email for login
        public string Password { get; set; } = string.Empty; // Password for login
        public string Number { get; set; } = string.Empty; // Contact number of the patient
        public string Address { get; set; } = string.Empty; // Address of the patient

        // Full name property for easier display
        public string FullName => $"{FirstName} {LastName}";

        // Navigation property for Appointments
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
