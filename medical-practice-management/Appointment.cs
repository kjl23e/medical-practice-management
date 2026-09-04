using Assignment2.Models;

namespace Assignment2.Models
{
    public class Appointment
    {
        public Patient? Patient;
        public Physician? Physician;
        public string? Day;
        public string? Time;
        public string? Room;
        public string[] Diagnoses = new string[0];
        public Treatment[] Treatments = new Treatment[0];
    }

    public class Treatment
    {
        public string? Name;
        public double Cost;
    }
}