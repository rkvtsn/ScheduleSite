using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Text;
using Mvc_Schedule.Models.DataModels.Entities;
using Mvc_Schedule.Models.DataModels.ModelViews;

namespace Mvc_Schedule.Models.DataModels.Repositories
{
	public class RepositoryScheduleTable : RepositoryBase<ConnectionContext>
	{
		public RepositoryScheduleTable(ConnectionContext ctx) : base(ctx) { }

		public ScheduleTableIndex ListForIndex(int groupId, bool week)
		{
			var group = _ctx.StudGroups.Find(groupId);
			var result = new ScheduleTableIndex
							{
								Group = group,
								IsOddWeek = week,
							};
			if (group != null)
			{
				result.Schedule = List(groupId, week);
				result.Lessons = _ctx.Lessons.OrderBy(x => x.Time).ToList();
				result.Weekdays = _ctx.Weekdays.OrderBy(x => x.WeekdayId).ToList();
			}
			return result;
		}

		public ScheduleTableCreate ListForCreate(int groupId, bool week)
		{
			var group = _ctx.StudGroups.Find(groupId);
			if (group == null) return null;

			var result = new ScheduleTableCreate
							{
								GroupId = group.GroupId,
								GroupName = group.Name,
								IsOddWeek = week,
								Lessons = _ctx.Lessons.OrderBy(x => x.Time).ToList(),
								Weekdays = _ctx.Weekdays.OrderBy(x => x.WeekdayId).ToList(),
								//Subjects = _ctx.Subjects.OrderBy(x => x.Name).ToList(),
								ScheduleTableRows = List(groupId, week)
							};

			return result;
		}

		public IList<ScheduleTable> List(int groupId, bool week)
		{
			return _ctx.ScheduleTables.Include(x => x.Subject)
									  .Where(x => x.GroupId == groupId && week == x.IsWeekOdd).ToList();
		}

		//public void ListAdd(ScheduleTableCreate table)
		//{
		//    var list = List(table.GroupId, table.IsOddWeek);
		//    foreach (var row in list)
		//    {
		//        _ctx.ScheduleTables.Remove(row);
		//    }
		//    if (table.ScheduleTableRows != null)
		//        foreach (var row in table.ScheduleTableRows)
		//            _ctx.ScheduleTables.Add(row);
		//}

		public void ListAdd(ScheduleTableCreate table)
		{
			var list = List(table.GroupId, table.IsOddWeek);
			foreach (var row in list)
			{
				_ctx.ScheduleTables.Remove(row);
			}

			//if (table.ScheduleTableRows != null)
			//    foreach (var row in table.ScheduleTableRows)
			//    {
			//		  row.Subject.Name = row.Subject.Name.Trim();
			//        var subject = _ctx.Subjects.SingleOrDefault(x => x.Name == row.Subject.Name);
			//        if (subject != null)
			//            row.Subject = subject;

			//        _ctx.ScheduleTables.Add(row);
			//    }

			if (table.ScheduleTableRows != null)
			{
				var subjectsQueue = new Dictionary<string, Subject>();

				foreach (var subjectName in table.ScheduleTableRows.Select(x => x.Subject.Name.Trim()).Distinct())
				{
					subjectsQueue[subjectName] = _ctx.Subjects.SingleOrDefault(x => x.Name == subjectName);
					if (subjectsQueue[subjectName] == null)
					{
						subjectsQueue[subjectName] = new Subject { Name = subjectName };
						_ctx.Subjects.Add(subjectsQueue[subjectName]);
					}
				}
				
				_ctx.SaveChanges();

				foreach (var row in table.ScheduleTableRows)
				{
					row.Subject = subjectsQueue[row.Subject.Name.Trim()];
					_ctx.ScheduleTables.Add(row);
				}
			}
		}

	}
}
