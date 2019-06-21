using M19G1.BLL;
using M19G1.DAL;
using M19G1.Mapping;
using M19G1.MappingViewModel;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class DriverServiceController : BaseController
    {
        private BLL.PersonalDriverService _driverService = new PersonalDriverService(new UnitOfWork());
        // GET: DriverService
        public ActionResult Index()
        {
            List<DriverServiceViewModel> users = DriverServiceViewModelMapping.ToViewModel(_driverService.GetAllDriverService());
            return View();
        }
    }
}