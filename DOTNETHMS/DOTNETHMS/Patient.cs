using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace DOTNETHMS
{
    class Patient
    {
        public string ID { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string AssignedDoctorName { get; private set; }
        public List<Appointment> Appointments { get; private set; }

        public Patient(string id, string fullName, string email, string phone, string address, string assignedDoctorName)
        {
            ID = id;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Address = address;
            AssignedDoctorName = assignedDoctorName;
            Appointments = new List<Appointment>();
        }

        public void LoadAppointments(List<Appointment> appointments)
        {
            Appointments = appointments;
        }

        public void DisplayAppointments()
        {
            List<Appointment> appointments = ReadAppointmentsFromFile();
            Console.WriteLine("\nAppointments for " + FullName + "\n");
            Console.WriteLine("Doctor\t\t| Description");
            Console.WriteLine("---------------------------------------------");
            foreach (Appointment appointment in appointments)
            {
                if (appointment.PatientID == ID)
                {
                    Console.WriteLine($"{appointment.DoctorName}\t| {appointment.Description}");
                }
            }
        }

        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("DOTNET Hospital Management System");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Patient Menu");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {FullName}\n");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List Patient Details");
                Console.WriteLine("2. List My Doctor Details");
                Console.WriteLine("3. List All Appointments");
                Console.WriteLine("4. Book Appointment");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Exit System");

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ListPatientDetails();
                        break;
                    case '2':
                        ListMyDoctorDetails();
                        break;
                    case '3':
                        ListAllAppointments();
                        break;
                    case '4':
                        BookAppointment();
                        break;
                    case '5':
                        exit = true;
                        break;
                    case '6':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ListPatientDetails()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("My Details\n");
            Console.WriteLine($"{FullName}'s Details\n");
            Console.WriteLine($"Patient ID: {ID}");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {Phone}");

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void ListMyDoctorDetails()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("My Doctor\n");
            Console.WriteLine("Your Doctor:");
            Console.WriteLine("Name\t\t| Email Address\t| Phone\t\t| Address");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            List<Doctor> doctors = ReadDoctorsFromFile();
            Doctor assignedDoctor = doctors.Find(d => d.FullName == AssignedDoctorName);

            if (assignedDoctor != null)
            {
                Console.WriteLine($"{assignedDoctor.FullName}\t| {assignedDoctor.Email}\t| {assignedDoctor.Phone}\t| {assignedDoctor.Address}");
            }
            else
            {
                Console.WriteLine("No assigned doctor.");
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void ListAllAppointments()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("My Appointments\n");
            Console.WriteLine($"Appointments for {FullName}\n");
            Console.WriteLine("Doctor\t\t| Patient\t\t| Description");
            Console.WriteLine("------------------------------------------------------------------------------------------");

            List<Appointment> appointments = ReadAppointmentsFromFile();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.PatientID == ID)
                {
                    Console.WriteLine($"{appointment.DoctorName}\t| {appointment.PatientName}\t| {appointment.Description}");
                }
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private void BookAppointment()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Book Appointment\n");

            if (!string.IsNullOrEmpty(AssignedDoctorName))
            {
                Console.WriteLine("You are not registered with any doctor!");
                Console.WriteLine("Please choose which doctor you would like to register with:");

                List<Doctor> doctors = ReadDoctorsFromFile();
                for (int i = 0; i < doctors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {doctors[i].FullName} | {doctors[i].Email} | {doctors[i].Phone} | {doctors[i].Address}");
                }

                Console.Write("Please choose a doctor: ");
                int doctorChoice;
                if (int.TryParse(Console.ReadLine(), out doctorChoice) && doctorChoice >= 1 && doctorChoice <= doctors.Count)
                {
                    Doctor selectedDoctor = doctors[doctorChoice - 1];
                    Console.Write($"You are booking a new appointment with {selectedDoctor.FullName}\nDescription of the appointment: ");
                    string description = Console.ReadLine();

                    Appointment newAppointment = new Appointment(selectedDoctor.FullName, FullName, description);
                    SaveAppointmentToFile(newAppointment);

                    Console.WriteLine("The appointment has been booked successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Appointment booking canceled.");
                }
            }
            else
            {
                Console.WriteLine("You are not registered with any doctor. Please contact the administrator.");
            }

            Console.WriteLine("\nPress any key to return to menu.");
            Console.ReadKey();
        }

        private List<Doctor> ReadDoctorsFromFile()
        {
            List<Doctor> doctors = new List<Doctor>();

            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/doctors.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        Doctor doctor = new Doctor(parts[0], parts[1], parts[2], parts[3], parts[4]);
                        doctors.Add(doctor);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading doctor data from file.");
            }

            return doctors;
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

        private void SaveAppointmentToFile(Appointment appointment)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("F:/New folder/DOTNETHMS/DOTNETHMS/appointments.txt", true))
                {
                    sw.WriteLine($"{appointment.DoctorName}|{appointment.PatientName}|{appointment.Description}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error saving appointment data to file.");
            }
        }
    }

  

}
