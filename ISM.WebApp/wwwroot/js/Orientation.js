function validateCreateORT() {
    var student_id = document.getElementById('ort_studentid').value;
    var content = document.getElementById('ort_content_id').value;
    var date = document.getElementById('ort_date_id').value;
    var time = document.getElementById('ort_time_id').value;
    var location_ort = document.getElementById('ort_location_id').value;
    var requirement = document.getElementById('ort_requirement_id').value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById('ort_date_id').value);
    if (content.trim().length <= 0) {
        enableButton('save_create');
        alert("Content must not be empty.");
        return;
    }
    if (!date) {
        enableButton('save_create');
        alert("Date must not be empty.");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_create');
        alert("Date must be greater than current date.");
        return;
    }
    if (!time) {
        enableButton('save_create');
        alert("Time must not be empty.");
        return;
    }
    if (location_ort.trim().length <= 0) {
        enableButton('save_create');
        alert("Location must not be empty.");
        return;
    }
    if (requirement.trim().length <= 0) {
        enableButton('save_create');
        alert("Requirement must not be empty.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/isExist",
        data: { id: student_id, content: content, date: date, time: time, location: location_ort },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save_create');
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
                            enableButton('save_create');
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
    if (content.trim().length <= 0) {
        enableButton('save_edit');
        alert("Content must not be empty.");
        return;
    }
    if (!date) {
        enableButton('save_edit');
        alert("Date must not be empty.");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_edit');
        alert("Date must be greater than current date.");
        return;
    }
    if (!time) {
        enableButton('save_edit');
        alert("Time must not be empty.");
        return;
    }
    if (location_ort.trim().length <= 0) {
        enableButton('save_edit');
        alert("Location must not be empty.");
        return;
    }
    if (requirement.trim().length <= 0) {
        enableButton('save_edit');
        alert("Requirement must not be empty.");
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
                enableButton('save_edit');
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
        url: "/Orientation/SetupNotification",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/Orientation';
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