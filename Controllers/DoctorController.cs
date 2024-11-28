using Microsoft.AspNetCore.Mvc;
using JKLHealthcare.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using JKLHealthcare.Hubs;
using Microsoft.EntityFrameworkCore;
using JKLHealthcare.Models;
using Microsoft.AspNetCore.Http;

namespace JKLHealthcare.Controllers
{
    public class DoctorController : Controller
    {
        private readonly JKLHealthcareDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DoctorController(JKLHealthcareDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(DoctorLoginViewModel model)
        {
            // Authenticate with specific username and password
            if (ModelState.IsValid && model.Username == "jklhealthcare" && model.Password == "health")
            {
                
                HttpContext.Session.SetString("DoctorLoggedIn", "true");

               
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password";
            return View(model);
        }

        //Doctor/Dashboard
        public IActionResult Dashboard()
        {
            
            if (HttpContext.Session.GetString("DoctorLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            // Loads patient appointments and caregiver availability
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .ToList();

            var caregivers = _context.Caregivers.ToList(); 

            var viewModel = new DoctorDashboardViewModel
            {
                Appointments = appointments,
                Caregivers = caregivers
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCaregiver(int appointmentId, int caregiverId)
        {
           
            if (HttpContext.Session.GetString("DoctorLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.CaregiverId = caregiverId;
                await _context.SaveChangesAsync();

                // Sends real-time notification to the patient
                await _hubContext.Clients.User(appointment.PatientId.ToString())
                    .SendAsync("ReceiveNotification", "Your caregiver has been assigned.");

                return RedirectToAction("Dashboard");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RescheduleAppointment(int appointmentId, DateTime newDateTime)
        {
            
            if (HttpContext.Session.GetString("DoctorLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.AppointmentDateTime = newDateTime;
                await _context.SaveChangesAsync();

                // Sends real-time notification to the patient
                await _hubContext.Clients.User(appointment.PatientId.ToString())
                    .SendAsync("ReceiveNotification", "Your appointment has been rescheduled.");

                return RedirectToAction("Dashboard");
            }

            return NotFound();
        }

        
        public IActionResult AddCaregiver()
        {
            
            if (HttpContext.Session.GetString("DoctorLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCaregiver(Caregiver caregiver)
        {
           
            if (HttpContext.Session.GetString("DoctorLoggedIn") != "true")
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                _context.Caregivers.Add(caregiver);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); 
            }

            return View(caregiver); 
        }
    }
}
