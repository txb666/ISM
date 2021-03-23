

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
    if (/^([A-Za-z0-9\s]+)$/.test(passport_number) == false && passport_number.length != 0) {
        alert("Passport number must not be empty or contain special character");
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
                alert("Edit Passport failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function editVisaLetter(id, visa_type, visa_period, apply_receive) {
    document.getElementById("edit_VisaType").value = visa_type;
    document.getElementById("edit_VisaPeriod").value = visa_period;
    document.getElementById("edit_ApplyReceive").value = apply_receive;
    document.getElementById("edit_VisaLetter_id").value = id;
}

function validateEditVisaLetter() {
    var id = document.getElementById("edit_VisaLetter_id").value;
    var visa_type = document.getElementById("edit_VisaType").value;
    var visa_period = document.getElementById("edit_VisaPeriod").value;
    var apply_receive = document.getElementById("edit_ApplyReceive").value;
    var searchButton = document.getElementById("searchBtn");
    if (visa_period.length == 0 || apply_receive.length == 0) {
        alert("Visa period or Apply and receive must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/VisaLetter/edit",
        data: { id: id, visa_type: visa_type, visa_period: visa_period, apply_receive: apply_receive },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit Visa Letter successfull");
                searchButton.click();
            }
            else {
                alert("Edit Visa Letter failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}