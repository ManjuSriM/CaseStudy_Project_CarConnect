using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.Exceptions;

namespace CarConnectCSharp.Entity
{
    class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_endDate != default && value >= _endDate)
                    throw new InvalidInputException("Start date must be earlier than end date.");
                _startDate = value;
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_startDate != default && value <= _startDate)
                    throw new InvalidInputException("End date must be later than start date.");
                _endDate = value;
            }
        }
        public int TotalCost { get; set; }
        public string? Status { get; set; }

        // Default Constructor
        public Reservation() { }

        // Parameterized Constructor
        public Reservation(int reservationID, int customerID, int vehicleID, DateTime startDate,
                           DateTime endDate, int totalCost, string status)
        {
            ReservationID = reservationID;
            CustomerID = customerID;
            VehicleID = vehicleID;
            StartDate = startDate;
            EndDate = endDate;
            TotalCost = totalCost;
            Status = status;
        }

        public int CalculateTotalCost(int dailyRate)
        {
            // Calculate the number of rental days
            int rentalDays = (EndDate - StartDate).Days;

            // Calculate and return the total cost
            TotalCost = rentalDays * dailyRate;
            return TotalCost;
        }
    }
}
