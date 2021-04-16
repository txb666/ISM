function validateCreateORT() {
    var student_id = document.getElementById('ort_studentid').value;
    var content = document.getElementById('ort_content_id').value;
    var date = document.getElementById('ort_date_id').value;
    var time = document.getElementById('ort_time_id').value;
    var location_ort = document.getElementById('ort_location_id').value;
    var requirement = document.getElementById('ort_requirement_id').value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById('ort_date_id').value);
    if (/^[a-zA-Z0-9 ,-]*$/.test(content) == false || !content) {
        alert("Content must not be empty or contain special character.");
        return;
    }
    if (!date) {
        alert("Date must not be empty.");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        alert("Date must be greater than current date.");
        return;
    }
    if (!time) {
        alert("Time must not be empty.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(location_ort) == false || !location_ort) {
        alert("Location must not be empty or contain special character.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(requirement) == false || !requirement) {
        alert("Requirement must not be empty or contain special character.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/isExist",
        data: { id: student_id, content: content, date: date, time: time, location: location_ort },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Orientation already exist.");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Orientation/isSameTime",
                    data: { id: student_id, date: date, time: time },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Cannot create other Orientation at same time.");
                            return;
                        }
                        else {
                            $.ajax({
                                type: "POST",
                                url: "/Orientation/CreateORT",
                                data: { id: student_id, content: content, date: date, time: time, location: location_ort, requirement: requirement },
                                dataType: "text",
                                success: function (msg) {
                                    if (msg == "true") {
                                        alert("Create successfull");
                                        document.getElementById("xmas-popup").style.display = "none";
                                        location.reload();
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
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function editORT(id, content, date, time, location_ort, requirement) {
    document.getElementById('edit_ort_id').value = id;
    document.getElementById('edit_ort_content_id').value = content;
    document.getElementById('edit_ort_date_id').value = date;
    document.getElementById('edit_ort_time_id').value = time;
    document.getElementById('edit_ort_location_id').value = location_ort;
    document.getElementById('edit_ort_requirement_id').value = requirement;
}

function validateEditORT() {
    var id = document.getElementById('edit_ort_id').value;
    var content = document.getElementById('edit_ort_content_id').value;
    var date = document.getElementById('edit_ort_date_id').value;
    var time = document.getElementById('edit_ort_time_id').value;
    var location_ort = document.getElementById('edit_ort_location_id').value;
    var requirement = document.getElementById('edit_ort_requirement_id').value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById('edit_ort_date_id').value);
    if (/^[a-zA-Z0-9 ,-]*$/.test(content) == false || !content) {
        alert("Content must not be empty or contain special character.");
        return;
    }
    if (!date) {
        alert("Date must not be empty.");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        alert("Date must be greater than current date.");
        return;
    }
    if (!time) {
        alert("Time must not be empty.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(location_ort) == false || !location_ort) {
        alert("Location must not be empty or contain special character.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(requirement) == false || !requirement) {
        alert("Requirement must not be empty or contain special character.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/EditORT",
        data: { id: id, content: content, date: date, time: time, location: location_ort, requirement: requirement },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                document.getElementById("xmas-popup-assign").style.display = "none";
                location.reload();
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

function deleteORT() {
    var id = document.getElementById('delete_ort_id').value;
    var temp = confirm("Do you want to delete?");
    if (temp) {
        $.ajax({
            type: "POST",
            url: "/Orientation/DeleteORT",
            data: { id: id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Delete successfull");
                    location.reload();
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

function validateNotificationORT() {
    var days_before = document.getElementById("ort_notification_input").value;
    if (!days_before) {
        alert("Days before must not be empty.");
        return;
    }
    if (/^[1-9]\d*$/.test(days_before) == false) {
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/SetupNotification",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/Orientation';
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