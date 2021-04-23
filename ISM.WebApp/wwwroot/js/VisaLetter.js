function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

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
function myFunction1() {
    document.getElementById("myDropdown_search").classList.toggle("show_search_passport");
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
    if (visa_period.trim().length == 0) {
        enableButton('save');
        alert("Visa period must not be empty");
        return;
    }
    if (apply_receive.trim().length == 0) {
        enableButton('save');
        alert("Apply and receive must not be empty");
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

function validateCreateOrEditVisaLetter() {
    var visa_letter_id = document.getElementById("edit_visa_letter_id").value;
    var student_id = document.getElementById("edit_student_id").value;
    var visa_type = document.getElementById("edit_visa_type").value;
    var visa_period = document.getElementById("edit_visa_period").value;
    var apply_receive = document.getElementById("edit_apply_receive").value;
    if (visa_period.trim().length == 0) {
        enableButton('save');
        alert("Visa period must not be empty");
        return;
    }
    if (apply_receive.trim().length == 0) {
        enableButton('save');
        alert("Apply and receive must not be empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/VisaLetter/CreateOrEdit",
        data: { student_id: student_id, visa_letter_id: visa_letter_id, visa_type: visa_type, visa_period: visa_period, apply_receive: apply_receive },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                alert("Edit successfull");
                window.location.href = "/VisaLetter";
            }
            else {
                alert("Edit failed");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}