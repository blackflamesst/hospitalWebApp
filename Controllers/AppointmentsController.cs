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
        public async Task<IActionResult> GetDoctors([FromQuery] int? categoryId)
        {
            IQueryable<Doctor> query = _context.Doctors.AsQueryable();
            if(categoryId.HasValue)
            {
                query = query.Where(d => d.Category_Id == categoryId.Value);
            }
            var doctors = await query.ToListAsync();
            return Ok(doctors);
        }

        [HttpGet("doctors/{doctor_id}/procedures")]
        public async Task<IActionResult> GetDoctorProcedures(int doctor_id)
        {
            var procedures = await _context.Procedures
                .Where(p => _context.Doctor_Procedures
                .Any(dp => dp.Doctor_Id == doctor_id && dp.Procedure_Id == p.Id))
                .ToListAsync();

            return Ok(procedures);
        }

        [HttpGet("available-dates")]
        public async Task<IActionResult> GetAvailableDates([FromQuery] int doctorId)
        {
            var dates = new List<object>();
            var today = DateOnly.FromDateTime(DateTime.Now);
            var existingAppointments = await _context.Appointments
                .Where(a => a.Doctor_Id == doctorId)
                .Select(a => a.Appointment_Date)
                .ToListAsync();

            for (int i = 0; i < 7; i++)
            {
                var date = today.AddDays(i);
                var isAvailable = !existingAppointments.Contains(date);
                dates.Add(new { date = date.ToString("yyyy-MM-dd"), isAvailable });
            }
            return Ok(dates);
        }

        [HttpGet("available-times")]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] int doctorId, [FromQuery] string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest("Date is not found");
            }

            var selectedDate = DateOnly.ParseExact(date, "yyyy-MM-dd", null);
            var existingAppointments = await _context.Appointments
                .Where(a => a.Doctor_Id == doctorId && a.Appointment_Date == selectedDate)
                .Select(a => a.Appointment_Time)
                .ToListAsync();

            var times = new List<object>();
            for (int hour = 9; hour <= 17; hour++)
            {
                var time = new TimeSpan(hour, 0, 0);
                var isAvailable = !existingAppointments.Any(t => t.ToTimeSpan() == time);
                times.Add(new { time = time.ToString(@"hh\:mm"), isAvailable });
            }

            return Ok(times);
        }

        [HttpPost("creating")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            Console.WriteLine($"Received appointment request: CustomerName={request.CustomerName}, Doctor_Id={request.Doctor_Id}, Procedure_Id={request.Procedure_Id}, Date={request.Appointment_Date}, Time={request.Appointment_Time}");

            var customer = await _context.Customers.FirstOrDefaultAsync(u => u.Name == request.CustomerName);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = request.CustomerName,
                    Age = 18,
                    Date_Of_Birth = DateTime.MinValue,
                    Login = "user",
                    Password = "default"
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            var procedure = await _context.Procedures.FindAsync(request.Procedure_Id);
            if (procedure == null) return BadRequest("Procedure not found");

            var appointment = new Appointment
            {
                Doctor_Id = request.Doctor_Id,
                Procedure_Id = request.Procedure_Id,
                Customer_Id = customer.Id,
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
