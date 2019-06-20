using M19G1.BLL;
using M19G1.DAL;
using M19G1.MappingViewModel;
using M19G1.Models;
using M19G1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AnonymousRequestController:Controller
    {
        private AnonymousRequestService _anonymousRequestService = new AnonymousRequestService(new UnitOfWork());
        private UserService _userService = new UserService(new UnitOfWork());
        private RoleService _roleService = new RoleService(new UnitOfWork());
        [HttpGet]
        public ActionResult AnonymousRequests()
        {
            List<AnonymousRequestViewModel> users = AnonymousRequestViewModelMapping.ToViewModel( _anonymousRequestService.GetAllRequests());
            SelectList selectListRoles = new SelectList(_roleService.GetAllRoles().Select(s => s.RoleName));
            ViewData["RoleName"] = selectListRoles;
            return View();
        }
        [HttpPost]
        public JsonResult ListRequests()
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
        public ActionResult LoadRequest(int id)
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
    }
}