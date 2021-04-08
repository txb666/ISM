function validateCreateOrEditStaff(user_id) {
    var telephone = document.getElementById('contact_infor_telephone_staff').value;
    var picture = document.getElementById('contact_infor_picture_staff').value;
    var position = document.getElementById('contact_infor_position_staff').value;
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            alert("Telephone not in right format");
            return;
        }
        if (!picture) {
            alert("Picture must not be empty");
            return;
        }
        if (!position) {
            alert("Position must not be empty");
            return;
        }
        $.ajax({
            type: "POST",
            url: "/ContactInformation/CreateOrEdit",
            data: { user_id: user_id, telephone: telephone, position: position, picture: picture },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = "/ContactInformation";
                }
                else {
                    alert("Failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        return;
    }
}

function validateCreateOrEditAdmin(user_id) {
    var telephone = document.getElementById('contact_infor_phone').value;
    var picture = document.getElementById('contact_infor_picture').value;
    var position = document.getElementById('contact_infor_position').value;
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            alert("Telephone not in right format");
            return;
        }
        if (!picture) {
            alert("Picture must not be empty");
            return;
        }
        if (!position) {
            alert("Position must not be empty");
            return;
        }
        $.ajax({
            type: "POST",
            url: "/ContactInformation/CreateOrEdit",
            data: { user_id: user_id, telephone: telephone, position: position, picture: picture },
            dataType: "text",
            success: function (msg) {
                if (msg == "true") {
                    alert("Successfull");
                    window.location.href = "/ContactInformation";
                }
                else {
                    alert("Failed")
                }
            },
            error: function (req, status, error) {
                alert(error);
            }
        });
    }
    else {
        return;
    }
}