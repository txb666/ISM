// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function validateLogin() {
    $.ajax({
        type: "POST",
        url: "/Login/AccountIsExist",
        data: { username: document.getElementById("login_username_id").value, password: document.getElementById("login_password_id").value },
        dataType: "text",
        success: function (msg) {
            if (msg == "true") {
                $.ajax({
                    type: "POST",
                    url: "/Login/AccountIsActive",
                    data: { username: document.getElementById("login_username_id").value, password: document.getElementById("login_password_id").value },
                    dataType: "text",
                    success: function (msg) {
                        if (msg == "true") {
                            var fdata = new FormData();
                            fdata.append("txtAccount", document.getElementById("login_username_id").value);
                            fdata.append("txtPassword", document.getElementById("login_password_id").value);
                            $.ajax({
                                type: "POST",
                                url: "/Login/Index",
                                contentType: false,
                                processData: false,
                                data: fdata,
                            });
                        }
                        else {
                            alert("Account is Inactive.");
                        }
                    },
                    error: function (req, status, error) {
                        alert(error);
                    }
                });
            }
            else {
                alert("Username or Password incorrect.");
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}

function search(page) {
    var form = document.getElementById('searchForm');
    var inputPage = document.getElementById('inputPage');
    inputPage.value = page;
    form.submit();
}

function searchDegreeOrMobility() {
    document.getElementById('search_degreeOrMobility').value = document.getElementById('outside_degreeOrMobility').value;
    search(1);
}

function searchByStudentGroup() {
    document.getElementById('search_student_group_id').value = document.getElementById('outside_studentGroup').value;
    search(1);
}

function showPopup(id) {
    document.getElementById(id).style.display = "block";
}

function hidePopup(id) {
    document.getElementById(id).style.display = "none";
}

$('img').on('click', function () {
    $('#overlay')
        .css({ backgroundImage: `url(${this.src})` })
        .addClass('open')
        .one('click', function () { $(this).removeClass('open'); });
});