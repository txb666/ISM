function validateCreateORT() {
    var student_id = document.getElementById('ort_studentid').value;
    var content = document.getElementById('ort_content_id').value;
    var date = document.getElementById('ort_date_id').value;
    var time = document.getElementById('ort_time_id').value;
    var location = document.getElementById('ort_location_id').value;
    var requirement = document.getElementById('ort_requirement_id').value;
    var searchbtn = document.getElementById('searchBtn');
    if (/^[a-zA-Z0-9 ,-]*$/.test(content) == false && content.length != 0) {
        alert("Content must not be empty or contain special character.");
        return;
    }
    if (!date) {
        alert("Date must not be empty.");
        return;
    }
    if (!time) {
        alert("Time must not be empty.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(location) == false && location.length != 0) {
        alert("Location must not be empty or contain special character.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(requirement) == false && requirement.length != 0) {
        alert("Requirement must not be empty or contain special character.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/isExist",
        data: { id: student_id, content: content, date: date, time: time, location: location },
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
                                data: { id: student_id, content: content, date: date, time: time, location: location, requirement: requirement },
                                dataType: "text",
                                success: function (msg) {
                                    if (msg == "true") {
                                        alert("Create successfull");
                                        searchbtn.click();
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

function editORT(id, content, date, time, location, requirement) {
    document.getElementById('edit_ort_id').value = id;
    document.getElementById('edit_ort_content_id').value = content;
    document.getElementById('edit_ort_date_id').value = date;
    document.getElementById('edit_ort_time_id').value = time;
    document.getElementById('edit_ort_location_id').value = location;
    document.getElementById('edit_ort_requirement_id').value = requirement;
}

function validateEditORT() {
    var id = document.getElementById('edit_ort_id').value;
    var content = document.getElementById('edit_ort_content_id').value;
    var date = document.getElementById('edit_ort_date_id').value;
    var time = document.getElementById('edit_ort_time_id').value;
    var location = document.getElementById('edit_ort_location_id').value;
    var requirement = document.getElementById('edit_ort_requirement_id').value;
    var searchbtn = document.getElementById('searchBtn');
    if (/^[a-zA-Z0-9 ,-]*$/.test(content) == false && content.length != 0) {
        alert("Content must not be empty or contain special character.");
        return;
    }
    if (!date) {
        alert("Date must not be empty.");
        return;
    }
    if (!time) {
        alert("Time must not be empty.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(location) == false && location.length != 0) {
        alert("Location must not be empty or contain special character.");
        return;
    }
    if (/^[a-zA-Z0-9 ,-]*$/.test(requirement) == false && requirement.length != 0) {
        alert("Requirement must not be empty or contain special character.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Orientation/EditORT",
        data: { id: id, content: content, date: date, time: time, location: location, requirement: requirement },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                searchbtn.click();
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
    var searchbtn = document.getElementById('searchBtn');
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
                    searchbtn.click();
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
}