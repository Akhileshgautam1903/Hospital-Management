using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akhilesh_ASPNET_Assesment.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Akhilesh_ASPNET_Assesment.Controllers
{
    public class UserController : Controller
    {
        IAdmin Admin;
        IUser User;
        public UserController(IUser User, IAdmin Admin)
        {
            this.User = User;
            this.Admin = Admin;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult UpdateWallet()
        {
            return View(User.FetchMyDetails());
        }

        public IActionResult MyDetails()
        {
            return View(User.FetchMyDetails());
        }

        public IActionResult ShowDoctors()
        {
            return View(Admin.FetchAllDoctors());
        }

        public IActionResult Book(int id)
        {
            return View(Admin.FetchAllDoctors().FirstOrDefault(d => d.Id == id));
        }

        public IActionResult CancelAppointment()
        {
            return View(User.FetchAllBookedSlots());
        }

        public IActionResult ShowBooked()
        {
            return View(User.FetchAllBookedSlots());
        }

        public IActionResult ShowCancelled()
        {
            return View(User.FetchAllCancelledSlots());
        }
    }
}