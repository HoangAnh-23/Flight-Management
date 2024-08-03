using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dao;
using DataAccess.Models;

namespace BusinessLogic.Repository
{
    public class AccountMemberRepository : IAccountMemberRepository
    {
        public AccountMember GetAccountByEmail(string email)=>AccountDAO.Instance.GetAccountByEmail(email);

        public string GetUserRole(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty");
            }

            var account = AccountDAO.Instance.GetAccountByEmail(email);

            if (account == null)
            {
                throw new ArgumentException("Invalid email", nameof(email));
            }

            return account.Role;
        }


        public bool Login(string email, string password)=>AccountDAO.Instance.Login(email, password);

        public void Register(AccountMember account)=>AccountDAO.Instance.Register(account);
    }
}
