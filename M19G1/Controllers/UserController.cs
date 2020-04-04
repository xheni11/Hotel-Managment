using M19G1.BLL;
using M19G1.Common.RandomPassword;
using M19G1.DAL;
using M19G1.Helpers;
using M19G1.IBLL;
using M19G1.Mapping;
using M19G1.MappingViewModel;
using M19G1.Models;
using M19G1.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserRequestService _userRequestService;
        private AnonymousRequestService _anonymousRequestService = new AnonymousRequestService(new UnitOfWork());
        private ILogService _logService;
        private PasswordHasher passwordHasher = new PasswordHasher();
        private EmailService _emailService = new EmailService();

        public UserController(IUserService userService, IRoleService roleService, IUserRequestService userRequestService,ILogService logService)
        {
            _userService = userService;
            _roleService = roleService;
            _userRequestService = userRequestService;
            _logService = logService;
        }
        #region
        [HttpGet]
        public ActionResult Index()
        {
            List<UserViewModel> users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id));
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        [HttpPut]
        public ActionResult ResetPassword(int id)
        {
            _emailService.SendAsync(SendPasswordToEmail(_userService.GetUserById(id)));
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        private string GeneratePassword(int id)
        {
            string password = PasswordGenerator.RandomPassword();
            _userService.ResetPassword(id, passwordHasher.HashPassword(password));
            return password;
        }

        private IdentityMessage SendPasswordToEmail(UserModel user)
        {
            string htmlContent = _userService.GetEmailTemplate(Paths.EMAIL_PATH);

            htmlContent = htmlContent.Replace("FIRSTNAME", user.FirstName);
            htmlContent = htmlContent.Replace("USERNAME", user.Username);
            htmlContent = htmlContent.Replace("PASSWORD", GeneratePassword(user.Id));
            return new IdentityMessage
            {
                Destination = user.Email,
                Body = htmlContent,
                Subject = "Welcome to Hotel Managment!"
            };
        }
        [HttpPut]
        public ActionResult UpdateUserActivity(int id)
        {
            _userService.UpdateUserActivity(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult ListUsers()
        //{
        //    List<UserViewModel> users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id));
        //    int recordsTotal;
        //    var start = Request.Form.GetValues("start").FirstOrDefault();
        //    var length = Request.Form.GetValues("length").FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
        //    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
        //    if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
        //    {
        //        users = UserViewModelMapping.ToViewModel(_userService.GetUsersOrderBy(sortColumn, searchValue, CurrentUser.Id)); //currentuser
        //    }
        //    recordsTotal = users.Count();
        //    var data = users.Skip(skip).Take(pageSize).ToArray();
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //    //return Json(new {  recordsFiltered = recordsTotal, recordsTotal = recordsTotal, draw=draw, data = data },JsonRequestBehavior.AllowGet);

        //}
        [HttpPost]
        public ActionResult AddUser(UserViewModel user)
        {
            string hashedPassword = passwordHasher.HashPassword(PasswordGenerator.RandomPassword());
            _userService.CreateUser(UserViewModelMapping.ToCreateUserModel(user), hashedPassword, CurrentUser.Id);
            SendPasswordToEmail(UserViewModelMapping.ToModel(user));
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        private IdentityMessage SendNewEmail(UserModel user)
        {
            string htmlContent = _userService.GetEmailTemplate(Paths.EMAIL_PATH_FOR_NEW_EMAIL);
            htmlContent = htmlContent.Replace("FIRSTNAME", user.FirstName);
            htmlContent = htmlContent.Replace("EMAIL", user.Email);
            return new IdentityMessage
            {
                Destination = user.Email,
                Body = htmlContent,
                Subject = "Email CHanged"
            };
        }

        [HttpPut]
        public ActionResult UpdateUser(UserViewModel user)
        {
            _userService.UpdateUser(UserViewModelMapping.ToModel(user));
            if (_userService.GetUserById(user.Id).Email != user.Email)
            {
                _emailService.SendAsync(SendNewEmail(UserViewModelMapping.ToModel(user)));
            }

            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUser(int id)
        {
            if (id == 0)
            {
                return Json("Index", JsonRequestBehavior.AllowGet);
            }
            return Json(_userService.GetUserById(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region
        [HttpGet]
        public ActionResult GetRequests()
        {
            List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests());
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }

        //[HttpGet]
        //public JsonResult ListRequests()
        //{
        //    List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests());
        //    int recordsTotal;
        //    var start = Request.Form.GetValues("start").FirstOrDefault();
        //    var length = Request.Form.GetValues("length").FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var draw = Request.Form.GetValues("draw").FirstOrDefault();
        //    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
        //    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
        //    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
        //    if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
        //    {
        //        users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetUsersOrderBy(sortColumn, searchValue)); //_controller.CurrentUser.Id kur te bej login
        //    }
        //    recordsTotal = users.Count();
        //    var data = users.Skip(skip).Take(pageSize).ToArray();
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}
        [HttpGet]
        public ActionResult GetRequest(int id)
        {
            return Json(_userRequestService.GetRequestById(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public ActionResult AcceptRequest(UserRequestViewModel user)
        {

            string hashedPassword = passwordHasher.HashPassword(PasswordGenerator.RandomPassword());
            _userService.CreateUser(UserRequestViewModelMapping.ToCreateViewModel(user), hashedPassword, CurrentUser.Id);
            _userRequestService.DeleteRequest(user.Id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public ActionResult DeleteRequest(int id)
        {
            _userRequestService.DeleteRequest(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 
        [HttpGet]
        public ActionResult GetAnonymousRequests()
        {
            List<AnonymousRequestViewModel> users = AnonymousRequestViewModelMapping.ToViewModel(_anonymousRequestService.GetAllRequests());
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        //[HttpGet]
        //public JsonResult ListRequest()
        //{
        //    List<AnonymousRequestViewModel> users = AnonymousRequestViewModelMapping.ToViewModel(_anonymousRequestService.GetAllRequests());
        //    int recordsTotal;
        //    var start = Request.Form.GetValues("start").FirstOrDefault();
        //    var length = Request.Form.GetValues("length").FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var draw = Request.Form.GetValues("draw").FirstOrDefault();
        //    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
        //    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
        //    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
        //    if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
        //    {
        //        //users = _anonymousRequestService.GetUsersOrderBy(sortColumn, searchValue); //_controller.CurrentUser.Id kur te bej login
        //    }
        //    recordsTotal = users.Count();
        //    var data = users.Skip(skip).Take(pageSize).ToArray();
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}
        [HttpGet]
        public ActionResult GetRequestAnonymous(int id)
        {
            return Json(_anonymousRequestService.GetRequestById(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
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
        //[HttpPost]
        //public JsonResult ListLogs()
        //{
        //    List<LogViewModel> users = LogViewModelMapping.ToViewModel(_logService.GetAllLogs());
        //    int recordsTotal;
        //    var start = Request.Form.GetValues("start").FirstOrDefault();
        //    var length = Request.Form.GetValues("length").FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var draw = Request.Form.GetValues("draw").FirstOrDefault();
        //    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
        //    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
        //    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
        //    if (!string.IsNullOrEmpty(sortColumn) || !string.IsNullOrEmpty(searchValue))
        //    {
        //        //users = _anonymousRequestService.GetUsersOrderBy(sortColumn, searchValue); //_controller.CurrentUser.Id kur te bej login
        //    }
        //    recordsTotal = users.Count();
        //    var data = users.Skip(skip).Take(pageSize).ToArray();
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}
        [HttpGet]
        public ActionResult GetLog(int id)
        {
            return Json(_logService.GetLogById(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
