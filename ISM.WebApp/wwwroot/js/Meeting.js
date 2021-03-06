function validateCreateMAT(staff_id) {
    var dateAT = document.getElementById("create_date_id").value;
    var startTimeAT = document.getElementById("create_startTime_id").value;
    var endTimeAT = document.getElementById("create_endTime_id").value;
    var date_check = new Date(document.getElementById("create_date_id").value);
    var current = new Date();
    if (!dateAT) {
        enableButton('save_create');
        alert("Date must not be empty.");
        return;
    }
    if (date_check.getTime() <= current.getTime()) {
        enableButton('save_create');
        alert("Date must not be smaller than current date.");
        return;
    }
    if (!startTimeAT || !endTimeAT) {
        enableButton('save_create');
        alert("Time must not be empty.");
        return;
    }
    if (startTimeAT >= endTimeAT) {
        enableButton('save_create');
        alert("End time must be greater than start time");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Meeting/CheckMAT",
        data: { staff_id: staff_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save_create');
                alert("Can not create Available time at same time.");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Meeting/CreateMAT",
                    data: { staff_id: staff_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Create successfull");
                            window.location.href = "/Meeting";
                        }
                        else {
                            enableButton('save_create');
                            alert("Create failed")
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteMAT(mat_id, staff_id, date, start_time, end_time) {
    var temp = confirm("Do you want to delete?");
    if (temp) {
        $.ajax({
            type: "POST",
            url: "/Meeting/CheckMeetingSchedule",
            data: { staff_id: staff_id, date: date, start_time: start_time, end_time, end_time },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    $.ajax({
                        type: "POST",
                        url: "/Meeting/DeleteMAT",
                        data: { mat_id: mat_id, staff_id: staff_id },
                        dataType: "text",
                        success: function (msg) {
                            if (msg == "true") {
                                alert("Delete successfull");
                                window.location.href = "/Meeting";
                            }
                            else {
                                alert("Delete failed")
                            }
                        },
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
                else {
                    alert("Cannot delete because available time already book by a student.")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}

function editMATform(mat_id, date, start_time, end_time) {
    document.getElementById("edit_date_id").value = date;
    document.getElementById("edit_startTime_id").value = start_time;
    document.getElementById("edit_endTime_id").value = end_time;
    document.getElementById("edit_mat_id").value = mat_id;
}

function validateEditMAT(staff_id) {
    var mat_id = document.getElementById("edit_mat_id").value;
    var dateAT = document.getElementById("edit_date_id").value;
    var startTimeAT = document.getElementById("edit_startTime_id").value;
    var endTimeAT = document.getElementById("edit_endTime_id").value;
    if (!dateAT) {
        enableButton('save_edit');
        alert("Date must not be empty.");
        return;
    }
    if (!startTimeAT || !endTimeAT) {
        enableButton('save_edit');
        alert("Time must not be empty.");
        return;
    }
    if (startTimeAT >= endTimeAT) {
        enableButton('save_edit');
        alert("End time must be greater than start time");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Meeting/CheckMAT",
        data: { staff_id: staff_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save_edit');
                alert("Already exist cannot edit.");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Meeting/EditMAT",
                    data: { mat_id: mat_id, staff_id: staff_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Edit successfull");
                            window.location.href = "/Meeting";
                        }
                        else {
                            enableButton('save_edit');
                            alert("Edit failed")
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function acceptMR(ms_id) {
    var temp = confirm("Do you want to accept?");
    if (temp) {
        $.ajax({
            type: "POST",
            url: "/Meeting/AcceptMR",
            data: { ms_id: ms_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = "/Meeting";
                }
                else {
                    enableButtonAccept("accept_btn_id");
                    alert("Failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        enableButtonAccept("accept_btn_id");
        return;
    }
}

function validateSetupNotificationMeeting() {
    var days_before = document.getElementById('notification_input_meeting').value;
    if (!days_before) {
        enableButton('save_setup');
        alert("Days before must not be empty.");
        return;
    }
    if (/^[1-9]\d*$/.test(days_before) == false) {
        enableButton('save_setup');
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Meeting/SetupNotification",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/Meeting';
            }
            else {
                enableButton('save_setup');
                alert("Failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function StudentBookMeeting(staff_id, staff_name, date, start, end) {
    document.getElementById("book_date_id").value = date;
    document.getElementById("book_startTime_id").value = start;
    document.getElementById("book_endTime_id").value = end;
    document.getElementById("book_staff_id").value = staff_id;
    document.getElementById("book_staff_name_id").value = staff_name;
}

function validateBookAMeeting(student_id) {
    var staff_id = document.getElementById("book_staff_id").value;
    var dateAT = document.getElementById("book_date_id").value;
    var startTimeAT = document.getElementById("book_startTime_id").value;
    var endTimeAT = document.getElementById("book_endTime_id").value;
    var note = document.getElementById("book_note_id").value;
    if (!note) {
        enableButton('save');
        alert("Please enter meeting note.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Meeting/isExist",
        data: { staff_id: staff_id, student_id: student_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save');
                alert("Can not book a meeting at same time.");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Meeting/BookAMeeting",
                    data: { staff_id: staff_id, student_id: student_id, date: dateAT, start_time: startTimeAT, end_time: endTimeAT, note: note },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Book successfull");
                            document.getElementById("xmas-popup").style.display = "none";
                            location.reload();
                        }
                        else {
                            enableButton('save');
                            alert("Book failed")
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}