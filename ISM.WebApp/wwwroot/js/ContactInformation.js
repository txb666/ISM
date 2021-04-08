function validateCreateOrEditStaff(user_id) {
    var telephone = document.getElementById('contact_infor_telephone_staff').value;
    var position = document.getElementById('contact_infor_position_staff').value;
    var picture = document.getElementById("contact_information_picture_staff").files[0];
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            alert("Telephone not in right format");
            return;
        }
        if (!position) {
            alert("Position must not be empty");
            return;
        }
        var fdata = new FormData();
        fdata.append("user_id", user_id);
        fdata.append("telephone", telephone);
        fdata.append("position", position);
        fdata.append("picture", picture);
        $.ajax({
            type: "POST",
            url: "/ContactInformation/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function (message) {
                alert(message);
                window.location.href = '/ContactInformation';
            },
            error: function (message) {
                alert(message);
            }
        });
    }
    else {
        return;
    }
}

function validateCreateOrEditAdmin(user_id) {
    var telephone = document.getElementById('contact_infor_phone').value;
    var position = document.getElementById('contact_infor_position').value;
    var picture = document.getElementById("contact_information_picture_admin").files[0];
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            alert("Telephone not in right format");
            return;
        }
        if (!position) {
            alert("Position must not be empty");
            return;
        }
        var fdata = new FormData();
        fdata.append("user_id", user_id);
        fdata.append("telephone", telephone);
        fdata.append("position", position);
        fdata.append("picture", picture);
        $.ajax({
            type: "POST",
            url: "/ContactInformation/CreateOrEdit",
            contentType: false,
            processData: false,
            data: fdata,
            success: function (message) {
                alert(message);
                window.location.href = '/ContactInformation';
            },
            error: function (message) {
                alert(message);
            }
        });
    }
    else {
        return;
    }
}