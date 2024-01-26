using Akhilesh_ASPNET_Assesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.DataAccess
{
    public interface IUser
    {
        bool RegisterUser(UserDetails user);
        bool LoginUser(long num, string pass);
        UserDetails FetchMyDetails();
        bool UpdateWallet(decimal amt);
        int BookSlot(long doc_phone, DateTime date);
        List<DoctorDetails> FetchAllBookedSlots();
        bool CancelSlot(int id);
        List<DoctorDetails> FetchAllCancelledSlots();
    }
}
