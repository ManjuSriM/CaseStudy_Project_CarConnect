using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.Exceptions;
using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;

namespace CarConnectCSharp.dao
{
    class ReservationService : IReservationService
    {
        private DatabaseContext dbContext;

        public ReservationService()
        {
            dbContext = new DatabaseContext();
        }

        public Reservation GetReservationById(int reservationId)
        {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReservationID", reservationId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (int)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        };
                    }
                }
            return null!;
        }

        // 2. Get all reservations by customer ID
        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Reservation WHERE CustomerID = @CustomerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        reservations.Add(new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (int)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        });
                    }
                    if (reservations.Count == 0)
                    {
                        throw new ReservationException("No reservations found for the given Customer ID.");
                    }
                }
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            return reservations;
        }

        // 3. Create a reservation
        public bool CreateReservation(Reservation reservationData)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = @"INSERT INTO Reservation 
                                (ReservationID, CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status) 
                                VALUES 
                                (@ReservationID, @CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReservationID", reservationData.ReservationID);
                    command.Parameters.AddWithValue("@CustomerID", reservationData.CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", reservationData.VehicleID);
                    command.Parameters.AddWithValue("@StartDate", reservationData.StartDate);
                    command.Parameters.AddWithValue("@EndDate", reservationData.EndDate);
                    command.Parameters.AddWithValue("@TotalCost", reservationData.TotalCost);
                    command.Parameters.AddWithValue("@Status", reservationData.Status);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new ReservationException("Failed to reserve.");
                    return true;
                }
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error in CreateReservation: " + ex.Message);
            }
            return false;
        }

        // 4. Update reservation
        public bool UpdateReservation(Reservation reservationData)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = @"UPDATE Reservation SET 
                                    CustomerID = @CustomerID, 
                                    VehicleID = @VehicleID, 
                                    StartDate = @StartDate, 
                                    EndDate = @EndDate, 
                                    TotalCost = @TotalCost, 
                                    Status = @Status
                                WHERE ReservationID = @ReservationID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", reservationData.CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", reservationData.VehicleID);
                    command.Parameters.AddWithValue("@StartDate", reservationData.StartDate);
                    command.Parameters.AddWithValue("@EndDate", reservationData.EndDate);
                    command.Parameters.AddWithValue("@TotalCost", reservationData.TotalCost);
                    command.Parameters.AddWithValue("@Status", reservationData.Status);
                    command.Parameters.AddWithValue("@ReservationID", reservationData.ReservationID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new ReservationException("Customer not found or no changes were made.");
                    return true;
                }
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error in UpdateReservation: " + ex.Message);
            }
            return false;
        }

        // 5. Cancel reservation
        public bool CancelReservation(int reservationId)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReservationID", reservationId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new ReservationException("Customer not found or no changes were made.");
                    return true;
                }
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error in CancelReservation: " + ex.Message);
            }
            return false;
        }

        // 6. Get all reservations
        public List<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Reservation";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new ReservationException("No reservations found in the database.");
                    }
                    while (reader.Read())
                    {
                        reservations.Add(new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (int)reader["TotalCost"],
                            Status = (string)reader["Status"]
                        });
                    }
                }
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            return reservations;
        }
    }
}
