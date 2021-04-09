function validateCreateMAT(staff_id) {
    var dateAT = document.getElementById("create_date_id").value;
    var startTimeAT = document.getElementById("create_startTime_id").value;
    var endTimeAT = document.getElementById("create_endTime_id").value;
    if (!dateAT) {
        alert("Date must not be empty.");
        return;
    }
    if (!startTimeAT || !endTimeAT) {
        alert("Time must not be empty.");
        return;
    }
    if (startTimeAT >= endTimeAT) {
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

function deleteMAT(mat_id, staff_id) {
    var temp = confirm("Do you want to delete?");
    if (temp) {
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
        return;
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
        alert("Date must not be empty.");
        return;
    }
    if (!startTimeAT || !endTimeAT) {
        alert("Time must not be empty.");
        return;
    }
    if (startTimeAT >= endTimeAT) {
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
                    alert("Failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        return;
    }
}

function validateSetupNotificationMeeting() {
    var days_before = document.getElementById('notification_input_meeting').value;
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
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
                alert("Failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}