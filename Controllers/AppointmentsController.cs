using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Context;
using Hospital.Models;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly HospitalContext _context;
        public AppointmentsController(HospitalContext context)
        {
            _context = context;
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("procedures")]
        public async Task<IActionResult> GetProcedures()
        {
            var procedures = await _context.Procedures.ToListAsync();
            return Ok(procedures);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(u => u.Name == request.CustomerName);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = request.CustomerName,
                    Age = 0,
                    DateOfBirth = DateTime.MinValue
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var procedure = await _context.Procedures.FindAsync(request.Procedure_Id);
            if (procedure == null) return BadRequest("Procedure not found");

            var appointment = new Appointment
            {
                Doctor_ID = request.Doctor_Id,
                Procedure_Id = request.Procedure_Id,
                Appointment_Date = request.Appointment_Date,
                Appointment_Time = request.Appointment_Time,
                Total_Cost = procedure.Price
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Appointment created successfully!", AppointmentId = appointment.Id });
        }

        public class AppointmentRequest
        {
            public string CustomerName { get; set; }
            public int Doctor_Id { get; set; }
            public int Procedure_Id { get; set; }
            public DateOnly Appointment_Date { get; set; }
            public TimeOnly Appointment_Time { get; set; }
        }
    }
}
