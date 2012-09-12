using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc_Schedule.Models.DataModels.Entities
{
	public class Subject
	{
		[Key]
		public int SubjectId { get; set; }

		[Required]
		[MaxLength(128, ErrorMessage = "������������ ����� �������� 128")]
		[Display(Name = "�������� ����������")]
		public string Name { get; set; }

		public virtual ICollection<ScheduleTable> ScheduleTable { get; set; }
	}
}