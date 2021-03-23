﻿function validateCreateStudent(degreeOrMobility) {
    var fullname = document.getElementById("create_fullname").value;
    var account = document.getElementById("create_account").value;
    var email = document.getElementById("create_email").value;
    var student_group_id = document.getElementById("create_student_group").value;
    var status = document.getElementById("create_status").value;
    if (/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email) == false) {
        alert("email not in right format");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(fullname) == false || /^\s*$/.test(fullname) == true) {
        alert("fullname must not be empty or contain special character");
        return;
    }
    if (/^[A-Za-z0-9\s]+$/.test(account) == false || /^\s*$/.test(account) == true) {
        alert("account must not be empty or contain special character");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Student/IsStudentExist",
        data: { account: account, email: email },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
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