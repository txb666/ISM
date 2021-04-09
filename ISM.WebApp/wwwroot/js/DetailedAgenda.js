function validateSetupNotification() {
    var days_before = document.getElementById('notification_input_detail_agenda').value;
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
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
    if (!date || !time_start || !time_end) {
        alert("date and time must not be empty");
        return;
    }
    if (/^\s*$/.test(time_zone) == true) {
        alert("Time Zone must not be empty");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(venue) == false || /^\s*$/.test(venue) == true) {
        alert("Venue must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(PIC) == false || /^\s*$/.test(PIC) == true) {
        alert("PIC must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        alert("Content must not be empty or contain special character");
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
    if (!date || !time_start || !time_end) {
        alert("date and time must not be empty");
        return;
    }
    if (/^\s*$/.test(time_zone) == true) {
        alert("Time Zone must not be empty");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(venue) == false || /^\s*$/.test(venue) == true) {
        alert("Venue must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(PIC) == false || /^\s*$/.test(PIC) == true) {
        alert("PIC must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(content) == false || /^\s*$/.test(content) == true) {
        alert("Content must not be empty or contain special character");
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
                document.getElementById("xmas-popup-assign1").style.display = "none";
                location.reload();
            }
            else {
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