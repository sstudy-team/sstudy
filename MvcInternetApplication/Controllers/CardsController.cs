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

		public object GetTable(int subject, int table, int step)
		{

			switch (subject)
			{
				case 1:
					switch (table)
					{
						case 1:
							return db.Sections.ToList();
						case 2:
							return db.Subsections.Where(p => p.SectionId.SectionId == step).ToList();
						case 3:
							return db.Topics.Where(p => p.SubsectionId.SubsectionId == step).ToList();
					}
					break;
			}

			return null;
		}


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


		[Authorize(Users = "admin")]
		public ActionResult ShowHier(string goal, int subject, int step)
		{

			ViewBag.subject = subject;

			if (goal == "subjects")
			{
				List<Subject> result = db.Subjects.ToList();
				return View("HierarchySub", result);
			}
			else if (goal == "sections")
			{
				List<Section> result = (List<Section>)GetTable(subject, 1, step);
				return View("HierarchySec", result);
			}
			else if (goal == "subsections")
			{
				List<Subsection> result = (List<Subsection>)GetTable(subject, 2, step);
				ViewBag.Id = step;
				return View("HierarchySubsec", result);
			}
			else if (goal == "topics")
			{
				List<Topic> result = (List<Topic>)GetTable(subject, 3, step);
				ViewBag.Id = step;
				return View("HierarchyTop", result);
			}

			return View("Hierarchy", null);
		}


		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult EditSubjects(List<Subject> model)
		{
			if (model != null)
			{
				if (ModelState.IsValid)
				{
					for (int count = 0; count < model.Count; count++)
					{
						if (db.Subjects.OrderBy(Card => Card.SubjectId).Skip(count).Take(1).FirstOrDefault() != model[count])
						{
							if (count <= db.Subjects.Count())
							{
								db.Subjects.OrderBy(Card => Card.SubjectId).Skip(count).Take(1).FirstOrDefault().Name = model[count].Name;
							}
							else
							{
								db.Subjects.Add(model[count]);
							}
						}
					}
					db.SaveChanges();
				}
			}

			return RedirectToAction("ShowHier", "Cards", new { goal = "subjects", subject = 0, step = 0 });
		}

		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult EditSections(List<Section> model)
		{
			if (model != null)
			{
				if (ModelState.IsValid)
				{
					for (int count = 0; count < model.Count; count++)
					{
						if (db.Sections.OrderBy(Card => Card.SectionId).Skip(count).Take(1).FirstOrDefault() != model[count])
						{
							if (count <= db.Sections.Count())
							{
								db.Sections.OrderBy(Card => Card.SectionId).Skip(count).Take(1).FirstOrDefault().Name = model[count].Name;
							}
							else
							{
								db.Sections.Add(model[count]);
							}
						}
					}
					db.SaveChanges();
				}
			}

			return RedirectToAction("ShowHier", "Cards", new { goal = "sections", subject = 1, step = 0 });
		}

		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult EditSubsections(List<Subsection> model, int secId)
		{
			if (model != null)
			{
				if (ModelState.IsValid)
				{
					for (int count = 0; count < model.Count; count++)
					{
						if (db.Subsections.Where(p => p.SectionId.SectionId == secId).OrderBy(Card => Card.SubsectionId).Skip(count).Take(1).FirstOrDefault() != model[count])
						{
							if (count <= db.Subsections.Where(p => p.SectionId.SectionId == secId).Count())
							{
								db.Subsections.Where(p => p.SectionId.SectionId == secId).OrderBy(Card => Card.SubsectionId).Skip(count).Take(1).FirstOrDefault().Name = model[count].Name;
							}
							else
							{
								db.Subsections.Add(model[count]);
							}
						}
					}
					db.SaveChanges();
				}
			}

			return RedirectToAction("ShowHier", "Cards", new { goal = "subsections", subject = 1, step = secId });
		}

		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult EditTopics(List<Topic> model, int subsecId)
		{
			if (model != null)
			{
				if (ModelState.IsValid)
				{
					for (int count = 0; count < model.Count; count++)
					{
						if (db.Topics.Where(p => p.SubsectionId.SubsectionId == subsecId).OrderBy(Card => Card.TopicId).Skip(count).Take(1).FirstOrDefault() != model[count])
						{
							if (count <= db.Topics.Where(p => p.SubsectionId.SubsectionId == subsecId).Count())
							{
								db.Topics.Where(p => p.SubsectionId.SubsectionId == subsecId).OrderBy(Card => Card.TopicId).Skip(count).Take(1).FirstOrDefault().Name = model[count].Name;
							}
							else
							{
								db.Topics.Add(model[count]);
							}
						}
					}
					db.SaveChanges();
				}
			}

			return RedirectToAction("ShowHier", "Cards", new { goal = "topics", subject = 1, step = subsecId });
		}

		[Authorize(Users = "admin")]
		public ActionResult CreateCard()
		{
			return View();
		}

		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult CreateCard(Card newCard)
		{
			ViewBag.msg = "Произошла ошибка";
			try
			{
				ViewBag.msg = "Карточка добавлена";
				db.Cards.Add(newCard);
				db.SaveChanges();
			}
			catch (Exception) { }

			return View();
		}

		[Authorize(Users = "admin")]
		public ActionResult ShowCards()
		{

			return View(db.Cards.ToList());
		}

		[Authorize(Users = "admin")]
		[HttpPost]
		public ActionResult SaveCards(List<Card> model)
		{
			if (model != null)
			{
				if (ModelState.IsValid)
				{
					for (int count = 0; count < model.Count; count++)
					{
						if (db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault() != model[count])
						{
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Information = model[count].Information;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Prev = model[count].Prev;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Section = model[count].Section;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Subject = model[count].Subject;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Subsection = model[count].Subsection;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Title = model[count].Title;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().Topic = model[count].Topic;
							db.Cards.OrderBy(Card => Card.CardId).Skip(count).Take(1).FirstOrDefault().TopicId = model[count].TopicId;
						}
					}
					db.SaveChanges();
				}
			}

			return View("ShowCards", db.Cards.ToList());
		}

	}
}
