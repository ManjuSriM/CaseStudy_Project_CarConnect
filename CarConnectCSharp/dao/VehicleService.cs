using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;
using CarConnectCSharp.Exceptions;

namespace CarConnectCSharp.Entity
{
    public class VehicleService : IVehicleService
    {
        private DatabaseContext dbContext;

        public VehicleService()
        {
            dbContext = new DatabaseContext();
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Vehicle WHERE VehicleID = @VehicleID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleID", vehicleId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = reader["Model"].ToString()!,
                        Make = reader["Make"].ToString()!,
                        Year = (int)reader["Year"],
                        Color = reader["Color"].ToString()!,
                        RegistrationNumber = reader["RegistrationNumber"].ToString()!,
                        Availability = (bool)reader["Availability"],
                        DailyRate = (int)reader["DailyRate"]
                    };
                }
                return null!;
            }

        }

        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();
            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Vehicle WHERE Availability = 1"; // Available vehicles
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    availableVehicles.Add(new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = reader["Model"].ToString()!,
                        Make = reader["Make"].ToString()!,
                        Year = (int)reader["Year"],
                        Color = reader["Color"].ToString()!,
                        RegistrationNumber = reader["RegistrationNumber"].ToString()!,
                        Availability = (bool)reader["Availability"],
                        DailyRate = (int)reader["DailyRate"]
                    });
                }
            }
                
            return availableVehicles;
        }

        public bool AddVehicle(Vehicle vehicleData)
        {
            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Vehicle (VehicleID, Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate) " +
                               "VALUES (@VehicleID, @Model, @Make, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleID", vehicleData.VehicleID);
                command.Parameters.AddWithValue("@Model", vehicleData.Model);
                command.Parameters.AddWithValue("@Make", vehicleData.Make);
                command.Parameters.AddWithValue("@Year", vehicleData.Year);
                command.Parameters.AddWithValue("@Color", vehicleData.Color);
                command.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                command.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                command.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            
        }

        public bool UpdateVehicle(Vehicle vehicleData)
        {
            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Vehicle SET Model = @Model, Make = @Make, Year = @Year, Color = @Color, " +
                               "RegistrationNumber = @RegistrationNumber, Availability = @Availability, DailyRate = @DailyRate " +
                               "WHERE VehicleID = @VehicleID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Model", vehicleData.Model);
                command.Parameters.AddWithValue("@Make", vehicleData.Make);
                command.Parameters.AddWithValue("@Year", vehicleData.Year);
                command.Parameters.AddWithValue("@Color", vehicleData.Color);
                command.Parameters.AddWithValue("@RegistrationNumber", vehicleData.RegistrationNumber);
                command.Parameters.AddWithValue("@Availability", vehicleData.Availability);
                command.Parameters.AddWithValue("@DailyRate", vehicleData.DailyRate);
                command.Parameters.AddWithValue("@VehicleID", vehicleData.VehicleID);

                int result = command.ExecuteNonQuery();
                if (result == 0)
                    throw new VehicleNotFoundException("The Vehicle ID is not found.");
                return true;
            }
          
        }

        public bool RemoveVehicle(int vehicleId)
        {
            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VehicleID", vehicleId);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> allVehicles = new List<Vehicle>();

            using (var connection = dbContext.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Vehicle"; // Fetch all vehicles
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    allVehicles.Add(new Vehicle
                    {
                        VehicleID = (int)reader["VehicleID"],
                        Model = reader["Model"].ToString()!,
                        Make = reader["Make"].ToString()!,
                        Year = (int)reader["Year"],
                        Color = reader["Color"].ToString()!,
                        RegistrationNumber = reader["RegistrationNumber"].ToString()!,
                        Availability = (bool)reader["Availability"],
                        DailyRate = (int)reader["DailyRate"]
                    });
                }
            }

            return allVehicles;
        }
    }
}
