﻿using M19G1.Common.Exceptions.Models;
using M19G1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace M19G1.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationSignInManager _signInManager;

        private ApplicationUserManager _userManager;

        public ApplicationUser CurrentUser { get; set; }
        public NotifficationModel Alert { get; set; }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (User.Identity.IsAuthenticated)
            {
                if (CurrentUser == null)
                {
                    CurrentUser = Request.GetOwinContext().GetUserManager<ApplicationUserManager>()
                        .FindById(User.Identity.GetUserId<int>());
                }
            }
            else
            {
                CurrentUser = null;
            }
        }
    }
}