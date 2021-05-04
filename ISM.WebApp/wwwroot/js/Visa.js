function skipNotificationVisa(user_id) {
    var check = confirm("Have you actually submitted the document for renew your visa?");
    if (check) {
        $.ajax({
            type: "POST",
            url: "/Visa/SkipNotification",
            data: { user_id: user_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = '/Visa';
                }
                else {
                    enableButtonSkip("visa_btn_skip");
                    alert("Failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        enableButtonSkip("visa_btn_skip");
        return;
    }
}

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
    if (/^[A-Za-z0-9\s]+$/.test(entry_port) == false || /^\s*$/.test(entry_port) == true) {
        enableButton('save_edit');
        alert("Entry port must not be empty or contain special character");
        return;
    }
    if (!start_date) {
        enableButton('save_edit');
        alert("Start date must not be empty.");
        return;
    }
    if (!expired_date) {
        enableButton('save_edit');
        alert("Expired date must not be empty.");
        return;
    }
    if (!entry_date) {
        enableButton('save_edit');
        alert("Entry date must not be empty.");
        return;
    }
    if (start_date >= expired_date) {
        enableButton('save_edit');
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
                enableButton('save_edit');
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
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (/^[A-Za-z0-9\s]+$/.test(entry_port) == false || /^\s*$/.test(entry_port) == true) {
        enableButton('save');
        alert("Entry port must not be empty");
        return;
    }
    if (!start_date) {
        enableButton('save');
        alert("Start date must not be empty.");
        return;
    }
    if (!expired_date) {
        enableButton('save');
        alert("Expired date must not be empty.");
        return;
    }
    if (!date_entry) {
        enableButton('save');
        alert("Entry date must not be empty.");
        return;
    }
    if (start_date >= expired_date) {
        enableButton('save');
        alert("Expired date must be greater than Start date.");
        return;
    }
    if (!fileName) {
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
                enableButton('save');
                alert("Edit failed");
            }
        });
    }
    else {
        if (!allowedExtensions.exec(fileName)) {
            enableButton('save');
            alert('Only jpg/jpeg and png files are allowed!');
            picture.value = '';
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
                enableButton('save');
                alert("Edit failed");
            }
        });
    }
}