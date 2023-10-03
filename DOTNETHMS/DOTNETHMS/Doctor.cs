using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DOTNETHMS
{
    class Doctor
    {
        public string ID { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }

        public List<Appointment> Appointments { get; private set; }

        public Doctor(string id, string fullName, string email, string phone, string address)
        {
            ID = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Address = address;
            Appointments = new List<Appointment>();
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            Appointments = appointments;
        }

        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("DOTNET Hospital Management System");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Doctor Menu");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {FullName}\n");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List Doctor Details");
                Console.WriteLine("2. List Patients");
                Console.WriteLine("3. List Appointments");
                Console.WriteLine("4. Check Particular Patient");
                Console.WriteLine("5. List Appointments With Patient");
                Console.WriteLine("6. Logout");
                Console.WriteLine("7. Exit");

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ListDoctorDetails();
                        break;
                    case '2':
                        ListPatients();
                        break;
                    case '3':
                        ListAppointments();
                        break;
                    case '4':
                        CheckParticularPatient();
                        break;
                    case '5':
                        ListAppointmentsWithPatient();
                        break;
                    case '6':
                        exit = true;
                        break;
                    case '7':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ListDoctorDetails()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("My Details\n");
            Console.WriteLine("Name\t\t| Email Address\t| Phone\t\t| Address");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{FullName}\t| {Email}\t| {Phone}\t| {Address}");

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void ListPatients()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("My Patients\n");
            Console.WriteLine("Patient\t\t| Doctor\t| Email Address\t| Phone\t\t| Address");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            List<Patient> patients = ReadPatientsFromFile();
            foreach (Patient patient in patients)
            {
                if (patient.AssignedDoctorName == FullName)
                {
                    Console.WriteLine($"{patient.FullName}\t| {patient.AssignedDoctorName}\t| {patient.Email}\t| {patient.Phone}\t| {patient.Address}");
                }
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void ListAppointments()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("All Appointments\n");
            Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            List<Appointment> appointments = ReadAppointmentsFromFile();
            foreach (Appointment appointment in appointments)
            {
                Console.WriteLine($"{appointment.DoctorName}\t| {appointment.PatientName}\t| {appointment.Description}");
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void CheckParticularPatient()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Check Patient Details\n");
            Console.Write("Enter the ID of the patient to check: ");
            string input = Console.ReadLine();

            List<Patient> patients = ReadPatientsFromFile();
            Patient patient = patients.Find(p => p.ID == input);

            if (patient != null)
            {
                Console.WriteLine($"\nPatient\t\t| Doctor\t| Email Address\t| Phone\t\t| Address");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{patient.FullName}\t| {patient.AssignedDoctorName}\t| {patient.Email}\t| {patient.Phone}\t| {patient.Address}");
            }
            else
            {
                Console.WriteLine("\nPatient not found with the provided ID.");
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void ListAppointmentsWithPatient()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Appointments With Patient\n");
            Console.Write("Enter the ID of the patient you would like to view appointments for: ");
            string input = Console.ReadLine();

            List<Appointment> appointments = ReadAppointmentsFromFile();
            List<Patient> patients = ReadPatientsFromFile();
            Patient selectedPatient = patients.Find(p => p.ID == input);

            if (selectedPatient != null)
            {
                Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");

                foreach (Appointment appointment in appointments)
                {
                    if (appointment.PatientName == selectedPatient.FullName)
                    {
                        Console.WriteLine($"{appointment.DoctorName}\t| {appointment.PatientName}\t| {appointment.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nPatient not found with the provided ID.");
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private List<Patient> ReadPatientsFromFile()
        {
            List<Patient> patients = new List<Patient>();

            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/patients.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        Patient patient = new Patient(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                        patients.Add(patient);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading patient data from file.");
            }

            return patients;
        }

        private List<Appointment> ReadAppointmentsFromFile()
        {
            List<Appointment> appointments = new List<Appointment>();

            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/appointments.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        Appointment appointment = new Appointment(parts[0], parts[1], parts[2]);
                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading appointment data from file.");
            }

            return appointments;
        }
    }
}
