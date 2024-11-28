using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKLHealthcare.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // Primary identifier for appointments

        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key for the Patient
        public Patient? Patient { get; set; } // Navigation property for Patient

        [ForeignKey("Caregiver")]
        public int? CaregiverId { get; set; } // Make CaregiverId nullable
        public Caregiver? Caregiver { get; set; } // Adjust navigation property accordingly


        public DateTime AppointmentDateTime { get; set; }

        public string Status { get; set; } = "Scheduled"; // Default status
    }
}
