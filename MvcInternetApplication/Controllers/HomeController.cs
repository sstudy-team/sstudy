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

		[Authorize]
		public ActionResult Cabinet(int? Message)
		{
            if (Message != null)
                if (Message == 1)
                {
                    ViewBag.Msg = "Пароль успешно изменен";
                }
                else
                {
                    ViewBag.Msg = "Произошла ошибка смены пароля";
                }

            return View();
		}

		[Authorize]
		public ActionResult Prof()
		{

			return View();
		}

		[Authorize]
		public ActionResult ProfCard()
		{

			return View();
		}

		public ActionResult Cards()
		{
			
			return View();
		}

	}
}

