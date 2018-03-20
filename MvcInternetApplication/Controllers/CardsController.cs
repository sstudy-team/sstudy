using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInternetApplication.Models;
using WebMatrix.WebData;
using MvcInternetApplication.Filters;

namespace MvcInternetApplication.Controllers
{
	[InitializeSimpleMembershipAttribute]
	public class CardsController : Controller
	{
		//
		// GET: /Cards/

		UsersContext db = new UsersContext();

		public ActionResult GetCards(int? number)
		{
			if (number != null)
			{
				ViewBag.Number = number;
			}
			else
			{
				ViewBag.Number = 1;
			}
			ViewBag.Saved = 0;

			return View("GetCards", db.Cards.ToList());
		}

		public ActionResult GetUserCards(int? number)
		{
			if (number != null)
			{
				ViewBag.Number = number;
			}
			else
			{
				ViewBag.Number = 1;
			}

			ViewBag.Saved = 1;

			List<Card> cards = new List<Card>();
			List<UserCard> store = db.UserCards.Where(Card => Card.UserId == WebSecurity.CurrentUserId).ToList();
			foreach (var item in store)
			{
				cards.Add(db.Cards.Where(Card => Card.CardId == item.CardId).FirstOrDefault());
			}

			return View("GetCards", cards.ToList());
		}

		[Authorize]
		public void SaveCard(int CardId)
		{
			if (db.UserCards.Where(Card => Card.UserId == WebSecurity.CurrentUserId).Where(Card => Card.CardId == CardId).FirstOrDefault() == null)
			{
				try
				{
					UserCard newUC = new UserCard { UserId = WebSecurity.CurrentUserId, CardId = (int)CardId };

					db.UserCards.Add(newUC);
					db.SaveChanges();
					//return RedirectToAction("Index", "Home");
				}
				catch (Exception) { }
			}
			//return RedirectToAction("GetCards");
		}

		[Authorize]
		public void DeleteCard(int CardId)
		{
			if (db.UserCards.Where(Card => Card.UserId == WebSecurity.CurrentUserId).Where(Card => Card.CardId == CardId).FirstOrDefault() != null)
			{
				try
				{
					db.UserCards.Remove(db.UserCards.Where(User => User.UserId == WebSecurity.CurrentUserId).Where(Card => Card.CardId == CardId).FirstOrDefault());
					db.SaveChanges();
				}
				catch (Exception) { }
			}
		}

	}
}
