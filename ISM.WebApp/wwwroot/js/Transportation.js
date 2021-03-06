function validateNotificationTransportation() {
    var hours_before = document.getElementById("notification_input").value;
    if (!hours_before) {
        enableButton('save');
        alert("Hours before must not be empty.");
        return;
    }
    if (/^[1-9]\d*$/.test(hours_before) == false) {
        enableButton('save');
        alert("Please input only positive number.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Transportation/SetupNotification",
        data: { hours_before: hours_before },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Successfull");
                window.location.href = "/Transportation";
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

function validateCreateTransportation() {
    var student_group_id = document.getElementById("create_student_group_id").value;
    var date = document.getElementById("create_date").value;
    var time = document.getElementById("create_time").value;
    var bus = document.getElementById("create_bus").value;
    var driver = document.getElementById("create_driver").value;
    var itinerary = document.getElementById("create_itinerary").value;
    var supporter = document.getElementById("create_supporter").value;
    var note = document.getElementById("create_note").value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById("create_date").value);
    if (!date || !time) {
        enableButton('save_create');
        alert("date and time must not be empty");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_create');
        alert("Date must be greater than current date");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(bus) == false || /^\s*$/.test(bus) == true) {
        enableButton('save_create');
        alert("Bus must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(driver) == false || /^\s*$/.test(driver) == true) {
        enableButton('save_create');
        alert("Driver must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(itinerary) == false || /^\s*$/.test(itinerary) == true) {
        enableButton('save_create');
        alert("Itinerary must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(supporter) == false || /^\s*$/.test(supporter) == true) {
        enableButton('save_create');
        alert("Supporter must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Transportation/Create",
        data: { student_group_id: student_group_id, date: date, time: time, bus: bus, driver: driver, itinerary: itinerary, supporter: supporter, note: note },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Create Transportation successfull");
                document.getElementById("xmas-popup").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_create');
                alert("Create Transportation failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function EditTransportation(transportation_id, date, time, bus, driver, itinerary, supporter, note) {
    document.getElementById("edit_transportation_id").value = transportation_id;
    document.getElementById("edit_date").value = date;
    document.getElementById("edit_time").value = time;
    document.getElementById("edit_bus").value = bus;
    document.getElementById("edit_driver").value = driver;
    document.getElementById("edit_itinerary").value = itinerary;
    document.getElementById("edit_supporter").value = supporter;
    document.getElementById("edit_note").value = note;
}

function validateEditTransportation() {
    var transportation_id = document.getElementById("edit_transportation_id").value;
    var date = document.getElementById("edit_date").value;
    var time = document.getElementById("edit_time").value;
    var bus = document.getElementById("edit_bus").value;
    var driver = document.getElementById("edit_driver").value;
    var itinerary = document.getElementById("edit_itinerary").value;
    var supporter = document.getElementById("edit_supporter").value;
    var note = document.getElementById("edit_note").value;
    var current_date = new Date();
    var check_date = new Date(document.getElementById("edit_date").value);
    if (!date || !time) {
        enableButton('save_edit');
        alert("date and time must not be empty");
        return;
    }
    if (check_date.getTime() <= current_date.getTime()) {
        enableButton('save_edit');
        alert("Date must be greater than current date");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(bus) == false || /^\s*$/.test(bus) == true) {
        enableButton('save_edit');
        alert("Bus must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(driver) == false || /^\s*$/.test(driver) == true) {
        enableButton('save_edit');
        alert("Driver must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(itinerary) == false || /^\s*$/.test(itinerary) == true) {
        enableButton('save_edit');
        alert("Itinerary must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(supporter) == false || /^\s*$/.test(supporter) == true) {
        enableButton('save_edit');
        alert("Supporter must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Transportation/Edit",
        data: { transportation_id: transportation_id, date: date, time: time, bus: bus, driver: driver, itinerary: itinerary, supporter: supporter, note: note },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Transportation successfull");
                document.getElementById("xmas-popup-assign").style.display = "none";
                location.reload();
            }
            else {
                enableButton('save_edit');
                alert("Edit Transportation failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function deleteTransportation(transportations_id) {
    if (confirm("Are you sure to delete?")) {
        $.ajax({
            type: "POST",
            url: "/Transportation/Delete",
            data: { transportations_id: transportations_id },
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
}