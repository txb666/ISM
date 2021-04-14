function validateNotificationInsuranceDegree() {
    var deadline = document.getElementById("degree_deadline_id").value;
    var days_before = document.getElementById("degree_daysBefore_id").value;
    if (!deadline) {
        alert("Deadline must not be empty.");
        return;
    }
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Insurance/NotificationDegree",
        data: { days_before: days_before, deadline: deadline },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = "/Insurance";
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

function validateNotificationInsuranceMobility() {
    var days_before = document.getElementById("mobility_days_before_id").value;
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Insurance/NotificationMobility",
        data: { days_before: days_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = "/Insurance";
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

function validateEditInsurance() {
    var id = document.getElementById("edit_id").value;
    var startDate = document.getElementById("edit_startDate").value;
    var expiryDate = document.getElementById("edit_expiryDate").value;
    if (!startDate || !expiryDate) {
        alert("start date and expiry date must not be empty");
        return;
    }
    if (startDate >= expiryDate) {
        alert("Expiry date must be greater than start date.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Insurance/Edit",
        data: { id: id, startDate: startDate, expiryDate: expiryDate },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Insurance successfull");
                window.location.href = "/Insurance";
            }
            else {
                alert("Edit Insurance failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function editInsurance(id, account, fullname, startDate, expiryDate) {
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_startDate").value = startDate;
    document.getElementById("edit_expiryDate").value = expiryDate;
}

function validateCreateOrEditInsurance() {
    var student_id = document.getElementById("edit_student_id").value;
    var insurance_id = document.getElementById("edit_insurance_id").value;   
    var start_date = document.getElementById("edit_start_date").value;
    var expiry_date = document.getElementById("edit_expiry_date").value;
    var picture = document.getElementById("edit_picture").files[0];
    var fileName = document.getElementById("edit_picture").value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (!allowedExtensions.exec(fileName)) {
        alert('Only jpg/jpeg and png files are allowed!');
        picture.value = '';
        return;
    }
    if (!start_date) {
        alert("Start date must not be empty.");
        return;
    }
    if (!expiry_date) {
        alert("Expired date must not be empty.");
        return;
    }
    if (start_date >= expiry_date) {
        alert("Expiry date must be greater than Start date.");
        return;
    }
    var fdata = new FormData();
    fdata.append("student_id", student_id);
    fdata.append("insurance_id", insurance_id);
    fdata.append("start_date", start_date);
    fdata.append("expiry_date", expiry_date);
    fdata.append("picture", picture);
    $.ajax({
        type: "post",
        url: "/Insurance/CreateOrEdit",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Edit successful");
            window.location.href = "/Insurance";
        },
        error: function () {
            alert("Edit failed");
        }
    });
}