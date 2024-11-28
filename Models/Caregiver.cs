namespace JKLHealthcare.Models
{
    public class Caregiver
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; 
        public string? Specialization { get; set; }      
        public string ContactInfo { get; set; } = string.Empty; 

       
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
