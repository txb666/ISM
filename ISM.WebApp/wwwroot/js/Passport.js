function skipNotificationPassport(user_id) {
    var check = confirm("Have you actually submitted the document for renew your passport?");
    if (check) {
        $.ajax({
            type: "POST",
            url: "/Passport/SkipNotification",
            data: { user_id: user_id },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = '/Passport';
                }
                else {
                    enableButtonSkip("passport_btn_skip");
                    alert("Failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        enableButtonSkip("passport_btn_skip");
        return;
    }
}

function validateNotificationPassport() {
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
        url: "/Passport/CreateOrEditNotificationConfig",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = '/Passport';
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

function editPassport(id, passport_number, start_date, expired_date, issuing_authority) {
    document.getElementById("edit_PassportNumber").value = passport_number;
    document.getElementById("edit_StartDate").value = start_date;
    document.getElementById("edit_ExpiredDate").value = expired_date;
    document.getElementById("edit_IssuingAuthority").value = issuing_authority;
    document.getElementById("edit_passport_id").value = id;
}

function validateEditPassport() {
    var id = document.getElementById("edit_passport_id").value;
    var passport_number = document.getElementById("edit_PassportNumber").value;
    var start_date = document.getElementById("edit_StartDate").value;
    var expired_date = document.getElementById("edit_ExpiredDate").value;
    var issuing_authority = document.getElementById("edit_IssuingAuthority").value;
    var searchButton = document.getElementById("searchBtn");
    if (/^[A-Za-z0-9\s]+$/.test(passport_number) == false || /^\s*$/.test(passport_number) == true) {
        enableButton('save');
        alert("Passport number must not be empty or contain special character");
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
    if (issuing_authority.trim().length == 0) {
        enableButton('save');
        alert("Issuing authority must not be empty.");
        return;
    }
    if (start_date >= expired_date) {
        enableButton('save');
        alert("Expired date must be greater than Start date.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Passport/Edit",
        data: { passport_id: id, passport_number: passport_number, start_date: start_date, expired_date: expired_date, issuing_authority: issuing_authority },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Passport successfull");
                searchButton.click();
            }
            else {
                enableButton('save');
                alert("Edit Passport failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function validateCreateOrEditPassport() {
    var student_id = document.getElementById("edit_student_id").value;
    var passport_id = document.getElementById("edit_passport_id").value;
    var passport_number = document.getElementById("edit_passport_number").value;
    var start_date = document.getElementById("edit_start_date").value;
    var expired_date = document.getElementById("edit_expired_date").value;
    var issuing_authority = document.getElementById("edit_issuing_authority").value;
    var picture = document.getElementById("edit_picture").files[0];
    var fileName = document.getElementById("edit_picture").value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (/^[A-Za-z0-9\s]+$/.test(passport_number) == false || /^\s*$/.test(passport_number) == true) {
        enableButton('save');
        alert("Passport number must not be empty or contain special character");
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
    if (issuing_authority.trim().length == 0) {
        enableButton('save');
        alert("Issuing authority must not be empty.");
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
        fdata.append("passport_id", passport_id);
        fdata.append("start_date", start_date);
        fdata.append("expired_date", expired_date);
        fdata.append("passport_number", passport_number);
        fdata.append("issuing_authority", issuing_authority);
        fdata.append("picture", picture);
        $.ajax({
            type: "post",
            url: "/Passport/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                window.location.href = "/Passport";
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
        fdata.append("passport_id", passport_id);
        fdata.append("start_date", start_date);
        fdata.append("expired_date", expired_date);
        fdata.append("passport_number", passport_number);
        fdata.append("issuing_authority", issuing_authority);
        fdata.append("picture", picture);
        $.ajax({
            type: "post",
            url: "/Passport/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                window.location.href = "/Passport";
            },
            error: function () {
                enableButton('save');
                alert("Edit failed");
            }
        });
    }
}