using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult UserView()
        {
            //List<UserModel> users = _userService.GetAllUsers();
            return View();
        }
    }
}