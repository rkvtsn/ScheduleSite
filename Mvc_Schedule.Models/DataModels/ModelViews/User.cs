using System.ComponentModel.DataAnnotations;

namespace Mvc_Schedule.Models.DataModels.ModelViews
{
	public class User
	{
		[Required]
		[MinLength(2)]
		[MaxLength(20)]
		[Display(Name = "Логин")]
		public string Username { get; set; }

		[Required]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool Remember { get; set; }
	}
}