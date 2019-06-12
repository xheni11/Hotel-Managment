using M19G1.BLL;
using M19G1.DAL;
using M19G1.DAL.Entities;
using M19G1.MappingViewModel;
using M19G1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService = new UserService(new UnitOfWork());
        private RoleService _roleService = new RoleService(new UnitOfWork());
        private BaseController _controller = new BaseController();
        // GET: Home
        public ActionResult Index()
        {
            List<UserModel> users = _userService.GetAllUsers();
            MultiSelectList selectListRoles = new MultiSelectList(_roleService.GetAllRoles().Select(s=>s.RoleName));

            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        public JsonResult ListUsers()
        {
            List<UserModel> users = _userService.GetAllUsers();
            //total number of rows count     
            int recordsTotal;
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();  
         

            var sortColumn=Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
            {
                users = _userService.GetUsersOrderBy(sortColumn, searchValue, 0); //_controller.CurrentUser.Id kur te bej login
            }
            recordsTotal = users.Count();
            
            //Paging     
            var data = users.Skip(skip).Take(pageSize).ToArray();

            //data.data = users.ToArray();
            //dataTable.recordsTotal = users.Count;
            //dataTable.recordsFiltered = users.Count();
           return Json(data, JsonRequestBehavior.AllowGet);
            
           //return Json(new {  recordsFiltered = recordsTotal, recordsTotal = recordsTotal, draw=draw, data = data },JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddUser(UserViewModel user)
        {
            UserModel userToUpdate = new UserModel();
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Username = user.Username;
            userToUpdate.Birthday = user.Birthday;
            userToUpdate.Gender = user.Gender;
            _userService.CreateUser(userToUpdate);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        public ActionResult RoleName()
        {

            return View();
        }
        public ActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(UserViewModelMapping.ToModel(user));

            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadUser(int id)
        {
            if (id == 0)
            {
                return Json("Index", JsonRequestBehavior.AllowGet);
            }
            return Json(_userService.GetUserById(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return  Json("Index", JsonRequestBehavior.AllowGet);
        }
        //private UserService _userService = new UserService(new UnitOfWork());

        //public ActionResult Index()
        //{
        //    List<UserModel> users = _userService.GetAllUsers();
        //    return View(users);
        //}

        //public ActionResult About()
        //{

        //    _userService.CreateDummyUser();
        //    List<UserModel> users = _userService.GetAllUsers();
        //    _userService.UpdateUserActivity(1);
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}