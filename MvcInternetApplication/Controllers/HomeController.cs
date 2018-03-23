using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInternetApplication.Models;
using MvcInternetApplication.Filters;

namespace MvcInternetApplication.Controllers
{
    public class HomeController : Controller
	{
		[InitializeSimpleMembershipAttribute]
		public ActionResult Index()
        {

			UsersContext a = new UsersContext();

			ViewBag.C = a.UserProfile.ToList().Count;

			return View();
        }

		public ActionResult Cards()
		{
			
			return View();
		}

	}
}

