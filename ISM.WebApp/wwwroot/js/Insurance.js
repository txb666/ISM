function validateNotificationInsuranceDegree() {
    var deadline = document.getElementById("degree_deadline_id").value;
    var days_before = document.getElementById("degree_daysBefore_id").value;
    var searchButton = document.getElementById("searchButton");
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
                searchButton.click();
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
    var searchButton = document.getElementById("searchButton");
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
                searchButton.click();
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

function editInsurance(flight_id, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d, picture_d) {
    document.getElementById("edit_id").value = flight_id;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_startDate").value = startDate;
    document.getElementById("edit_expiryDate").value = expiryDate;
}