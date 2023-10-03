using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DOTNETHMS
{
    class Administrator
{
    public string ID { get; private set; }
    public string FullName { get; private set; }

    public Administrator(string id, string fullName)
    {
        ID = id;
        FullName = fullName;
    }

    public void DisplayMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Administrator Menu");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System {FullName}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List All Doctors");
            Console.WriteLine("2. Check Doctor Details");
            Console.WriteLine("3. List All Patients");
            Console.WriteLine("4. Check Patient Details");
            Console.WriteLine("5. Add Doctor");
            Console.WriteLine("6. Add Patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit");

            char choice = Console.ReadKey().KeyChar;

            switch (choice)
            {
                case '1':
                    ListAllDoctors();
                    break;
                case '2':
                    CheckDoctorDetails();
                    break;
                case '3':
                    ListAllPatients();
                    break;
                case '4':
                    CheckPatientDetails();
                    break;
                case '5':
                    AddDoctor();
                    break;
                case '6':
                    AddPatient();
                    break;
                case '7':
                    exit = true;
                    break;
                case '8':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Please try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ListAllDoctors()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("All Doctors\n");
        Console.WriteLine("Name\t\t| Email Address\t| Phone\t\t| Address");
        Console.WriteLine("--------------------------------------------------------------------------------------------------");

        List<Doctor> doctors = ReadDoctorsFromFile();
        foreach (Doctor doctor in doctors)
        {
            Console.WriteLine($"{doctor.FullName}\t| {doctor.Email}\t| {doctor.Phone}\t| {doctor.Address}");
        }

        Console.WriteLine("\nPress any key to return to menu.");
        Console.ReadKey();
    }

    private void CheckDoctorDetails()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Doctor Details\n");
        Console.Write("Please enter the ID of the doctor whose details you are checking. Or press 'n' to return to menu: ");
        string input = Console.ReadLine();

        List<Doctor> doctors = ReadDoctorsFromFile();
        Doctor doctor = doctors.Find(d => d.ID == input);

        if (doctor != null)
        {
            Console.WriteLine($"\nName\t\t| Email Address\t| Phone\t\t| Address");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{doctor.FullName}\t| {doctor.Email}\t| {doctor.Phone}\t| {doctor.Address}");
        }
        else
        {
            Console.WriteLine("\nDoctor not found with the provided ID.");
        }

        Console.WriteLine("\nPress any key to return to menu.");
        Console.ReadKey();
    }

    private void ListAllPatients()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("All Patients\n");
        Console.WriteLine("Patient\t\t| Doctor\t| Email Address\t| Phone\t\t| Address");
        Console.WriteLine("--------------------------------------------------------------------------------------------------");

        List<Patient> patients = ReadPatientsFromFile();
        foreach (Patient patient in patients)
        {
            Console.WriteLine($"{patient.FullName}\t| {patient.AssignedDoctorName}\t| {patient.Email}\t| {patient.Phone}\t| {patient.Address}");
        }

        Console.WriteLine("\nPress any key to return to menu.");
        Console.ReadKey();
    }

    private void CheckPatientDetails()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Patient Details\n");
        Console.Write("Please enter the ID of the patient you are checking. Or press 'n' to return to menu: ");
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

    private void AddDoctor()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Add Doctor\n");
        Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        Console.Write("Street Number: ");
        string streetNumber = Console.ReadLine();
        Console.Write("Street: ");
        string street = Console.ReadLine();
        Console.Write("City: ");
        string city = Console.ReadLine();
        Console.Write("State: ");
        string state = Console.ReadLine();

        string id = GenerateID();
        string address = $"{streetNumber} {street}, {city}, {state}";

        Doctor newDoctor = new Doctor(id, $"{firstName} {lastName}", email, phone, address);
        SaveDoctorToFile(newDoctor);

        Console.WriteLine($"{newDoctor.FullName} added to the system!");
        Console.WriteLine("\nPress any key to return to menu.");
        Console.ReadKey();
    }

    private void AddPatient()
    {
        Console.Clear();
        Console.WriteLine("DOTNET Hospital Management System");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Add Patient\n");
        Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        Console.Write("Street Number: ");
        string streetNumber = Console.ReadLine();
        Console.Write("Street: ");
        string street = Console.ReadLine();
        Console.Write("City: ");
        string city = Console.ReadLine();
        Console.Write("State: ");
        string state = Console.ReadLine();

        string id = GenerateID();
        string address = $"{streetNumber} {street}, {city}, {state}";


        List<Doctor> doctors = ReadDoctorsFromFile();
        Console.WriteLine("Please choose a doctor for the patient:");
        for (int i = 0; i < doctors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {doctors[i].FullName} | {doctors[i].Email} | {doctors[i].Phone} | {doctors[i].Address}");
        }

        Console.Write("Please choose a doctor: ");
        int doctorChoice;
        if (int.TryParse(Console.ReadLine(), out doctorChoice) && doctorChoice >= 1 && doctorChoice <= doctors.Count)
        {
            Doctor selectedDoctor = doctors[doctorChoice - 1];

            Patient newPatient = new Patient(id, $"{firstName} {lastName}", email, phone, address, selectedDoctor.FullName);
            SavePatientToFile(newPatient);

            Console.WriteLine($"{newPatient.FullName} added to the system and assigned to {selectedDoctor.FullName}!");
        }
        else
        {
            Console.WriteLine("Invalid choice. Patient registration canceled.");
        }

        Console.WriteLine("\nPress any key to return to menu.");
        Console.ReadKey();
    }

    private string GenerateID()
    {
        return DateTime.Now.ToString("yyMMddHHmmss");
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

    private void SaveDoctorToFile(Doctor doctor)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("F:/New folder/DOTNETHMS/DOTNETHMS/doctors.txt", true))
            {
                sw.WriteLine($"{doctor.ID}|{doctor.FullName}|{doctor.Email}|{doctor.Phone}|{doctor.Address}");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error saving doctor data to file.");
        }
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

    private void SavePatientToFile(Patient patient)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("F:/New folder/DOTNETHMS/DOTNETHMS/patients.txt", true))
            {
                sw.WriteLine($"{patient.ID}|{patient.FullName}|{patient.Email}|{patient.Phone}|{patient.Address}|{patient.AssignedDoctorName}");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error saving patient data to file.");
        }
    }
}
}
