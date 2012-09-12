﻿function loadGroups(data) {
	var result = "Выберите группу";
	if (data != null && data.length > 0) {
		var groupsDiv = $("#groups ul");
		groupsDiv.html("");
		$.each(data, function (index, d) {
			var group = '<li><a href="/Schedule/Index/' + d.Id + '">' + d.Name + '</a></li>';
			groupsDiv.append(group);
		});
	} else {
		result = "Извините, данный факультет в разработке";
		$("#groups ul").append('<li><a href="#">Групп нет</a></li>');
	}
	$("#page-title").html(result);
}
$(document).ready(function () {
	$("#groups").hide();
	$("#facults ul li a").click(function () {
		$.each($("#facults ul li a"), function (index, f) {
			if (f.className == "selected") f.className = "";
		});
		$(this).addClass("selected");

		var facultId = $(this).attr("id");
		$("#groups").fadeOut(function () {
			$.ajax({
				type: "POST",
				contentType: "application/json;charset=utf-8",
				url: "/Default/GetGroups",
				data: '{"id":"' + facultId + '"}',
				dataType: "json",
				success: loadGroups
			});
		}).fadeIn();
		return false;
	});
	$("#facults").fadeIn();
});