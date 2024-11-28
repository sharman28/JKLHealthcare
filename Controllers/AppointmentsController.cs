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

        // GET: Appointments/Create
        public IActionResult Create()
        {
            // Retrieve the logged-in patient's email from the session
            var patientEmail = HttpContext.Session.GetString("PatientEmail");

            if (string.IsNullOrEmpty(patientEmail))
            {
                // Redirect to login if the patient is not logged in
                return RedirectToAction("Login", "Patients");
            }

            // Retrieve the PatientId from the database based on the email
            var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);

            if (patient == null)
            {
                // Redirect to login if patient is not found
                return RedirectToAction("Login", "Patients");
            }

            // Pass PatientId to the view through ViewData
            ViewData["PatientId"] = patient.PatientId;

            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentDateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the logged-in patient's email from the session
                var patientEmail = HttpContext.Session.GetString("PatientEmail");
                var patient = _context.Patients.FirstOrDefault(p => p.Email == patientEmail);

                if (patient == null)
                {
                    return RedirectToAction("Login", "Patients");
                }

                // Set the PatientId from the retrieved patient object
                appointment.PatientId = patient.PatientId;

                // Save the appointment to the database
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                // Redirect to the Patient Profile page after successful booking
                return RedirectToAction("Profile", "Patients", new { id = appointment.PatientId });
            }

            // If ModelState is invalid, reload the form with the PatientId in ViewData
            ViewData["PatientId"] = appointment.PatientId;
            return View(appointment); // In case of validation errors, stay on the form
        }
    }
}
