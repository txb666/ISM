function validateEditFlight() {
    var check = document.getElementById("degreeOrMobility_id_check").value;
    var searchBtn = document.getElementById("searchButton");
    if (check == "Mobility") {
        var id = document.getElementById("edit_id").value;
        var flight_number_a = document.getElementById("edit_flight_number_a").value;
        var flight_number_d = document.getElementById("edit_flight_number_d").value;
        var arrival_date_a = document.getElementById("edit_arrival_date_a").value;
        var arrival_date_d = document.getElementById("edit_arrival_date_d").value;
        var arrival_time_a = document.getElementById("edit_arrival_time_a").value;
        var arrival_time_d = document.getElementById("edit_arrival_time_d").value;
        var airport_departure_a = document.getElementById("edit_airport_departure_a").value;
        var airport_departure_d = document.getElementById("edit_airport_departure_d").value;
        var airport_arrival_a = document.getElementById("edit_airport_arrival_a").value;
        var airport_arrival_d = document.getElementById("edit_airport_arrival_d").value;
        var picture_a = document.getElementById("edit_picture_a").value;
        var picture_d = document.getElementById("edit_picture_d").value;
        if (!arrival_date_a) {
            alert("Date must not be empty.");
            return;
        }
        if (!arrival_time_a) {
            alert("Time must not be empty.");
            return;
        }
        if (!arrival_date_d) {
            alert("Date must not be empty.");
            return;
        }
        if (!arrival_time_d) {
            alert("Time must not be empty.");
            return;
        }
        if (!flight_number_a) {
            alert("Flight number must not be empty.");
            return;
        }
        if (!flight_number_d) {
            alert("Flight Number must not be empty.");
            return;
        }
        if (!airport_departure_a) {
            alert("Airport must not be empty.");
            return;
        }
        if (!airport_departure_d) {
            alert("Airport must not be empty.");
            return;
        }
        if (!airport_arrival_a) {
            alert("Airport must not be empty.");
            return;
        }
        if (!airport_arrival_d) {
            alert("Airport must not be empty.");
            return;
        }
        if (!picture_a) {
            alert("Picture must not be empty.");
            return;
        }
        if (!picture_d) {
            alert("Picture must not be empty.");
            return;
        }
        $.ajax({
            type: "POST",
            url: "/Flight/Edit",
            data: { degreeOrMobility: check, flight_id: id, flight_number_a: flight_number_a, arrival_date_a: arrival_date_a, arrival_time_a: arrival_time_a, airport_departure_a: airport_departure_a, airport_arrival_a: airport_arrival_a, picture_a: picture_a, flight_number_d: flight_number_d, arrival_date_d: arrival_date_d, arrival_time_d: arrival_time_d, airport_departure_d: airport_departure_d, airport_arrival_d: airport_arrival_d, picture_d: picture_d },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Edit Flight successfull");
                    searchBtn.click();
                }
                else {
                    alert("Edit Flight failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        var id = document.getElementById("edit_id").value;
        var flight_number_a = document.getElementById("edit_flight_number_a").value;
        var arrival_date_a = document.getElementById("edit_arrival_date_a").value;
        var arrival_time_a = document.getElementById("edit_arrival_time_a").value;
        var airport_departure_a = document.getElementById("edit_airport_departure_a").value;
        var airport_arrival_a = document.getElementById("edit_airport_arrival_a").value;
        var picture_a = document.getElementById("edit_picture_a").value;
        if (!arrival_date_a) {
            alert("Date must not be empty.");
            return;
        }
        if (!arrival_time_a) {
            alert("Time must not be empty.");
            return;
        }
        if (!flight_number_a) {
            alert("Flight number must not be empty.");
            return;
        }
        if (!airport_departure_a) {
            alert("Airport must not be empty.");
            return;
        }
        if (!airport_arrival_a) {
            alert("Airport must not be empty.");
            return;
        }
        if (!picture_a) {
            alert("Picture must not be empty.");
            return;
        }
        $.ajax({
            type: "POST",
            url: "/Flight/Edit",
            data: { degreeOrMobility: check, flight_id: id, flight_number_a: flight_number_a, arrival_date_a: arrival_date_a, arrival_time_a: arrival_time_a, airport_departure_a: airport_departure_a, airport_arrival_a: airport_arrival_a, picture_a: picture_a },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Edit Flight successfull");
                    searchBtn.click();
                }
                else {
                    alert("Edit Flight failed");
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
}

function editFlightDegree(degreeOrMobility, id, account, fullname, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a) {
    document.getElementById("degreeOrMobility_id_check").value = degreeOrMobility;
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_flight_number_a").value = flight_number_a;
    document.getElementById("edit_arrival_date_a").value = arrival_date_a;
    document.getElementById("edit_arrival_time_a").value = arrival_time_a;
    document.getElementById("edit_airport_departure_a").value = airport_departure_a;
    document.getElementById("edit_airport_arrival_a").value = airport_arrival_a;
    document.getElementById("edit_picture_a").value = picture_a;
}

function editFlightMobility(degreeOrMobility, id, account, fullname, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d, picture_d) {
    document.getElementById("degreeOrMobility_id_check").value = degreeOrMobility;
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_flight_number_a").value = flight_number_a;
    document.getElementById("edit_flight_number_d").value = flight_number_d;
    document.getElementById("edit_arrival_date_a").value = arrival_date_a;
    document.getElementById("edit_arrival_date_d").value = arrival_date_d;
    document.getElementById("edit_arrival_time_a").value = arrival_time_a;
    document.getElementById("edit_arrival_time_d").value = arrival_time_d;
    document.getElementById("edit_airport_departure_a").value = airport_departure_a;
    document.getElementById("edit_airport_departure_d").value = airport_departure_d;
    document.getElementById("edit_airport_arrival_a").value = airport_arrival_a;
    document.getElementById("edit_airport_arrival_d").value = airport_arrival_d;
    document.getElementById("edit_picture_a").value = picture_a;
    document.getElementById("edit_picture_d").value = picture_d;
}

function validateNotificationFlightDegree() {
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
        url: "/Flight/NotificationDegree",
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

function validateNotificationFlightMobility() {
    var days_before = document.getElementById("mobility_days_before_id").value;
    if (/^[1-9]\d*$/.test(days_before) == false && days_before.length != 0) {
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Flight/NotificationMobility",
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