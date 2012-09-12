using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Mvc_Schedule.Models.DataModels;

namespace Mvc_Schedule
{
	// Примечание: Инструкции по включению классического режима IIS6 или IIS7 
	// см. по ссылке http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Theme",
				"ChangeTheme",
				new { controller = "Default" },
				new { action = "ChangeTheme" }
			);

			routes.MapRoute(
				"Errors",
				"{action}/{id}",
				new { controller = "Default", id = UrlParameter.Optional },
				new { action = "Error" }
			);

			routes.MapRoute(
				"Account",
				"{action}",
				new { controller = "Default" },
				new { action = "LogOn|LogOff" }
			);

			routes.MapRoute(
				"Schedule", // Имя маршрута
				"Schedule/{action}/{id}/{week}", // URL-адрес с параметрами
				new { controller = "Schedule", action = "Index", id = UrlParameter.Optional, week = UrlParameter.Optional } // Параметры по умолчанию
			);

			routes.MapRoute(
				"Default", // Имя маршрута
				"{controller}/{action}/{id}", // URL-адрес с параметрами
				new { controller = "Default", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RegisterGlobalFilters(GlobalFilters.Filters);
			Database.SetInitializer(new DbInitializer());
			RegisterRoutes(RouteTable.Routes);
		}
	}
}