function validateCreateStudent(degreeOrMobility) {
    var fullname = document.getElementById("create_fullname").value;
    var account = document.getElementById("create_account").value;
    var email = document.getElementById("create_email").value;
    var student_group_id = document.getElementById("create_student_group").value;
    var status = document.getElementById("create_status").value;
    if (/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email) == false) {
        enableButton('save_create');
        alert("Email not in right format");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(fullname) == false || /^\s*$/.test(fullname) == true) {
        enableButton('save_create');
        alert("Fullname must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9]+$/.test(account) == false || /^\s*$/.test(account) == true) {
        enableButton('save_create');
        alert("Account must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Student/IsStudentExist",
        data: { account: account, email: email },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                enableButton('save_create');
                alert("Account or email already exist");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Student/Create",
                    data: { degreeOrMobility: degreeOrMobility, fullname: fullname, email: email, account: account, student_group_id: student_group_id, status: status },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Create Student successfull");
                            window.location.href = "/Student";
                        }
                        else {
                            enableButton('save_create');
                            alert("Create Student failed")
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

function editStudent(id, fullname, email, account, student_group_id, status) {
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_email").value = email;
    document.getElementById("edit_originalEmail").value = email;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_student_group").value = student_group_id;
    document.getElementById("edit_status_student").value = status;
}

function validateEditStudent() {
    var id = document.getElementById("edit_id").value;
    var originalEmail = document.getElementById("edit_originalEmail").value;
    var fullname = document.getElementById("edit_fullname").value;
    var email = document.getElementById("edit_email").value;
    var status = document.getElementById("edit_status_student").value;
    if (/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email) == false) {
        enableButton('save_edit');
        alert("Email empty or not in right format");
        return;
    }
    if (/^([A-Za-z0-9\s]+)$/.test(fullname) == false || /^\s*$/.test(fullname) == true) {
        enableButton('save_edit');
        alert("Fullname must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Student/isEmailExist",
        data: { email: email },
        dataType: "text",
        success: function (msg) {
            if (msg == "true" && email != originalEmail) {
                enableButton('save_edit');
                alert("Email already exist");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Student/Edit",
                    data: { id: id, fullname: fullname, email: email, status: status, originalEmail: originalEmail },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Edit Student successfull");
                            searchButton.click();
                        }
                        else {
                            enableButton('save_edit');
                            alert("Edit Student failed")
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