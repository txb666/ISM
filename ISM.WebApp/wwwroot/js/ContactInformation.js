function validateCreateOrEditStaff(user_id) {
    var telephone = document.getElementById('contact_infor_telephone_staff').value;
    var position = document.getElementById('contact_infor_position_staff').value;
    var picture = document.getElementById("contact_information_picture_staff").files[0];
    var fileName = document.getElementById("contact_information_picture_staff").value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            enableButton('save_edit');
            alert("Telephone not in right format");
            return;
        }
        if (!position) {
            enableButton('save_edit');
            alert("Position must not be empty");
            return;
        }
        if (!fileName) {
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("telephone", telephone);
            fdata.append("position", position);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/ContactInformation/CreateOrEdit",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/ContactInformation';
                },
                error: function () {
                    enableButton('save_edit');
                    alert("Failed");
                }
            });
        }
        else {
            if (!allowedExtensions.exec(fileName)) {
                enableButton('save_edit');
                alert('Only jpg/jpeg and png files are allowed!');
                picture.value = '';
                return;
            }
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("telephone", telephone);
            fdata.append("position", position);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/ContactInformation/CreateOrEdit",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/ContactInformation';
                },
                error: function () {
                    enableButton('save_edit');
                    alert("Failed");
                }
            });
        }
    }
    else {
        return;
    }
}

function validateCreateOrEditAdmin(user_id) {
    var telephone = document.getElementById('contact_infor_phone').value;
    var position = document.getElementById('contact_infor_position').value;
    var picture = document.getElementById("contact_information_picture_admin").files[0];
    var fileName = document.getElementById("contact_information_picture_admin").value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    var check = confirm("Do you want to save?");
    if (check) {
        if (/^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/.test(telephone) == false) {
            enableButton('save_create');
            alert("Telephone not in right format");
            return;
        }
        if (!position) {
            enableButton('save_create');
            alert("Position must not be empty");
            return;
        }
        if (!fileName) {
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("telephone", telephone);
            fdata.append("position", position);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/ContactInformation/CreateOrEdit",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/ContactInformation';
                },
                error: function () {
                    enableButton('save_create');
                    alert("Failed");
                }
            });
        }
        else {
            if (!allowedExtensions.exec(fileName)) {
                enableButton('save_create');
                alert('Only jpg/jpeg and png files are allowed!');
                picture.value = '';
                return;
            }
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("telephone", telephone);
            fdata.append("position", position);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/ContactInformation/CreateOrEdit",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/ContactInformation';
                },
                error: function () {
                    enableButton('save_create');
                    alert("Failed");
                }
            });
        }
    }
    else {
        return;
    }
}