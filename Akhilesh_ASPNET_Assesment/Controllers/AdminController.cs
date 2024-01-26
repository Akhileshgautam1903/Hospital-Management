using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akhilesh_ASPNET_Assesment.DataAccess;
using Akhilesh_ASPNET_Assesment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Akhilesh_ASPNET_Assesment.Controllers
{
    public class AdminController : Controller
    {
        IAdmin Admin;
        public AdminController(IAdmin Admin)
        {
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

        public IActionResult ShowDoctors()
        {
            return View(Admin.FetchAllDoctors());
        }

        public IActionResult DeleteDoctor()
        {
            return View(Admin.FetchAllDoctors().Where(d => d.PhoneNumber != DoctorDetails.DocPhoneNumber));
        }

        public IActionResult UpdateDoctors()
        {
            return View(Admin.FetchAllDoctors());
        }

        public IActionResult Update(int id)
        {
            return View(Admin.FetchAllDoctors().FirstOrDefault(d => d.Id == id));
        }

        public IActionResult AddDoctor()
        {
            return View();
        }

        public IActionResult Photos()
        {
            return View(Admin.FetchImages());
        }

        public IActionResult ShowPatients()
        {
            return View(Admin.FetchUserDetails());
        }
    }
}