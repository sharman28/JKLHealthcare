using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JKLHealthcare.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } 

        [ForeignKey("Patient")]
        public int PatientId { get; set; } // Foreign key for the Patient
        public Patient? Patient { get; set; } 

        [ForeignKey("Caregiver")]
        public int? CaregiverId { get; set; } // Makes CaregiverId nullable
        public Caregiver? Caregiver { get; set; } 


        public DateTime AppointmentDateTime { get; set; }

        public string Status { get; set; } = "Scheduled"; 
    }
}
