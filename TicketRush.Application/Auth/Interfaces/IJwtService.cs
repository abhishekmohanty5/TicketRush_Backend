using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRush.Application.Auth.Interfaces
{
    public interface IJwtService
    {
       string GenerateAccessToken(int userId, string email, string role);
       
    }
}
