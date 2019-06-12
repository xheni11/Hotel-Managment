using M19G1.IBLL;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class DummyController : BaseController
    {
        private readonly IUserService _userService;

        public DummyController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
          
            return View();
        }
    }
}