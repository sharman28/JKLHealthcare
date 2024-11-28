namespace JKLHealthcare.Models
{
    public class DoctorLoginViewModel
    {
        public string Username { get; set; } = string.Empty; // or `required` modifier in C# 11+
        public string Password { get; set; } = string.Empty; // or `required` modifier in C# 11+

    }
}
