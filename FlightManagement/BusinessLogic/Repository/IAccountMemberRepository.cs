using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public interface IAccountMemberRepository
    {
        Boolean Login(string email, string password);
        string GetUserRole(string userRole);
        AccountMember GetAccountByEmail(string email);
        void Register(AccountMember account);
    }
}
