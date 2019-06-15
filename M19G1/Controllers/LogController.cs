using M19G1.BLL;
using M19G1.DAL;
using M19G1.Mapping;
using M19G1.Models;
using M19G1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class LogController : Controller
    {
        private LogService _logService = new LogService(new UnitOfWork());

        [HttpGet]
        public ActionResult Logs()
        {
            List<LogViewModel> users =LogViewModelMapping.ToViewModel( _logService.GetAllLogs());
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
    }
}