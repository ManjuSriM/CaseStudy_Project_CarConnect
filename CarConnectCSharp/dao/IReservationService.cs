using CarConnectCSharp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.dao
{
    interface IReservationService
    {
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationsByCustomerId(int customerId);
        bool CreateReservation(Reservation reservationData);
        bool UpdateReservation(Reservation reservationData);
        bool CancelReservation(int reservationId);
        List<Reservation> GetAllReservations();
    }
}
