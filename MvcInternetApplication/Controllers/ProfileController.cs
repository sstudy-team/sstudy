using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInternetApplication.Models;
using WebMatrix.WebData;

namespace MvcInternetApplication.Controllers
{
	[Authorize]
	public class ProfileController : Controller
    {
		//
		// GET: /Profile/

		UsersContext db = new UsersContext();

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

		public ActionResult Prof()
		{
			return View();
		}

		public ActionResult ProfCard()
		{
			return View();
		}

		public ActionResult CreateStep2()
		{
			return View();
		}
		
		public ActionResult CreateStep1()
		{
			return View();
		}

		public ActionResult ProfSavedCards()
		{
			return View();
		}

		public ActionResult ProfVoteCards()
		{
			return View();
		}

	}
}
