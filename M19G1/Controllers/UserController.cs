using M19G1.BLL;
using M19G1.Common.RandomPassword;
using M19G1.DAL;
using M19G1.Mapping;
using M19G1.MappingViewModel;
using M19G1.Models;
using M19G1.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private UserService _userService = new UserService(new UnitOfWork());
        private RoleService _roleService = new RoleService(new UnitOfWork());
        private UserRequestService _userRequestService = new UserRequestService(new UnitOfWork());
        private AnonymousRequestService _anonymousRequestService = new AnonymousRequestService(new UnitOfWork());
        private LogService _logService = new LogService(new UnitOfWork());
        private PasswordHasher passwordHasher = new PasswordHasher();
        private PasswordGenerator passwordGenerator = new PasswordGenerator();
        private EmailService _emailService = new EmailService();
        #region
        [HttpGet]
        public ActionResult Index()
        {
            List<UserViewModel> users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id));
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        [HttpGet]
        public ActionResult GeneratorPassword(int id)
        {
            string password = passwordGenerator.RandomPassword();
            string hashedPassword = passwordHasher.HashPassword(password);

            _userService.GenerateNewPassword(id, hashedPassword);
            IdentityMessage identityMessage = new IdentityMessage
            {
                Destination = _userService.GetUserById(id).Email,//vendos currentuser.email kur te besh login
                Body = "New password for hotel app",
                Subject = "Your new password for hotel app: " + password
            };
            _emailService.SendAsync(identityMessage);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateUserActivity(int id)
        {
            _userService.UpdateUserActivity(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListUsers()
        {
            List<UserViewModel> users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id));
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
                users = UserViewModelMapping.ToViewModel(_userService.GetUsersOrderBy(sortColumn, searchValue, CurrentUser.Id)); //currentuser
            }
            recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

            //return Json(new {  recordsFiltered = recordsTotal, recordsTotal = recordsTotal, draw=draw, data = data },JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult AddUser(UserViewModel user)
        {
            string hashedPassword = passwordHasher.HashPassword(passwordGenerator.RandomPassword());
            _userService.CreateUser(UserViewModelMapping.ToCreateUserModel(user), hashedPassword, CurrentUser.Id);
            IdentityMessage identityMessage = new IdentityMessage
            {
                Destination = user.Email,
                Body = "Credentials of hotel app account",
                Subject = "Username: " + user.Username + "\n Password: " + passwordGenerator.RandomPassword()
            };
            _emailService.SendAsync(identityMessage);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(UserViewModelMapping.ToModel(user));
            if (_userService.GetUserById(user.Id).Email != user.Email)
            {
                IdentityMessage identityMessage = new IdentityMessage
                {
                    Destination = _userService.GetUserById(user.Id).Email,
                    Body = "Email of hotel app changed",
                    Subject = "Your email of hotel app has been changed. New email is : " + user.Email
                };
                _emailService.SendAsync(identityMessage);
                IdentityMessage identityMessage2 = new IdentityMessage
                {
                    Destination = user.Email,
                    Body = "Email of hotel app changed",
                    Subject = "Your email of hotel app has been changed. Your old email was : " + _userService.GetUserById(user.Id).Email
                };
                _emailService.SendAsync(identityMessage);
            }
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
        [HttpGet]
#endregion
        #region
        public ActionResult Requests()
        {
            List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests());
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        [HttpPost]
        public JsonResult ListRequests()
        {
            List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests());
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
                users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetUsersOrderBy(sortColumn, searchValue)); //_controller.CurrentUser.Id kur te bej login
            }
            recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult LoadRequest(int id)
        {
            return Json(_userRequestService.GetRequestById(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AcceptRequest(UserRequestViewModel user)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            PasswordGenerator passwordGenerator = new PasswordGenerator();
            string hashedPassword = passwordHasher.HashPassword(passwordGenerator.RandomPassword());
            _userService.CreateUser(UserRequestViewModelMapping.ToCreateViewModel(user), hashedPassword, CurrentUser.Id);
            _userRequestService.DeleteRequest(user.Id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteRequest(int id)
        {
            _userRequestService.DeleteRequest(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 
        [HttpGet]
        public ActionResult AnonymousRequests()
        {
            List<AnonymousRequestViewModel> users = AnonymousRequestViewModelMapping.ToViewModel(_anonymousRequestService.GetAllRequests());
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        [HttpPost]
        public JsonResult ListRequest()
        {
            List<AnonymousRequestViewModel> users = AnonymousRequestViewModelMapping.ToViewModel(_anonymousRequestService.GetAllRequests());
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
                //users = _anonymousRequestService.GetUsersOrderBy(sortColumn, searchValue); //_controller.CurrentUser.Id kur te bej login
            }
            recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult LoadRequestAnonymous(int id)
        {
            return Json(_anonymousRequestService.GetRequestById(id), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteAnonymous(int id)
        {
            _userService.MakeUserAnonymous(id);
            _anonymousRequestService.ConfirmedAnonymous(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region
        [HttpGet]
        public ActionResult Logs()
        {
            List<LogViewModel> users = LogViewModelMapping.ToViewModel(_logService.GetAllLogs());
            return View();
        }
        [HttpPost]
        public JsonResult ListLogs()
        {
            List<LogViewModel> users = LogViewModelMapping.ToViewModel(_logService.GetAllLogs());
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
                //users = _anonymousRequestService.GetUsersOrderBy(sortColumn, searchValue); //_controller.CurrentUser.Id kur te bej login
            }
            recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult LoadLog(int id)
        {
            return Json(_logService.GetLogById(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
