using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;
using CarConnectCSharp.Exceptions;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace CarConnectCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Services
            ICustomerService customerService = new CustomerService();
            IVehicleService vehicleService = new VehicleService();
            IReservationService reservationService = new ReservationService();
            IAdminService adminService = new AdminService();

            bool isRunning = true;

            while (isRunning)
            {
                try
                {
                    // Show menu options
                    Console.WriteLine(" ");
                    Console.WriteLine("Select an operation:");
                    Console.WriteLine("1. Get Customer by ID");
                    Console.WriteLine("2. Customer login");
                    Console.WriteLine("3. Admin login");
                    Console.WriteLine("4. Add a new vehicle");
                    Console.WriteLine("5. Get available vehicles");
                    Console.WriteLine("6. Create a reservation");
                    Console.WriteLine("7. Cancel reservation");
                    Console.WriteLine("8. Delete a customer by ID");
                    Console.WriteLine("9. Get a full reservation report");
                    Console.WriteLine("10. Delete admin by id");
                    Console.WriteLine("11. For database connection");
                    Console.WriteLine("12. Update Admin");
                    Console.WriteLine("13. Register a customer");
                    Console.WriteLine("14. Register a admin");
                    Console.WriteLine("15. Get reservation by ID");
                    Console.WriteLine("16. Update reservation");
                    Console.WriteLine("17. Update customer");
                    Console.WriteLine("18. Retrive vehicle by ID");
                    Console.WriteLine("19. Remove vehicle");
                    Console.WriteLine("20. Get admin by ID");
                    Console.WriteLine("21. Exit");
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            // Get customer by ID
                            try
                            {
                                Console.Write("Enter customer id: ");
                                int customerId = Convert.ToInt32(Console.ReadLine());
                                var customer = customerService.GetCustomerById(customerId);

                                if (customer == null)
                                {
                                    throw new CustomerNotFoundException("Customer not found with the given ID.");
                                }

                                Console.WriteLine("Customer Details:");
                                Console.WriteLine($"ID: {customer.CustomerID}");
                                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                                Console.WriteLine($"Email: {customer.Email}");
                                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                                Console.WriteLine($"Address: {customer.Address}");
                                Console.WriteLine($"Username: {customer.Username}");
                                Console.WriteLine($"Registration Date: {customer.RegistrationDate}");
                            }
                            catch (CustomerNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                            break;

                        case 2:

                            // Customer Login
                            try
                            {
                                Console.Write("Enter customer username: ");
                                string username = Console.ReadLine()!;
                                Console.Write("Enter customer password: ");
                                string password = Console.ReadLine()!;
                                AuthenticationService authService = new AuthenticationService();
                                bool isAuthenticated = authService.AuthenticateCustomer(username, password);

                                if (isAuthenticated)
                                {
                                    Console.WriteLine("Customer login successful!");
                                }
                                else
                                {
                                    throw new AuthenticationException("Invalid customer credentials.");
                                }
                            }
                            catch (AuthenticationException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;


                        case 3:
                            // Admin login

                            try
                            {
                                Console.Write("Enter admin username: ");
                                string username = Console.ReadLine()!;
                                Console.Write("Enter admin password: ");
                                string password = Console.ReadLine()!;
                                AuthenticationService authService = new AuthenticationService();
                                bool isAuthenticated = authService.AuthenticateAdmin(username, password);

                                if (isAuthenticated)
                                {
                                    Console.WriteLine("Admin login successful!");
                                }
                                else
                                {
                                    throw new AuthenticationException("Invalid admin credentials.");
                                }
                            }
                            catch (AuthenticationException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;


                        case 4:
                            // Add a new vehicle
                            try
                            {
                                Console.Write("Enter vehicle id: ");
                                int vehicleid = Convert.ToInt32(Console.ReadLine());
                                var existingVehicle = vehicleService.GetVehicleById(vehicleid);
                                if (existingVehicle != null)
                                {
                                    throw new InvalidInputException("Vehicle ID already exists. Please choose a different ID.");
                                }
                                Console.Write("Enter vehicle make: ");
                                string make = Console.ReadLine()!;
                                Console.Write("Enter vehicle model: ");
                                string model = Console.ReadLine()!;
                                Console.Write("Enter vehicle year: ");
                                int year = Convert.ToInt32(Console.ReadLine()!);
                                Console.Write("Enter vehicle color: ");
                                string color = Console.ReadLine()!;
                                Console.Write("Enter registration number: ");
                                string regNumber = Console.ReadLine()!;
                                Console.Write("Is the vehicle available (1/0): ");
                                bool availability = Convert.ToBoolean(Console.ReadLine());
                                Console.Write("Enter daily rate: ");
                                int dailyRate = Convert.ToInt32(Console.ReadLine());

                                var newVehicle = new Vehicle(vehicleid, make, model, year, color, regNumber, availability, dailyRate);

                                vehicleService.AddVehicle(newVehicle);
                                Console.WriteLine("Vehicle added successfully.");
                            }
                            catch (InvalidInputException ex)
                            {
                                Console.WriteLine($"Vehicle Input Error: {ex.Message}");
                            }
                            break;

                        case 5:
                            // Get available vehicles
                            try
                            {
                                var availableVehicles = vehicleService.GetAvailableVehicles();
                                if (availableVehicles == null || availableVehicles.Count == 0)
                                    throw new VehicleNotFoundException("No available vehicles.");
                                Console.WriteLine("Available Vehicles:");
                                foreach (var v in availableVehicles)
                                {
                                    Console.WriteLine($"Vehicle ID: {v.VehicleID}, Model: {v.Model}");
                                }
                            }
                            catch (VehicleNotFoundException ex)
                            {
                                Console.WriteLine($"Vehicle Error: {ex.Message}");
                            }
                            break;

                        case 6:
                            // Create a reservation
                            try
                            {
                                Console.Write("Enter reservation ID: ");
                                int reservationId = Convert.ToInt32(Console.ReadLine());

                                var reservationExists = reservationService.GetReservationById(reservationId);
                                if (reservationExists != null)
                                    throw new ReservationException("Reservation ID already exists.");

                                Console.Write("Enter customer ID: ");
                                int customerId = Convert.ToInt32(Console.ReadLine());
                                // Optional check if customer exists

                                Console.Write("Enter vehicle ID: ");
                                int vehicleId = Convert.ToInt32(Console.ReadLine());
                                var vehicleExists = vehicleService.GetVehicleById(vehicleId);
                                if (vehicleExists == null)
                                    throw new VehicleNotFoundException("Vehicle not found.");

                                Console.Write("Enter start date (yyyy-mm-dd): ");
                                DateTime startDate = Convert.ToDateTime(Console.ReadLine());
                                Console.Write("Enter end date (yyyy-mm-dd): ");
                                DateTime endDate = Convert.ToDateTime(Console.ReadLine());

                                Console.Write("Enter status: ");
                                string status = Console.ReadLine()!;

                                // Calculate total cost
                                var reservation = new Reservation
                                {
                                    ReservationID = reservationId,
                                    CustomerID = customerId,
                                    VehicleID = vehicleId,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    Status = status
                                };

                                int totalCost = reservation.CalculateTotalCost(vehicleExists.DailyRate);
                                reservation.TotalCost = totalCost;

                                // Create the reservation
                                bool created = reservationService.CreateReservation(reservation);
                                if (created)
                                    Console.WriteLine($"Reservation created successfully. Total cost: {totalCost}.");
                                else
                                    Console.WriteLine("Failed to create reservation.");
                            }
                            catch (ReservationException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            catch (VehicleNotFoundException ex)
                            {
                                Console.WriteLine($"Vehicle Error: {ex.Message}");
                            }
                            break;


                        case 7:
                            // Cancel reservation
                            try
                            {
                                Console.Write("Enter reservation ID to cancel: ");
                                int reservationId = Convert.ToInt32(Console.ReadLine());
                                var reservationExists = reservationService.GetReservationById(reservationId);
                                if (reservationExists == null)
                                    throw new ReservationException("Reservation not found.");

                                reservationService.CancelReservation(reservationId);
                                Console.WriteLine("Reservation cancelled.");
                            }
                            catch (ReservationException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 8:
                            // Delete a customer by ID
                            try
                            {
                                Console.Write("Enter customer ID to delete: ");
                                int customerIdToDelete = Convert.ToInt32(Console.ReadLine());
                                var customerToDelete = customerService.GetCustomerById(customerIdToDelete);
                                if (customerToDelete == null)
                                    throw new InvalidInputException("Customer not found.");

                                customerService.DeleteCustomer(customerIdToDelete);
                                Console.WriteLine("Customer deleted.");
                            }
                            catch (InvalidInputException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 9:
                            // Generate full reservation report
                            try
                            {
                                ReservationService reservationservice = new ReservationService();
                                VehicleService vehicleservice = new VehicleService();

                                // Check if reservations are available
                                var reservations = reservationservice.GetAllReservations();
                                if (reservations == null || reservations.Count == 0)
                                {
                                    throw new InvalidOperationException("No reservations found to generate the report.");
                                }

                                ReportGenerator reportGenerator = new ReportGenerator(reservationservice, vehicleservice);
                                reportGenerator.GenerateFullReservationReport();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error generating the report: {ex.Message}");
                            }
                            break;

                        case 10:
                            // Delete Admin by ID
                            try
                            {
                                Console.Write("Enter Admin ID to delete: ");
                                int adminId = Convert.ToInt32(Console.ReadLine());

                                bool isDeleted = adminService.DeleteAdmin(adminId);

                                if (!isDeleted)
                                    throw new Exception("Admin deletion failed. Admin ID might not exist.");

                                Console.WriteLine("Admin deleted successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 11:
                            //Database connection
                            try
                            {
                                DatabaseContext db = new DatabaseContext();
                                SqlConnection conn = db.GetConnection();
                                conn.Open();

                                if (conn.State != System.Data.ConnectionState.Open)
                                    throw new DatabaseConnectionException("Unable to open database connection.");

                                Console.WriteLine("Connection to CarConnect successful.");
                                conn.Close();
                            }
                            catch (DatabaseConnectionException ex)
                            {
                                Console.WriteLine($"Custom DB Error: {ex.Message}");
                            }
                            break;

                        case 12:
                            // For Update Admin
                            try
                            {
                                Console.WriteLine("Enter Admin Details to Update:");
                                Console.Write("Admin ID: ");
                                int adminId = int.Parse(Console.ReadLine()!);
                                var existingadmin = adminService.GetAdminById(adminId);
                                if (existingadmin == null)
                                {
                                    throw new AdminNotFoundException("Admin ID already exists. Please choose a different ID.");
                                }
                                Console.Write("Username: ");
                                string username = Console.ReadLine()!;
                                Console.Write("Password: ");
                                string password = Console.ReadLine()!;
                                Console.Write("First Name: ");
                                string firstName = Console.ReadLine()!;
                                Console.Write("Last Name: ");
                                string lastName = Console.ReadLine()!;
                                Console.Write("Email: ");
                                string email = Console.ReadLine()!;
                                Console.Write("Phone Number: ");
                                string phone = Console.ReadLine()!;
                                Console.Write("Role: ");
                                string role = Console.ReadLine()!;
                                Console.Write("Join Date (yyyy-mm-dd): ");
                                DateTime joinDate = DateTime.Parse(Console.ReadLine()!);

                                Admin updatedAdmin = new Admin
                                {
                                    AdminID = adminId,
                                    Username = username,
                                    Password = password,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Email = email,
                                    PhoneNumber = phone,
                                    Role = role,
                                    JoinDate = joinDate
                                };
                                bool isUpdated = adminService.UpdateAdmin(updatedAdmin);
                                Console.WriteLine("Admin updated successfully.");
                            }
                            catch (AdminNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");  
                            }
                            break;

                        case 13:
                            // For Register Customer
                            try
                            {
                                Console.WriteLine("Enter Customer Details for Registration:");

                                // Get Customer data
                                Console.Write("Customer ID: ");
                                int customerId = int.Parse(Console.ReadLine()!);
                                var existingcustomer = customerService.GetCustomerById(customerId);
                                if (existingcustomer != null)
                                {
                                    throw new Exception("Customer Id already exists.");
                                }

                                Console.Write("First Name: ");
                                string firstName = Console.ReadLine()!;
                                Console.Write("Last Name: ");
                                string lastName = Console.ReadLine()!;
                                Console.Write("Email: ");
                                string email = Console.ReadLine()!;
                                Console.Write("Phone Number: ");
                                string phone = Console.ReadLine()!;
                                Console.Write("Username: ");
                                string username = Console.ReadLine()!;
                                Console.Write("Password: ");
                                string password = Console.ReadLine()!;
                                Console.Write("Address: ");
                                string address = Console.ReadLine()!;
                                Console.Write("Registration Date (yyyy-mm-dd): ");
                                DateTime registrationDate = DateTime.Parse(Console.ReadLine()!);

                                Customer newCustomer = new Customer
                                {
                                    CustomerID = customerId,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Email = email,
                                    PhoneNumber = phone,
                                    Username = username,
                                    Password = password,
                                    Address = address,
                                    RegistrationDate = registrationDate
                                };
                                bool isRegistered = customerService.RegisterCustomer(newCustomer);

                                Console.WriteLine("Customer registered successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                            }
                            break;

                        case 14:
                            // Register a new admin
                            try
                            {
                                Console.Write("Enter Admin ID: ");
                                int adminId = Convert.ToInt32(Console.ReadLine());

                                var existingAdmin = adminService.GetAdminById(adminId);
                                if (existingAdmin != null)
                                {
                                    throw new InvalidInputException("Admin ID already exists. Please choose a different ID.");
                                }

                                Console.Write("Enter Username: ");
                                string username = Console.ReadLine()!;
                                Console.Write("Enter Password: ");
                                string password = Console.ReadLine()!;
                                Console.Write("Enter First Name: ");
                                string firstName = Console.ReadLine()!;
                                Console.Write("Enter Last Name: ");
                                string lastName = Console.ReadLine()!;
                                Console.Write("Enter Email: ");
                                string email = Console.ReadLine()!;
                                Console.Write("Enter Phone Number: ");
                                string phone = Console.ReadLine()!;
                                Console.Write("Enter Role: ");
                                string role = Console.ReadLine()!;
                                DateTime joinDate = DateTime.Now;

                                var newAdmin = new Admin
                                {
                                    AdminID = adminId,
                                    Username = username,
                                    Password = password,
                                    FirstName = firstName,
                                    LastName = lastName,
                                    Email = email,
                                    PhoneNumber = phone,
                                    Role = role,
                                    JoinDate = joinDate
                                };

                                bool isRegistered = adminService.RegisterAdmin(newAdmin);

                                if (isRegistered)
                                    Console.WriteLine("Admin registered successfully.");
                                else
                                    Console.WriteLine("Failed to register admin.");
                            }
                            catch (InvalidInputException ex)
                            {
                                Console.WriteLine($"Admin Input Error: {ex.Message}");
                            }
                            break;

                        case 15:
                            // Get reservation details by ID
                            try
                            {
                                Console.Write("Enter Reservation ID: ");
                                int reservationId = Convert.ToInt32(Console.ReadLine());

                                var reservation = reservationService.GetReservationById(reservationId);

                                if (reservation == null)
                                {
                                    throw new ReservationException("No reservation found with the provided ID.");
                                }

                                Console.WriteLine("\n--- Reservation Details ---");
                                Console.WriteLine($"Reservation ID : {reservation.ReservationID}");
                                Console.WriteLine($"Customer ID    : {reservation.CustomerID}");
                                Console.WriteLine($"Vehicle ID     : {reservation.VehicleID}");
                                Console.WriteLine($"Start Date     : {reservation.StartDate.ToShortDateString()}");
                                Console.WriteLine($"End Date       : {reservation.EndDate.ToShortDateString()}");
                                Console.WriteLine($"Total Cost     : {reservation.TotalCost}");
                                Console.WriteLine($"Status         : {reservation.Status}");
                            }
                            catch (ReservationException ex)
                            {
                                Console.WriteLine($"Reservation Error: {ex.Message}");
                            }
                            break;

                        case 16:
                            try
                            {
                                Console.Write("Enter Reservation ID to update: ");
                                int reservationId = Convert.ToInt32(Console.ReadLine());
                                var existingReservation = reservationService.GetReservationById(reservationId);
                                if (existingReservation == null)
                                {
                                    throw new ReservationException("Reservation ID not found.");
                                }
                                Console.Write("Enter new Customer ID: ");
                                int customerId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter new Vehicle ID: ");
                                int vehicleId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter new Start Date (yyyy-mm-dd): ");
                                DateTime startDate = DateTime.Parse(Console.ReadLine()!);
                                Console.Write("Enter new End Date (yyyy-mm-dd): ");
                                DateTime endDate = DateTime.Parse(Console.ReadLine()!);
                                Console.Write("Enter Daily Rate of Vehicle: ");
                                int dailyRate = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter new Status (e.g., Confirmed/Cancelled): ");
                                string status = Console.ReadLine()!;
                                Reservation updatedReservation = new Reservation
                                {
                                    ReservationID = reservationId,
                                    CustomerID = customerId,
                                    VehicleID = vehicleId,
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    Status = status
                                };
                                updatedReservation.CalculateTotalCost(dailyRate);

                                bool isUpdated = reservationService.UpdateReservation(updatedReservation);
                                if (isUpdated)
                                {
                                    Console.WriteLine("Reservation updated successfully.");
                                }
                            }
                            catch (ReservationException ex)
                            {
                                Console.WriteLine($"Reservation Update Error: {ex.Message}");
                            }
                            break;

                        case 17:
                            // Update an existing customer
                            try
                            {
                                Console.Write("Enter Customer ID to update: ");
                                int customerId = Convert.ToInt32(Console.ReadLine());

                                var existingCustomer = customerService.GetCustomerById(customerId);
                                if (existingCustomer == null)
                                    throw new CustomerNotFoundException("Customer with the given ID does not exist.");

                                Console.Write("Enter new first name: ");
                                existingCustomer.FirstName = Console.ReadLine()!;
                                Console.Write("Enter new last name: ");
                                existingCustomer.LastName = Console.ReadLine()!;
                                Console.Write("Enter new email: ");
                                existingCustomer.Email = Console.ReadLine()!;
                                Console.Write("Enter new phone number: ");
                                existingCustomer.PhoneNumber = Console.ReadLine()!;
                                Console.Write("Enter new address: ");
                                existingCustomer.Address = Console.ReadLine()!;

                                bool updateResult = customerService.UpdateCustomer(existingCustomer);
                                if (updateResult)
                                    Console.WriteLine("Customer updated successfully.");
                                else
                                    Console.WriteLine("Customer update failed.");
                            }
                            catch (CustomerNotFoundException ex)
                            {
                                Console.WriteLine($"Update Error: {ex.Message}");
                            }
                            break;

                        case 18:
                            // Retrieve vehicle by ID
                            try
                            {
                                Console.Write("Enter Vehicle ID: ");
                                int vehicleId = Convert.ToInt32(Console.ReadLine());

                                var vehicle = vehicleService.GetVehicleById(vehicleId);
                                if (vehicle == null)
                                {
                                    throw new VehicleNotFoundException("Vehicle with the given ID does not exist.");
                                }

                                Console.WriteLine("\n--- Vehicle Details ---");
                                Console.WriteLine($"Vehicle ID       : {vehicle.VehicleID}");
                                Console.WriteLine($"Make             : {vehicle.Make}");
                                Console.WriteLine($"Model            : {vehicle.Model}");
                                Console.WriteLine($"Year             : {vehicle.Year}");
                                Console.WriteLine($"Color            : {vehicle.Color}");
                                Console.WriteLine($"Reg. Number      : {vehicle.RegistrationNumber}");
                                Console.WriteLine($"Availability     : {(vehicle.Availability ? "Available" : "Not Available")}");
                                Console.WriteLine($"Daily Rate       : {vehicle.DailyRate}");
                            }
                            catch (VehicleNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 19:
                            // Remove a vehicle
                            try
                            {
                                Console.Write("Enter Vehicle ID to remove: ");
                                int vehicleId = Convert.ToInt32(Console.ReadLine());

                                bool success = vehicleService.RemoveVehicle(vehicleId);
                                if (!success)
                                {
                                    throw new VehicleNotFoundException("Vehicle with the given ID does not exist or could not be removed.");
                                }

                                Console.WriteLine("Vehicle removed successfully.");
                            }
                            catch (VehicleNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 20:
                            // Get Admin by ID
                            try
                            {
                                Console.Write("Enter Admin ID: ");
                                int adminId = Convert.ToInt32(Console.ReadLine());

                                Admin admin = adminService.GetAdminById(adminId);
                                if (admin == null)
                                {
                                    throw new AdminNotFoundException("Admin not found with the given ID.");
                                }
                                Console.WriteLine("Admin Details:");
                                Console.WriteLine($"ID: {admin.AdminID}");
                                Console.WriteLine($"Username: {admin.Username}");
                                Console.WriteLine($"Name: {admin.FirstName} {admin.LastName}");
                                Console.WriteLine($"Email: {admin.Email}");
                                Console.WriteLine($"Phone: {admin.PhoneNumber}");
                                Console.WriteLine($"Role: {admin.Role}");
                                Console.WriteLine($"Join Date: {admin.JoinDate}");
                            }
                            catch (AdminNotFoundException ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                            break;

                        case 21:
                            // Exit
                            isRunning = false;
                            Console.WriteLine("Exiting program...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex.Message}");
                }
            }
        }
    }
}

