using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.Models
{
    public class UserDetails
    {
        public static long UserPhoneNumber { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Wallet { get; set; }
    }
}
