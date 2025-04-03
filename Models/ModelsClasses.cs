namespace Hospital.Models
{

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category_Id { get; set; }
        public int Office { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Procedure
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }

    public class DoctorProcedure
    {
        public int Doctor_Id { get; set; }
        public int Procedure_Id { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public int Doctor_Id { get; set; }
        public int Procedure_Id { get; set; }
        public int Customer_Id { get; set; }
        public DateOnly Appointment_Date { get; set; }
        public TimeOnly Appointment_Time { get; set; }
        public int Total_Cost { get; set; }
    }
}
