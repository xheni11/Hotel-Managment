using M19G1.BLL;
using M19G1.Common.RandomPassword;
using M19G1.DAL;
using M19G1.MappingViewModel;
using M19G1.Models;
using M19G1.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRequestController:Controller
    {
        private UserRequestService _userRequestService = new UserRequestService(new UnitOfWork());
        private UserService _userService = new UserService(new UnitOfWork());
        [HttpGet]
        public ActionResult Requests()
        {
            List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel(_userRequestService.GetAllRequests());
            return View();
        }
        [HttpPost]
        public JsonResult ListRequests()
        {
            List<UserRequestViewModel> users = UserRequestViewModelMapping.ToViewModel( _userRequestService.GetAllRequests());
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
            _userService.CreateUser(UserRequestViewModelMapping.ToCreateViewModel(user),hashedPassword);
            _userRequestService.DeleteRequest(user.Id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteRequest(int id)
        {
            _userRequestService.DeleteRequest(id);
            return Json("Index", JsonRequestBehavior.AllowGet);
        }
    }
}