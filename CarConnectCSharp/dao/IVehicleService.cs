using CarConnectCSharp.Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.dao
{
    public interface IVehicleService
    {
        Vehicle GetVehicleById(int vehicleId);
        List<Vehicle> GetAvailableVehicles();
        bool AddVehicle(Vehicle vehicleData);
        bool UpdateVehicle(Vehicle vehicleData);
        bool RemoveVehicle(int vehicleId);
        List<Vehicle> GetAllVehicles();
       
    }
}