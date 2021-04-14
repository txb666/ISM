function editVisa(id, start_date, expired_date, entry_date, entry_port) {
    document.getElementById("edit_startDate").value = start_date;
    document.getElementById("edit_expiredDate").value = expired_date;
    document.getElementById("edit_entryDate").value = entry_date;
    document.getElementById("edit_entryPort").value = entry_port;
    document.getElementById("edit_visa_id").value = id;
}

function validateEditVisa() {
    var id = document.getElementById("edit_visa_id").value;
    var start_date = document.getElementById("edit_startDate").value;
    var expired_date = document.getElementById("edit_expiredDate").value;
    var entry_date = document.getElementById("edit_entryDate").value;
    var entry_port = document.getElementById("edit_entryPort").value;
    var searchButton = document.getElementById("searchBtn");
    if (/^([A-Za-z0-9\s]+)$/.test(entry_port) == false && entry_port.length != 0) {
        alert("Entry port must not be empty or contain special character");
        return;
    }
    if (!start_date) {
        alert("Start date must not be empty.");
        return;
    }
    if (!expired_date) {
        alert("Expired date must not be empty.");
        return;
    }
    if (!entry_date) {
        alert("Entry date must not be empty.");
        return;
    }
    if (start_date >= expired_date) {
        alert("Expired date must be greater than Start date.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Visa/Edit",
        data: { visa_id: id, start_date: start_date, expired_date: expired_date, entry_date: entry_date, entry_port: entry_port },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Visa successfull");
                searchButton.click();
            }
            else {
                alert("Edit Visa failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function validateNotificationVisa() {
    var days_before = document.getElementById("notification_input").value;
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Visa/CreateOrEditNotificationConfig",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/Visa';
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

function validateCreateOrEditVisa() {
    var student_id = document.getElementById("edit_student_id").value;
    var visa_id = document.getElementById("edit_visa_id").value;
    var start_date = document.getElementById("edit_start_date").value;
    var expired_date = document.getElementById("edit_expired_date").value;
    var date_entry = document.getElementById("edit_date_entry").value;
    var entry_port = document.getElementById("edit_entry_port").value;
    var picture = document.getElementById("edit_picture").files[0];
    var fileName = document.getElementById("edit_picture").value;
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (entry_port.trim().length == 0) {
        alert("Entry port must not be empty");
        return;
    }
    if (!start_date) {
        alert("Start date must not be empty.");
        return;
    }
    if (!expired_date) {
        alert("Expired date must not be empty.");
        return;
    }
    if (!date_entry) {
        alert("Entry date must not be empty.");
        return;
    }
    if (start_date >= expired_date) {
        alert("Expired date must be greater than Start date.");
        return;
    }
    if (extFile != "jpg" || extFile != "jpeg" || extFile != "png") {
        alert("Only jpg/jpeg and png files are allowed. Please choose jpg/jpeg/png file only.");
        return;
    }
    var fdata = new FormData();
    fdata.append("student_id", student_id);
    fdata.append("visa_id", visa_id);
    fdata.append("start_date", start_date);
    fdata.append("expired_date", expired_date);
    fdata.append("date_entry", date_entry);
    fdata.append("entry_port", entry_port);
    fdata.append("picture", picture);
    $.ajax({
        type: "post",
        url: "/Visa/CreateOrEdit",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Edit successful");
            window.location.href = "/Visa";
        },
        error: function () {
            alert("Edit failed");
        }
    });
}