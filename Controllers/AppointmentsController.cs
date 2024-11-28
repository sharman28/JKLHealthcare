using Microsoft.AspNetCore.Mvc;
using JKLHealthcare.Models;
using JKLHealthcare.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JKLHealthcare.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly JKLHealthcareDbContext _context;

        public AppointmentsController(JKLHealthcareDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            // Retrieve the logged-in patient's email from the session
            var patientEmail = HttpContext.Session.GetString("PatientEmail");

            if (string.IsNullOrEmpty(patientEmail))
            {
                
                return RedirectToAction("Login", "Patients");
            }

           
            var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);

            if (patient == null)
            {
               
                return RedirectToAction("Login", "Patients");
            }

           
            ViewData["PatientId"] = patient.PatientId;

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentDateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
               
                var patientEmail = HttpContext.Session.GetString("PatientEmail");
                var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);

                if (patient == null)
                {
                    return RedirectToAction("Login", "Patients");
                }

                
                appointment.PatientId = patient.PatientId;

                // Saves the appointment to the database
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("Profile", "Patients", new { id = appointment.PatientId });
            }

            // If ModelState is invalid, reload the form with the PatientId in ViewData
            ViewData["PatientId"] = appointment.PatientId;
            return View(appointment); 
        }
    }
}
