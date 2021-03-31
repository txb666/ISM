
function validateEditFlight() {
    var id = document.getElementById("edit_id").value;
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
    $.ajax({
        type: "POST",
        url: "/Flight/Edit",
        data: { id: id, flight_number_a: flight_number_a, arrival_date_a: arrival_date_a, arrival_time_a: arrival_time_a, airport_departure_a: airport_departure_a, airport_arrival_a: airport_arrival_a, picture_a: picture_a, flight_number_d: flight_number_d, arrival_date_d: arrival_date_d, arrival_time_d: arrival_time_d, airport_departure_d: airport_departure_d, airport_arrival_d: airport_arrival_d, picture_d: picture_d},
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Flight successfull");
                window.location.href = "/Flight";
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

function editFlight(id, account, fullname, flight_number_a, arrival_date_a, arrival_time_a, airport_departure_a, airport_arrival_a, picture_a, flight_number_d, arrival_date_d, arrival_time_d, airport_departure_d, airport_arrival_d, picture_d) {
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