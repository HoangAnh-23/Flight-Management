using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace DataAccess.Models
{
    public partial class AccountMember
    {
        public int Id { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public DateTime? DOb { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public static class SessionDataAcc
        {
            public static List<AccountMember> accs { get; set; } = new List<AccountMember>();
        }
    }
}
