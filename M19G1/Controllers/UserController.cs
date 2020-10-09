using M19G1.Common.Enums;
using M19G1.Common.Exceptions.Models;
using M19G1.Common.RandomPassword;
using M19G1.Helpers;
using M19G1.IBLL;
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
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserRequestService _userRequestService;
        private IAnonymousRequestService _anonymousRequestService ;
        private ILogService _logService;
        private PasswordHasher passwordHasher = new PasswordHasher();
        private EmailService _emailService = new EmailService();
        private List<NotifficationModel> _notifficationModels;
        private NotifficationModel _alertModel;
        public UserController(IUserService userService, IRoleService roleService, IUserRequestService userRequestService,ILogService logService,IAnonymousRequestService anonymousRequestService)
        {
            _userService = userService;
            _roleService = roleService;
            _userRequestService = userRequestService;
            _logService = logService;
            _anonymousRequestService = anonymousRequestService;
            _notifficationModels =new List<NotifficationModel>();
            _alertModel = new NotifficationModel();
        }
        #region Users
        [HttpGet]
        public ActionResult Index(string sortOrder,string searchField, int ?pageNumber)
        {
            List<UserViewModel> users;
            int count = _userService.CountAllNotAnonymous(CurrentUser.Id);

            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.UsernameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";
            switch (sortOrder)
            {
                case "FirstName_desc":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id,true,"FirstName", searchField,pageNumber??1, Helpers.Constants.PAGE_SIZE));
                    break;
                case "LastName":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, false, "LastName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "LastName_desc":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, true, "LastName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Email":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, false, "Email", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Email_desc":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, true, "Email", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Username":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, false, "UserName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Username_desc":
                    users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, true, "UserName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                default:
                   users = UserViewModelMapping.ToViewModel(_userService.GetNotAnonymous(CurrentUser.Id, false, "FirstName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));
                    break;
            }
            
            return View(new PaginatedList<UserViewModel>(users, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE,count));

        }
        [HttpGet]
        public ActionResult ResetPassword(int id)
        {
            UserModel user = _userService.GetUserById(id);
            string password=GeneratePassword(user.Id);
            _emailService.SendAsync(SendPasswordToEmail(UserViewModelMapping.ToEmailUserViewModel(user),password, "Ndryshimi i passwordit u krye me sukses!"));
            return RedirectToAction("Index");
        }
        private string GeneratePassword(int id)
        {
            string password = PasswordGenerator.RandomPassword();
            _userService.ResetPassword(id, passwordHasher.HashPassword(password));
            return password;
        }

        private IdentityMessage SendPasswordToEmail(EmailUserViewModel user,string password,string action)
        {
            string htmlContent = _userService.GetEmailTemplate(Paths.EMAIL_PATH);

            htmlContent = htmlContent.Replace("FIRSTNAME", user.FirstName);
            htmlContent = htmlContent.Replace("USERNAME", user.UserName);
            htmlContent = htmlContent.Replace("PASSWORD", password);
            htmlContent = htmlContent.Replace("ACTION", action);
            return new IdentityMessage
            {
                Destination = user.Email,
                Body = htmlContent,
                Subject = "Welcome to Hotel Managment!"
            };
        }
        [HttpGet]
        public ActionResult UpdateUserActivity(int id)
        {
            NotifficationModel _alertModel= _userService.UpdateUserActivity(id);
            _notifficationModels.Add(_alertModel);
            TempData["Alerts"] = _notifficationModels;
            return RedirectToAction("Index");
        }

      
        [HttpPost]
        public ActionResult AddUpdateUser(AddUserViewModel user)
        {
            List<RoleModel> roles = _roleService.GetAllRoles();
            user.Roles = roles;

            if (ModelState.IsValid)
            {
                if (user.Id != null)
                {
                    _alertModel= _userService.UpdateUser(UserViewModelMapping.ToCreateUserModel(user));
                    _notifficationModels.Add(_alertModel);
                    if (_alertModel.Type != AlertConstants.ALERT_TYPE.Danger.ToString("g"))
                    {
                        TempData["Alerts"] = _notifficationModels;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    string password = PasswordGenerator.RandomPassword();
                    string hashedPassword = passwordHasher.HashPassword(password);
                    _alertModel = _userService.CreateUser(UserViewModelMapping.ToCreateUserModel(user), hashedPassword, CurrentUser.Id);
                    _notifficationModels.Add(_alertModel);
                    if (_alertModel.Type != AlertConstants.ALERT_TYPE.Danger.ToString("g"))
                    {
                        var response = _emailService.SendAsync(SendPasswordToEmail(UserViewModelMapping.ToEmailUserViewModel(user), password, " Regjistrimi i llogarisë tuaj u krye me sukses!"));
                        if (!response.IsCompleted)
                        {
                            _notifficationModels.Add(new NotifficationModel { Type = AlertConstants.ALERT_TYPE.Danger.ToString("g"), Message = "Email error" });

                        }
                        TempData["Alerts"] = _notifficationModels;
                        return RedirectToAction("Index");
                    }
                }

            }
            TempData["Alerts"] = _notifficationModels;
            return View(user);
        }

       
        [HttpGet]
        public ActionResult AddUpdateUser(int id)
        {
            List<RoleModel> roles = _roleService.GetAllRoles();
            AddUserViewModel model = new AddUserViewModel();
            if (id > 0)
            {
                model = UserViewModelMapping.ToModel(_userService.GetUserById(id));
            }
            model.Roles = roles;
            return View(model);
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
                Subject = "Email Changed"
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
        public ActionResult DeleteUser(int id)
        {
            _alertModel= _userService.DeleteUser(id);
            _notifficationModels.Add(_alertModel);
            TempData["Alerts"] = _notifficationModels;
            return RedirectToAction("Index"); 
        }

        #endregion
        #region Requests
        [HttpGet]
        public ActionResult Requests(string sortOrder, string searchField, int? pageNumber)
        {
            List<UserRequestViewModel> users ;
            int count = _userRequestService.CountAllRecords(CurrentUser.Id);
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.UsernameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";
            switch (sortOrder)
            {
                case "FirstName_desc":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests( true, "FirstName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));
                    break;
                case "LastName":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(false, "LastName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "LastName_desc":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(true, "LastName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Email":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(false, "Email", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Email_desc":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(true, "Email", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Username":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(false, "UserName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                case "Username_desc":
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(true, "UserName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));

                    break;
                default:
                    users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests(false, "FirstName", searchField, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE));
                    break;
            }

            return View(new PaginatedList<UserRequestViewModel>(users, pageNumber ?? 1, Helpers.Constants.PAGE_SIZE, count));
        }

  
        [HttpGet]
        public ActionResult AcceptRequest(int id)
        {
            try
            {
                UserRequestViewModel user = UserRequestViewModelMapping.ToViewModel( _userRequestService.GetRequestById(id));
                string hashedPassword = passwordHasher.HashPassword(PasswordGenerator.RandomPassword());
                _userService.CreateUser(UserRequestViewModelMapping.ToCreateViewModel(user), hashedPassword, CurrentUser.Id);
                _userRequestService.DeleteRequest(user.Id);
                _alertModel = new NotifficationModel { Type = AlertConstants.ALERT_TYPE.Success.ToString("g"), ClassType = AlertConstants.ALERT_TYPE.Success.ToString("g"), Message = "User request has been accpted successfuly!" };
            }
            catch(Exception ex)
            {
                _alertModel = new NotifficationModel { Type = AlertConstants.ALERT_TYPE.Danger.ToString("g"), ClassType = AlertConstants.ALERT_TYPE.Danger.ToString("g"), Message = "Unknown error has occured. Please try again!"+ex };
            }
            _notifficationModels.Add(_alertModel);
            TempData["Alerts"] = _notifficationModels;
            return RedirectToAction("Requests");
        }
        [HttpGet]
        public ActionResult DeleteRequest(int id)
        {
            try
            { 
            _userRequestService.DeleteRequest(id);
            _alertModel = new NotifficationModel { Type = AlertConstants.ALERT_TYPE.Success.ToString("g"), ClassType = AlertConstants.ALERT_TYPE.Success.ToString("g"), Message = "User request has been deleted successfuly!" };
            }
            catch
            {
                _alertModel = new NotifficationModel { Type = AlertConstants.ALERT_TYPE.Danger.ToString("g"), ClassType = AlertConstants.ALERT_TYPE.Danger.ToString("g"), Message = "Unknown error has occured. Please try again!" };
            }
            _notifficationModels.Add(_alertModel);
            TempData["Alerts"] = _notifficationModels;
            return RedirectToAction("Requests");
        }
        #endregion

        #region AnonymousRequest
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
 

        #region Logs
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
