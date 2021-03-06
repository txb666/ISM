function validateSetupNotification() {
    var days_before = document.getElementById('notification_input_detail_agenda').value;
    if (!days_before) {
        enableButton('save');
        alert("Days before must not be empty.");
        return;
    }
    if (/^[1-9]\d*$/.test(days_before) == false) {
        enableButton('save');
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/DetailedAgenda/SetupNotification",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/DetailedAgenda';
            }
            else {
                enableButton('save');
                alert("Failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function validateCreateDetailedAgenda() {
    var student_group_id = document.getElementById("create_student_group_id").value;
    var date = document.getElementById("create_date").value;
    var time_start = document.getElementById("create_time_start").value;
    var time_end = document.getElementById("create_time_end").value;
    var time_zone = document.getElementById("create_time_zone").value;
    var venue = document.getElementById("create_venue").value;
    var PIC = document.getElementById("create_PIC").value;
    var content = document.getElementById("create_content").value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById("create_date").value);
    if (!date || !time_start || !time_end) {
        enableButton('save_create');
        alert("Date and time must not be empty");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_create');
        alert("Date must be greater than current date");
        return;
    }
    if (time_start >= time_end) {
        enableButton('save_create');
        alert("Start time must be smaller than end time");
        return;
    }
    if (/^\s*$/.test(time_zone) == true) {
        enableButton('save_create');
        alert("Time Zone must not be empty");
        return;
    }
    if (venue.trim().length <= 0) {
        enableButton('save_create');
        alert("Venue must not be empty.");
        return;
    }
    if (PIC.trim().length <= 0) {
        enableButton('save_create');
        alert("PIC must not be empty.");
        return;
    }
    if (content.trim().length <= 0) {
        enableButton('save_create');
        alert("Content must not be empty.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/DetailedAgenda/Create",
        data: { student_group_id: student_group_id, date: date, time_start: time_start, time_end: time_end, time_zone: time_zone, venue: venue, PIC: PIC, content: content },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Create successfull");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_create');
                alert("Create failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function EditDetailedAgenda(detailed_agenda_id, date, time_start, time_end, time_zone, venue, PIC, content) {
    document.getElementById("edit_detailed_agenda_id").value = detailed_agenda_id;
    document.getElementById("edit_date").value = date;
    document.getElementById("edit_time_start").value = time_start;
    document.getElementById("edit_time_end").value = time_end;
    document.getElementById("edit_time_zone").value = time_zone;
    document.getElementById("edit_venue").value = venue;
    document.getElementById("edit_PIC").value = PIC;
    document.getElementById("edit_content").value = content;
}

function validateEditDetailedAgenda() {
    var detailed_agenda_id = document.getElementById("edit_detailed_agenda_id").value;
    var date = document.getElementById("edit_date").value;
    var time_start = document.getElementById("edit_time_start").value;
    var time_end = document.getElementById("edit_time_end").value;
    var time_zone = document.getElementById("edit_time_zone").value;
    var venue = document.getElementById("edit_venue").value;
    var PIC = document.getElementById("edit_PIC").value;
    var content = document.getElementById("edit_content").value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById("edit_date").value);
    if (!date || !time_start || !time_end) {
        enableButton('save_edit');
        alert("date and time must not be empty");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_edit');
        alert("Date must be greater than current date");
        return;
    }
    if (time_start >= time_end) {
        enableButton('save_edit');
        alert("Start time must be smaller than end time");
        return;
    }
    if (/^\s*$/.test(time_zone) == true) {
        enableButton('save_edit');
        alert("Time Zone must not be empty");
        return;
    }
    if (venue.trim().length <= 0) {
        enableButton('save_edit');
        alert("Venue must not be empty.");
        return;
    }
    if (PIC.trim().length <= 0) {
        enableButton('save_edit');
        alert("PIC must not be empty.");
        return;
    }
    if (content.trim().length <= 0) {
        enableButton('save_edit');
        alert("Content must not be empty.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/DetailedAgenda/Edit",
        data: { detailed_agenda_id: detailed_agenda_id, date: date, time_start: time_start, time_end: time_end, time_zone: time_zone, venue: venue, PIC: PIC, content: content },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                document.getElementById("xmas-popup-assign").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_edit');
                alert("Edit failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteDetailedAgenda(detailed_agenda_id) {
    if (confirm("Are you sure to delete?")) {
        $.ajax({
            type: "POST",
            url: "/DetailedAgenda/Delete",
            data: { detailed_agenda_id: detailed_agenda_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete successfull");
                    location.reload();
                }
                else {
                    alert("Delete failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}