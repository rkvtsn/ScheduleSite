
$(document).ready(function() {
	// I <3 NY
	// rly!
	var index = $("#indexer").val(); // Id - баластовый счётчик, но без него совсем скучно
	var groupId = $("#GroupId").val(); // Индетификатор группы
	//var subjectsOptions = $("#Subjects").html(); // Фича со списком дисциплин - TODO сменить на толковый Ajax

	$(".add_element").click(function () {
		var weekdayId = ($(this).parent("P").parent(".lesson").parent(".weekday").attr("id"));
		var lessonId = ($(this).parent("P").parent(".lesson").attr("id"));
		//var htmlPaste = '<li><label for="ScheduleTableRows_' + index + '__Auditory">Аудитория</label><input class="text-box single-line" id="ScheduleTableRows_' + index + '__Auditory" name="ScheduleTableRows[' + index + '].Auditory" type="text" value="" /><label for="ScheduleTableRows_' + index + '__SubjectId">Дисциплина</label><select id="ScheduleTableRows_' + index + '__SubjectId" name="ScheduleTableRows[' + index + '].SubjectId">' + subjectsOptions + '</select><input id="ScheduleTableRows_' + index + '__LessonId" name="ScheduleTableRows[' + index + '].LessonId" type="hidden" value="' + lessonId + '" /><input id="ScheduleTableRows_' + index + '__GroupId" name="ScheduleTableRows[' + index + '].GroupId" type="hidden" value="' + groupId + '" /><input id="ScheduleTableRows_' + index + '__WeekdayId" name="ScheduleTableRows[' + index + '].WeekdayId" type="hidden" value="' + weekdayId + '" /></li>';
		var htmlPaste = '<li><label for="ScheduleTableRows_' + index + '__Auditory"> Аудитория </label><input class="text-box single-line" id="ScheduleTableRows_' + index + '__Auditory" name="ScheduleTableRows[' + index + '].Auditory" type="text" value="" /><label for="ScheduleTableRows_' + index + '__Subject_Name"> Название дисциплины </label><input type="text" class="subjects" id="ScheduleTableRows_' + index + '__Subject_Name" name="ScheduleTableRows[' + index + '].Subject.Name" /><input id="ScheduleTableRows_' + index + '__LessonId" name="ScheduleTableRows[' + index + '].LessonId" type="hidden" value="' + lessonId + '" /><input id="ScheduleTableRows_' + index + '__GroupId" name="ScheduleTableRows[' + index + '].GroupId" type="hidden" value="' + groupId + '" /><input id="ScheduleTableRows_' + index + '__WeekdayId" name="ScheduleTableRows[' + index + '].WeekdayId" type="hidden" value="' + weekdayId + '" /></li>';
		$(this).parent("P").prev(".lesson_form").append(htmlPaste).find('.subjects').autocomplete({
			source: function (request, response) {
				$.ajax({
					url: '/Schedule/GetSubjects',
					type: "POST",
					dataType: "json",
					data: { id: request.term },
					success: function (data) {
						response($.map(data, function (item) {
							return { label: item, value: item };
						}));
					}
				});
			},
			minLength: 1
		});
		index++;
	});

	$(".remove_element").click(function () {
		var element = $(this).parent("P").prev(".lesson_form").children("LI").last();
		if (element != null) element.remove();
	});
	// & Paris!

	$(".lesson").hide();
	$(".weekday h3").click(function () {
		$(this).parent(".weekday").children(".lesson").fadeToggle();
		var plus = $(this).children("SPAN");
		if (plus.html() == "+") {
			plus.html(" ");
		} else {
			plus.html("+");
		}
	});

	$(".remove").click(function () {
		$(this).parent().remove();
	});
});	