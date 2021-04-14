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
        $.ajax({
            type: "POST",
            url: "/Flight/Edit",
            data: { degreeOrMobility: check, flight_id: id, flight_number_a: flight_number_a, arrival_date_a: arrival_date_a, arrival_time_a: arrival_time_a, airport_departure_a: airport_departure_a, airport_arrival_a: airport_arrival_a, flight_number_d: flight_number_d, arrival_date_d: arrival_date_d, arrival_time_d: arrival_time_d, airport_departure_d: airport_departure_d, airport_arrival_d: airport_arrival_d },
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
        $.ajax({
            type: "POST",
            url: "/Flight/Edit",
            data: { degreeOrMobility: check, flight_id: id, flight_number_a: flight_number_a, arrival_date_a: arrival_date_a, arrival_time_a: arrival_time_a, airport_departure_a: airport_departure_a, airport_arrival_a: airport_arrival_a },
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
                window.location.href = "/Flight";
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
                window.location.href = "/Flight";
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

function validateCreateOrEditFlight() {
    var check = document.getElementById("degreeOrMobility_check").value;
    if (check == "Mobility") {
        var student_id = document.getElementById("edit_student_id").value;
        var flight_id = document.getElementById("edit_flight_id").value;
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
        var picture_a = document.getElementById("edit_picture_a").files[0];
        var fileName = document.getElementById("edit_picture_a").value;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
        var picture_d = document.getElementById("edit_picture_d").files[0];
        var fileName_d = document.getElementById("edit_picture_d").value;
        var idxDot_d = fileName_d.lastIndexOf(".") + 1;
        var extFile_d = fileName.substr(idxDot_d, fileName_d.length).toLowerCase();
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
        if (!extFile == "jpg" || !extFile == "jpeg" || !extFile == "png") {
            alert("Only jpg/jpeg and png files are allowed!.");
            return;
        }
        if (!extFile_d == "jpg" || !extFile_d == "jpeg" || !extFile_d == "png") {
            alert("Only jpg/jpeg and png files are allowed!");
            return;
        }
        var fdata = new FormData();
        fdata.append("student_id", student_id);
        fdata.append("flight_id", flight_id);
        fdata.append("flight_number_a", flight_number_a);
        fdata.append("flight_number_d", flight_number_d);
        fdata.append("arrival_date_a", arrival_date_a);
        fdata.append("arrival_date_d", arrival_date_d);
        fdata.append("arrival_time_a", arrival_time_a);
        fdata.append("arrival_time_d", arrival_time_d);
        fdata.append("airport_departure_a", airport_departure_a);
        fdata.append("airport_departure_d", airport_departure_d);
        fdata.append("airport_arrival_a", airport_arrival_a);
        fdata.append("airport_arrival_d", airport_arrival_d);
        fdata.append("picture_a", picture_a);
        fdata.append("picture_d", picture_d);
        $.ajax({
            type: "post",
            url: "/Flight/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                window.location.href = "/Flight";
            },
            error: function () {
                alert("Edit failed");
            }
        });
    }
    else {
        var student_id = document.getElementById("edit_student_id").value;
        var flight_id = document.getElementById("edit_flight_id").value;
        var flight_number_a = document.getElementById("edit_flight_number_a").value;
        var arrival_date_a = document.getElementById("edit_arrival_date_a").value;
        var arrival_time_a = document.getElementById("edit_arrival_time_a").value;
        var airport_departure_a = document.getElementById("edit_airport_departure_a").value;
        var airport_arrival_a = document.getElementById("edit_airport_arrival_a").value;
        var picture_a = document.getElementById("edit_picture_a").files[0];
        var fileName = document.getElementById("edit_picture_a").value;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
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
        if (!extFile == "jpg" || !extFile == "jpeg" || !extFile == "png") {
            alert("Only jpg/jpeg and png files are allowed!");
            return;
        }
        var fdata = new FormData();
        fdata.append("student_id", student_id);
        fdata.append("flight_id", flight_id);
        fdata.append("flight_number_a", flight_number_a);
        fdata.append("arrival_date_a", arrival_date_a);
        fdata.append("arrival_time_a", arrival_time_a);
        fdata.append("airport_departure_a", airport_departure_a);
        fdata.append("airport_arrival_a", airport_arrival_a);
        fdata.append("picture_a", picture_a);
        $.ajax({
            type: "post",
            url: "/Flight/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function () {
                alert("Edit successful");
                window.location.href = "/Flight";
            },
            error: function () {
                alert("Edit failed");
            }
        });
    }
}