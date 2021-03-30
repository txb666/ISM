﻿function validateCreateCurrentAccomodation() {
    var student_id;
    var student_group_id;
    if (document.getElementById("create_student_id") !== null) {
        student_id = document.getElementById("create_student_id").value;
    }
    if (document.getElementById("create_student_group_id")) {
        student_group_id = document.getElementById("create_student_group_id").value;
    }
    var type = document.getElementById("create_type").value;
    var location = document.getElementById("create_location").value;
    var description = document.getElementById("create_description").value;
    var fee = document.getElementById("create_fee").value;
    var note = document.getElementById("create_note").value;
    var picture = document.getElementById("create_picture").files[0];
    if (/^[A-Za-z0-9\s]+$/.test(type) == false || /^\s*$/.test(type) == true) {
        alert("type must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(location) == false || /^\s*$/.test(location) == true) {
        alert("location must not be empty or contain special character");
        return;
    }
    var fdata = new FormData();
    fdata.append("student_id", student_id);
    fdata.append("student_group_id", student_group_id);
    fdata.append("type", type);
    fdata.append("location", location);
    fdata.append("description", description);
    fdata.append("fee", fee);
    fdata.append("note", note);
    fdata.append("picture", picture);
    $.ajax({
        type: "POST",
        url: "/CurrentAccomodation/isCurrentAccomodationExist",
        data: { student_id: student_id, student_group_id: student_group_id },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Information about current accomodation of this student/student group already exist");
                return;
            }
            else {
                $.ajax({
                    type: "post",
                    url: "/CurrentAccomodation/Create",
                    contentType: false,
                    processData: false,
                    data: fdata,
                    success: function (message) {
                        alert(message);
                        window.location.href = '/CurrentAccomodation';
                    },
                    error: function (message) {
                        alert(message);
                    }
                });
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    }); 
   
}

function editCurrentAccomodation(current_accomodation_id, student_id, student_group_id, type, location, description, fee, note, pictureName) {
    document.getElementById('edit_current_accomodation_id').value = current_accomodation_id;
    document.getElementById('edit_pictureName').value = pictureName;
    if (document.getElementById("edit_student_id") !== null) {
        document.getElementById('edit_student_id').value = student_id;
    }
    if (document.getElementById("edit_student_group_id") !== null) {
        document.getElementById('edit_student_group_id').value = student_group_id;
    }
    document.getElementById('edit_type').value = type;
    document.getElementById('edit_location').value = location;
    document.getElementById('edit_description').value = description;
    document.getElementById('edit_fee').value = fee;
    document.getElementById('edit_note').value = note;
}

function validateEditCurrentAccomodation() {
    var student_id;
    var student_group_id;
    if (document.getElementById("edit_student_id") !== null) {
        student_id = document.getElementById("edit_student_id").value;
    }
    if (document.getElementById("edit_student_group_id")) {
        student_group_id = document.getElementById("edit_student_group_id").value;
    }
    var current_accomodation_id = document.getElementById("edit_current_accomodation_id").value;
    var type = document.getElementById("edit_type").value;
    var location = document.getElementById("edit_location").value;
    var description = document.getElementById("edit_description").value;
    var fee = document.getElementById("edit_fee").value;
    var note = document.getElementById("edit_note").value;
    var pictureName = document.getElementById("edit_pictureName").value;
    var picture = document.getElementById("edit_picture").files[0];
    if (/^[A-Za-z0-9\s]+$/.test(type) == false || /^\s*$/.test(type) == true) {
        alert("type must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(location) == false || /^\s*$/.test(location) == true) {
        alert("location must not be empty or contain special character");
        return;
    }
    var fdata = new FormData();
    fdata.append("student_id", student_id);
    fdata.append("student_group_id", student_group_id);
    fdata.append("current_accomodation_id", current_accomodation_id);
    fdata.append("type", type);
    fdata.append("location", location);
    fdata.append("description", description);
    fdata.append("fee", fee);
    fdata.append("note", note);
    fdata.append("picture", picture);
    fdata.append("pictureName", pictureName);
    $.ajax({
        type: "post",
        url: "/CurrentAccomodation/Edit",
        contentType: false,
        processData: false,
        data: fdata,
        success: function () {
            alert("Edit successful");
            window.location.href = '/CurrentAccomodation';
        },
        error: function () {
            alert("Edit failed");
        }
    });
}