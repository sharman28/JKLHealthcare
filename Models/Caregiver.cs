namespace JKLHealthcare.Models
{
    public class Caregiver
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Non-nullable with default value
        public string? Specialization { get; set; }       // Nullable
        public string ContactInfo { get; set; } = string.Empty; // Non-nullable with default value

        // Navigation properties (if needed)
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
