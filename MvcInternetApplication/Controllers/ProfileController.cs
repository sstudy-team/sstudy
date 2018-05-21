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

		public ActionResult CreateCard2(Subject Sub, Section Sec, Subsection Subsec, Topic Top)
		{
			ViewBag.Sub = Sub;
			ViewBag.Sec = Sec;
			ViewBag.Subsec = Subsec;
			ViewBag.Top = Top;

			return View();
		}
		
		public ActionResult CreateCard3(Subject Sub, Section Sec, Subsection Subsec, Topic Top, string Title, string Prev, string Inf)
		{
			Card NewCard = new Card();
			NewCard.Title = Title;
			NewCard.Prev = Prev;
			NewCard.Information = Inf;

			NewCard.Subject = Sub;
			NewCard.Section = Sec;
			NewCard.Subsection = Subsec;
			NewCard.Topic = Top;


			return View(NewCard);
		}

	}
}
