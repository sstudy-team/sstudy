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
		
		public ActionResult Profile(int? Message)
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

		public ActionResult PersonalPage()
		{
			return View();
		}

		public ActionResult ProfCardMenu()
		{
			return View();
		}


		public ActionResult ApproveCard()
		{
			return View();
		}


		public ActionResult CreateCard1()
		{
			return View();
		}

		public ActionResult CreateCard2()
		{
			return View();
		}
		
		public ActionResult CreateCard3()
		{
			return View();
		}

	}
}
