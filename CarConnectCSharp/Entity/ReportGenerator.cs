using CarConnectCSharp.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Entity
{
    class ReportGenerator
    {
        private ReservationService reservationService;
        private VehicleService vehicleService;

        public ReportGenerator(ReservationService resService, VehicleService vehService)
        {
            reservationService = resService;
            vehicleService = vehService;
        }

        public void GenerateFullReservationReport()
        {
            var reservations = reservationService.GetAllReservations();
            decimal totalRevenue = 0;
            int confirmedCount = 0, cancelledCount = 0, completedCount = 0;

            Console.WriteLine("               VEHICLE RENTAL RESERVATION REPORT             ");

            foreach (var res in reservations)
            {
                var vehicle = vehicleService.GetVehicleById(res.VehicleID);
                if (vehicle == null)
                    continue;

                if (res.Status == "Confirmed" || res.Status == "Completed")
                    totalRevenue += res.TotalCost;

                if (res.Status == "Confirmed") confirmedCount++;
                else if (res.Status == "Cancelled") cancelledCount++;
                else if (res.Status == "Completed") completedCount++;

                Console.WriteLine($"Reservation ID   : {res.ReservationID}");
                Console.WriteLine($"Customer ID      : {res.CustomerID}");
                Console.WriteLine($"Vehicle          : {vehicle.Make} {vehicle.Model} ({vehicle.Year})");
                Console.WriteLine($"Start Date       : {res.StartDate:dd-MMM-yyyy}");
                Console.WriteLine($"End Date         : {res.EndDate:dd-MMM-yyyy}");
                Console.WriteLine($"Status           : {res.Status}");
                Console.WriteLine($"Total Cost       : {res.TotalCost}");
                Console.WriteLine("---------------------------------------------------------------\n");
            }

            Console.WriteLine("*********************** SUMMARY *******************************");
            Console.WriteLine($"Total Reservations : {reservations.Count}");
            Console.WriteLine($"Confirmed          : {confirmedCount}");
            Console.WriteLine($"Completed          : {completedCount}");
            Console.WriteLine($"Cancelled          : {cancelledCount}");
            Console.WriteLine($"Total Revenue      : {totalRevenue}");
        }
    }
}

