function validateSaveInformation(user_id) {
    var fullname = document.getElementById("profile_fullname_id").value;
    var nationality = document.getElementById("profile_nationality_id").value;
    var dob = document.getElementById("profile_dob_id").value;
    var gender = document.getElementById("profile_gender_id").value;
    var contact = document.getElementById("profile_contact_id").value;
    var picture = document.getElementById("profile_picture_id").files[0];
    var fileName = document.getElementById("profile_picture_id").value;
    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    var dob_check = new Date(document.getElementById("profile_dob_id").value);
    var current = new Date();
    var check = confirm("Do you want to save?");
    if (check) {
        if (!fullname || !nationality || !dob || !gender || !contact) {
            alert("Please fill out all information fields.");
            return;
        }
        if (dob_check.getTime() > current.getTime()) {
            alert("Date of Birth must not be greater than current date.");
            return;
        }
        if (dob_check.getFullYear() < 1940) {
            alert("Year of birth date must be greater than 1940.");
            return;
        }
        if (!fileName) {
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("fullname", fullname);
            fdata.append("nationality", nationality);
            fdata.append("dob", dob);
            fdata.append("gender", gender);
            fdata.append("contact", contact);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/Information/SaveProfile",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/Information';
                },
                error: function () {
                    alert("Failed");
                }
            });
        }
        else {
            if (!allowedExtensions.exec(fileName)) {
                alert('Only jpg/jpeg and png files are allowed!');
                picture.value = '';
                return;
            }
            var fdata = new FormData();
            fdata.append("user_id", user_id);
            fdata.append("fullname", fullname);
            fdata.append("nationality", nationality);
            fdata.append("dob", dob);
            fdata.append("gender", gender);
            fdata.append("contact", contact);
            fdata.append("picture", picture);
            $.ajax({
                type: "post",
                url: "/Information/SaveProfile",
                contentType: false,
                processData: false,
                data: fdata,
                success: function () {
                    alert("Successful");
                    window.location.href = '/Information';
                },
                error: function () {
                    alert("Failed");
                }
            });
        }
    }
    else {
        return;
    }
}

function validateChangePassword(user_id) {
    var current_password = document.getElementById("edit_current_password").value;
    var new_password = document.getElementById("edit_new_password").value;
    var confirm_password = document.getElementById("edit_confirm_new_password").value;
    if (!current_password || !new_password || !confirm_password) {
        alert("Please fill out all fields.");
        return;
    }
    if (/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(new_password) == false) {
        alert("Password must be minimum eight characters, at least one letter and one number.");
        return;
    }
    if (new_password != confirm_password) {
        alert("Please re-enter confirm new password. Confirm new password must be equal to new password.");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Information/CheckPasswordisMatch",
        data: { current_password: current_password },
        dataType: "text",
        success: function (msg) {
            if (msg == "false") {
                alert("Current password not match.");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Information/UpdatePassword",
                    data: { user_id: user_id, new_password: new_password },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Change password successfull");
                            window.location.href = "/Information";
                        }
                        else {
                            alert("Change password failed");
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}