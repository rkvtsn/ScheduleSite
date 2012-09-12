using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mvc_Schedule.Models;
using Mvc_Schedule.Models.DataModels.ModelViews;

namespace Mvc_Schedule.Controllers
{
	public class DefaultController : Controller
	{
		public DefaultController()
		{
			ViewBag.Title = "Главная";
		}

		//
		// GET: /Default/

		public ActionResult Index()
		{
			using (var db = new DomainContext())
			{
				var model = db.Facults.ListNames();
				return View(model);
			}
		}

		[HttpPost]
		public JsonResult GetGroups(string id)
		{
			int facultId;
			if (int.TryParse(id, out facultId))
				using (var db = new DomainContext())
				{
					var data = db.Groups.ByFacult(facultId);
					return Json(data);
				}
			return Json(null);
		}


		public JsonResult ChangeTheme()
		{
			var cookie = System.Web.HttpContext.Current.Request.Cookies["Theme"];
			if (cookie == null)
			{
				System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("Theme", "True") { Expires = DateTime.Now.AddDays(2) });
			}
			else
				if (cookie.Value == "False")
				{
					System.Web.HttpContext.Current.Response.Cookies["Theme"].Value = "True";
					System.Web.HttpContext.Current.Response.Cookies["Theme"].Expires = DateTime.Now.AddDays(2);
				}
				else
				{
					System.Web.HttpContext.Current.Response.Cookies["Theme"].Value = "False";
					System.Web.HttpContext.Current.Response.Cookies["Theme"].Expires = DateTime.Now.AddDays(2);
					return Json("False");
				}

			return Json("True");
		}


		/*
		 * 
		 * --- Форма Входа --- 
		 * 
		 */

		public ActionResult LogOn()
		{
			if (Request.IsAuthenticated)
				return RedirectToAction("Index", "Default");
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOn(User user, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (FormsAuthentication.Authenticate(user.Username, user.Password))
				{
					FormsAuthentication.SetAuthCookie(user.Username, user.Remember);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
						&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Default");
					}
				}
				else
				{
					ViewBag.Error = "Извините, введены неверные данные.";
				}
			}
			return View();
		}

		[Authorize]
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Default");
		}

		public ViewResult Error(int id = 101)
		{
			switch (id)
			{
				case 404:
					ViewBag.Message = "Ошибка 404. Запрашиваемой страницы не существует.";
					break;
				case 400:
					ViewBag.Message = "Ошибка 400. Ой что-то не то...";
					break;
				case 500:
					ViewBag.Message = "Ошибка 500. Сервер отдыхает.";
					break;
				default:
					id = 101;
					ViewBag.Message = "Ошибка 101. Непонятно... Попробуйте ещё.";
					break;
			}
			Response.StatusCode = id;
			return View();
		}
	}
}
