using System.ComponentModel.DataAnnotations;

namespace Mvc_Schedule.Models.DataModels.Entities
{
	public class ScheduleTable
	{
		[Key]
		public int ScheduleTableId { get; set; }
		
		[Required]
		[MaxLength(32)]
		[Display(Name = "���������")]
		public string Auditory { get; set; }

		[MaxLength(127)]
		[Display(Name = "�������������")]
		public string LectorName { get; set; }

		//�������� ������
		[Display(Name = "׸���� ������")]
		public bool IsWeekOdd { get; set; }

		[Display(Name = "����������")]
		public int SubjectId { get; set; }

		[Display(Name = "������")]
		public int LessonId { get; set; }

		[Display(Name = "���� ������")]
		public int WeekdayId { get; set; }

		[Display(Name = "������")]
		public int GroupId { get; set; }

		[ForeignKey("WeekdayId")]
		public virtual Weekday Weekday { get; set; }
		[ForeignKey("GroupId")]
		public virtual StudGroup StudGroup { get; set; }
		[ForeignKey("SubjectId")]
		public virtual Subject Subject { get; set; }
		[ForeignKey("LessonId")]
		public virtual Lesson Lesson { get; set; }
	}
}