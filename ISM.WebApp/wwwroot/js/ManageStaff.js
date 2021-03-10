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


function myFunction1() {
    document.getElementById("myDropdown_search").classList.toggle("show_search");
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
    if (/^([A-Za-z0-9_\-\.])+\@([gmail|GMAIL])+\.(com)$/.test(email)==false) {
        alert("email empty or not in right format");
        return;
    }
    if (/([^\s]*)/.test(fullname)==false) {
        alert("fullname empty");
        return;
    }
    if (/([^\s]*)/.test(account)==false) {
        alert("account empty");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Staff/Create",
        data: { fullname: fullname, email: email, account: account, startDate: startDate, endDate: endDate, status: status },
        dataType: "text",
        success: function (msg) {
            if (msg > 0) {
                alert("Create Staff successfull");
            }
            else {
                alert("Create Staff failed")
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}