﻿function editVisa(id, start_date, expired_date, entry_date, entry_port) {
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
        url: "/Visa/CreateOrEdit",
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