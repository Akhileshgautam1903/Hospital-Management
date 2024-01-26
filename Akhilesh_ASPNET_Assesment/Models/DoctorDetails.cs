using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.Models
{
    public class DoctorDetails
    {
        public static long DocPhoneNumber { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Field { get; set; }
        public string Status { get; set; }
        public TimeSpan Start_time { get; set; }
        public TimeSpan End_time { get; set; }
        public int Slots { get; set; }
        public int Fees { get; set; }

        //extra for appointments
        public string Appointment_date { get; set; }
        public string App_Start_time { get; set; }
        public string App_End_time { get; set; }
        public string Appointment_status { get; set; }
    }
}
