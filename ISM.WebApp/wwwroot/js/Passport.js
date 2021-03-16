

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