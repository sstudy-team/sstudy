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
			int page = 1;

			page = number != null ? (int)number : 1;

			if (number != null)
			{
				ViewBag.Number = number;
				page = (int)number;
			}
			else
			{
				ViewBag.Number = 1;
			}
			ViewBag.Saved = 0;
			ViewBag.Count = db.Cards.Count();

			List<Card> NewList = db.Cards.OrderBy(Card => Card.CardId).Skip((page - 1) * 8).Take(8).ToList();

			ViewBag.Save = new int[8];

			for (int i = 0; i < NewList.Count(); i++)
			{
				if (db.UserCards.Where(user => user.UserId == WebSecurity.CurrentUserId).Where(us => us.CardId == db.Cards.OrderBy(Card => Card.CardId).Skip((page - 1) * 8 + i).First().CardId) == null)
					ViewBag.Save[i] = 0;
				else
					ViewBag.Save[i] = 1;
			}

			return View("GetCards", NewList);
		}

		public ActionResult GetUserCards(int? number)
		{
			int page = 1;

			page = number != null ? (int)number : 1;

			if (number != null)
			{
				ViewBag.Number = number;
			}
			else
			{
				ViewBag.Number = 1;
			}

			ViewBag.Saved = 1;
			ViewBag.Count = db.Cards.Count();

			List<UserCard> NewList = db.UserCards.Where(Card => Card.UserId == WebSecurity.CurrentUserId).OrderBy(Card => Card.UserCardId).Skip((page - 1) * 8).Take(8).ToList();

			List<Card> cards = new List<Card>();
			
			foreach (var item in NewList)
			{
				cards.Add(db.Cards.Where(Card => Card.CardId == item.CardId).FirstOrDefault());
			}

			ViewBag.Save = new int[8];

			for (int i = 0; i < 8; i++)
			{
				ViewBag.Save[i] = 1;
			}

			return View("GetCards", cards.ToList());
		}

		public ActionResult Pages(int number, int count, int saved)
		{
			ViewBag.Number = number;
			ViewBag.Count = count;
			ViewBag.Saved = saved;

			return View();
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

		[Authorize]
		public void CreateCard(Card NewCard)
		{
			NewCard.Founder = WebSecurity.CurrentUserId;
			NewCard.Expert1 = 0;
			NewCard.Expert2 = 0;
			NewCard.Expert3 = 0;
			NewCard.Expert4 = 0;
			NewCard.Expert5 = 0;
			NewCard.Expert6 = 0;
			NewCard.Expert7 = 0;
			NewCard.Expert8 = 0;
			NewCard.Expert9 = 0;

			db.Cards.Add(NewCard);
			db.SaveChanges();
		}

		[Authorize]
		public void ApproveCard(int CardId, int Move)
		{
			UnApprovedCard Card = db.UnApprovedCards.Where(unCard => unCard.UnApprovedCardId == CardId).FirstOrDefault();
			
			if (Move == 1)
			{
				if (db.UserProfile.Where(user => user.UserId == WebSecurity.CurrentUserId).FirstOrDefault().Role == 1)
				{
					if (Card.Expert1 == 0)
					{
						Card.Expert1 = WebSecurity.CurrentUserId;
					}
					else if (Card.Expert2 == 0)
					{
						Card.Expert2 = WebSecurity.CurrentUserId;
					}
					else
					{
						Card.Expert3 = WebSecurity.CurrentUserId;
					}
				}
				else if (db.UserProfile.Where(user => user.UserId == WebSecurity.CurrentUserId).FirstOrDefault().Role == 2)
				{
					if (Card.Expert4 == 0)
					{
						Card.Expert4 = WebSecurity.CurrentUserId;
					}
					else if (Card.Expert5 == 0)
					{
						Card.Expert5 = WebSecurity.CurrentUserId;
					}
					else
					{
						Card.Expert6 = WebSecurity.CurrentUserId;
					}
				}
				else if (db.UserProfile.Where(user => user.UserId == WebSecurity.CurrentUserId).FirstOrDefault().Role == 3)
				{
					if (Card.Expert7 == 0)
					{
						Card.Expert7 = WebSecurity.CurrentUserId;
					}
					else if (Card.Expert8 == 0)
					{
						Card.Expert8 = WebSecurity.CurrentUserId;
					}
					else
					{
						Card.Expert9 = WebSecurity.CurrentUserId;


						Card NewCard = new Card();
						NewCard.Title = Card.Title ;
						NewCard.Prev = Card.Prev;
						NewCard.Information = Card.Information;
						NewCard.Subject = Card.Subject;
						NewCard.Section = Card.Section;
						NewCard.Subsection = Card.Subsection;
						NewCard.Topic = Card.Topic;
						NewCard.Founder = Card.Founder;
						NewCard.Expert1 = Card.Expert1;
						NewCard.Expert2 = Card.Expert2;
						NewCard.Expert3 = Card.Expert3;
						NewCard.Expert4 = Card.Expert4;
						NewCard.Expert5 = Card.Expert5;
						NewCard.Expert6 = Card.Expert6;
						NewCard.Expert7 = Card.Expert7;
						NewCard.Expert8 = Card.Expert8;
						NewCard.Expert9 = Card.Expert9;

						db.UnApprovedCards.Remove(Card);
						db.Cards.Add(NewCard);

						db.SaveChanges();
					}
				}
			}
			else if (Move == -1)
			{

			}
			else if (Move == 0)
			{

			}
		}

		[Authorize]
		public void EditCard()
		{

		}

	}
}
