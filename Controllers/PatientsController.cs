using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JKLHealthcare.Models;
using JKLHealthcare.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace JKLHealthcare.Controllers
{
    public class PatientsController : Controller
    {
        private readonly JKLHealthcareDbContext _context;
        private readonly PasswordHasher<Patient> _passwordHasher;

        public PatientsController(JKLHealthcareDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Patient>();
        }

        
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Patient patient)
        {
            if (ModelState.IsValid)
            {
                // Hashes the password before saving it
                patient.Password = _passwordHasher.HashPassword(patient, patient.Password);
                _context.Patients.Add(patient);
                _context.SaveChanges();

                HttpContext.Session.SetString("PatientEmail", patient.Email);
                return RedirectToAction("Dashboard");
            }
            return View(patient);
        }

       
        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Email == email);
            if (patient != null)
            {
                // Verifies the hashed password
                var result = _passwordHasher.VerifyHashedPassword(patient, patient.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("PatientEmail", patient.Email);
                    return RedirectToAction("Dashboard");
                }
            }
            ViewBag.Error = "Invalid login attempt";
            return View();
        }

        
        public IActionResult Dashboard()
        {
            var patientEmail = HttpContext.Session.GetString("PatientEmail");
            if (string.IsNullOrEmpty(patientEmail))
            {
                return RedirectToAction("Login");
            }

            var patient = _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.Email == patientEmail);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

       
        public IActionResult Profile(int id)
        {
            var patient = _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        
        public IActionResult Edit()
        {
            var patientEmail = HttpContext.Session.GetString("PatientEmail");
            if (string.IsNullOrEmpty(patientEmail))
            {
                return RedirectToAction("Login");
            }

            var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient updatedPatient)
        {
            if (ModelState.IsValid)
            {
                var patientEmail = HttpContext.Session.GetString("PatientEmail");
                var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);

                if (patient == null)
                {
                    return NotFound();
                }

               
                patient.Email = updatedPatient.Email;
                patient.Number = updatedPatient.Number;
                patient.DateOfBirth = updatedPatient.DateOfBirth;
                patient.Address = updatedPatient.Address;

                // Handles the password update securely
                if (!string.IsNullOrEmpty(updatedPatient.Password))
                {
                    patient.Password = _passwordHasher.HashPassword(patient, updatedPatient.Password);
                }

                _context.Patients.Update(patient);
                _context.SaveChanges();

                
                if (patientEmail != updatedPatient.Email)
                {
                    HttpContext.Session.SetString("PatientEmail", patient.Email);
                }

                return RedirectToAction("Dashboard");
            }

            return View(updatedPatient);
        }
    }
}
