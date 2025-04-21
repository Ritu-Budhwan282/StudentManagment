using StudentManagmentSystem.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUserProfile user);
    }
}
