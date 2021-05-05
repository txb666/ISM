/* When the user clicks on the button, 
     toggle between hiding and showing the dropdown content */

function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}


// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn_search')) {
        var dropdowns = document.getElementsByClassName("dropdown-content_search");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show_search')) {
                openDropdown.classList.remove('show_search');
            }
        }
    }
}

function validateCreateStaff() {
    var fullname = document.getElementById("create_fullname").value;
    var email = document.getElementById("create_email").value;
    var account = document.getElementById("create_account").value;
    var startDate = document.getElementById("create_startDate").value;
    var endDate = document.getElementById("create_endDate").value;
    var status = document.getElementById("create_status").value;
    if (/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/.test(email) == false) {
        enableButton('save_create');
        alert("Email not in right format.");
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
    if (account.length <= 3) {
        enableButton('save_create');
        alert("Length of account must be greater than 3 characters");
        return;
    }
    if (startDate || endDate) {
        if (startDate >= endDate) {
            enableButton('save_create');
            alert("End date must be greater than start date.");
            return;
        }
    }
    $.ajax({
        type: "POST",
        url: "/Staff/IsStaffAlreadyExist",
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
                    url: "/Staff/Create",
                    data: { fullname: fullname, email: email, account: account, startDate: startDate, endDate: endDate, status: status },
                    dataType: "text",
                    success: function (msg) {
                        if (msg > 0) {
                            $.ajax({
                                type: "POST",
                                url: "/Staff/CreateAccountNotification",
                                data: { account: account, email: email },
                                dataType: "text",
                                success: function (msg) {
                                    if (msg == "true") {
                                        alert("Create Staff successfull");
                                        location.reload();
                                    }
                                    else {
                                        enableButton('save_create');
                                        alert("Create Staff failed")
                                    }
                                },
                                error: function (req, status, error) {
                                    alert(error);
                                }
                            });
                        }
                        else {
                            enableButton('save_create');
                            alert("Create Staff failed")
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

function editStaff(id, fullname, email, account, startDate, endDate, status) {
    document.getElementById("edit_fullname").value = fullname;
    document.getElementById("edit_email").value = email;
    document.getElementById("edit_account").value = account;
    document.getElementById("edit_startDate").value = startDate;
    document.getElementById("edit_endDate").value = endDate;
    document.getElementById("edit_status").value = status;
    document.getElementById("edit_id").value = id;
    document.getElementById("edit_originalEmail").value = email;
}

function validateEditStaff() {
    var id = document.getElementById("edit_id").value;
    var originalEmail = document.getElementById("edit_originalEmail").value;
    var fullname = document.getElementById("edit_fullname").value;
    var email = document.getElementById("edit_email").value;
    var startDate = document.getElementById("edit_startDate").value;
    var endDate = document.getElementById("edit_endDate").value;
    var status = document.getElementById("edit_status").value;
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
    if (startDate || endDate) {
        if (startDate >= endDate) {
            enableButton('save_edit');
            alert("End date must be greater than start date.");
            return;
        }
    }
    $.ajax({
        type: "POST",
        url: "/Staff/isEmailExist",
        data: { email: email },
        dataType: "text",
        success: function (msg) {
            if (msg == "true" && email != originalEmail) {
                alert("Email already exist");
                return;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Staff/Edit",
                    data: { id: id, fullname: fullname, email: email, startDate: startDate, endDate: endDate, status: status, originalEmail: originalEmail },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            alert("Edit Staff successfull");
                            location.reload();
                        }
                        else {
                            enableButton('save_edit');
                            alert("Edit Staff failed")
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

function myFunction1() {
    document.getElementById("myDropdown_search").classList.toggle("show_search_staff");
}

function filterFunction() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    div = document.getElementById("myDropdown_search");
    a = div.getElementsByTagName("a");
    for (i = 0; i < a.length; i++) {
        txtValue = a[i].textContent || a[i].innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            a[i].style.display = "";
        } else {
            a[i].style.display = "none";
        }
    }
}