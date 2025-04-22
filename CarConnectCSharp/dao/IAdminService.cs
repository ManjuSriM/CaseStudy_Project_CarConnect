using CarConnectCSharp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.dao
{
    public interface IAdminService
    {
        Admin GetAdminById(int adminId);
        Admin GetAdminByUsername(string username);
        bool RegisterAdmin(Admin adminData);
        bool UpdateAdmin(Admin adminData);
        bool DeleteAdmin(int adminId);
    }
}
