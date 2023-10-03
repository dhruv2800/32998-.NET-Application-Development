using System;

namespace DOTNETHMS
{
    class Appointment
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string Description { get; set; }
        public string PatientID { get; internal set; }

        public Appointment(string doctorName, string patientName, string description)
        {
            DoctorName = doctorName;
            PatientName = patientName;
            Description = description;
        }

        
    }
}