using Akhilesh_ASPNET_Assesment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.DataAccess
{
    public interface IAdmin
    {
        bool LoginAdmin(long num, string pass);
        bool AddImage(ImageDetails Data);
        List<ImageDetails> FetchImages();
        bool AddDoctor(DoctorDetails doc);
        bool UpdateDoctor(DoctorDetails doc);
        List<DoctorDetails> FetchAllDoctors();
        bool DeleteDoctor(int id);
        List<UserDetails> FetchUserDetails();
    }
}
