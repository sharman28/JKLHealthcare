using Microsoft.AspNetCore.Identity;

namespace JKLHealthcare.Utilities
{
    public class PasswordHasher
    {
        private readonly IPasswordHasher<object> _hasher;

        public PasswordHasher()
        {
            _hasher = new PasswordHasher<object>();
        }

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(new object(), password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
