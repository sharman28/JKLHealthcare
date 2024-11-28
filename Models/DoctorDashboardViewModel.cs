using System.Collections.Generic;

namespace JKLHealthcare.Models
{
    public class DoctorDashboardViewModel
    {
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Caregiver> Caregivers { get; set; } = new List<Caregiver>();

    }
}
