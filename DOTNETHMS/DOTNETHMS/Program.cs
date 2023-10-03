using System;
using System.IO;
using System.Linq;

namespace DOTNETHMS
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("DOTNET Hospital Management System");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("Login Menu\n");
                Console.WriteLine("Please choose a role:");
                Console.WriteLine("1. Patient");
                Console.WriteLine("2. Doctor");
                Console.WriteLine("3. Administrator");
                Console.WriteLine("4. Exit");

                char choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        PatientLogin();
                        break;
                    case '2':
                        DoctorLogin();
                        break;
                    case '3':
                        AdministratorLogin();
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void PatientLogin()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Patient Login\n");

            Console.Write("Enter your ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            if (ValidatePatientLogin(id, password))
            {
                DisplayPatientMenu(id);
            }
            else
            {
                Console.WriteLine("\nInvalid ID or password. Please try again.");
                Console.ReadKey();
            }
        }

        private static void DoctorLogin()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Doctor Login\n");

            Console.Write("Enter your ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            if (ValidateDoctorLogin(id, password))
            {
                DisplayDoctorMenu(id);
            }
            else
            {
                Console.WriteLine("\nInvalid ID or password. Please try again.");
                Console.ReadKey();
            }
        }

        private static void AdministratorLogin()
        {
            Console.Clear();
            Console.WriteLine("DOTNET Hospital Management System");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Administrator Login\n");

            Console.Write("Enter your ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();

            if (ValidateAdministratorLogin(id, password))
            {
                DisplayAdministratorMenu(id);
            }
            else
            {
                Console.WriteLine("\nInvalid ID or password. Please try again.");
                Console.ReadKey();
            }
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (char.IsControl(key.KeyChar))
                {
                    continue;
                }

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); 
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*"); 
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); 
            return password;
        }

        private static bool ValidatePatientLogin(string id, string password)
        {
            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/patientpass.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 3 && parts[0] == id && parts[2] == password)
                        {
                            return true; 
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading patient data from file.");
            }

            return false; 
        }

        private static bool ValidateDoctorLogin(string id, string password)
        {
            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/doctorpass.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 3 && parts[0] == id && parts[2] == password)
                        {
                            return true; 
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading doctor data from file.");
            }

            return false; 
        }

        private static bool ValidateAdministratorLogin(string id, string password)
        {
            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/adminpass.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 3 && parts[0] == id && parts[2] == password)
                        {
                            return true; 
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading administrator data from file.");
            }

            return false;
        }

        private static void DisplayPatientMenu(string id)
        {
            Console.WriteLine($"Welcome, Patient {id}!");

            Patient patient = ReadPatientDetails(id);

            if (patient != null)
            {
               
                Console.WriteLine("Valid Credentials");
                
            }
            else
            {
                Console.WriteLine("Patient details not found.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            if (patient != null)
            {
                patient.DisplayMenu();
            }
        }

        private static Patient ReadPatientDetails(string id)
        {
            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/patients.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts[0] == id)
                        {
                            return new Patient(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading patient data from file.");
            }

            return null; 
        }


        private static void DisplayDoctorMenu(string id)
        {
            Console.WriteLine($"Welcome, Doctor {id}!");

            Doctor doctor = ReadDoctorDetails(id);

            if (doctor != null)
            {
                Console.WriteLine("Valid Credentials");
            }
            else
            {
                Console.WriteLine("Doctor details not found.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            if (doctor != null)
            {
                doctor.DisplayMenu();
            }
        }

        private static Doctor ReadDoctorDetails(string id)
        {
            try
            {
                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/doctors.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts[0] == id)
                        {
                            return new Doctor(parts[0], parts[1], parts[2], parts[3], parts[4]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading doctor data from file.");
            }

            return null; 
        }


        private static void DisplayAdministratorMenu(string id)
        {
            Console.WriteLine($"Welcome, Administrator {id}!");

            Administrator administrator = ReadAdministratorDetails(id);

            if (administrator != null)
            {
                Console.WriteLine("Valid Credentials");
            }
            else
            {
                Console.WriteLine("Administrator details not found.");
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            if (administrator != null)
            {
                administrator.DisplayMenu();
            }
        }

        private static Administrator ReadAdministratorDetails(string id)
        {
            try
            {

                using (StreamReader sr = new StreamReader("F:/New folder/DOTNETHMS/DOTNETHMS/adminpass.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts[0] == id)
                        {
                            return new Administrator(parts[0], parts[1]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading administrator data from file.");
            }

            return null;
        }

    }
}
