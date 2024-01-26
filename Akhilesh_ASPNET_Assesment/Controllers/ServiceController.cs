using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akhilesh_ASPNET_Assesment.DataAccess;
using Akhilesh_ASPNET_Assesment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Akhilesh_ASPNET_Assesment.Controllers
{
    public class ServiceController : Controller
    {
        IAdmin Admin;
        IUser User;
        public ServiceController(IAdmin Admin, IUser User)
        {
            this.Admin = Admin;
            this.User = User;
        }

        //ADMIN PART
        [HttpGet]
        public bool loginAdmin(long num, string pass)
        {
            return Admin.LoginAdmin(num, pass);
        }

        [HttpPost]
        public bool addDoc([FromBody]DoctorDetails doc)
        {
            return Admin.AddDoctor(doc);
        }

        [HttpPost]
        public bool updateDoc([FromBody]DoctorDetails doc)
        {
            return Admin.UpdateDoctor(doc);
        }

        [HttpGet]
        public bool deleteDoc(int id)
        {
            return Admin.DeleteDoctor(id);
        }

        //SOMETHING ELSE
        [HttpPost]
        public bool uploadPic([FromForm]ImageDetails Data)
        {
            return Admin.AddImage(Data);
        }



        //USER PART
        [HttpGet]
        public bool loginUser(long num, string pass)
        {
            return User.LoginUser(num, pass);
        }

        [HttpPost]
        public bool registerUser([FromBody]UserDetails user)
        {
            return User.RegisterUser(user);
        }

        [HttpGet]
        public bool updateWallet(decimal amt)
        {
            return User.UpdateWallet(amt);
        }

        [HttpPost]
        public int bookSlot(long doc_phone, DateTime date)
        {
            return User.BookSlot(doc_phone, date);
        }

        [HttpGet]
        public bool cancelSlot(int id)
        {
            return User.CancelSlot(id);
        }
    }
}