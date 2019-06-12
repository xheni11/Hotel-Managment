using M19G1.BLL;
using M19G1.DAL;
using M19G1.MappingViewModel;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService = new UserService(new UnitOfWork());
        private RoleService _roleService = new RoleService(new UnitOfWork());
        private BaseController _controller = new BaseController();
        [HttpGet]
        public ActionResult Index()
        {
            List<UserModel> users = _userService.GetAllUsers();
            MultiSelectList selectListRoles = new MultiSelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }

        [HttpPost]
        public JsonResult ListUsers()
        {
            List<UserModel> users = _userService.GetAllUsers();    
            int recordsTotal;
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
            {
                users = _userService.GetUsersOrderBy(sortColumn, searchValue, 0); //_controller.CurrentUser.Id kur te bej login
            }
            recordsTotal = users.Count();   
            var data = users.Skip(skip).Take(pageSize).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

            //return Json(new {  recordsFiltered = recordsTotal, recordsTotal = recordsTotal, draw=draw, data = data },JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult AddUser(UserViewModel user)
        {
            UserModel userToAdd = new UserModel();
            userToAdd.FirstName = user.FirstName;
            userToAdd.LastName = user.LastName;
            userToAdd.Username = user.Username;
            userToAdd.Birthday = user.Birthday;
            userToAdd.Gender = user.Gender;
            _userService.CreateUser(userToAdd);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(UserViewModelMapping.ToModel(user));

            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoadUser(int id)
        {
            if (id == 0)
            {
                return Json("Index", JsonRequestBehavior.AllowGet);
            }
            return Json(_userService.GetUserById(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
    }
}